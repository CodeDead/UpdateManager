using System;
using System.Net;
using System.Threading.Tasks;
using CodeDead.UpdateManager.Objects;
using CodeDead.UpdateManager.Utils;

namespace CodeDead.UpdateManager
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
        /// The current application's platform
        /// </summary>
        private string _currentPlatform;

        #endregion

        /// <summary>
        /// Initialize a new UpdateManager
        /// </summary>
        public UpdateManager()
        {
            // Empty constructor
        }

        /// <summary>
        /// Initialize a new UpdateManager
        /// </summary>
        /// <param name="currentPlatform">The current application's platform</param>
        public UpdateManager(string currentPlatform)
        {
            CurrentPlatform = currentPlatform;
        }

        /// <summary>
        /// Initialize a new UpdateManager
        /// </summary>
        /// <param name="currentPlatform">The current application's platform</param>
        /// <param name="updateUrl">The URL of the remote location where the PlatformUpdates can be retrieved</param>
        public UpdateManager(string currentPlatform, string updateUrl)
        {
            CurrentPlatform = currentPlatform;
            UpdateUrl = updateUrl;
        }

        /// <summary>
        /// Initialize a new UpdateManager
        /// </summary>
        /// <param name="currentPlatform">The current application's platform</param>
        /// <param name="updateUrl">The URL of the remote location where the PlatformUpdates can be retrieved</param>
        /// <param name="dataType">The DataType that can be used to deserialize the update information</param>
        public UpdateManager(string currentPlatform, string updateUrl, DataType dataType)
        {
            CurrentPlatform = currentPlatform;
            UpdateUrl = updateUrl;
            DataType = dataType;
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
        /// Gets or sets the current platform
        /// </summary>
        public string CurrentPlatform
        {
            get => _currentPlatform;
            set => _currentPlatform = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        /// <summary>
        /// Retrieve the latest Update object
        /// </summary>
        /// <returns>The Update object that was retrieved from a remote location</returns>
        public Update GetLatestVersion()
        {
            if (UpdateUrl == null) throw new ArgumentNullException(nameof(UpdateUrl));
            if (UpdateUrl.Length == 0) throw new ArgumentException(nameof(UpdateUrl));

            if (CurrentPlatform == null) throw new ArgumentNullException(nameof(CurrentPlatform));
            if (CurrentPlatform.Length == 0) throw new ArgumentException(nameof(CurrentPlatform));

            string data = new WebClient().DownloadString(UpdateUrl);
            PlatformUpdates updates;

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (DataType)
            {
                default:
                    updates = PlatformUpdatesDeserializer.DeserializeJson(data);
                    break;
                case DataType.Xml:
                    updates = PlatformUpdatesDeserializer.DeserializeXml(data);
                    break;
            }

            foreach (PlatformUpdate platformUpdate in updates.UpdatePlatformList)
            {
                if (platformUpdate.PlatformName == CurrentPlatform)
                    return platformUpdate.Update;
            }

            return null;
        }

        /// <summary>
        /// Asynchronously retrieve the latest Update object
        /// </summary>
        /// <returns>The Update object that was retrieved from a remote location</returns>
        public async Task<Update> GetLatestVersionAsync()
        {
            if (UpdateUrl == null) throw new ArgumentNullException(nameof(UpdateUrl));
            if (UpdateUrl.Length == 0) throw new ArgumentException(nameof(UpdateUrl));

            string data = await new WebClient().DownloadStringTaskAsync(UpdateUrl);
            PlatformUpdates updates;

            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (DataType)
            {
                default:
                    updates = await PlatformUpdatesDeserializer.DeserializeJsonAsync(data);
                    break;
                case DataType.Xml:
                    updates = await PlatformUpdatesDeserializer.DeserializeXmlAsync(data);
                    break;
            }

            Update update = null;

            await Task.Run(() =>
            {
                foreach (PlatformUpdate platformUpdate in updates.UpdatePlatformList)
                {
                    if (platformUpdate.PlatformName != CurrentPlatform) continue;
                    update = platformUpdate.Update;
                    break;
                }
            });

            return update;
        }
    }
}
