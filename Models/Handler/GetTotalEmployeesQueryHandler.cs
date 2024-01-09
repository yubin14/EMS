using EMS.Models.Query;
using EMS.Repository;
using MediatR;

namespace EMS.Models.Handler
{
    public class GetTotalEmployeesQueryHandler : IRequestHandler<GetTotalEmployeesQuery, int>
    {
        private readonly IEmployeeRepository _employeerepository;

        public GetTotalEmployeesQueryHandler(IEmployeeRepository employeerepository)
        {
            _employeerepository = employeerepository;
        }

        public async Task<int> Handle(GetTotalEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _employeerepository.GetTotalEmployeesAsync();
        }
    }
}
