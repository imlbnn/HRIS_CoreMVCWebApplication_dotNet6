using Radzen;
using Radzen.Blazor;
using HRISBlazorServerApp.Dtos.Employee;
using Microsoft.AspNetCore.Components;

namespace HRISBlazorServerApp.Pages.BaseFiles
{

    public class EmployeeBase : PageBase
    {
        public int count;

        public List<GetEmployeesDto> getEmployees = new List<GetEmployeesDto>();


        public Task IsLoaded;

        public bool isLoading;


        public RadzenDataGrid<GetEmployeesDto> grid;

        private IQueryable<GetEmployeesDto> _query;


        public async Task LoadData(LoadDataArgs args)
        {
            try
            {
                isLoading = true;
                var data = (await employeeService.GetEmployees());

                _query = data.AsQueryable();

                count = _query.Count();

                getEmployees = _query
                        .Skip(args.Skip.Value)
                        .Take(args.Top.Value)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                isLoading = false;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            getEmployees = (await employeeService.GetEmployees().ConfigureAwait(false)).ToList();

            //await base.OnInitializedAsync();
        }

        public void CreateNewEmployee()
        {
            UriHelper.NavigateTo("/employee/create");
        }
    }
}
