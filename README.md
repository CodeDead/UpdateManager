# UpdateManager
UpdateManager was created by DeadLine. This library was developed free of charge.

This library can be used to check for application updates. It is designed for WPF and Windows Forms applications.
In order to use it, you require an XML or JSON file on a remote or local server that represents the Update class.

## Dependencies
* [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

## Usage
UpdateManager is available as a [NuGet package](https://www.nuget.org/packages/CodeDead.UpdateManager/). You can find it here:  
https://www.nuget.org/packages/CodeDead.UpdateManager/

A sample project can be found here:
https://github.com/CodeDead/UpdateManager/tree/master/UpdateManager.Sample

Create a new *UpdateManager* object like this:
```C#
// Import statement
using CodeDead.UpdateManager.Classes;

// Initialize a new UpdateManager object
UpdateManager updateManager = new UpdateManager();
```

You can check for updates like this:
```C#
try
{
  // Retrieve the latest Update object from the remote location
  Update update = updateManager.GetLatestVersion();
}
catch (Exception ex)
{
  MessageBox.Show(ex.Message, "Application title", MessageBoxButton.OK, MessageBoxImage.Error);
}
```
## Update types
Updates can be stored and parsed in two different formats: *JSON* or *XML*. By default, the *DataType* property will be set to *Json*. You can change the *DataType* property by setting the appropriate property on the *UpdateManager* object:
```C#
// Initialize a new UpdateManager object
UpdateManager updateManager = new UpdateManager();
// Set the data type of the remote Update object representation
updateManager.DataType = DataType.Json;
```

```C#
// Initialize a new UpdateManager object
UpdateManager updateManager = new UpdateManager();
// Set the data type of the remote Update object representation
updateManager.DataType = DataType.Xml;
```

### JSON Update example
```JSON
{
	"MajorVersion": 1,
	"MinorVersion": 0,
	"BuildVersion": 0,
	"RevisionVersion": 0,
	"UpdateUrl": "https://codedead.com/update.exe",
	"InfoUrl": "https://codedead.com",
	"UpdateInfo": "A new version is now available. Please click the download button to download version 1.0.0.0"
}
```

### XML Update example
```XML
<?xml version="1.0"?>
<Update xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <MajorVersion>1</MajorVersion>
  <MinorVersion>0</MinorVersion>
  <BuildVersion>0</BuildVersion>
  <RevisionVersion>0</RevisionVersion>
  <UpdateUrl>https://example.com/update.exe</UpdateUrl>
  <InfoUrl>https://codedead.com/</InfoUrl>
  <UpdateInfo>A new version is now available. Please click the download button to download version 1.0.0.0</UpdateInfo>
</Update>
```

# About
This library is maintained by CodeDead. You can find more about us using the following links:
* [Website](https://codedead.com)
* [Twitter](https://twitter.com/C0DEDEAD)
* [Facebook](https://facebook.com/deadlinecodedead)
* [Reddit](https://reddit.com/r/CodeDead/)

We would also like to thank JetBrains for the open source license that they granted us to work with wonderful tools such as [Rider](https://jetbrains.com/rider) and [Resharper](https://jetbrains.com/resharper).
