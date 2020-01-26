using System;
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
                UpdateNowText = StringVariables.UpdateNowText
            };
            window.ShowDialog();
        }
    }
}
