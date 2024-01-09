using EMS.Models.Command;
using EMS.Repository;
using MediatR;

namespace EMS.Models.Handler
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeerepository;

        public AddEmployeeCommandHandler(IEmployeeRepository employeerepository)
        {
            _employeerepository = employeerepository;
        }

        public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Department = request.Department
            };

            await _employeerepository.AddEmployeeAsync(employee);

            return employee.EmployeeID;
        }
    }
}
