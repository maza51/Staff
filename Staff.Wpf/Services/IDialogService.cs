using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff.Wpf.Services;

public interface IDialogService
{
    void ShowMessage(string message, string caption = "");
    bool ShowMessageWithChoice(string message, string caption = "");
}
