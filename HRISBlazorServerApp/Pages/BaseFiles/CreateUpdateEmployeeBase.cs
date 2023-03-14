using HRISBlazorServerApp.Dtos.CivilStatus;
using HRISBlazorServerApp.Dtos.Department;
using HRISBlazorServerApp.Dtos.DepartmentSection;
using HRISBlazorServerApp.Dtos.Employee;
using HRISBlazorServerApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace HRISBlazorServerApp.Pages.BaseFiles
{
    public class CreateUpdateEmployeeBase : PageBase
    {
        [Parameter]
        public string empid { get; set; }

        public string btnText = string.Empty;

        public string deptCode { get; set; }

        public CreateEmployee createEmployee = new CreateEmployee();
        public UpdateEmployee updateEmployee = new UpdateEmployee();
        public List<GetDepartmentDto> departmentList = new List<GetDepartmentDto>();
        public List<GetDepartmentSectionDto> departmentSectionList = new List<GetDepartmentSectionDto>();
        public List<GetCivilStatusDto> civilStatusList = new List<GetCivilStatusDto>();

        protected override async Task OnInitializedAsync()
        {
            btnText = string.IsNullOrEmpty(empid) ? "Save New Employee" : "Update Employee";

            if (!string.IsNullOrEmpty(empid))
                updateEmployee.EmpID = empid;

            departmentList = (await departmentService.GetDepartments().ConfigureAwait(false)).ToList();

            civilStatusList = (await civilStatusService.GetCivilStatus().ConfigureAwait(false)).ToList();
        }

        public async Task HandleSubmit()
        {
            if (!string.IsNullOrEmpty(empid))
            {
                await employeeService.CreateEmployee(createEmployee);
            }
            else
            {
                await employeeService.UpdateEmployee(updateEmployee);
            }

            UriHelper.NavigateTo("/employee");
        }

        public async Task ReturnToEmployeeMain()
        {
            UriHelper.NavigateTo("/employee");
        }


        public async Task OnValueChanged(string value)
        {
            deptCode = value;
            if (!string.IsNullOrEmpty(empid))
            {
                createEmployee.DepartmentCode = deptCode;
                createEmployee.DepartmentName = departmentList.FirstOrDefault(x => x.Code == deptCode).Description;
            }
            else
            {
                updateEmployee.DepartmentCode = deptCode;
                updateEmployee.DepartmentName = departmentList.FirstOrDefault(x => x.Code == deptCode).Description;
            }

            departmentSectionList = (await departmentSectionService.GetDepartmentalSection(deptCode).ConfigureAwait(false)).ToList();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(empid))
            {
                var data = (await employeeService.GetEmployeeByEmpID(empid));
                updateEmployee = _mapper.Map<UpdateEmployee>(data);
            }
        }


    }
}
