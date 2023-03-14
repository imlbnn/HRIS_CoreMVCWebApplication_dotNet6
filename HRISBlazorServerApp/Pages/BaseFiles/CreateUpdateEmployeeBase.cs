using HRISBlazorServerApp.Dtos.CivilStatus;
using HRISBlazorServerApp.Dtos.Department;
using HRISBlazorServerApp.Dtos.DepartmentSection;
using HRISBlazorServerApp.Dtos.Employee;
using HRISBlazorServerApp.Models;
using Microsoft.AspNetCore.Components;

namespace HRISBlazorServerApp.Pages.BaseFiles
{
    public class CreateUpdateEmployeeBase : PageBase
    {
        [Parameter]
        public string empid { get; set; }

        public string btnText = string.Empty;

        public CreateEmployee createEmployee = new CreateEmployee();
        public UpdateEmployee updateEmployee = new UpdateEmployee();
        public List<GetDepartmentDto> departmentList = new List<GetDepartmentDto>();
        public List<GetDepartmentSectionDto> departmentSectionList = new List<GetDepartmentSectionDto>();
        public List<GetCivilStatusDto> civilStatusList = new List<GetCivilStatusDto>();

        protected override async Task OnInitializedAsync()
        {
            btnText = string.IsNullOrEmpty(empid) ? "Save New Employee" : "Update Employee";

            departmentList = (await departmentService.GetDepartments().ConfigureAwait(false)).ToList();

            civilStatusList = (await civilStatusService.GetCivilStatus().ConfigureAwait(false)).ToList();
        }

        public async Task HandleSubmit()
        {
            
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
