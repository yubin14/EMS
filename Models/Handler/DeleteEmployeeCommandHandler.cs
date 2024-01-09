using EMS.Models.Command;
using EMS.Repository;
using MediatR;

namespace EMS.Models.Handler
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeerepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeerepository)
        {
            _employeerepository = employeerepository;
        }

        public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeerepository.GetEmployeeByIdAsync(request.EmployeeID);

            if (employee != null)
            {
                await _employeerepository.DeleteEmployeeAsync(request.EmployeeID);
            }

            return default;
        }
    }
}
