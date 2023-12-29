using EMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _dbContext;
        public EmployeeRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync(int skip, int take)
        {
            return await _dbContext.Employees.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> GetTotalEmployeesAsync()
        {
            return await _dbContext.Employees.CountAsync();
        }
        //public async Task<List<Employee>> GetAllEmployeesAsync()
        //{
        // return await _dbContext.Employees.ToListAsync();
        //}
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _dbContext.Employees.FindAsync(id); 
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)

        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
