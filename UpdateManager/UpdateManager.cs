﻿using System;
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

        /// <summary>
        /// Gets or sets the IWebProxy that can be used to retrieve data
        /// </summary>
        public IWebProxy WebProxy { get; set; }

        /// <summary>
        /// Gets or sets the WebHeaderCollection that can be used to retrieve data
        /// </summary>
        public WebHeaderCollection WebClientHeaders { get; set; }

        #endregion

        /// <summary>
        /// Retrieve the latest PlatformUpdates object
        /// </summary>
        /// <returns>The PlatformUpdates object that was retrieved from a remote location</returns>
        public PlatformUpdates GetLatestVersions()
        {
            if (UpdateUrl == null) throw new ArgumentNullException(nameof(UpdateUrl));
            if (UpdateUrl.Length == 0) throw new ArgumentException(nameof(UpdateUrl));

            string data;
            using (WebClient wc = new WebClient())
            {
                wc.Proxy = WebProxy;
                wc.Headers = WebClientHeaders;

                data = wc.DownloadString(UpdateUrl);
            }

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

            return updates;
        }

        /// <summary>
        /// Asynchronously retrieve the latest PlatformUpdates object
        /// </summary>
        /// <returns>The PlatformUpdates object that was retrieved from a remote location</returns>
        public async Task<PlatformUpdates> GetLatestVersionsAsync()
        {
            if (UpdateUrl == null) throw new ArgumentNullException(nameof(UpdateUrl));
            if (UpdateUrl.Length == 0) throw new ArgumentException(nameof(UpdateUrl));

            string data;
            using (WebClient wc = new WebClient())
            {
                wc.Proxy = WebProxy;
                wc.Headers = WebClientHeaders;

                data = await wc.DownloadStringTaskAsync(UpdateUrl);
            }

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

            return updates;
        }

        /// <summary>
        /// Retrieve the latest Update object
        /// </summary>
        /// <param name="includePreRelease">Include pre-releases to retrieve the latest Update object</param>
        /// <returns>The Update object that was retrieved from a remote location</returns>
        public Update GetLatestVersion(bool includePreRelease)
        {
            if (CurrentPlatform == null) throw new ArgumentNullException(nameof(CurrentPlatform));
            if (CurrentPlatform.Length == 0) throw new ArgumentException(nameof(CurrentPlatform));

            PlatformUpdates updates = GetLatestVersions();
            Update latestUpdate = GetFinalUpdate(updates);

            if (!includePreRelease) return latestUpdate;
            Update preRelease = GetFinalPreRelease(updates);
            return preRelease.UpdateAvailable(new Version(latestUpdate.MajorVersion, latestUpdate.MinorVersion,
                latestUpdate.BuildVersion, latestUpdate.RevisionVersion))
                ? preRelease
                : latestUpdate;
        }

        /// <summary>
        /// Asynchronously retrieve the latest Update object
        /// </summary>
        /// <param name="includePreRelease">Include pre-releases to retrieve the latest Update object</param>
        /// <returns>The Update object that was retrieved from a remote location</returns>
        public async Task<Update> GetLatestVersionAsync(bool includePreRelease)
        {
            if (CurrentPlatform == null) throw new ArgumentNullException(nameof(CurrentPlatform));
            if (CurrentPlatform.Length == 0) throw new ArgumentException(nameof(CurrentPlatform));

            PlatformUpdates updates = await GetLatestVersionsAsync();
            Update latestUpdate = null;

            await Task.Run(() =>
            {
                latestUpdate = GetFinalUpdate(updates);
                if (!includePreRelease) return;
                Update preRelease = GetFinalPreRelease(updates);
                if (preRelease.UpdateAvailable(new Version(latestUpdate.MajorVersion, latestUpdate.MinorVersion,
                    latestUpdate.BuildVersion, latestUpdate.RevisionVersion)))
                {
                    latestUpdate = preRelease;
                }
            });

            return latestUpdate;
        }

        /// <summary>
        /// Retrieve the latest Update object
        /// </summary>
        /// <param name="platformUpdates">The PlatformUpdates object that contains a list of PlatformUpdate objects</param>
        /// <returns>The latest Update object that corresponds to the current platform</returns>
        private Update GetFinalUpdate(PlatformUpdates platformUpdates)
        {
            Update latestUpdate = null;
            foreach (PlatformUpdate platformUpdate in platformUpdates.PlatformUpdateList)
            {
                if (platformUpdate.PlatformName != CurrentPlatform) continue;
                if (latestUpdate != null && platformUpdate.Update.UpdateAvailable(new Version(
                        latestUpdate.MajorVersion, latestUpdate.MinorVersion,
                        latestUpdate.BuildVersion, latestUpdate.RevisionVersion)))
                {
                    latestUpdate = platformUpdate.Update;
                }
                else if (latestUpdate == null)
                {
                    latestUpdate = platformUpdate.Update;
                }
            }

            return latestUpdate;
        }

        /// <summary>
        /// Retrieve the latest Update object
        /// </summary>
        /// <param name="platformUpdates">The PlatformUpdates object that contains a list of PlatformUpdate objects</param>
        /// <returns>The latest Update object that corresponds to the current platform</returns>
        private Update GetFinalPreRelease(PlatformUpdates platformUpdates)
        {
            Update latestUpdate = null;
            foreach (PlatformUpdate platformUpdate in platformUpdates.PlatformUpdateList)
            {
                if (platformUpdate.PlatformName != CurrentPlatform) continue;
                if (latestUpdate != null && platformUpdate.PreRelease.UpdateAvailable(new Version(
                        latestUpdate.MajorVersion, latestUpdate.MinorVersion,
                        latestUpdate.BuildVersion, latestUpdate.RevisionVersion)))
                {
                    latestUpdate = platformUpdate.PreRelease;
                }
                else if (latestUpdate == null)
                {
                    latestUpdate = platformUpdate.PreRelease;
                }
            }

            return latestUpdate;
        }
    }
}
