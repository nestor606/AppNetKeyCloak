using Datos.Config;
using Datos.Entidades;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Contexto
{
    public class ApplicationsContext:DbContext
    {
        public DbSet<Empleado> Empleado { get; set; }

        public ApplicationsContext()
        {

        }

        public ApplicationsContext(DbContextOptions<ApplicationsContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=RRHH_TEST;Trusted_Connection=True;Encrypt=Optional;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmpleadoConfig());
        }
    }
}
