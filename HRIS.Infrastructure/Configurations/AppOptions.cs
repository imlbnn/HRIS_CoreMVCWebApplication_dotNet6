using HRIS.Application.Common.Interfaces.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Infrastructure.Configurations
{
    public class AppOptions : IApp
    {
        public string Name { get; set; }
    }
}
