﻿using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using Microsoft.Win32;

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
        /// <summary>
        /// The location on the drive where the file was downloaded to
        /// </summary>
        private string _downloadLocation;
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
        /// <summary>
        /// The text that should be displayed to the user to ask whether or not the update should be executed or not after the download has completed
        /// </summary>
        public string UpdateNowText { get; set; }

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
                SaveFileDialog sfd = new SaveFileDialog();
                string extension = DownloadUrl.Substring(DownloadUrl.Length - 3);
                string filter = extension.ToUpper() + " file (*." + extension + ")|*." + extension;
                sfd.Filter = filter;

                if (sfd.ShowDialog() != true) return;
                WebClient wc = new WebClient();

                wc.DownloadProgressChanged += (s, ev) =>
                {
                    PgbDownloadStatus.Value = ev.ProgressPercentage;
                };
                wc.DownloadFileCompleted += WebClient_OnDownloadFileCompleted;

                PgbDownloadStatus.Visibility = Visibility.Visible;
                TxtInfo.Visibility = Visibility.Collapsed;
                _downloadLocation = sfd.FileName;

                wc.DownloadFileAsync(new Uri(DownloadUrl), sfd.FileName);
            }
            catch (Exception ex)
            {
                PgbDownloadStatus.Visibility = Visibility.Collapsed;
                TxtInfo.Visibility = Visibility.Visible;
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
            PgbDownloadStatus.Visibility = Visibility.Collapsed;
            TxtInfo.Visibility = Visibility.Visible;

            if (MessageBox.Show(UpdateNowText, Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start(_downloadLocation);
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
