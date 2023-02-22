using HRIS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Entities
{
    public class CustomEmployee : SoftDeletableEntity
    {
        public int ID { get; set; }
        public string EmpID { get; set; }
        public string DefinedEmpID { get; set; }

        //public virtual Employee Employee { get; set; }
    }
}
