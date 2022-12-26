using Microsoft.EntityFrameworkCore;
using Staff.Application.Exceptions;
using Staff.Application.Interfaces;
using Staff.DataAccess;
using Staff.Domain;

namespace Staff.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _dbContext;

    public EmployeeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _dbContext.Employees.AsQueryable()
            .Include(x => x.Department)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        var employeeInDb = await _dbContext.Employees.AsQueryable()
            .Include(x => x.Department)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        
        if (employeeInDb == null)
            throw new NotFoundException($"employee with id {id} not found");

        return employeeInDb;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        var employeeInDb = await _dbContext.Employees
            .FindAsync(employee.Id);

        if (employeeInDb == null)
            throw new NotFoundException($"employee with id {employee.Id} not found");

        employeeInDb.FirstName = employee.FirstName;
        employeeInDb.LastName = employee.LastName;
        employeeInDb.MiddleName = employee.MiddleName;
        employeeInDb.Email = employee.Email;
        employeeInDb.Phone = employee.Phone;
        employeeInDb.Position = employee.Position;
        employeeInDb.DateBirth = employee.DateBirth;
        employeeInDb.Department = employee.Department;

        await _dbContext.SaveChangesAsync();

        return employeeInDb;
    }

    public async Task DeleteAsync(int id)
    {
        var employeeInDb = await _dbContext.Employees
            .FindAsync(id);

        if (employeeInDb == null)
            throw new NotFoundException($"employee with id {id} not found");

        _dbContext.Employees.Remove(employeeInDb);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        if (employee.Id > 0)
            throw new BadRequestException(""); //Todo
        
        if (employee.Department != null)
        {
            var departmentInDb = await _dbContext.Departments
                .FindAsync(employee.Department.Id);

            employee.Department = departmentInDb ?? employee.Department;
        }

        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();

        return employee;
    }
}