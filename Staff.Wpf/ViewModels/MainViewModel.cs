using Staff.Application.Interfaces;
using Staff.Domain;
using Staff.Wpf.Commands;
using Staff.Wpf.Services;
using Staff.Wpf.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Staff.Wpf.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private List<Employee> _employees;
    public List<Employee> Employees
    {
        get => _employees;
        set
        {
            _employees = value;
            OnPropertyChanged("Employees");
        }
    }

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

    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;
    private readonly IDialogService _dialogService;
    private readonly EmployeeView _employeeView;
    private readonly DepartmentView _departmentView;
    private readonly EmployeeViewModel _employeeViewModel;
    private readonly DepartmentViewModel _departmentViewModel;

    public MainViewModel(IEmployeeService empService,
        IDepartmentService departmentService,
        EmployeeView employeeView,
        IDialogService dialogService,
        EmployeeViewModel employeeViewModel,
        DepartmentView departmentView,
        DepartmentViewModel departmentViewModel)
    {
        _employeeService = empService;
        _departmentService = departmentService;
        _dialogService = dialogService;
        _employeeView = employeeView;
        _employeeViewModel = employeeViewModel;
        _departmentView = departmentView;
        _departmentViewModel = departmentViewModel;

        Task.Run(async () => await UpdateEmployeeList());
        Task.Run(async () => await UpdateDepartmentList());
    }

    private RelayCommand _showAddEmployeeViewCommand;
    public RelayCommand ShowAddEmployeeViewCommand =>
        _showAddEmployeeViewCommand ?? 
            (_showAddEmployeeViewCommand = new RelayCommand(ShowAddEmploeeExecute));

    private void ShowAddEmploeeExecute(object obj)
    {
        _employeeViewModel.Employee = new Employee();
        _employeeView.ShowDialog();
    }

    private RelayCommand _showEditEmployeeViewCommand;
    public RelayCommand ShowEditEmployeeViewCommand =>
        _showEditEmployeeViewCommand ?? 
            (_showEditEmployeeViewCommand = 
                new RelayCommand(ShowEditEmploeeExecute, ShowEditEmployeeCanExcute));

    private bool ShowEditEmployeeCanExcute(object arg) => SelectedEmployee != null;

    private void ShowEditEmploeeExecute(object obj)
    {
        _employeeViewModel.Employee = SelectedEmployee;
        _employeeViewModel.SelectedDeparment = SelectedEmployee.Department;
        _employeeView.ShowDialog();
    }

    private RelayCommand _deleteEmployeeCommand;
    public RelayCommand DeleteEmployeeCommand =>
        _deleteEmployeeCommand ?? 
            (_deleteEmployeeCommand = new RelayCommand(DeleteEmploeeExecute, DeleteEmployeeCanExcute));

    private bool DeleteEmployeeCanExcute(object arg) => SelectedEmployee != null;

    private async void DeleteEmploeeExecute(object obj)
    {
        if (_dialogService.ShowMessageWithChoice("Delete Employee?"))
        {
            try
            {
                await _employeeService.DeleteAsync(SelectedEmployee.Id);
                await UpdateEmployeeList();
                _dialogService.ShowMessage("Deleted");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }

    private async Task UpdateEmployeeList()
    {
        Employees = await _employeeService.GetAllAsync();
    }

    private RelayCommand _showAddDepartmentViewCommand;
    public RelayCommand ShowAddDepartmentViewCommand =>
        _showAddDepartmentViewCommand ??
            (_showAddDepartmentViewCommand = new RelayCommand(ShowAddDepartmentExecute));

    private void ShowAddDepartmentExecute(object obj)
    {
        _departmentViewModel.Department = new Department();
        _departmentView.ShowDialog();
    }

    private RelayCommand _showEditDepartmentViewCommand;
    public RelayCommand ShowEditDepartmentViewCommand =>
        _showEditDepartmentViewCommand ??
            (_showEditDepartmentViewCommand =
                new RelayCommand(ShowEditDepartmentExecute, ShowEditDepartmentCanExcute));

    private bool ShowEditDepartmentCanExcute(object arg) => SelectedDepartment != null;

    private void ShowEditDepartmentExecute(object obj)
    {
        _departmentViewModel.Department = SelectedDepartment;
        _departmentView.ShowDialog();
    }

    private async Task UpdateDepartmentList()
    {
        Departments = await _departmentService.GetAllAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
