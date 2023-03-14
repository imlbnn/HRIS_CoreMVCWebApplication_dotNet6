using HRISBlazorServerApp.Dtos.Department;
using Microsoft.AspNetCore.Mvc;

namespace HRISBlazorServerApp.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<GetDepartmentDto>> GetDepartments();
        Task<IEnumerable<GetDepartmentDto>> GetDepartmentsConsistingName(string name);
    }
}
