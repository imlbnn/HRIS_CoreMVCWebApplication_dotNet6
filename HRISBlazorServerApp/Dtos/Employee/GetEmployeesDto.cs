
namespace HRISBlazorServerApp.Dtos.Employee
{
    public class GetEmployeesDto
    {
        public string EmpID { get; set; }

        public int SerialID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentSectionCode { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string CivilStatusCode { get; set; }

        public GetEmployeeDepartmentDto Department { get; set; }

        public GetEmployeeDepartmentSectionDto DepartmentSection { get; set; }

        public GetEmployeeCivilStatusDto CivilStatus { get; set; }
    }
}
