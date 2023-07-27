
using HRIS.Application.Common.Models;
using HRIS.Application.Employees.Dtos.Queries;
using HRIS_CoreMVC_dotNet6.Models;

namespace HRIS_CoreMVC_dotNet6.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<PaginatedList<HRIS.Application.Employees.Dtos.Queries.GetEmployeesDto>> GetEmployees(string searchKey, int pageNumber, int pageSize, string orderBy = "LastName");
        
        Task<GetEmployeesDto> GetEmployeeByEmpID(string empid);

        Task<bool> CreateEmployee(CreateEmployee request);
        Task<Tuple<bool, string>> UpdateEmployee(UpdateEmployee request);
        Task<bool> ArchiveEmployee(string empid);
    }
}
