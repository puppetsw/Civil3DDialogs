using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace CivilDialogs;

/// <summary>
/// Interaction logic for PointGroupSelectView.xaml
/// </summary>
public partial class SelectPointGroupDialog : Window
{
	public ObservableCollection<PointGroup> PointGroups { get; } = new();

	public SelectPointGroupDialog()
	{
		InitializeComponent();

		SourceInitialized += OnSourceInitialized;
		Loaded += OnLoaded;
	}

	private void OnSourceInitialized(object x, EventArgs y)
	{
		this.HideMinimizeAndMaximizeButtons();
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		LoadPointGroups();
		Loaded -= OnLoaded;
	}

	private void LoadPointGroups()
	{
		using var tr = new TransactAndForget(true);

		foreach (ObjectId pointGroupId in CivilApplication.ActiveDocument.PointGroups)
		{
			var pointGroup = tr.GetObject<PointGroup>(pointGroupId, OpenMode.ForRead);
			PointGroups.Add(pointGroup);
		}

		CmbPointGroup.ItemsSource = PointGroups;

		if (CmbPointGroup.Items.Count > 0)
			CmbPointGroup.SelectedIndex = 0;
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

	public PointGroup SelectedPointGroup { get; private set; }

	private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		SelectedPointGroup = CmbPointGroup.SelectedItem as PointGroup;
		TxtDescription.Text = SelectedPointGroup?.Description;
	}

	public bool? ShowModal()
	{
		return Application.ShowModalWindow(this);
	}
}