using HRIS.Application.Common.Interfaces;
using HRIS.Application.Common.Interfaces.Application;
using System;

namespace HRIS.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
