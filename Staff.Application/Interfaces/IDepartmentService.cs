using Staff.Domain;

namespace Staff.Application.Interfaces;

public interface IDepartmentService
{
    Task<Department> GetByIdAsync(int id);
    Task<List<Department>> GetAllAsync();
    Task<Department> UpdateAsync(Department department);
    Task<Department> CreateAsync(Department department);
}