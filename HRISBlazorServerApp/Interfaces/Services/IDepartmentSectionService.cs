using HRISBlazorServerApp.Dtos.DepartmentSection;
using Microsoft.AspNetCore.Mvc;

namespace HRISBlazorServerApp.Interfaces.Services
{
    public interface IDepartmentSectionService
    {
        Task<IEnumerable<GetDepartmentSectionDto>> GetDepartmentalSection(string code);
    }
}
