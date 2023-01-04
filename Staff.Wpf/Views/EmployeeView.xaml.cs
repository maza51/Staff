using Microsoft.Extensions.DependencyInjection;
using Staff.Wpf.ViewModels;
using System.Windows.Controls;

namespace Staff.Wpf.Views;

public partial class EmployeeView : UserControl
{
    public EmployeeView()
    {
        InitializeComponent();

        DataContext = ((App)App.Current).ServiceProvider.GetService<EmployeeViewModel>();
    } 
}
