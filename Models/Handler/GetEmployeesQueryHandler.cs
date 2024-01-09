
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EMS.Models;
using EMS.Repository;
using EMS.Models.Query;

namespace EMS.Models.Handler
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, EmployeeListViewModel>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeListViewModel> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var skip = (request.Page - 1) * request.PageSize;
            var employees = await _employeeRepository.GetAllEmployeesAsync(skip, request.PageSize);
            var totalItems = await _employeeRepository.GetTotalEmployeesAsync();

            return new EmployeeListViewModel
            {
                Employees = employees,
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                TotalItems = totalItems
            };
        }
    }
}