using HRIS.Application.Common.Interfaces.Application;
using HRIS.Application.Common.Interfaces.Repositories;
using HRIS.Application.Common.Interfaces.Services;
using HRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Infrastructure.Persistence.Repositories
{
    public class CivilStatusRepository : GenericRepositoryAsync<CivilStatus>, ICivilStatusRepository
    {
        public CivilStatusRepository(ApplicationDbContext dbContext,
                                    IDateTime dateTimeService,
                                    ICurrentUserService currentUserService)
                                    : base(dbContext,
                                          dateTimeService,
                                          currentUserService)
        {
            SetGetQuery(dbContext.Set<CivilStatus>());
        }
    }
}
