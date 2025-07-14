

using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Sleeper.Views;

public partial class MessageBox : Window
{
    public MessageBox(string message)
    {
        InitializeComponent();
        MessageText.Text = message;
    }

    private void OnOkClicked(object sender, RoutedEventArgs e)
    {
        Close(); // Close the popup
    }
}