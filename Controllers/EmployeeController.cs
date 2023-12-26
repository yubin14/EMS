using EMS.Models;
using EMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EMS.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _webHostEnv;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger, IWebHostEnvironment webHostEnv)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
            _webHostEnv = webHostEnv;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                

                var employees = await _employeeRepository.GetAllEmployeesAsync();
               
                _logger.LogInformation("Retrieved all employees successfully.");
                return View(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting employees.");
                
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            try
            {
                    var employee = new Employee()
                    {
                        FirstName = addEmployeeRequest.FirstName,
                        LastName = addEmployeeRequest.LastName,
                        Email = addEmployeeRequest.Email,
                        Department = addEmployeeRequest.Department,
                    };

                    await _employeeRepository.AddEmployeeAsync(employee);

                    _logger.LogInformation("Employee added successfully.");

                    // Redirect to the Index action to show the updated list of employees.
                    return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an employee.");
                // Optionally, redirect or show an error view
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    EmployeeID = employee.EmployeeID,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Department = employee.Department,
                };

                return View("View", viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(model.EmployeeID);

                if (employee != null)
                {
                    employee.FirstName = model.FirstName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;
                    employee.Department = model.Department;

                    await _employeeRepository.UpdateEmployeeAsync(employee);

                    _logger.LogInformation($"Employee {model.EmployeeID} updated successfully.");

                    return RedirectToAction("Index");
                }

                _logger.LogWarning($"Attempted to update non-existent employee with ID {model.EmployeeID}.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating employee with ID {model.EmployeeID}.");
                // Optionally, redirect or show an error view
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(model.EmployeeID);

                if (employee != null)
                {
                    await _employeeRepository.DeleteEmployeeAsync(model.EmployeeID);

                    _logger.LogInformation($"Employee {model.EmployeeID} deleted successfully.");

                    return RedirectToAction("Index");
                }

                _logger.LogWarning($"Attempted to delete non-existent employee with ID {model.EmployeeID}.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting employee with ID {model.EmployeeID}.");
                // Optionally, redirect or show an error view
                return RedirectToAction("Index");
            }
        }
    }

}
