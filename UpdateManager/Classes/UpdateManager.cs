using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
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
        private string _updateUrl;
        /// <summary>
        /// The version of the application
        /// </summary>
        private Version _applicationVersion;
        /// <summary>
        /// The string variables that can be used to display information to the user
        /// </summary>
        private StringVariables _stringVariables;
        #endregion

        /// <summary>
        /// Initialize a new UpdateManager
        /// </summary>
        public UpdateManager()
        {
            StringVariables = new StringVariables();
        }

        /// <summary>
        /// Initiate a new UpdateManager object
        /// </summary>
        /// <param name="version">Your application version</param>
        /// <param name="updateUrl">The URL where your XML update file is located</param>
        /// <param name="stringVariables">StringVariables object containing strings that can be used to display information to the user</param>
        /// <param name="dataType">The DataType that can be used to deserialize the update information</param>
        public UpdateManager(Version version, string updateUrl, StringVariables stringVariables, DataType dataType)
        {
            UpdateUrl = updateUrl;
            DataType = dataType;
            Version = new Version(version.Major, version.Minor, version.Build, version.Revision);
            StringVariables = stringVariables;
        }

        /// <summary>
        /// Initialize a new UpdateManager object
        /// </summary>
        /// <param name="version">Your application version</param>
        /// <param name="updateUrl">The URL where your XML update file is located</param>
        /// <param name="dataType">The DataType that can be used to deserialize the update information</param>
        public UpdateManager(Version version, string updateUrl, DataType dataType)
        {
            UpdateUrl = updateUrl;
            DataType = dataType;
            Version = new Version(version.Major, version.Minor, version.Build, version.Revision);
            StringVariables = new StringVariables();
        }

        #region Properties

        /// <summary>
        /// Gets or sets the DataType that can be used to deserialize the update information
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// Gets or sets the update URL
        /// </summary>
        public string UpdateUrl
        {
            get => _updateUrl;
            set => _updateUrl = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the local version of the application
        /// </summary>
        public Version Version
        {
            get => _applicationVersion;
            set => _applicationVersion = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets whether errors should be displayed while checking for updates
        /// </summary>
        public bool ShowErrors { get; set; } = true;

        /// <summary>
        /// Gets or sets whether a dialog should be displayed when no updates are available
        /// </summary>
        public bool ShowNoUpdates { get; set; } = true;

        /// <summary>
        /// Gets or sets the StringVariables object that contains translations
        /// </summary>
        public StringVariables StringVariables
        {
            get => _stringVariables;
            set => _stringVariables = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        /// <summary>
        /// Check if there are updates available
        /// </summary>
        public void CheckForUpdates()
        {
            try
            {
                string data = new WebClient().DownloadString(UpdateUrl);
                Update update;
                switch (DataType)
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
                    if (ShowNoUpdates)
                    {
                        MessageBox.Show(_stringVariables.NoNewVersionText, _stringVariables.TitleText, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowErrors)
                {
                    MessageBox.Show(ex.Message, _stringVariables.TitleText, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Asynchronously check if there are updates available
        /// </summary>
        public async Task CheckForUpdatesAsync()
        {
            try
            {
                string data = await new WebClient().DownloadStringTaskAsync(UpdateUrl);
                Update update;
                switch (DataType)
                {
                    default:
                        update = await DeserializeJsonAsync(data);
                        break;
                    case DataType.Xml:
                        update = await DeserializeXmlAsync(data);
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
                    if (ShowNoUpdates)
                    {
                        MessageBox.Show(_stringVariables.NoNewVersionText, _stringVariables.TitleText, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ShowErrors)
                {
                    MessageBox.Show(ex.Message, _stringVariables.TitleText, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Deserialize XML data into an Update object
        /// </summary>
        /// <param name="data">The XML data that should be deserialized</param>
        /// <returns>The Update object that was deserialized</returns>
        private Update DeserializeXml(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            Update update;
            XmlSerializer serializer = new XmlSerializer(typeof(Update));
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                    writer.Flush();
                }

                stream.Position = 0;
                update = (Update)serializer.Deserialize(stream);
                update.SetApplicationVersion(_applicationVersion);
            }

            return update;
        }

        /// <summary>
        /// Deserialize XML data into an Update object asynchronously
        /// </summary>
        /// <param name="data">The XML data that should be deserialized</param>
        /// <returns>The Update object that was deserialized or null if an error occurred</returns>
        private async Task<Update> DeserializeXmlAsync(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            await Task.Run(async () =>
            {
                Update update;
                XmlSerializer serializer = new XmlSerializer(typeof(Update));
                using (MemoryStream stream = new MemoryStream())
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        await writer.WriteAsync(data);
                        await writer.FlushAsync();
                    }

                    stream.Position = 0;
                    update = (Update)serializer.Deserialize(stream);
                    update.SetApplicationVersion(_applicationVersion);
                }

                return update;
            });
            return null;
        }

        /// <summary>
        /// Deserialize JSON data into an Update object
        /// </summary>
        /// <param name="data">The JSON data that should be deserialized</param>
        /// <returns>The Update object that was deserialized</returns>
        private Update DeserializeJson(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            Update update = new JavaScriptSerializer().Deserialize<Update>(data);
            update.SetApplicationVersion(_applicationVersion);
            return update;
        }

        /// <summary>
        /// Deserialize JSON data into an Update object asynchronously
        /// </summary>
        /// <param name="data">The JSON data that should be deserialized</param>
        /// <returns>The Update object that was deserialized</returns>
        private async Task<Update> DeserializeJsonAsync(string data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.Length == 0) throw new ArgumentException(nameof(data));

            await Task.Run(() =>
            {
                Update update = new JavaScriptSerializer().Deserialize<Update>(data);
                update.SetApplicationVersion(_applicationVersion);
                return update;
            });
            return null;
        }
    }
}
