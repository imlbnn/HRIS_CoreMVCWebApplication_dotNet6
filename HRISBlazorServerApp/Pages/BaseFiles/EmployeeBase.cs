using Radzen;
using Radzen.Blazor;
using HRISBlazorServerApp.Dtos.Employee;
using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;

namespace HRISBlazorServerApp.Pages.BaseFiles
{

    public class EmployeeBase : PageBase
    {

        public int CurPage = 1;

        public int TotalPages;

        public int count;

        public int minPage = 1;
        public int maxPage;
        public int minRange { get; set; }
        public int maxRange { get; set; }


        public List<GetEmployeesDto> getEmployees = new List<GetEmployeesDto>();


        public Task IsLoaded;

        public bool isLoading;


        public RadzenDataGrid<GetEmployeesDto> grid;

        private IQueryable<GetEmployeesDto> _query;


        //public async Task LoadData(LoadDataArgs args)
        //{
        //    try
        //    {
        //        isLoading = true;
        //        var data = (await employeeService.GetEmployees());

        //        _query = data.AsQueryable();

        //        count = _query.Count();

        //        getEmployees = _query
        //                .Skip(args.Skip.Value)
        //                .Take(args.Top.Value)
        //                .ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        isLoading = false;
        //    }
        //}

        protected override async Task OnInitializedAsync()
        {
            await ShowPage();
            //getEmployees = (await employeeService.GetEmployees().ConfigureAwait(false)).ToList();

            //await base.OnInitializedAsync();
        }

        public void CreateNewEmployee()
        {
            UriHelper.NavigateTo("/employee/create");
        }

        public void EditEmployee(string empid)
        {
            UriHelper.NavigateTo($"employee/edit/{empid}");
        }

        public async Task DeleteEmployee(string empid)
        {
            await employeeService.ArchiveEmployee(empid);
            GetEmployees();
        }

        private async void GetEmployees()
        {
            getEmployees.Clear();

            CurPage = 1;

            var data = (await employeeService.GetEmployees(string.Empty, CurPage, 10).ConfigureAwait(false));
            
            getEmployees = data.Items.ToList();

            TotalPages = maxPage = data.TotalPages;

            minRange = Math.Max(minPage, CurPage - 2);

            maxRange = Math.Min(maxPage, CurPage + 2);

            await InvokeAsync(() => StateHasChanged());
        }

        public void ViewEmployee(string empid)
        {
            UriHelper.NavigateTo($"employee/{empid}");
        }

        protected async Task NextPage()
        {
            if (CurPage < TotalPages)
            {
                CurPage++;
                await ShowPage();
            }
        }

        protected async Task ShowPage(int i)
        {
            CurPage = i;
            await ShowPage();
        }

        protected async Task PrevPage()
        {
            if (CurPage > 1)
            {
                CurPage--;
                await ShowPage();
            }

        }

        protected async Task ShowPage()
        {
            var data = (await employeeService.GetEmployees("", CurPage, 10).ConfigureAwait(false));
            
            getEmployees = data.Items.ToList();
            
            TotalPages = maxPage = data.TotalPages;
            
            minRange = Math.Max(minPage, CurPage - 2);

            maxRange = Math.Min(maxPage, CurPage + 2);


    }


}
}
