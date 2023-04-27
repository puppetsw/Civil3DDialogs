using Autodesk.AutoCAD.ApplicationServices.Core;
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
	}
}
