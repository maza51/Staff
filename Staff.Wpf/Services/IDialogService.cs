namespace Staff.Wpf.Services;

public interface IDialogService
{
    void ShowMessage(string message, string caption = "");
    bool ShowMessageWithChoice(string message, string caption = "");
}
