using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
