# SingleEntry for Xamarin (Android)
This is a simple app to show how to use the ScanAPI SDK.
**THE SOCKET XAMARIN SDK IS IN PREVIEW/BETA AND IS SUBJECT TO CHANGE.**
The application logic will likely stay the same but might require to rename
some of the API names.
## Requisite

The ScanAPI SDK NuGet is required for this sample to work.

## Installation
Download the ScanAPI SDK NuGet from Socket Store. Unzip the file to a location
of your choice.

In the Xamarin studio go to the preferences and scroll down to the NuGet
settings to add a new source. Point this new source to the location where the
ScanAPI SDK has been extracted.

Once this is done, load the SingleEntry solution in the Xamarin studio, and
compile.

## Configuration
The Socket Scanner is by default configured to support the HID mode interface, and it will need to be configure to the SPP mode (Serial Port Profile). Please check our website to find the barcode to configure the scanner in SPP mode.

Last, the application logic must create a timer to consume the asynchronous events coming from ScanAPI. This is generally done just after opening ScanAPI.

Please refer to ScanAPI.pdf documentation for more details regarding ScanAPI.
