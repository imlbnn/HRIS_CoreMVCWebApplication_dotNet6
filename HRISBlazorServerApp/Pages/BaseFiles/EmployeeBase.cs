using Radzen;
using Radzen.Blazor;
using HRISBlazorServerApp.Dtos.Employee;

namespace HRISBlazorServerApp.Pages.BaseFiles
{

    public class EmployeeBase : PageBase
    {
        public int count;

        public IEnumerable<GetEmployeesDto> getEmployees { get; set; }


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
            await base.OnInitializedAsync();
        }
    }
}
