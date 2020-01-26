using System;
using System.Collections.Generic;

namespace CodeDead.UpdateManager.Objects
{
    /// <summary>
    /// Class that contains logic to retrieve updates for multiple platforms
    /// </summary>
    public sealed class PlatformUpdates
    {
        #region Variables
        
        private List<PlatformUpdate> _platformUpdateList;

        #endregion

        /// <summary>
        /// Initialize a new PlatformUpdates object
        /// </summary>
        public PlatformUpdates()
        {
            PlatformUpdateList = new List<PlatformUpdate>();
        }

        /// <summary>
        /// Initialize a new PlatformUpdates object
        /// </summary>
        /// <param name="platformUpdateList">The list of PlatformUpdate objects</param>
        public PlatformUpdates(List<PlatformUpdate> platformUpdateList)
        {
            PlatformUpdateList = platformUpdateList;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the list of platforms for which there are updates
        /// </summary>
        public List<PlatformUpdate> PlatformUpdateList
        {
            get => _platformUpdateList;
            set => _platformUpdateList = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion
    }
}
