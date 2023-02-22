using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Dtos.Queries
{
    public class GetEmployeesDto : IMapFrom<Employee>
    {
        private string _fullName;

        public int SerialID { get; set; }
        public string EmpID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string FullName
        {
            get => _fullName;
            set
            {
                string name = string.IsNullOrEmpty(MiddleName) ? (FirstName + " " + LastName) : (FirstName + " " + MiddleName + " " + LastName);
                value = name;
            }
        }

        public DateTime DateOfBirth { get; set; }

        public GetEmployeeDepartmentDto Department { get; set; }

        public GetEmployeeDepartmentSectionDto DepartmentSection { get; set; }

        public GetEmployeeCivilStatusDto CivilStatus { get; set; }
    }
}
