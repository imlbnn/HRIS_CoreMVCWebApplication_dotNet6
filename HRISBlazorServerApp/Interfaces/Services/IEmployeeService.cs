using HRIS.Application.Employees.Dtos.Commands;
using HRIS.Application.Employees.Dtos.Queries;

namespace HRISBlazorServerApp.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<GetEmployeesDto>> GetEmployees();
        Task<GetEmployeesDto> GetEmployeeByEmpID(string empid);

        Task<bool> CreateEmployee(CreateEmployeeDto request);
        Task<Tuple<bool, string>> UpdateEmployee(UpdateEmployeeDto request);
        Task<Tuple<bool, string>> ArchiveEmployee(string empid);
    }
}
