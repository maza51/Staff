using Staff.Application.Interfaces;
using Staff.Application.Services;
using Staff.Domain;
using Staff.Wpf.Commands;
using Staff.Wpf.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Staff.Wpf.ViewModels;

public class DepartmentViewModel : INotifyPropertyChanged
{
    private readonly IDepartmentService _departmentService;
    private readonly IDialogService _dialogService;

    private Department _department;
    public Department Department
    {
        get => _department;
        set
        {
            _department = value;
            OnPropertyChanged("Department");
        }
    }
    public DepartmentViewModel(IDepartmentService departmentService, IDialogService dialogService)
    {
        _departmentService = departmentService;

        Department = new Department();
        _dialogService = dialogService;
    }

    private RelayCommand _addDepartmentCommand;
    public RelayCommand AddDepartmentCommand =>
        _addDepartmentCommand ??
            (_addDepartmentCommand = new RelayCommand(AddDepartmentExecute, AddDepartmentCanExecute));

    private bool AddDepartmentCanExecute(object obj)
    {
        return !string.IsNullOrEmpty(Department.Name);
    }

    private async void AddDepartmentExecute(object obj) // Обновить главную страницу
    {
        try
        {
            if (Department.Id > 0)
                await _departmentService.UpdateAsync(Department);
            else
                await _departmentService.CreateAsync(Department);

            _dialogService.ShowMessage("successfully");
        }
        catch (Exception ex) { _dialogService.ShowMessage(ex.Message); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
