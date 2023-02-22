using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Common.Interfaces
{
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }
        DateTime DateCreated { get; set; }
        string? ModifiedBy { get; set; }
        DateTime? DateModified { get; set; }
    }
}
