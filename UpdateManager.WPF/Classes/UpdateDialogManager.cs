using System;
using System.Windows;
using CodeDead.UpdateManager.Classes;
using CodeDead.UpdateManager.WPF.Windows;

namespace CodeDead.UpdateManager.WPF.Classes
{
    /// <summary>
    /// Class that contains logic to display an update dialog
    /// </summary>
    public sealed class UpdateDialogManager
    {
        #region Variables

        private StringVariables _stringVariables;

        #endregion

        /// <summary>
        /// Initialize a new UpdateDialogManager
        /// </summary>
        public UpdateDialogManager()
        {
            StringVariables = new StringVariables();
        }

        /// <summary>
        /// Initialize a new UpdateDialogManager
        /// </summary>
        /// <param name="showNoUpdates">Sets whether a message should be displayed if no updates are available</param>
        public UpdateDialogManager(bool showNoUpdates)
        {
            StringVariables = new StringVariables();
            ShowNoUpdates = showNoUpdates;
        }

        /// <summary>
        /// Initialize a new UpdateDialogManager
        /// </summary>
        /// <param name="stringVariables">The StringVariables object that can be used to display text in the update dialog</param>
        public UpdateDialogManager(StringVariables stringVariables)
        {
            StringVariables = stringVariables;
        }

        /// <summary>
        /// Initialize a new UpdateDialogManager
        /// </summary>
        /// <param name="stringVariables">The StringVariables object that can be used to display text in the update dialog</param>
        /// <param name="showNoUpdates">Sets whether a message should be displayed if no updates are available</param>
        public UpdateDialogManager(StringVariables stringVariables, bool showNoUpdates)
        {
            StringVariables = stringVariables;
            ShowNoUpdates = showNoUpdates;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the StringVariables
        /// </summary>
        public StringVariables StringVariables
        {
            get => _stringVariables;
            set => _stringVariables = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets whether a message should be displayed if no updates are available
        /// </summary>
        public bool ShowNoUpdates { get; set; } = true;

        #endregion

        /// <summary>
        /// Display an update dialog, if applicable
        /// </summary>
        /// <param name="applicationUpdate">The Update that contains the latest information regarding the version</param>
        public void DisplayUpdateDialog(Update applicationUpdate)
        {
            if (applicationUpdate == null) throw new ArgumentNullException(nameof(applicationUpdate));

            if (applicationUpdate.CheckForUpdate())
            {
                UpdateWindow window = new UpdateWindow
                {
                    Title = StringVariables.TitleText,
                    InformationTextBlockContent = applicationUpdate.UpdateInfo,
                    InformationButtonContent = StringVariables.InformationButtonText,
                    CancelButtonContent = StringVariables.CancelButtonText,
                    DownloadButtonContent = StringVariables.DownloadButtonText,
                    DownloadUrl = applicationUpdate.UpdateUrl,
                    InformationUrl = applicationUpdate.InfoUrl,
                    UpdateNowText = StringVariables.UpdateNowText
                };
                window.ShowDialog();
            }
            else
            {
                if (ShowNoUpdates)
                {
                    MessageBox.Show(StringVariables.NoNewVersionText, StringVariables.TitleText, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
