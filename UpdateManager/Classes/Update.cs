using System;
using System.Xml.Serialization;

namespace CodeDead.UpdateManager.Classes
{
    /// <summary>
    /// Check whether a new version is available or not
    /// </summary>
    public sealed class Update
    {
        #region XML_Update
        /// <summary>
        /// The major version of the most current update
        /// </summary>
        public int MajorVersion;
        /// <summary>
        /// The minor version of the most current update
        /// </summary>
        public int MinorVersion;
        /// <summary>
        /// The build version of the most current update
        /// </summary>
        public int BuildVersion;
        /// <summary>
        /// The revision version of the most current update
        /// </summary>
        public int RevisionVersion;
        /// <summary>
        /// The update URL of the most current update
        /// </summary>
        public string UpdateUrl;
        /// <summary>
        /// The information URL of the most current update
        /// </summary>
        public string InfoUrl;
        /// <summary>
        /// The information in plain text regarding this update
        /// </summary>
        public string UpdateInfo;
        #endregion

        #region Assigned_Variables
        /// <summary>
        /// The current application version
        /// </summary>
        [XmlIgnore]
        private Version _applicationVersion;
        #endregion

        /// <summary>
        /// Set the version of the Application
        /// </summary>
        /// <param name="version">The Version of the application</param>
        internal void SetApplicationVersion(Version version)
        {
            _applicationVersion = version;
        }

        /// <summary>
        /// Check whether or not there is an update available
        /// </summary>
        /// <returns>A boolean to represent whether there is an update available or not</returns>
        internal bool CheckForUpdate()
        {
            int result = new Version(MajorVersion, MinorVersion, BuildVersion, RevisionVersion)
                .CompareTo(_applicationVersion);
            return result > 0;
        }
    }
}
