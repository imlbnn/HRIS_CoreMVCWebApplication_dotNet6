using AutoMapper;
using HRIS.Application.Employees.Dtos.Queries;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Services.Page;
using HRISBlazorServerApp.Pages.BaseFiles;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System.ComponentModel.Design;

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

            _query = (await employeeService.GetEmployees()).AsQueryable();

            count = _query.Count();

            getEmployees = _query
                .Skip(args.Skip.Value)
                .Take(args.Top.Value)
                .ToList();


        }

        protected override async Task OnInitializedAsync()
        {
            //_query = (await employeeService.GetEmployees()).AsQueryable();
        }
    }
}
