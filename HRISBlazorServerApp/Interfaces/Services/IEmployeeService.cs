using HRISBlazorServerApp.Dtos.Employee;

namespace HRISBlazorServerApp.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<GetEmployeesDto>> GetEmployees();
        Task<GetEmployeesDto> GetEmployeeByEmpID(string empid);

        Task<bool> CreateEmployee(CreateEmployeeDto request);
        Task<Tuple<bool, string>> UpdateEmployee(UpdateEmployeeDto request);
        Task<bool> ArchiveEmployee(string empid);
    }
}
