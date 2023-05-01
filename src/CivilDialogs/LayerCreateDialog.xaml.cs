#nullable enable

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Windows;
using CivilDialogs.Models;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using ColorDialog = Autodesk.AutoCAD.Windows.ColorDialog;
using MessageBox = System.Windows.MessageBox;

namespace CivilDialogs;

/// <summary>
/// Interaction logic for LayerCreateDialog.xaml
/// </summary>
public sealed partial class LayerCreateDialog
{
	public LayerTableRecord? Layer { get; private set; }

	public ObservableCollection<PropertyBase> Properties { get; private set; } = new();

	public ObservableCollection<string> YesNoSelect { get; } = new() { "Yes", "No" };


	public static readonly DependencyProperty LayerNameProperty = DependencyProperty.Register(
		nameof(LayerName), typeof(string), typeof(LayerCreateDialog), new PropertyMetadata(default(string)));

	public string LayerName
	{
		get => (string)GetValue(LayerNameProperty);
		set => SetValue(LayerNameProperty, value);
	}

	public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
		nameof(Color), typeof(Color), typeof(LayerCreateDialog), new PropertyMetadata(default(Color)));

	public Color Color
	{
		get => (Color)GetValue(ColorProperty);
		set => SetValue(ColorProperty, value);
	}


	public static readonly DependencyProperty LineWeightProperty = DependencyProperty.Register(
		nameof(LineWeight), typeof(LineWeight), typeof(LayerCreateDialog), new PropertyMetadata(default(LineWeight)));

	public LineWeight LineWeight
	{
		get => (LineWeight)GetValue(LineWeightProperty);
		set => SetValue(LineWeightProperty, value);
	}

	public static readonly DependencyProperty LineTypeDisplayNameProperty = DependencyProperty.Register(
		nameof(LineTypeDisplayName), typeof(string), typeof(LayerCreateDialog), new PropertyMetadata(default(string)));

	public string LineTypeDisplayName
	{
		get => (string)GetValue(LineTypeDisplayNameProperty);
		set => SetValue(LineTypeDisplayNameProperty, value);
	}

	public static readonly DependencyProperty IsOnProperty = DependencyProperty.Register(
		nameof(IsOn), typeof(bool), typeof(LayerCreateDialog), new PropertyMetadata(default(bool)));

	public bool IsOn
	{
		get => (bool)GetValue(IsOnProperty);
		set => SetValue(IsOnProperty, value);
	}

	public static readonly DependencyProperty IsLockedProperty = DependencyProperty.Register(
		nameof(IsLocked), typeof(bool), typeof(LayerCreateDialog), new PropertyMetadata(default(bool)));

	public bool IsLocked
	{
		get => (bool)GetValue(IsLockedProperty);
		set => SetValue(IsLockedProperty, value);
	}

	public static readonly DependencyProperty IsFrozenProperty = DependencyProperty.Register(
		nameof(IsFrozen), typeof(bool), typeof(LayerCreateDialog), new PropertyMetadata(default(bool)));

	public bool IsFrozen
	{
		get => (bool)GetValue(IsFrozenProperty);
		set => SetValue(IsFrozenProperty, value);
	}

	public static readonly DependencyProperty IsPlottableProperty = DependencyProperty.Register(
		nameof(IsPlottable), typeof(bool), typeof(LayerCreateDialog), new PropertyMetadata(default(bool)));

	public bool IsPlottable
	{
		get => (bool)GetValue(IsPlottableProperty);
		set => SetValue(IsPlottableProperty, value);
	}

	public ObjectId LineTypeObjectId { get; set; }

	public LayerCreateDialog()
	{
		InitializeComponent();
		SourceInitialized += OnSourceInitialized;
		Loaded += OnLoaded;
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		Properties = new ObservableCollection<PropertyBase>
		{
			new LayerNameProperty(),
			new LayerColorProperty(),
			new LayerLinetypeProperty(),
			new LayerLineweightProperty(),
			new LayerLockedProperty(),
			new LayerOnProperty(),
			new LayerFrozenProperty(),
			new LayerPlotStyleProperty(),
			new LayerPlotProperty()
		};

		Loaded -= OnLoaded;

		Color = Color.FromColorIndex(ColorMethod.ByPen, 7);

		using var tr = new TransactAndForget(true);

		var lineTypeTable = tr.GetObject<LinetypeTable>(Application.DocumentManager.MdiActiveDocument.Database.LinetypeTableId, OpenMode.ForRead);
		LineTypeObjectId = lineTypeTable["ByLayer"];
		LineTypeDisplayName = "ByLayer";

		DataGrid.ItemsSource = Properties;
	}

	private void OnSourceInitialized(object sender, EventArgs e)
	{
		this.HideMinimizeAndMaximizeButtons();
	}

	private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
	{
		DialogResult = false;
		Close();
	}

	private void ButtonOK_OnClick(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrEmpty(LayerName))
		{
			MessageBox.Show("Layer name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			return;
		}

		Layer = new LayerTableRecord();
		Layer.Color = Color;
		Layer.LineWeight = LineWeight;
		Layer.LinetypeObjectId = LineTypeObjectId;
		Layer.Name = LayerName;
		Layer.IsOff = !IsOn;
		Layer.IsLocked = IsLocked;
		Layer.IsFrozen = IsFrozen;
		Layer.IsPlottable = IsPlottable;

		DialogResult = true;
		Close();
	}

	[Obsolete("This method is obsolete. Use ShowModal() instead.")]
	public new DialogResult ShowDialog() => throw new NotImplementedException();

	public bool? ShowModal() => Application.ShowModalWindow(this);

	private void BtnShowColorDialog_OnClick(object sender, RoutedEventArgs e)
	{
		var dialog = new ColorDialog();

		if (dialog.ShowModal() == true)
		{
			Color = dialog.Color;
		}
	}

	private void BtnShowLineWeightDialog_OnClick(object sender, RoutedEventArgs e)
	{
		var dialog = new LineWeightDialog
		{
			LineWeight = LineWeight
		};

		if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			return;

		LineWeight = dialog.LineWeight;
	}

	private void BtnShowLineTypeDialog_OnClick(object sender, RoutedEventArgs e)
	{
		var dialog = new LinetypeDialog();

		if (dialog.ShowModal() == true)
		{
			using var tr = new TransactAndForget(true);

			var lt = tr.GetObject<LinetypeTableRecord>(dialog.Linetype, OpenMode.ForRead);

			LineTypeDisplayName = lt.Name;
			LineTypeObjectId = dialog.Linetype;
		}
	}
}
