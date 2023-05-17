using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Config
{
    internal class EmpleadoConfig : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            
            builder.HasKey(x => x.ID).HasName("ID");
            builder.HasIndex(x => x.Ndoc_Emp, "UNIQ_NDOC_EMP").IsUnique();
            builder.HasIndex(X => X.Telefono, "UNIQ_TELEFONO").IsUnique();

            builder.Property(x => x.Nombre).HasMaxLength(50).HasColumnName("Nombre").HasColumnType("varchar");
            builder.Property(x => x.Apellido).HasMaxLength(50).HasColumnName("Apellido").HasColumnType("varchar");
            builder.Property(x => x.Tdoc_Emp).HasMaxLength(2).HasColumnName("Tdoc_Emp").HasColumnType("varchar");
            builder.Property(x => x.Ndoc_Emp).HasColumnName("Ndoc_Emp");
            builder.Property(X => X.Telefono).HasMaxLength(20).HasColumnName("Telefono").HasColumnType("varchar");
            builder.Property(X => X.Profesion).HasMaxLength(20).HasColumnName("Profesion").HasColumnType("varchar");
            builder.Property(x => x.Ciudad).HasMaxLength(15).HasColumnName("Ciudad").HasColumnType("varchar");
            builder.Property(x => x.Direccion).HasMaxLength(20).HasColumnName("Direccion").HasColumnType("varchar");
            builder.Property(x => x.Email).HasMaxLength(100).HasColumnType("Email").HasColumnType("varchar");
            builder.Property(x => x.Estado).HasMaxLength(10).HasColumnType("varchar");
            builder.Property(x => x.Genero).HasMaxLength(10).HasColumnType("varchar");
            builder.Property(x => x.FechaCreacion).HasColumnType("datetime");
            builder.Property(x => x.FechaActualizacion).HasColumnType("datetime");


        }
    }
}
