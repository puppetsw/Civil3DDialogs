using System.Windows;

namespace CivilDialogs;

/// <summary>
/// Interaction logic for LayerCreateDialog.xaml
/// </summary>
public partial class LayerCreateDialog : Window
{
    public LayerCreateDialog()
    {
        InitializeComponent();

        SourceInitialized += (_, _) => this.HideMinimizeAndMaximizeButtons();
    }

    private void ButtonCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private void ButtonOK_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }
}
