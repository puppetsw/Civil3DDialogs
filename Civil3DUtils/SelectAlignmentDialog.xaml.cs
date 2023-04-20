using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace Civil3DUtils;

/// <summary>
/// Interaction logic for SurfaceSelectView.xaml
/// </summary>
public partial class SelectAlignmentDialog : Window
{
	public ObservableCollection<Alignment> Alignments { get; } = new();

	public SelectAlignmentDialog()
	{
		InitializeComponent();

		SourceInitialized += OnSourceInitialized;

		Loaded += OnLoaded;
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		LoadAlignments();
		Loaded -= OnLoaded;
	}

	private void LoadAlignments()
	{
		using var tr = new TransactAndForget(true);

		var alignmentIds = CivilApplication.ActiveDocument.GetAlignmentIds();

		foreach (ObjectId alignmentId in alignmentIds)
		{
			var alignment = tr.GetObject<Alignment>(alignmentId, OpenMode.ForRead);
			Alignments.Add(alignment);
		}

		CmbAlignments.ItemsSource = Alignments;

		if (CmbAlignments.Items.Count > 0)
			CmbAlignments.SelectedIndex = 0;
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

	public Alignment SelectedAlignment { get; private set; }

	private void CmbAlignments_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		SelectedAlignment = CmbAlignments.SelectedItem as Alignment;
		TxtDescription.Text = SelectedAlignment?.Description;
	}

	public bool? ShowModal()
	{
		return Application.ShowModalWindow(this);
	}
}
