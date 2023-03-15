using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRISBlazorServerApp.Models
{
    public class CreateEmployee
    {
        [Required(ErrorMessage ="Lastname is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Department Section is required")]
        public string DepartmentSectionCode { get; set; }

        public string DepartmentSectionName { get; set; }

        [Required(ErrorMessage ="Date Of Birth is required")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Civil Status is required")]
        public string CivilStatusCode { get; set; }

        public string CivilStatusName { get; set; }


    }
}
