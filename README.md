# UpdateManager
UpdateManager was created by DeadLine. This library was developed free of charge.

This library can be used to check for application updates. It is designed for WPF and Windows Forms applications.
In order to use it, you require an XML file on a remote or local server that represents the Update class.

Do not attempt to use this library on a web application, for it will show a MessageBox.

# Usage

Create a new UpdateManager object like this:
```C#
UpdateManager updateManager = new UpdateManager(System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, "http://example.com/update.xml", "Application title");
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
