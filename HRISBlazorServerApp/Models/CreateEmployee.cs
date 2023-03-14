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
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentSectionCode { get; set; }

        public string DepartmentSectionName { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        public string CivilStatusCode { get; set; }

        public string CivilStatusName { get; set; }


    }
}
