using System;
using System.Windows;

namespace UpdateManager.Windows
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow
    {
        #region Variables
        /// <summary>
        /// The content for the Information TextBlock
        /// </summary>
        private string _informationTextBlockContent;
        /// <summary>
        /// The content for the Information Button
        /// </summary>
        private string _informationButtonContent;
        /// <summary>
        /// The content for the Cancel Button
        /// </summary>
        private string _cancelButtonContent;
        /// <summary>
        /// The content for the Download Button
        /// </summary>
        private string _downloadButtonContent;
        #endregion

        #region Properties
        /// <summary>
        /// The URL that should be opened when the Information Button is pressed
        /// </summary>
        public string InformationUrl { get; set; }
        /// <summary>
        /// The URL that should be opened when the Download Button is pressed
        /// </summary>
        public string DownloadUrl { get; set; }
        /// <summary>
        /// The content for the Information TextBlock
        /// </summary>
        public string InformationTextblockContent
        {
            get => _informationTextBlockContent;
            set
            {
                _informationTextBlockContent = value;
                TxtInfo.Text = value;
            }
        }
        /// <summary>
        /// The content for the Information Button
        /// </summary>
        public string InformationButtonContent
        {
            get => _informationButtonContent;
            set
            {
                _informationButtonContent = value;
                BtnInformation.Content = value;
            }
        }
        /// <summary>
        /// The content for the Cancel Button
        /// </summary>
        public string CancelButtonContent
        {
            get => _cancelButtonContent;
            set
            {
                _cancelButtonContent = value;
                BtnCancel.Content = value;
            }
        }
        /// <summary>
        /// The content for the Download Button
        /// </summary>
        public string DownloadButtonContent
        {
            get => _downloadButtonContent;
            set
            {
                _downloadButtonContent = value;
                BtnDownload.Content = value;
            }
        }
        #endregion

        /// <inheritdoc />
        /// <summary>
        /// Initialize a new UpdateWindow
        /// </summary>
        public UpdateWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method that is called when the Information Button is pressed
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnInformation_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(InformationUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method that is called when the Cancel Button is pressed
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Method that is called when the Download Button is pressed
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnDownload_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(DownloadUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
