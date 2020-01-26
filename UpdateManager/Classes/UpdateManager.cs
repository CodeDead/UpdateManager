using System;
using System.Net;
using System.Threading.Tasks;
using CodeDead.UpdateManager.Classes.Utils;

namespace CodeDead.UpdateManager.Classes
{
    /// <summary>
    /// Class that has the ability to check for software updates
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

        #endregion

        /// <summary>
        /// Initialize a new UpdateManager
        /// </summary>
        public UpdateManager()
        {
            // Empty constructor
        }

        /// <summary>
        /// Initiate a new UpdateManager object
        /// </summary>
        /// <param name="version">Your application version</param>
        /// <param name="updateUrl">The URL where your XML update file is located</param>
        /// <param name="dataType">The DataType that can be used to deserialize the update information</param>
        public UpdateManager(Version version, string updateUrl, DataType dataType)
        {
            UpdateUrl = updateUrl;
            DataType = dataType;
            ApplicationVersion = new Version(version.Major, version.Minor, version.Build, version.Revision);
        }

        /// <summary>
        /// Initiate a new UpdateManager object
        /// </summary>
        /// <param name="version">Your application version</param>
        /// <param name="updateUrl">The URL where your XML update file is located</param>
        /// <param name="dataType">The DataType that can be used to deserialize the update information</param>
        /// <param name="showNoUpdates">Sets whether a dialog should be displayed when no updates are available</param>
        public UpdateManager(Version version, string updateUrl, DataType dataType, bool showNoUpdates)
        {
            UpdateUrl = updateUrl;
            DataType = dataType;
            ApplicationVersion = new Version(version.Major, version.Minor, version.Build, version.Revision);
            ShowNoUpdates = showNoUpdates;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the DataType that can be used to deserialize the update information
        /// </summary>
        public DataType DataType { get; set; } = DataType.Json;

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
        public Version ApplicationVersion
        {
            get => _applicationVersion;
            set => _applicationVersion = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets whether a dialog should be displayed when no updates are available
        /// </summary>
        public bool ShowNoUpdates { get; set; } = true;

        #endregion

        /// <summary>
        /// Retrieve the latest Update object
        /// </summary>
        public Update GetLatestVersion()
        {
            if (UpdateUrl == null) throw new ArgumentNullException(nameof(UpdateUrl));
            if (UpdateUrl.Length == 0) throw new ArgumentException(nameof(UpdateUrl));

            string data = new WebClient().DownloadString(UpdateUrl);
            Update update;

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (DataType)
            {
                default:
                    update = UpdateDeserializer.DeserializeJson(data, ApplicationVersion);
                    break;
                case DataType.Xml:
                    update = UpdateDeserializer.DeserializeXml(data, ApplicationVersion);
                    break;
            }

            return update;
        }

        /// <summary>
        /// Asynchronously retrieve the latest Update object
        /// </summary>
        public async Task<Update> GetLatestVersionAsync()
        {
            if (UpdateUrl == null) throw new ArgumentNullException(nameof(UpdateUrl));
            if (UpdateUrl.Length == 0) throw new ArgumentException(nameof(UpdateUrl));

            string data = await new WebClient().DownloadStringTaskAsync(UpdateUrl);
            Update update;

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (DataType)
            {
                default:
                    update = await UpdateDeserializer.DeserializeJsonAsync(data, ApplicationVersion);
                    break;
                case DataType.Xml:
                    update = await UpdateDeserializer.DeserializeXmlAsync(data, ApplicationVersion);
                    break;
            }

            return update;
        }
    }
}
