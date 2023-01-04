using Staff.Application.Interfaces;
using Staff.Domain;
using Staff.Wpf.Commands;
using Staff.Wpf.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Staff.Wpf.ViewModels;

class DepartmentViewModel : BaseViewModel
{
    private Employee _selectedEmployee;
    public Employee SelectedEmployee
    {
        get => _selectedEmployee;
        set
        {
            _selectedEmployee = value;
            OnPropertyChanged("SelectedEmployee");
        }
    }
    private List<Department> _departments;
    public List<Department> Departments
    {
        get => _departments;
        set
        {
            _departments = value;
            OnPropertyChanged("Departments");
        }
    }

    private Department _selectedDepartment;
    public Department SelectedDepartment
    {
        get => _selectedDepartment;
        set
        {
            _selectedDepartment = value;
            OnPropertyChanged("SelectedDepartment");
        }
    }

    private readonly IDepartmentService _departmentService;
    private readonly DepartmentDetailView _departmentDetailView;
    private readonly DepartmentDetailViewModel _departmentDetailViewModel;

    public DepartmentViewModel(
        IDepartmentService departmentService,
        DepartmentDetailView departmentDetailView,
        DepartmentDetailViewModel departmentViewModel)
    {
        _departmentService = departmentService;
        _departmentDetailView = departmentDetailView;
        _departmentDetailViewModel = departmentViewModel;

        Task.Run(async () => await UpdateDepartmentList());
    }

    private RelayCommand _showAddDepartmentViewCommand;
    public RelayCommand ShowAddDepartmentViewCommand =>
        _showAddDepartmentViewCommand ??
            (_showAddDepartmentViewCommand = new RelayCommand(ShowAddDepartmentExecute));

    private async void ShowAddDepartmentExecute(object obj)
    {
        _departmentDetailViewModel.Department = new Department();
        _departmentDetailView.ShowDialog();
        await UpdateDepartmentList();
    }

    private RelayCommand _showEditDepartmentViewCommand;
    public RelayCommand ShowEditDepartmentViewCommand =>
        _showEditDepartmentViewCommand ??
            (_showEditDepartmentViewCommand =
                new RelayCommand(ShowEditDepartmentExecute, ShowEditDepartmentCanExcute));

    private bool ShowEditDepartmentCanExcute(object arg) => SelectedDepartment != null;

    private async void ShowEditDepartmentExecute(object obj)
    {
        _departmentDetailViewModel.Department = SelectedDepartment;
        _departmentDetailView.ShowDialog();
        await UpdateDepartmentList();
    }

    private RelayCommand _refreshCommand;
    public RelayCommand RefreshCommand =>
        _refreshCommand ??
            (_refreshCommand = new RelayCommand(RefreshExecute));

    private async void RefreshExecute(object obj)
    {
        await UpdateDepartmentList();
    }

    private async Task UpdateDepartmentList()
    {
        Departments = await _departmentService.GetAllAsync();
    }
}
