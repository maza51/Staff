using Staff.Application.Interfaces;
using Staff.Domain;
using Staff.Wpf.Commands;
using Staff.Wpf.Services;
using Staff.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Staff.Wpf.ViewModels;

public class EmployeeViewModel : BaseViewModel
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


    private List<Employee> _filteredEmployees;
    public List<Employee> FilteredEmployees
    {
        get => _filteredEmployees;
        set
        {
            _filteredEmployees = value;
            OnPropertyChanged("FilteredEmployees");
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

    private string _filter;
    public string Filter
    {
        get => _filter;
        set
        {
            _filter = value;
            OnPropertyChanged("Filter");

            FilteredEmployees = Employees.Where(x => 
                x.FirstName.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                x.MiddleName.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                x.LastName.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                x.Email.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                x.Phone.Contains(_filter)).ToList();
        }
    }

    private readonly IEmployeeService _employeeService;
    private readonly IDialogService _dialogService;
    private readonly EmployeeDetailView _employeeDetailView;
    private readonly EmployeeDetailViewModel _employeeViewModel;

    public EmployeeViewModel(
        IEmployeeService employeeService,
        IDialogService dialogService,
        EmployeeDetailView employeeDetailView,
        EmployeeDetailViewModel employeeViewModel)
    {
        _employeeService = employeeService;
        _dialogService = dialogService;
        _employeeDetailView = employeeDetailView;
        _employeeViewModel = employeeViewModel;

        Task.Run(async () => await UpdateEmployeeList());
    }

    private RelayCommand _showAddEmployeeViewCommand;
    public RelayCommand ShowAddEmployeeViewCommand =>
        _showAddEmployeeViewCommand ??
            (_showAddEmployeeViewCommand = new RelayCommand(ShowAddEmploeeExecute));

    private async void ShowAddEmploeeExecute(object obj)
    {
        _employeeViewModel.Employee = new Employee();
        _employeeDetailView.ShowDialog();
        await UpdateEmployeeList();
    }

    private RelayCommand _showEditEmployeeViewCommand;
    public RelayCommand ShowEditEmployeeViewCommand =>
        _showEditEmployeeViewCommand ??
            (_showEditEmployeeViewCommand =
                new RelayCommand(ShowEditEmploeeExecute, ShowEditEmployeeCanExcute));

    private bool ShowEditEmployeeCanExcute(object arg) => SelectedEmployee != null;

    private async void ShowEditEmploeeExecute(object obj)
    {
        _employeeViewModel.Employee = SelectedEmployee;
        _employeeViewModel.SelectedDeparment = SelectedEmployee.Department!;
        _employeeDetailView.ShowDialog();
        await UpdateEmployeeList();
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

    private RelayCommand _refreshCommand;
    public RelayCommand RefreshCommand =>
        _refreshCommand ??
            (_refreshCommand = new RelayCommand(RefreshExecute));

    private async void RefreshExecute(object obj)
    {
        await UpdateEmployeeList();
    }

    private async Task UpdateEmployeeList()
    {
        Employees = await _employeeService.GetAllAsync();
        FilteredEmployees = Employees;
    }
}
