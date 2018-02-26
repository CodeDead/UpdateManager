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

# Update XML example
```XML
<?xml version="1.0"?>
<Update xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MajorVersion>1</MajorVersion>
  <MinorVersion>0</MinorVersion>
  <BuildVersion>0</BuildVersion>
  <RevisionVersion>0</RevisionVersion>
  <UpdateUrl>https://example.com/update.exe</UpdateUrl>
  <InfoUrl>https://codedead.com/</InfoUrl>
  <UpdateInfo>A new version is now available.Please click the download button to download version 1.0.0.0</UpdateInfo>
</Update>
```

# License
This library is licensed under the GPLv3.
