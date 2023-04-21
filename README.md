# Civil3DUtils

A library of helpful dialogs and helper methods to use with your Autodesk Civil 3D .NET projects. 

## Description

I created this library to be used with multiple Civil 3D projects. It contains common dialogs and helper methods that I have used. Such as the `SelectSurfaceDialog`. I wanted a way to display a dialog if there were multiple surfaces in a drawing to the user, similar to the way Civil 3D does it in-built. 

```cs
var surfaces = CivilApplication.ActiveDocument.GetSurfaceIds();

Surface selectedSurface;
if (surfaces.Count > 1)
  var dialog = new SelectSurfaceDialog();
  if (dialog.ShowModal == true)
    selectedSurface = dialog.SelectedSurface;
  else
    selectedSurface = //Get first surface
```

![image](https://user-images.githubusercontent.com/79826944/233510297-7b0108a3-d9ad-4911-bfc9-f651a89115c4.png)

### Dialogs
* SelectAlignmentDialog
* SelectSurfaceDialog
* SelectPointGroupDialog
* LayerSelectDialog
* LayerCreateDialog

### Helper Methods
* TransactAndForget

***More to come***

## Getting Started

### Dependencies

* .NET Framework 4.7
* Built with Autodesk Civil 3D 2023. (Should work on earlier versions, may need to lower the .NET Framework version)

### Building the library

To build the plugin make sure that you have references to the following Autodesk DLLs from your Autodesk Civil 3D installation directories.

* accoremgd.dll
* acdbmgd.dll
* acmgd.dll
* AecBaseMgd.dll
* AeccDbMgd.dll

Make sure to set the `Copy Local` property of each `Autodesk` reference to `False`.

## License

This project is licensed under the MIT License - see the LICENSE file for details
