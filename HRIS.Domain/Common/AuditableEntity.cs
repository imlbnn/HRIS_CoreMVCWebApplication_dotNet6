using HRIS.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Common
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DateModified { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
