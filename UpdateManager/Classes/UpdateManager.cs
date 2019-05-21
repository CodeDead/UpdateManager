using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Windows;
using System.Xml.Serialization;
using CodeDead.UpdateManager.Windows;

namespace CodeDead.UpdateManager.Classes
{
    /// <summary>
    /// The UpdateManager has the ability to check for software updates
    /// </summary>
    public sealed class UpdateManager
    {
        #region Variables
        /// <summary>
        /// The URL that can be used to check for updates
        /// </summary>
        private readonly string _updateUrl;
        /// <summary>
        /// The version of the application
        /// </summary>
        private readonly Version _applicationVersion;
        /// <summary>
        /// The string variables that can be used to display information to the user
        /// </summary>
        private StringVariables _stringVariables;
        /// <summary>
        /// The DataType that can be used to deserialize the update information
        /// </summary>
        private readonly DataType _dataType;
        #endregion

        /// <summary>
        /// Initiate a new UpdateManager object
        /// </summary>
        /// <param name="version">Your application version</param>
        /// <param name="updateUrl">The URL where your XML update file is located</param>
        /// <param name="stringVariables">StringVariables object containing strings that can be used to display information to the user</param>
        /// <param name="datatype">The DataType that can be used to deserialize the update information</param>
        public UpdateManager(Version version, string updateUrl, StringVariables stringVariables, DataType datatype)
        {
            _updateUrl = updateUrl;
            _dataType = datatype;
            _applicationVersion = new Version(version.Major, version.Minor, version.Build, version.Revision);
            SetStringVariables(stringVariables);
        }

        /// <summary>
        /// Initialize a new UpdateManager object
        /// </summary>
        /// <param name="version">Your application version</param>
        /// <param name="updateUrl">The URL where your XML update file is located</param>
        /// <param name="dataType">The DataType that can be used to deserialize the update information</param>
        public UpdateManager(Version version, string updateUrl, DataType dataType)
        {
            _updateUrl = updateUrl;
            _dataType = dataType;
            _applicationVersion = new Version(version.Major, version.Minor, version.Build, version.Revision);
            SetStringVariables(new StringVariables());
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
                string data = await new WebClient().DownloadStringTaskAsync(_updateUrl);
                Update update;
                switch (_dataType)
                {
                    default:
                        update = DeserializeJson(data);
                        break;
                    case DataType.Xml:
                        update = DeserializeXml(data);
                        break;
                }


                if (update.CheckForUpdate())
                {
                    UpdateWindow window = new UpdateWindow
                    {
                        Title = _stringVariables.TitleText,
                        InformationTextBlockContent = update.UpdateInfo,
                        InformationButtonContent = _stringVariables.InformationButtonText,
                        CancelButtonContent = _stringVariables.CancelButtonText,
                        DownloadButtonContent = _stringVariables.DownloadButtonText,
                        DownloadUrl = update.UpdateUrl,
                        InformationUrl = update.InfoUrl,
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

        /// <summary>
        /// Deserialize the XML data into an Update object
        /// </summary>
        /// <param name="data">The XML data that should be deserialized</param>
        /// <returns>The Update object that was deserialized</returns>
        private Update DeserializeXml(string data)
        {
            Update update;
            XmlSerializer serializer = new XmlSerializer(typeof(Update));
            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(data);
                writer.Flush();
                stream.Position = 0;
                update = (Update)serializer.Deserialize(stream);
                update.SetApplicationVersion(_applicationVersion);
                writer.Dispose();
            }

            return update;
        }

        /// <summary>
        /// Deserialize the Json data into an Update object
        /// </summary>
        /// <param name="data">The Json data that should be deserialized</param>
        /// <returns>The Update object that was deserialized</returns>
        private Update DeserializeJson(string data)
        {
            Update update = new JavaScriptSerializer().Deserialize<Update>(data);
            update.SetApplicationVersion(_applicationVersion);
            return update;
        }

        /// <summary>
        /// Change the StringVariables during runtime
        /// </summary>
        /// <param name="stringVariables">StringVariables object that contains all translation data</param>
        public void SetStringVariables(StringVariables stringVariables)
        {
            _stringVariables = stringVariables ?? throw new ArgumentNullException(nameof(stringVariables));
        }
    }
}
