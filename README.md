# UpdateManager
UpdateManager was created by DeadLine. This library was developed free of charge.

This library can be used to check for application updates. It is designed for WPF and Windows Forms applications.
In order to use it, you require an XML file on a remote or local server that represents the Update class.
# Usage

Create a new UpdateManager object like this:
```C#
UpdateManager.Classes.UpdateManager updateManager = new UpdateManager.Classes.UpdateManager(Assembly.GetExecutingAssembly().GetName().Version, "https://example.com/update.xml", "Application title", "A new version is now available.\n\nClick the download button to immediately download the update!", "Information", "Cancel", "Download", "No new version is currently available.");
```

Check for updates like this:
```C#
try
{
  updateManager.CheckForUpdate(showErrors, showNoUpdates);
}
catch (Exception ex)
{
  MessageBox.Show(ex.Message, "Application title", MessageBoxButton.OK, MessageBoxImage.Error);
}
```

# License
This library is licensed under the GPLv3.
