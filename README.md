# UpdateManager
UpdateManager was created by DeadLine. This library was developed free of charge.

This library can be used to check for application updates. It is designed for WPF and Windows Forms applications.
In order to use it, you require an XML file on a remote or local server that represents the Update class.

## Dependencies
* [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

## Usage
UpdateManager is available as a [NuGet package](https://www.nuget.org/packages/CodeDead.UpdateManager/). You can find it here:  
https://www.nuget.org/packages/CodeDead.UpdateManager/

Create a new UpdateManager object like this:
```C#
// Import statement
using CodeDead.UpdateManager.Classes;
// Setting text variables
StringVariables stringVariables = new StringVariables
{
	CancelButtonText = "Cancel",
	DownloadButtonText = "Download",
	InformationButtonText = "Information",
	NoNewVersionText = "You are running the latest version!",
	TitleText = "Your application title",
	UpdateNowText = "Would you like to update the application now?"
};
UpdateManager updateManager = new UpdateManager(Assembly.GetExecutingAssembly().GetName().Version, "https://yoururl/update.xml", stringVariables);
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

## Update XML example
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

