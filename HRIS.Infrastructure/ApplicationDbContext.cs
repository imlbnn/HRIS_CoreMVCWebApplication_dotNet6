using System.Diagnostics;
using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using HRIS.Domain.Entities;
using HRIS.Infrastructure.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HRIS.Infrastructure
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
           IOptionsMonitor<Configurations.DbContextOptions> dbOptions,
           IOptions<OperationalStoreOptions> operationalStoreOptions
            ) : base(options, operationalStoreOptions)
        {
            var _dbOptions = dbOptions.CurrentValue;
            if (_dbOptions.UseIsolationLevelReadUncommitted)
            {
                Database.OpenConnection();
                Database.ExecuteSqlRaw("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            }
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<DepartmentalSection> DepartmentSections { get; set; }

        public DbSet<CustomEmployee> CustomEmployees { get; set; }

        public DbSet<CivilStatus> CivilStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
