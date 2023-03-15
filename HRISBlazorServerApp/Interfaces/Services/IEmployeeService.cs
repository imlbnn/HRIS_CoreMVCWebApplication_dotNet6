using HRISBlazorServerApp.Dtos.Employee;
using HRISBlazorServerApp.Models;

namespace HRISBlazorServerApp.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<PaginatedList<GetEmployeesDto>> GetEmployees(string searchKey, int pageNumber, int pageSize, string orderBy = "LastName");
        
        Task<GetEmployeesDto> GetEmployeeByEmpID(string empid);

        Task<bool> CreateEmployee(CreateEmployee request);
        Task<Tuple<bool, string>> UpdateEmployee(UpdateEmployee request);
        Task<bool> ArchiveEmployee(string empid);
    }
}
