using Microsoft.EntityFrameworkCore;
using Staff.Application.Exceptions;
using Staff.Application.Interfaces;
using Staff.DataAccess;
using Staff.Domain;

namespace Staff.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _dbContext;

    public DepartmentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Department> GetByIdAsync(int id)
    {
        var departmentInDb = await _dbContext.Departments
            .FindAsync(id);

        if (departmentInDb == null)
            throw new NotFoundException($"department with id {id} not found");

        return departmentInDb;
    }

    public async Task<Department> CreateAsync(Department department)
    {
        await _dbContext.Departments.AddAsync(department);
        await _dbContext.SaveChangesAsync();

        return department;
    }

    public async Task<List<Department>> GetAllAsync()
    {
        return await _dbContext.Departments.AsQueryable()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Department> UpdateAsync(Department department)
    {
        var departmentInDb = await _dbContext.Departments
            .FindAsync(department.Id);

        if (departmentInDb == null)
            throw new NotFoundException($"department with id {department.Id} not found");

        departmentInDb.Name = department.Name;
        await _dbContext.SaveChangesAsync();

        return departmentInDb;
    }
}