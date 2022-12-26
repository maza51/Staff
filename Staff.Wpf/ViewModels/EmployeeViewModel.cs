using Staff.Application.Interfaces;
using Staff.Domain;
using Staff.Wpf.Commands;
using Staff.Wpf.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Staff.Wpf.ViewModels;

public class EmployeeViewModel : INotifyPropertyChanged
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;
    private readonly IDialogService _dialogService;

    private Employee _employee;
    public Employee Employee
    {
        get => _employee;
        set
        {
            _employee = value;
            OnPropertyChanged("Employee");
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

    private Department _selectedDeparment;
    public Department SelectedDeparment
    {
        get => _selectedDeparment;
        set
        {
            _selectedDeparment = value;
            OnPropertyChanged("SelectedDeparment");
        }
    }

    public EmployeeViewModel(IEmployeeService employeeService,
        IDepartmentService departmentService,
        IDialogService dialogService)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
        _dialogService = dialogService;

        Employee = new Employee();
        Departments = new List<Department>();
    }

    private RelayCommand _addEmployeeCommand;
    public RelayCommand AddEmployeeCommand =>
        _addEmployeeCommand ?? 
            (_addEmployeeCommand = new RelayCommand(AddEmploeeExecute, AddEmployeeCanExecute));

    private bool AddEmployeeCanExecute(object obj)
    {
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        var re = new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        var rp = new Regex(@"^(\+[0-9]{11})$");

        return rp.IsMatch(Employee.Phone ?? "") &&
            re.IsMatch(Employee.Email ?? "") &&
            !string.IsNullOrEmpty(Employee.FirstName) &&
            !string.IsNullOrEmpty(Employee.MiddleName) &&
            !string.IsNullOrEmpty(Employee.LastName) &&
            !string.IsNullOrEmpty(Employee.Position);
    }

    private async void AddEmploeeExecute(object obj) // Обновить главную страницу
    {
        Employee.Department = SelectedDeparment;

        try
        {
            if (Employee.Id > 0)
                await _employeeService.UpdateAsync(Employee);
            else
                await _employeeService.CreateAsync(Employee);

            _dialogService.ShowMessage("successfully");
        }
        catch(Exception ex) { _dialogService.ShowMessage(ex.Message); }
    }

    private RelayCommand _onwWindowLoadedCommand;
    public RelayCommand OnWindowLoadedCommand =>
        _onwWindowLoadedCommand ?? 
            (_onwWindowLoadedCommand = new RelayCommand(OnWindowLoadedExecute));

    private async void OnWindowLoadedExecute(object obj)
    {
        Departments = await _departmentService.GetAllAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
