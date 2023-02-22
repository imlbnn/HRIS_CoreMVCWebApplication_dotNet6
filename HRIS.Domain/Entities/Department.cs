using HRIS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Entities
{
    public class Department : SoftDeletableEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
