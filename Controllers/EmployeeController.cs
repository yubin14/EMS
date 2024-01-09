using EMS.Models.Query;
using EMS.Models.Command;
using EMS.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EMS.Models;

namespace EMS.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var query = new GetEmployeesQuery { Page = page, PageSize = pageSize };
            var employeeList = await _mediator.Send(query);

            return View(employeeList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeCommand command)
        {
            try
            {
                var employeeId = await _mediator.Send(command);

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
            var query = new GetEmployeeByIdQuery { EmployeeID = id };
            var employee = await _mediator.Send(query);

            if (employee != null)
            {
                var command = new UpdateEmployeeCommand
                {
                    EmployeeID = employee.EmployeeID,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Department = employee.Department,
                };

                return View("View", command);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating employee with ID {command.EmployeeID}.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteEmployeeCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting employee with ID {command.EmployeeID}.");
                return RedirectToAction("Index");
            }
        }
    }
}
