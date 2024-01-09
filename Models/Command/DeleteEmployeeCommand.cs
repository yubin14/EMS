using MediatR;

namespace EMS.Models.Command
{
    public class DeleteEmployeeCommand : IRequest<int>
    {
        public int EmployeeID { get; set; }
    }
}
