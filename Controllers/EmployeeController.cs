using EMS.Models;
using EMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EMS.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger )
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            try
            {
   
                int skip = (page - 1) * pageSize;

                var employees = await _employeeRepository.GetAllEmployeesAsync(skip, pageSize);

                var totalEmployees = await _employeeRepository.GetTotalEmployeesAsync();

                var viewModel = new EmployeeListViewModel
                {
                    Employees = employees,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalEmployees
                };

                _logger.LogInformation("Retrieved all employees successfully.");
                return View(viewModel);
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

                    
                    return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an employee.");
                
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
                
                return RedirectToAction("Index");
            }
        }
    }

}
