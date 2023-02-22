using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Configurations
{
    public class DbContextOptions
    {
        public bool UseIsolationLevelReadUncommitted { get; set; }
        public int CommandTimeout { get; set; } = 30;
    }
    
}
