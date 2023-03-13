using AutoMapper;
using HRIS.Application.CivilStatuses.Dtos;
using HRIS.Application.Common.Mappings;
using HRIS.Application.DepartmentalSections.Dtos.Queries;
using HRIS.Application.Departments.Dtos.Queries;
using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Dtos.Queries
{
    public class GetEmployeesDto : IMapFrom<Employee>
    {
        public string EmpID { get; set; }

        public int SerialID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentSectionCode { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string CivilStatusCode { get; set; }

        public GetEmployeeDepartmentDto Department { get; set; }

        public GetEmployeeDepartmentSectionDto DepartmentSection { get; set; }

        public GetEmployeeCivilStatusDto CivilStatus { get; set; }

        //public void MapFrom(Profile profile)
        //{
        //    profile.CreateMap<Employee, GetEmployeesDto>()
        //        .ForMember(d => d.Department, o => o.MapFrom(s => s.Department))
        //        .ForMember(d => d.DepartmentSection, o => o.MapFrom(s => s.DepartmentSection))
        //        .ForMember(d => d.CivilStatus, o => o.MapFrom(s => s.CivilStatus)).ReverseMap();
        //}
    }

}
