using System;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using CivilDialogs;
using SamplePlugin;

[assembly: CommandClass(typeof(SampleCommands))]
namespace SamplePlugin
{
	public class Sample : IExtensionApplication
	{
		public void Initialize()
		{
		}

		public void Terminate()
		{
		}
	}


	public class SampleCommands
	{
		[CommandMethod("WMS", "SelectSurfaceSample", CommandFlags.Modal)]
		public void SelectSurfaceCommand()
		{
			var dialog = new SelectSurfaceDialog();

			if (dialog.ShowModal() == true)
			{
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage($"\nSelected Surface: {dialog.SelectedSurface.Name}");
			}
		}

		[CommandMethod("WMS", "SelectAlignmentSample", CommandFlags.Modal)]
		public void SelectAlignmentCommand()
		{
			var dialog = new SelectAlignmentDialog();

			if (dialog.ShowModal() == true)
			{
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage($"\nSelected Alignment: {dialog.SelectedAlignment.Name}");
			}
		}

		[CommandMethod("WMS", "SelectPointGroupSample", CommandFlags.Modal)]
		public void SelectPointGroupCommand()
		{
			var dialog = new SelectPointGroupDialog();

			if (dialog.ShowModal() == true)
			{
				Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage($"\nSelected Point Group: {dialog.SelectedPointGroup.Name}");
			}
		}

		[CommandMethod("WMS", "ShowCreateLayerDialog", CommandFlags.Modal)]
		public void ShowCreateLayerDialog()
		{
			var dialog = new LayerCreateDialog();

			dialog.ShowModal();

			if (dialog.Layer == null)
				return;

			using var tr = Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction();

			var layerTable = (LayerTable)tr.GetObject(Application.DocumentManager.MdiActiveDocument.Database.LayerTableId, OpenMode.ForRead);

			if (layerTable.Has(dialog.Layer.Name))
			{
				return;
			}

			layerTable.UpgradeOpen();
			layerTable.Add(dialog.Layer);
			tr.AddNewlyCreatedDBObject(dialog.Layer, true);
			tr.Commit();
		}
	}
}
