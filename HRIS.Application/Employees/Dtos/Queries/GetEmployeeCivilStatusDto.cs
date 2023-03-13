﻿using HRIS.Application.CivilStatuses.Dtos;
using HRIS.Application.Common.Mappings;
using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Application.Employees.Dtos.Queries
{
    public class GetEmployeeCivilStatusDto : IMapFrom<CivilStatus>
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
