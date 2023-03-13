using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Departments.Dtos.Commands
{
    public class CreateDepartmentDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
