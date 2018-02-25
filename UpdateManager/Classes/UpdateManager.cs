using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Xml.Serialization;
using UpdateManager.Windows;

namespace UpdateManager.Classes
{
    /// <summary>
    /// The UpdateManager has the ability to check for software updates
    /// </summary>
    public class UpdateManager
    {
        #region Variables
        /// <summary>
        /// The URL that can be used to check for updates
        /// </summary>
        private readonly string _updateUrl;
        /// <summary>
        /// The Update object containing data about the current update
        /// </summary>
        private Update _update;
        /// <summary>
        /// The version of the application
        /// </summary>
        private readonly Version _applicationVersion;
        /// <summary>
        /// The title of the application
        /// </summary>
        private readonly string _titleText;
        /// <summary>
        /// The content of the Information label
        /// </summary>
        private readonly string _informationLabelContent;
        /// <summary>
        /// The content of the Information button
        /// </summary>
        private readonly string _informationButtonContent;
        /// <summary>
        /// The content of the Cancel button
        /// </summary>
        private readonly string _cancelButtonContent;
        /// <summary>
        /// The content of the Download button
        /// </summary>
        private readonly string _downloadButtonContent;
        /// <summary>
        /// The text that should be displayed if no updates are available
        /// </summary>
        private readonly string _noNewVersionText;
        #endregion

        /// <summary>
        /// Initiate a new UpdateManager object
        /// </summary>
        /// <param name="version">Your application version</param>
        /// <param name="updateUrl">The URL where your XML update file is located</param>
        /// <param name="titleText">Your application title text</param>
        /// <param name="informationLabelText">The content that should be displayed in the Information label</param>
        /// <param name="informationButtonText">The content that should be displayed in the Information button</param>
        /// <param name="cancelButtonText">The content that should be displayed in the Cancel button</param>
        /// <param name="downloadButtonText">The content that should be displayed in the Download button</param>
        /// <param name="noNewVersion">Text that should be displayed when no updates are available</param>
        public UpdateManager(Version version, string updateUrl, string titleText, string informationLabelText, string informationButtonText, string cancelButtonText, string downloadButtonText, string noNewVersion)
        {
            _updateUrl = updateUrl;

            _update = new Update();
            _applicationVersion = new Version(version.Major, version.Minor, version.Build, version.Revision);

            _update.SetApplicationVersion(_applicationVersion);
            _titleText = titleText;
            _noNewVersionText = noNewVersion;

            _informationLabelContent = informationLabelText;
            _informationButtonContent = informationButtonText;
            _cancelButtonContent = cancelButtonText;
            _downloadButtonContent = downloadButtonText;
        }

        /// <summary>
        /// Check if there are updates available
        /// </summary>
        /// <param name="showErrors">Show a notification if an error occured</param>
        /// <param name="showNoUpdates">Show a notification if no updates are available</param>
        public async void CheckForUpdate(bool showErrors, bool showNoUpdates)
        {
            try
            {
                WebClient wc = new WebClient();
                string xml = await wc.DownloadStringTaskAsync(_updateUrl);

                XmlSerializer serializer = new XmlSerializer(_update.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(xml);
                    writer.Flush();
                    stream.Position = 0;
                    _update = (Update)serializer.Deserialize(stream);
                    _update.SetApplicationVersion(_applicationVersion);
                    writer.Dispose();
                }

                if (_update.CheckForUpdate())
                {
                    UpdateWindow window = new UpdateWindow
                    {
                        Title = _titleText,
                        InformationLabelContent = _informationLabelContent,
                        InformationButtonContent = _informationButtonContent,
                        CancelButtonContent = _cancelButtonContent,
                        DownloadButtonContent = _downloadButtonContent,
                        DownloadUrl = _update.UpdateUrl,
                        InformationUrl = _update.InfoUrl
                    };
                    window.ShowDialog();
                }
                else
                {
                    if (showNoUpdates)
                    {
                        MessageBox.Show(_noNewVersionText, _titleText, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                if (showErrors)
                {
                    MessageBox.Show(ex.Message, _titleText, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
