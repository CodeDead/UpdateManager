using System;
using System.Xml.Serialization;

namespace UpdateManager.Classes
{
    /// <summary>
    /// Check whether a new version is available or not.
    /// </summary>
    public class Update
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
            Version update = new Version(MajorVersion, MinorVersion, BuildVersion, RevisionVersion);
            int result = update.CompareTo(_applicationVersion);
            return result > 0;
        }

        /// <summary>
        /// Get the formatted Update version number
        /// </summary>
        /// <returns>The formatted update version number</returns>
        internal string GetUpdateVersion()
        {
            return MajorVersion + "." + MinorVersion + "." + BuildVersion + "." + RevisionVersion;
        }
    }
}
