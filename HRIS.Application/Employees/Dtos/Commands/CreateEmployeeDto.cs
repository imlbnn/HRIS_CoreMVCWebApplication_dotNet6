using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Dtos.Commands
{
    public class CreateEmployeeDto : IMapFrom<Employee>
    {
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


        //public CreateCustomEmployeeDto CreateCustomEmployeeDto { get; set; }
    }

    public class CreateCustomEmployeeDto : IMapTo<CustomEmployee>
    {
        public int SerialID { get; set; }

        public string DefinedEmpID { get; set; }

    }

}
