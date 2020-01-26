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
        
        private List<PlatformUpdate> _updatePlatformList;

        #endregion

        /// <summary>
        /// Initialize a new PlatformUpdates object
        /// </summary>
        public PlatformUpdates()
        {
            UpdatePlatformList = new List<PlatformUpdate>();
        }

        /// <summary>
        /// Initialize a new PlatformUpdates object
        /// </summary>
        /// <param name="updatePlatformList">The list of PlatformUpdate objects</param>
        public PlatformUpdates(List<PlatformUpdate> updatePlatformList)
        {
            UpdatePlatformList = updatePlatformList;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the list of platforms for which there are updates
        /// </summary>
        public List<PlatformUpdate> UpdatePlatformList
        {
            get => _updatePlatformList;
            set => _updatePlatformList = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion
    }
}
