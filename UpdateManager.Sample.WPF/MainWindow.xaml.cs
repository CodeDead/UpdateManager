using System.Reflection;
using System.Windows;
using CodeDead.UpdateManager.Classes;

namespace UpdateManager.Sample.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialize a new MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method that is called when the Update button is clicked
        /// </summary>
        /// <param name="sender">The sender that initiated this method</param>
        /// <param name="e">The RoutedEventArgs</param>
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Initialize a new UpdateManager object
            CodeDead.UpdateManager.Classes.UpdateManager updateManager = new CodeDead.UpdateManager.Classes.UpdateManager();

            // Initialize and set the StringVariables that can be used by the dialog functionality
            StringVariables stringVariables = new StringVariables
            {
                CancelButtonText = "Cancel",
                DownloadButtonText = "Download",
                InformationButtonText = "Information",
                NoNewVersionText = "You are running the latest version!",
                TitleText = "UpdateManager Sample",
                UpdateNowText = "Would you like to update to the latest version now?"
            };

            // Automatically get the current application's version by utilizing
            updateManager.ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version;

            // Set the data type of the remote Update object representation
            updateManager.DataType = DataType.Json;
            // Set the remote address where the Update object representation is located
            updateManager.UpdateUrl = "https://codedead.com/Software/PK%20Finder/update.json";
            // Set that a message should be displayed if no updates are available
            updateManager.ShowNoUpdates = true;
            // Set the StringVariables object
            updateManager.StringVariables = stringVariables;

            // Retrieve the latest Update object from the remote location asynchronously
            Update update = await updateManager.GetLatestVersionAsync();

            // Display an update dialog, if applicable
            updateManager.DisplayUpdateDialog(update);
        }
    }
}
