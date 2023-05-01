# Civil3DUtils

A library of helpful dialogs and helper methods to use with your Autodesk Civil 3D .NET projects. 

## Description

I created this library to be used with multiple of my Civil 3D projects. It contains common dialogs that I have used and created.



### Dialogs
* [SelectAlignmentDialog](#selectalignmentdialog)
* [SelectSurfaceDialog](#selectsurfacedialog)
* [SelectPointGroupDialog](#selectpointgroupdialog)
* [LayerSelectDialog](#layerselectdialog)
* [LayerCreateDialog](#layercreatedialog)

## SelectAlignmentDialog

```cs
var dialog = new SelectAlignmentDialog();

if (dialog.ShowModal() == true)
   var alignment = dialog.SelectedAlignment;
```

![Screenshot 2023-05-01 141017](https://user-images.githubusercontent.com/79826944/235407630-b821b507-7ea8-45c1-ace6-7a6b5de5eb98.png)


## SelectSurfaceDialog

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

## SelectPointGroupDialog

```cs
var dialog = new SelectPointGroupDialog();

if (dialog.ShowModal() == true)
   var pointGroup = dialog.SelectedPointGroup;
```

![image](https://user-images.githubusercontent.com/79826944/235407768-ba462521-f847-48fd-8434-56131f0d4795.png)

## LayerSelectDialog

## LayerCreateDialog

```cs

var dialog = new LayerCreateDialog();

dialog.ShowModal();

if (dialog.Layer != null)
{
   using var tr = Application.DocumentManager.MdiActiveDocument.StartTransaction();
   
   
   
   tr.Commit();
}
   


```

![image](https://user-images.githubusercontent.com/79826944/235406600-b58b7540-378b-4818-ae4d-8dc29dbe8498.png)


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
