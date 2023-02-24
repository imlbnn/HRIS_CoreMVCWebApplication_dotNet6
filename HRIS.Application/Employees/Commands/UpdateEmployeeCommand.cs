using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest<Tuple<bool,string>>, IMapTo<Employee>
    {
        [Required(ErrorMessage ="Employee ID is required")]
        public string EmpID { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Employee ID is required")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string DepartmentCode { get; set; }

        [Required(ErrorMessage = "Department Section is required")]
        public string DepartmentSectionCode { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Civil Status is required")]
        public string CivilStatusCode { get; set; }
    }
}
