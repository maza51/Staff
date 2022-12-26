using Staff.Domain;

namespace Staff.Application.Interfaces;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(int id);
    Task<Employee> UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
    Task<Employee> CreateAsync(Employee employee);
}