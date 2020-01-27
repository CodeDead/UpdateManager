using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using Microsoft.Win32;

namespace CodeDead.UpdateManager.Extensions.Windows
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
        /// <summary>
        /// The location on the drive where the file was downloaded to
        /// </summary>
        private string _downloadLocation;
        /// <summary>
        /// The WebClient object that will download the update
        /// </summary>
        private WebClient _downloadClient;
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
        public string InformationTextBlockContent
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
        /// <summary>
        /// The text that should be displayed to the user to ask whether or not the update should be executed or not after the download has completed
        /// </summary>
        public string UpdateNowText { get; set; }

        /// <summary>
        /// Gets or sets whether the built-in downloading functionality should be used
        /// </summary>
        public bool UseDownloader { get; set; } = true;


        /// <summary>
        /// Gets or sets whether a downloaded executable should be executed as an administrator
        /// </summary>
        public bool ExecuteAsAdministrator { get; set; }
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
            _downloadClient?.CancelAsync();
            Close();
        }

        /// <summary>
        /// Method that is called when the Download Button is pressed
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private void BtnDownload_OnClick(object sender, RoutedEventArgs e)
        {
            if (!UseDownloader)
            {
                try
                {
                    System.Diagnostics.Process.Start(DownloadUrl);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
                Close();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            try
            {
                string extension = DownloadUrl.Substring(DownloadUrl.Length - 3);
                string filter = extension.ToUpper() + " file (*." + extension + ")|*." + extension;
                sfd.Filter = filter;

                if (sfd.ShowDialog() != true) return;
                _downloadClient = new WebClient();

                _downloadClient.DownloadProgressChanged += (s, ev) =>
                {
                    PgbDownloadStatus.Value = ev.ProgressPercentage;
                };
                _downloadClient.DownloadFileCompleted += WebClient_OnDownloadFileCompleted;

                PgbDownloadStatus.Visibility = Visibility.Visible;
                _downloadLocation = sfd.FileName;

                _downloadClient.DownloadFileAsync(new Uri(DownloadUrl), sfd.FileName);
                BtnDownload.IsEnabled = false;
            }
            catch (Exception ex)
            {
                PgbDownloadStatus.Visibility = Visibility.Collapsed;
                BtnDownload.IsEnabled = true;
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method that is called when a file has been downloaded
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The AsyncCompletedEventArgs</param>
        private void WebClient_OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                _downloadClient.DownloadFileCompleted -= WebClient_OnDownloadFileCompleted;
                _downloadClient.Dispose();
                return;
            }

            PgbDownloadStatus.Visibility = Visibility.Collapsed;
            BtnDownload.IsEnabled = true;

            if (MessageBox.Show(UpdateNowText, Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process
                    {
                        StartInfo = {FileName = _downloadLocation, UseShellExecute = true}
                    };

                    if (ExecuteAsAdministrator)
                    {
                        proc.StartInfo.Verb = "runas";
                    }
                    
                    proc.Start();
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Close();
            }
        }
    }
}
