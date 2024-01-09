using EMS.Models.Command;
using EMS.Repository;
using MediatR;

namespace EMS.Models.Handler
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeerepository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeerepository)
        {
            _employeerepository = employeerepository;
        }

        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeerepository.GetEmployeeByIdAsync(request.EmployeeID);

            if (employee != null)
            {
                employee.FirstName = request.FirstName;
                employee.LastName = request.LastName;
                employee.Email = request.Email;
                employee.Department = request.Department;

                await _employeerepository.UpdateEmployeeAsync(employee);
            }
            return default;
        }
    }


}



