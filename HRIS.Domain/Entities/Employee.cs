using HRIS.Domain.Common;

namespace HRIS.Domain.Entities
{
    public class Employee : SoftDeletableEntity
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

        public virtual Department Department { get; set; }

        public virtual DepartmentalSection DepartmentSection { get; set; }

        public virtual CivilStatus CivilStatus { get; set; }




    }
}
