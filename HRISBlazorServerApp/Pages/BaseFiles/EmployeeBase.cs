using AutoMapper;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Services.Page;
using HRISBlazorServerApp.Pages.BaseFiles;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System.ComponentModel.Design;
using HRISBlazorServerApp.Dtos.Employee;

namespace HRISBlazorServerApp.Pages.BaseFiles
{

    public class EmployeeBase : PageBase
    {
        public int count;

        public IEnumerable<GetEmployeesDto> getEmployees { get; set; }


        public Task IsLoaded;


        public RadzenDataGrid<GetEmployeesDto> grid;

        private IQueryable<GetEmployeesDto> _query;


        public async Task LoadData(LoadDataArgs args)
        {
            var data = (await employeeService.GetEmployees());

            getEmployees = data;
        }

        protected override async Task OnInitializedAsync()
        {
            //var data = (await employeeService.GetEmployees());

            //getEmployees = data;


            CreateEmployeeDto dto = new CreateEmployeeDto()
            {
                LastName = "asdasd",
                FirstName = "Test",
                MiddleName = "Test",
                DepartmentCode = "I",
                DepartmentSectionCode = "02",
                DateOfBirth = DateTime.Now,
                CivilStatusCode = "04"  
            };


            var asd = await employeeService.CreateEmployee(dto);

            var res = asd;


        }
    }
}
