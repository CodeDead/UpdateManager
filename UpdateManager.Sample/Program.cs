using System;
using System.Reflection;
using CodeDead.UpdateManager.Classes;

namespace UpdateManager.Sample
{
    /// <summary>
    /// Sample class for using an UpdateManager
    /// </summary>
    internal class Program
    {
        [STAThread]
        private static void Main()
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

            // Set the version of the current application
            updateManager.ApplicationVersion = new Version(1, 0, 0, 0);
            // Alternatively, you can automatically get the current application's version by utilizing:
            updateManager.ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version;

            // Set the data type of the remote Update object representation
            updateManager.DataType = DataType.Json;
            // Set the remote address where the Update object representation is located
            updateManager.UpdateUrl = "https://codedead.com/Software/PK%20Finder/update.json";
            // Set that a message should be displayed if no updates are available
            updateManager.ShowNoUpdates = true;
            // Set the StringVariables object
            updateManager.StringVariables = stringVariables;

            // Retrieve the latest Update object from the remote location
            Update update = updateManager.GetLatestVersion();
            // Display an update dialog, if applicable
            updateManager.DisplayUpdateDialog(update);
        }
    }
}
