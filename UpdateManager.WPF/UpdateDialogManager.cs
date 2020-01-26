using System;
using System.Windows;
using CodeDead.UpdateManager.Objects;
using CodeDead.UpdateManager.WPF.Objects;
using CodeDead.UpdateManager.WPF.Windows;

namespace CodeDead.UpdateManager.WPF
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
        /// <param name="stringVariables">The StringVariables object that can be used to display text in the update dialog</param>
        public UpdateDialogManager(StringVariables stringVariables)
        {
            StringVariables = stringVariables;
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
        /// Gets or sets whether the information button should be displayed
        /// </summary>
        public bool ShowInformationButton { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the cancel button should be displayed
        /// </summary>
        public bool ShowCancelButton { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the download button should be displayed
        /// </summary>
        public bool ShowDownloadButton { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the built-in downloading functionality should be used
        /// </summary>
        public bool UseDownloader { get; set; } = true;

        #endregion

        /// <summary>
        /// Display an update dialog, if applicable
        /// </summary>
        /// <param name="applicationUpdate">The Update that contains the latest information regarding the version</param>
        public void DisplayUpdateDialog(Update applicationUpdate)
        {
            if (applicationUpdate == null) throw new ArgumentNullException(nameof(applicationUpdate));

            UpdateWindow window = new UpdateWindow
            {
                Title = StringVariables.TitleText,
                InformationTextBlockContent = applicationUpdate.UpdateInfo,
                InformationButtonContent = StringVariables.InformationButtonText,
                CancelButtonContent = StringVariables.CancelButtonText,
                DownloadButtonContent = StringVariables.DownloadButtonText,
                DownloadUrl = applicationUpdate.UpdateUrl,
                InformationUrl = applicationUpdate.InfoUrl,
                UpdateNowText = StringVariables.UpdateNowText,
                UseDownloader = UseDownloader
            };

            if (!ShowCancelButton && !ShowDownloadButton && !ShowInformationButton)
            {
                window.GrdButtons.Visibility = Visibility.Collapsed;
            }

            if (!ShowCancelButton)
            {
                window.BtnCancel.Visibility = Visibility.Collapsed;
            }

            if (!ShowDownloadButton)
            {
                window.BtnDownload.Visibility = Visibility.Collapsed;
            }

            if (!ShowInformationButton)
            {
                window.BtnInformation.Visibility = Visibility.Collapsed;
            }

            window.ShowDialog();
        }
    }
}
