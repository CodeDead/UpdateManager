<p align="center">
  <img src="https://codedead.com/Software/UpdateManager/logo.png">
</p>

# UpdateManager [![NuGet](https://img.shields.io/nuget/v/CodeDead.UpdateManager)](https://www.nuget.org/packages/CodeDead.UpdateManager/) [![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://codedead.com/?page_id=302)
This library can be used to check for application updates. It is designed for WPF and Windows Forms applications.
In order to use it, you require an XML or JSON file on a remote or local server that represents the Update class.

## Sample projects
Sample projects can be found here:  
https://github.com/CodeDead/UpdateManager/tree/master/UpdateManager.Sample  
https://github.com/CodeDead/UpdateManager/tree/master/UpdateManager.Sample.WPF

## Usage
UpdateManager is available as a NuGet package. You can find it here:  
https://www.nuget.org/packages/CodeDead.UpdateManager/

You can install the package using your package manager:
```NuGet
Install-Package CodeDead.UpdateManager
```
Command-line:
```CLI
dotnet add package CodeDead.UpdateManager
```

After the package has been added to your project, you can create a new *UpdateManager* object using the default constructor:
```C#
UpdateManager updateManager = new UpdateManager();
```

It is important to set the platform of the current application. The *CurrentPlatform* property is used to determine which *Update* object is applicable to the running application:

```C#
updateManager.CurrentPlatform = "win32";
```

In order to retrieve the latest version, you will also need to specify a remote location where the *PlatformUpdates* object is stored:
```C#
updateManager.UpdateUrl = "https://codedead.com/Software/UpdateManager/example.json";
```

After the *CurrentPlatform* and *UpdateUrl* properties have been set, you can check for updates by calling the *GetLatestVersion()* method of an *UpdateManager* object. An *Update* object will be returned, that you can then use to check for updates:
```C#
// Retrieve the latest Update object from the remote location
Update update = updateManager.GetLatestVersion(false); // Set to true to include pre-releases
// Check if an update is available
bool updateAvailable = update.UpdateAvailable(Assembly.GetExecutingAssembly().GetName().Version);
```

---

A dialog can be displayed to the user if an update is available by installing the *CodeDead.UpdateManager.Extensions* package. This package is available for .NET Framework applications and can be installed using NuGet:
```NuGet
Install-Package CodeDead.UpdateManager.Extensions
```
Command-line:
```CLI
dotnet add package CodeDead.UpdateManager.Extensions
```

After the package has been added to your project, you can create a new *UpdateDialogManager* object using the default constructor:
```C#
UpdateDialogManager updateDialogManager = new UpdateDialogManager();
```

At this point, it is important to make sure that translations are available to the dialog for your users. You can add translations by adding them to the *StringVariables* property of an *UpdateDialogManager* object:
```C#
// Initialize and set the StringVariables that can be used by the dialog functionality
StringVariables stringVariables = new StringVariables
{
    CancelButtonText = "Cancel",
    DownloadButtonText = "Download",
    InformationButtonText = "Information",
    TitleText = "UpdateManager Sample",
    UpdateNowText = "Would you like to update to the latest version now?"
};
// Set the variables to be displayed
updateDialogManager.StringVariables = stringVariables;
```

Using an *Update* object that was retrieved using the *UpdateManager* object, you can now display an update dialog:
```C#
updateDialogManager.DisplayUpdateDialog(update);
```

## PlatformUpdates
*Update* objects can be retrieved for multiple platforms. The *PlatformUpdates* object is the root object that should be retrieved from a remote location.  
  
A *PlatformUpdates* object can be stored and parsed in two different formats: *JSON* or *XML*. By default, the *DataType* property will be set to *Json*. You can change the *DataType* property by setting the appropriate property on the *UpdateManager* object:
```C#
// Set the data type of the remote Update object representation
updateManager.DataType = DataType.Json;
```

```C#
// Set the data type of the remote Update object representation
updateManager.DataType = DataType.Xml;
```

### *PlatformUpdates* JSON example
```JSON
{
  "PlatformUpdateList": [
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

### *PlatformUpdates* XML example
```XML
<?xml version="1.0"?>
<PlatformUpdates
	xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
	xmlns:xsd="http://www.w3.org/2001/XMLSchema">
	<PlatformUpdateList>
		<PlatformUpdate>
			<PlatformName>win32</PlatformName>
			<Update>
				<MajorVersion>2</MajorVersion>
				<MinorVersion>0</MinorVersion>
				<BuildVersion>0</BuildVersion>
				<RevisionVersion>0</RevisionVersion>
				<UpdateUrl>https://codedead.com/Software/PK%20Finder/PK_setup.exe</UpdateUrl>
				<InfoUrl>https://codedead.com</InfoUrl>
				<UpdateInfo>A new version is now available. Please click the download button to download version 2.0.0.0</UpdateInfo>
			</Update>
		</PlatformUpdate>
		<PlatformUpdate>
			<PlatformName>linux</PlatformName>
			<Update>
				<MajorVersion>2</MajorVersion>
				<MinorVersion>0</MinorVersion>
				<BuildVersion>0</BuildVersion>
				<RevisionVersion>0</RevisionVersion>
				<UpdateUrl>https://codedead.com/Software/PK%20Finder/PK_setup.exe</UpdateUrl>
				<InfoUrl>https://codedead.com</InfoUrl>
				<UpdateInfo>A new version is now available. Please click the download button to download version 2.0.0.0</UpdateInfo>
			</Update>
		</PlatformUpdate>
	</PlatformUpdateList>
</PlatformUpdates>
```

# About
This library is maintained by CodeDead. You can find more about us using the following links:
* [Website](https://codedead.com)
* [Twitter](https://twitter.com/C0DEDEAD)
* [Facebook](https://facebook.com/deadlinecodedead)
* [Reddit](https://reddit.com/r/CodeDead/)

We would also like to thank JetBrains for the open source license that they granted us to work with wonderful tools such as [Rider](https://jetbrains.com/rider) and [Resharper](https://jetbrains.com/resharper).
