using Microsoft.Extensions.DependencyInjection;
using Staff.Wpf.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Staff.Wpf.Views;

public partial class EmployeeDetailView : Window
{
    public EmployeeDetailView()
    {
        InitializeComponent();

        var model = ((App)App.Current).ServiceProvider.GetService<EmployeeDetailViewModel>();

        if (model != null)
        {
            model.RequestClose += () => this.Close();
            DataContext = model;
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        this.Visibility = Visibility.Collapsed;
        e.Cancel = true;
        base.OnClosing(e);
    }
}
