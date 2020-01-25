namespace CodeDead.UpdateManager.WPF.Classes
{
    /// <summary>
    /// Class containing all available strings that can be displayed to the user
    /// </summary>
    public sealed class StringVariables
    {
        #region Properties

        /// <summary>
        /// The text that should appear in the title of the update dialog if an update is available
        /// </summary>
        public string TitleText { get; set; }

        /// <summary>
        /// The text that should appear in the information button
        /// </summary>
        public string InformationButtonText { get; set; }

        /// <summary>
        /// The text that should appear in the cancel buttons
        /// </summary>
        public string CancelButtonText { get; set; }

        /// <summary>
        /// The text that should appear in the download button
        /// </summary>
        public string DownloadButtonText { get; set; }

        /// <summary>
        /// The text that should appear in the MessageBox when the user is running the latest version (or newer)
        /// </summary>
        public string NoNewVersionText { get; set; }

        /// <summary>
        /// The text that should be displayed to the user to ask whether or not the update should be executed or not after the download has completed
        /// </summary>
        public string UpdateNowText { get; set; }

        #endregion

        /// <summary>
        /// Initialize a new StringVariables object
        /// </summary>
        public StringVariables()
        {
            TitleText = "";
            InformationButtonText = "";
            CancelButtonText = "";
            DownloadButtonText = "";
            NoNewVersionText = "";
            UpdateNowText = "";
        }
    }
}
