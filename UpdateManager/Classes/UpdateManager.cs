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
        /// The string variables that can be used to display information to the user
        /// </summary>
        private readonly StringVariables _stringVariables;
        #endregion

        /// <summary>
        /// Initiate a new UpdateManager object
        /// </summary>
        /// <param name="version">Your application version</param>
        /// <param name="updateUrl">The URL where your XML update file is located</param>
        /// <param name="stringVariables">StringVariables object containing strings that can be used to display information to the user</param>
        public UpdateManager(Version version, string updateUrl, StringVariables stringVariables)
        {
            _updateUrl = updateUrl;

            _update = new Update();
            _applicationVersion = new Version(version.Major, version.Minor, version.Build, version.Revision);

            _update.SetApplicationVersion(_applicationVersion);
            _stringVariables = stringVariables;
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
                        Title = _stringVariables.TitleText,
                        InformationTextblockContent = _update.UpdateInfo,
                        InformationButtonContent = _stringVariables.InformationButtonText,
                        CancelButtonContent = _stringVariables.CancelButtonText,
                        DownloadButtonContent = _stringVariables.DownloadButtonText,
                        DownloadUrl = _update.UpdateUrl,
                        InformationUrl = _update.InfoUrl,
                        UpdateNowText = _stringVariables.UpdateNowText
                    };
                    window.ShowDialog();
                }
                else
                {
                    if (showNoUpdates)
                    {
                        MessageBox.Show(_stringVariables.NoNewVersionText, _stringVariables.TitleText, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                if (showErrors)
                {
                    MessageBox.Show(ex.Message, _stringVariables.TitleText, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
