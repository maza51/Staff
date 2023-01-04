using Microsoft.Extensions.DependencyInjection;
using Staff.Wpf.ViewModels;
using System.Windows.Controls;

namespace Staff.Wpf.Views;

public partial class DepartmentView : UserControl
{
    public DepartmentView()
    {
        InitializeComponent();

        DataContext = ((App)App.Current).ServiceProvider.GetService<DepartmentViewModel>();
    }
}
