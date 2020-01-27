using System;
using CodeDead.UpdateManager;
using CodeDead.UpdateManager.Objects;

namespace UpdateManager.Sample
{
    /// <summary>
    /// Sample class for using an UpdateManager
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            // Initialize a new UpdateManager object
            CodeDead.UpdateManager.UpdateManager updateManager = new CodeDead.UpdateManager.UpdateManager();
            
            // Set the platform of the current application
            updateManager.CurrentPlatform = "win32";
            // Set the data type of the remote Update object (optional for JSON, since this is the default data type)
            updateManager.DataType = DataType.Json;
            // Set the remote address where the Update object representation is located
            updateManager.UpdateUrl = "https://codedead.com/Software/UpdateManager/example.json";

            try
            {
                // Retrieve the latest Update object from the remote location
                Update update = updateManager.GetLatestVersion(false);
                // Alternatively, you can automatically get the current application's version by utilizing Assembly.GetExecutingAssembly().GetName().Version;
                Console.WriteLine("Update available: " + update.UpdateAvailable(new Version(1, 0, 0, 0)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadLine();
        }
    }
}
