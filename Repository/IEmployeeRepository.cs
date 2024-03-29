﻿using EMS.Models;

namespace EMS.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync(int skip, int take);
        Task<int> GetTotalEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
