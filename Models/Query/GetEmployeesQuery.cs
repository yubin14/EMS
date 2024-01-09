using MediatR;



namespace EMS.Models.Query
{
    public class GetEmployeesQuery : IRequest<EmployeeListViewModel>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}