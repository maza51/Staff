using System.Windows;

namespace Staff.Wpf.Services;

public class DefaultDialogService : IDialogService
{
    public void ShowMessage(string message, string caption = "")
    {
        MessageBox.Show(message, caption);
    }

    public bool ShowMessageWithChoice(string message, string caption = "")
    {
        return MessageBox.Show(message,
                    caption,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}
