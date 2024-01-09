using MediatR;

namespace EMS.Models.Query
{
    public class GetEmployeeByIdQuery : IRequest<Employee>
    {
        public int EmployeeID { get; set; }
    }
}
