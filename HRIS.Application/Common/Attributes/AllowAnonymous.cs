
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymous : Attribute
    {
    }
}
