# UpdateManager
UpdateManager was created by DeadLine. This library was developed free of charge.

This library can be used to check for application updates. It is designed for WPF and Windows Forms applications.
In order to use it, you require an XML or JSON file on a remote or local server that represents the Update class.

## Dependencies
* .NET Standard 2.0

## Sample projects

Sample projects can be found here:  
https://github.com/CodeDead/UpdateManager/tree/master/UpdateManager.Sample  
https://github.com/CodeDead/UpdateManager/tree/master/UpdateManager.Sample.WPF

## Usage
UpdateManager is available as a [NuGet package](https://www.nuget.org/packages/CodeDead.UpdateManager/). You can find it here:  
https://www.nuget.org/packages/CodeDead.UpdateManager/

```NuGet
Install-Package CodeDead.UpdateManager
```

```CLI
dotnet add package CodeDead.UpdateManager
```

Create a new *UpdateManager* object like this:
```C#
// Import statement
using CodeDead.UpdateManager.Classes;

// Initialize a new UpdateManager object
UpdateManager updateManager = new UpdateManager();
```

You can check for updates like this:
```C#
// Retrieve the latest Update object from the remote location
Update update = updateManager.GetLatestVersion();
// Check if an update is available
bool updateAvailable = update.UpdateAvailable(Assembly.GetExecutingAssembly().GetName().Version);
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
  "UpdatePlatformList": [
    {
      "PlatformName": "win32",
      "Update": {
        "MajorVersion": 2,
        "MinorVersion": 0,
        "BuildVersion": 0,
        "RevisionVersion": 0,
        "UpdateUrl": "https://codedead.com/example.exe",
        "InfoUrl": "https://codedead.com",
        "UpdateInfo": "A new version is now available.\nPlease click the download button to download version 2.0.0.0"
      }
    },
    {
      "PlatformName": "linux",
      "Update": {
        "MajorVersion": 2,
        "MinorVersion": 0,
        "BuildVersion": 0,
        "RevisionVersion": 0,
        "UpdateUrl": "https://codedead.com/example.exe",
        "InfoUrl": "https://codedead.com",
        "UpdateInfo": "A new version is now available.\nPlease click the download button to download version 2.0.0.0"
      }
    }
  ]
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
