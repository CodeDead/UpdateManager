using System;
using System.Xml.Serialization;

namespace UpdateManager
{
    /// <summary>
    /// Check whether a new version is available or not.
    /// </summary>
    public class Update
    {
        #region XML_Update
        // ReSharper disable once UnassignedField.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int MajorVersion;
        // ReSharper disable once UnassignedField.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int MinorVersion;
        // ReSharper disable once UnassignedField.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int BuildVersion;
        // ReSharper disable once UnassignedField.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public int RevisionVersion;
        // ReSharper disable once UnassignedField.Global
        public string UpdateUrl;
        #endregion

        #region Assigned_Variables
        [XmlIgnore]
        private Version _applicationVersion;
        #endregion


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
