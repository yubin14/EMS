﻿

using MediatR;

namespace EMS.Models.Command
{
    public class AddEmployeeCommand : IRequest<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

}
