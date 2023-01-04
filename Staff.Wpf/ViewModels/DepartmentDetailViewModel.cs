using Staff.Application.Interfaces;
using Staff.Domain;
using Staff.Wpf.Commands;
using Staff.Wpf.Services;
using System;

namespace Staff.Wpf.ViewModels;

public class DepartmentDetailViewModel : BaseViewModel
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

    public event Action RequestClose;

    public DepartmentDetailViewModel(
        IDepartmentService departmentService,
        IDialogService dialogService)
    {
        _departmentService = departmentService;
        _dialogService = dialogService;

        Department = new Department();
    }

    private RelayCommand _addDepartmentCommand;
    public RelayCommand AddDepartmentCommand =>
        _addDepartmentCommand ??
            (_addDepartmentCommand = new RelayCommand(AddDepartmentExecute, AddDepartmentCanExecute));

    private bool AddDepartmentCanExecute(object obj)
    {
        return !string.IsNullOrEmpty(Department.Name);
    }

    private async void AddDepartmentExecute(object obj)
    {
        try
        {
            if (Department.Id > 0)
                await _departmentService.UpdateAsync(Department);
            else
                await _departmentService.CreateAsync(Department);

            _dialogService.ShowMessage("successfully");

            RequestClose?.Invoke();
        }
        catch (Exception ex) { _dialogService.ShowMessage(ex.Message); }
    }
}
