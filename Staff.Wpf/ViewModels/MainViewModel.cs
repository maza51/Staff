using Microsoft.Win32;
using Staff.Application.Interfaces;
using Staff.Domain;
using Staff.Wpf.Commands;
using Staff.Wpf.Services;
using System;
using System.Collections.Generic;

namespace Staff.Wpf.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IEmployeeService _employeeService;
    private readonly IDialogService _dialogService;
    private readonly IExporter<List<Employee>> _exporter;
    private readonly IImporter<List<Employee>> _importer;

    public MainViewModel(
        IEmployeeService employeeService,
        IExporter<List<Employee>> exporter,
        IDialogService dialogService,
        IImporter<List<Employee>> importer)
    {
        _employeeService = employeeService;
        _exporter = exporter;
        _dialogService = dialogService;
        _importer = importer;
    }

    private RelayCommand _importDataCommand;
    public RelayCommand ImportDataCommand =>
        _importDataCommand ??
            (_importDataCommand = new RelayCommand(ImportDataExecute));

    private async void ImportDataExecute(object obj)
    {
        var dialog = new OpenFileDialog();
        dialog.Title = "Open a File";

        if (dialog.ShowDialog() == true)
        {
            try
            {
                var importedEmployees = await _importer.ImportAsync(dialog.FileName);
                await _employeeService.ImportProcessAsync(importedEmployees);
                _dialogService.ShowMessage("successfully");
            }
            catch (Exception ex) { _dialogService.ShowMessage(ex.Message); }
        }
    }

    private RelayCommand _exportDataCommand;
    public RelayCommand ExportDataCommand =>
        _exportDataCommand ??
            (_exportDataCommand = new RelayCommand(ExportDataExecute));

    private async void ExportDataExecute(object obj)
    {
        var employees = await _employeeService.GetAllAsync();

        var dialog = new SaveFileDialog();
        dialog.Title = "Select a Directory";
        dialog.FileName = "ExportedData_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");

        if (dialog.ShowDialog() == true)
        {
            try
            {
                await _exporter.ExportAsync(employees, dialog.FileName);
                _dialogService.ShowMessage("successfully");
            }
            catch (Exception ex) { _dialogService.ShowMessage(ex.Message); }
        }
    }
}
