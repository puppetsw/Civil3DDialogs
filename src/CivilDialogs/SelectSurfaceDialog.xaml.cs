using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.Civil.ApplicationServices;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using Surface = Autodesk.Civil.DatabaseServices.Surface;

namespace CivilDialogs;

/// <summary>
/// Interaction logic for SurfaceSelectView.xaml
/// </summary>
public partial class SelectSurfaceDialog : Window
{
	private ObservableCollection<Surface> Surfaces { get; } = new();

	public SelectSurfaceDialog()
	{
		InitializeComponent();

		SourceInitialized += OnSourceInitialized;

		Loaded += OnLoaded;
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		LoadSurfaces();
		Loaded -= OnLoaded;
	}

	private void OnSourceInitialized(object x, EventArgs y)
	{
		this.HideMinimizeAndMaximizeButtons();
	}

	private void Button_OK_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = true;
		Close();
	}

	private void Button_Cancel_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = false;
		Close();
	}

	public Surface SelectedSurface { get; private set; }

	private void LoadSurfaces()
	{
		using var tr = new TransactAndForget(true);

		var surfaceIds = CivilApplication.ActiveDocument.GetSurfaceIds();

		foreach (ObjectId surfaceId in surfaceIds)
		{
			var surface = tr.GetObject<Surface>(surfaceId, OpenMode.ForRead);
			Surfaces.Add(surface);
		}

		CmbSurfaces.ItemsSource = Surfaces;

		if (CmbSurfaces.Items.Count > 0)
			CmbSurfaces.SelectedIndex = 0;
	}

	private void CmbSurfaces_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		SelectedSurface = CmbSurfaces.SelectedItem as Surface;
		TxtDescription.Text = SelectedSurface?.Description;
	}

	public bool? ShowModal()
	{
		return Application.ShowModalWindow(this);
	}

	private void BtnSelectObject_OnClick(object sender, RoutedEventArgs e)
	{
		Editor editor = Application.DocumentManager.MdiActiveDocument.Editor;

		var peo = new PromptEntityOptions("\nSelect Surface: ");
		peo.SetRejectMessage("\nSelected entity must be of type: Surface");

		peo.AddAllowedClass(typeof(Surface), true);

		var entity = editor.GetEntity(peo);

		if (entity.Status != PromptStatus.OK)
			return;

		using var tr = new TransactAndForget(true);

		var surface = tr.GetObject<Surface>(entity.ObjectId, OpenMode.ForRead);

		int index = 0;
		foreach (Surface surface1 in Surfaces)
		{
			if (surface.ObjectId.Equals(surface1.ObjectId))
			{
				CmbSurfaces.SelectedIndex = index;
				break;
			}
			index++;
		}
	}
}
