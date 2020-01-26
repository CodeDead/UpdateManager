using System;

namespace CodeDead.UpdateManager.Objects
{
    /// <summary>
    /// Class that represents an application update
    /// </summary>
    public sealed class Update
    {
        /// <summary>
        /// Initialize a new Update
        /// </summary>
        public Update()
        {
            // Empty constructor
        }

        /// <summary>
        /// Initialize a new Update
        /// </summary>
        /// <param name="majorVersion">The major version of the most current update</param>
        /// <param name="minorVersion">The minor version of the most current update</param>
        /// <param name="buildVersion">The build version of the most current update</param>
        /// <param name="revisionVersion">The revision version of the most current update</param>
        public Update(int majorVersion, int minorVersion, int buildVersion, int revisionVersion)
        {
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            BuildVersion = buildVersion;
            RevisionVersion = revisionVersion;
        }

        #region Properties

        /// <summary>
        /// Gets or sets he major version of the most current update
        /// </summary>
        public int MajorVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor version of the most current update
        /// </summary>
        public int MinorVersion { get; set; }

        /// <summary>
        /// Gets or sets the build version of the most current update
        /// </summary>
        public int BuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the revision version of the most current update
        /// </summary>
        public int RevisionVersion { get; set; }

        /// <summary>
        /// Gets or sets the update URL of the most current update
        /// </summary>
        public string UpdateUrl { get; set; }

        /// <summary>
        /// Gets or sets the information URL of the most current update
        /// </summary>
        public string InfoUrl { get; set; }

        /// <summary>
        /// Gets or sets the information in plain text regarding this update
        /// </summary>
        public string UpdateInfo { get; set; }

        #endregion

        /// <summary>
        /// Check whether the application version is lower than the update version
        /// </summary>
        /// <param name="applicationVersion">The current application version</param>
        /// <returns>True if an update is available, otherwise false</returns>
        public bool UpdateAvailable(Version applicationVersion)
        {
            if (applicationVersion == null) throw new ArgumentNullException(nameof(applicationVersion));

            int result = new Version(MajorVersion, MinorVersion, BuildVersion, RevisionVersion)
                .CompareTo(applicationVersion);
            return result > 0;
        }
    }
}
