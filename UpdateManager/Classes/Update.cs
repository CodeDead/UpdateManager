using System;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace CodeDead.UpdateManager.Classes
{
    /// <summary>
    /// Class that represents an application update
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
        [ScriptIgnore]
        private Version _applicationVersion;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current application version
        /// </summary>
        [XmlIgnore]
        [ScriptIgnore]
        public Version ApplicationVersion
        {
            get => _applicationVersion;
            set => _applicationVersion = value ?? throw new ArgumentNullException(nameof(value));
        }
        #endregion

        /// <summary>
        /// Check whether or not there is an update available
        /// </summary>
        /// <returns>A boolean to represent whether there is an update available or not</returns>
        public bool CheckForUpdate()
        {
            if (ApplicationVersion == null) throw new ArgumentNullException(nameof(ApplicationVersion));

            int result = new Version(MajorVersion, MinorVersion, BuildVersion, RevisionVersion)
                .CompareTo(ApplicationVersion);
            return result > 0;
        }
    }
}
