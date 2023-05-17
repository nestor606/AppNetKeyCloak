using Datos.Entidades;
using Dominio.Modelo;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public static class ClsEmpleadoMapper
    {
        public static Empleado Map(this ClsEmpleadoDom model) {

            return new Empleado()
            {

                ID = model.ID,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Ndoc_Emp = model.Ndoc_emp,
                Tdoc_Emp = model.Tdoc_Emp,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Ciudad = model.Ciudad,
                Email = model.Email,
                Estado = model.Estado,
                Genero = model.Genero,
                Profesion = model.Profesion,
                FechaCreacion = model.FechaCreacion,
                FechaActualizacion = model.FechaActualizacion

            };

        }
        public static List<Empleado> Map(this List<ClsEmpleadoDom> models) {

            List<Empleado> Dtos = new List<Empleado>();

            foreach (ClsEmpleadoDom ModelItem in models )
            {
                Dtos.Add(Map(ModelItem));
            }
            return Dtos;
        }
        public static List<ClsEmpleadoDom> Map(this List<Empleado> model)
        {
            List<ClsEmpleadoDom> Dtos = new List<ClsEmpleadoDom>();

            foreach (Empleado modelItem in model)
            {
                Dtos.Add(Map(modelItem));
                //clienteDtos.Add(new ClienteDto(modelItem.strNumeroIdentificacion));
            }

            return Dtos;
        }
        public static ClsEmpleadoDom Map(this Empleado DTO) {

            return new ClsEmpleadoDom()
            {

                ID = DTO.ID,
                Nombre = DTO.Nombre,
                Apellido = DTO.Apellido,
                Ndoc_emp = DTO.Ndoc_Emp,
                Tdoc_Emp = DTO.Tdoc_Emp,
                Telefono = DTO.Telefono,
                Direccion = DTO.Direccion,
                Email = DTO.Email,
                Ciudad = DTO.Ciudad,
                Estado = DTO.Estado,
                Genero = DTO.Genero,
                Profesion = DTO.Profesion,
                FechaCreacion = DTO.FechaCreacion,
                FechaActualizacion = DTO.FechaActualizacion
            };
        }

    }
}
