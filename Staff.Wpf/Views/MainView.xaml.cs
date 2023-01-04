using Staff.Wpf.ViewModels;
using System;
using System.Windows;

namespace Staff.Wpf.Views;

public partial class MainView : Window
{
    public MainView(MainViewModel mainViewMidel)
    {
        DataContext = mainViewMidel;
        InitializeComponent();
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        ((App)App.Current).Shutdown();
    }
}
