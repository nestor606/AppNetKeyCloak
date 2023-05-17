using Dominio.Modelo;
using DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Mappers
{
    public static class ClsEmployeMapper
    {
        public static ClsEmpleadoDTO Map(this ClsEmpleadoDom model)
        {
            return new ClsEmpleadoDTO()
            {
                ID = model.ID,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Ndoc_Emp = model.Ndoc_emp,
                Tdoc_Emp = model.Tdoc_Emp,
                Telefono = model.Telefono,
                Direccion = model.Direccion,
                Email = model.Email,
                Genero = model.Genero,
                Estado = model.Estado,
                FechaActualizacion = model.FechaActualizacion,
                FechaCreacion = model.FechaCreacion
            };
        }
        public static List<ClsEmpleadoDTO> Map(this List<ClsEmpleadoDom> model)
        {

            List<ClsEmpleadoDTO> Dtos = new List<ClsEmpleadoDTO>();

            foreach (ClsEmpleadoDom modelItem in model)
            {

                Dtos.Add(Map(modelItem));
            }
            return Dtos;

        }
        public static ClsEmpleadoDom Map(this ClsEmpleadoDTO DTO) {

            return new ClsEmpleadoDom()
            {

                ID = DTO.ID,
                Nombre = DTO.Nombre,
                Apellido = DTO.Apellido,
                Ndoc_emp = DTO.Ndoc_Emp,
                Tdoc_Emp = DTO.Tdoc_Emp,
                Direccion = DTO.Direccion,
                Telefono = DTO.Telefono,
                Email = DTO.Email,
                Estado = DTO.Estado,
                Genero = DTO.Genero,
                Ciudad = DTO.Ciudad,
                Profesion = DTO.Profesion,
                FechaActualizacion = DTO.FechaActualizacion,
                FechaCreacion = DTO.FechaCreacion

            };
        }
    }
}