using EMS.Models.Query;
using EMS.Repository;
using MediatR;

namespace EMS.Models.Handler
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeRepository _employeerepository;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeerepository)
        {
            _employeerepository = employeerepository;
        }

        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _employeerepository.GetEmployeeByIdAsync(request.EmployeeID);
        }
    }
}
