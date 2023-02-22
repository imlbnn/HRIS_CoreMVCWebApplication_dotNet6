using AutoMapper;
using HRIS.Application.Common.Mappings;
using HRIS.Application.Common.Security;
using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeDto>, IMapTo<Employee>
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentSectionCode { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string CivilStatusCode { get; set; }

    }
}
