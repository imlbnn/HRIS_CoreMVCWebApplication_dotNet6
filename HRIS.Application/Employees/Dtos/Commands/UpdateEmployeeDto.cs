using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRIS.Application.CivilStatuses.Dtos;
using HRIS.Application.DepartmentalSections.Dtos.Queries;
using HRIS.Application.Departments.Dtos.Queries;
using HRIS.Application.Employees.Dtos.Queries;

namespace HRIS.Application.Employees.Dtos.Commands
{
    public class UpdateEmployeeDto : IMapFrom<GetEmployeesDto>
    {
        [Required(ErrorMessage = "Employee ID is required"), DisplayName("Employee ID"),]
        public string EmpID { get; set; }

        [Required(ErrorMessage = "Lastname is required"), DisplayName("Lastname"),]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Firstname is required"), DisplayName("Firstname"),]
        public string FirstName { get; set; }

        [DisplayName("Middlename"),]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Must select department"), DisplayName("Department")]
        public string DepartmentCode { get; set; }

        [Required(ErrorMessage = "Must select department section"), DisplayName("Department Section"),]
        public string DepartmentSectionCode { get; set; }

        [Required(ErrorMessage = "Date of birth is required"), DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Must select civil status"), DisplayName("Civil Status"),]
        public string CivilStatusCode { get; set; }


        //public GetEmployeeDepartmentDto Department { get; set; }

        //public GetEmployeeDepartmentSectionDto DepartmentSection { get; set; }

        //public GetEmployeeCivilStatusDto CivilStatus { get; set; }
    }
}
