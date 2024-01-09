using EMS.Models.Query;
using EMS.Models.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class EmployeeApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees(int page = 1, int pageSize = 5)
    {
        var query = new GetEmployeesQuery { Skip = (page - 1) * pageSize, Take = pageSize };
        var employees = await _mediator.Send(query);
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var query = new GetEmployeeByIdQuery { EmployeeID = id };
        var employee = await _mediator.Send(query);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeCommand command)
    {
        var employeeId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
    {
        if (id != command.EmployeeID)
        {
            return BadRequest("Mismatched IDs");
        }

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var command = new DeleteEmployeeCommand { EmployeeID = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
