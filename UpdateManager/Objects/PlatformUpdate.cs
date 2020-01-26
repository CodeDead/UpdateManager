using System;

namespace CodeDead.UpdateManager.Objects
{
    /// <summary>
    /// Class that contains the logic for a specific platform update
    /// </summary>
    public sealed class PlatformUpdate
    {
        #region Variables

        private string _platformName;
        private Update _update;

        #endregion

        /// <summary>
        /// Initialize a new PlatformUpdate
        /// </summary>
        public PlatformUpdate()
        {
            PlatformName = "";
            Update = new Update();
        }

        /// <summary>
        /// Initialize a new PlatformUpdate
        /// </summary>
        /// <param name="platformName">The name of the platform for which the update applies</param>
        public PlatformUpdate(string platformName)
        {
            PlatformName = platformName;
            Update = new Update();
        }

        /// <summary>
        /// Initialize a new PlatformUpdate
        /// </summary>
        /// <param name="update">The Update object that corresponds to the current platform</param>
        public PlatformUpdate(Update update)
        {
            PlatformName = "";
            Update = update;
        }

        /// <summary>
        /// Initialize a new PlatformUpdate
        /// </summary>
        /// <param name="platformName">The name of the platform for which the update applies</param>
        /// <param name="update">The Update object that corresponds to the current platform</param>
        public PlatformUpdate(string platformName, Update update)
        {
            PlatformName = platformName;
            Update = update;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the platform name
        /// </summary>
        public string PlatformName
        {
            get => _platformName;
            set => _platformName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the Update object
        /// </summary>
        public Update Update
        {
            get => _update;
            set => _update = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion
    }
}
