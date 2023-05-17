using Aplicacion.Interfaces;
using Aplicacion.Utilidadades;
using Dominio.Interface.Repositorio;
using Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Aplicacion.Utilidadades.MensajesBase;

namespace Aplicacion.Servicios
{
    public class ClsEmpleadoServ : IServicioBase<ClsEmpleadoDom, int>
    {
        private readonly IRepositorioBase<ClsEmpleadoDom, int> _repositorioBase;
        private Excepcion excepcion = new Excepcion();

        public ClsEmpleadoServ(IRepositorioBase<ClsEmpleadoDom, int> repos)
        {
            _repositorioBase = repos;
        }

        public void Actualizar(ClsEmpleadoDom entidad)
        {
            try
            {
                _repositorioBase.Actualizar(entidad);
                _repositorioBase.salvarTodo();

            }
            catch (Exception ex)
            {

                throw excepcion.Error(ex, Error.Actualizar.GetEnumDescription());
            }
        }

        public void Eliminar(int id)
        {
            _repositorioBase.Eliminar(id);
            _repositorioBase.salvarTodo();
        }

        public ClsEmpleadoDom Insertar(ClsEmpleadoDom tntidad)
        {
            try
            {
                var result = _repositorioBase.Insertar(tntidad);
                _repositorioBase.salvarTodo();
                return result;

            }
            catch (Exception ex)
            {

                throw excepcion.Error(ex, Error.Insertar.GetEnumDescription());
            }
        }

        public ClsEmpleadoDom ListarID(int id)
        {
            return _repositorioBase.ListarID(id);
        }

        public List<ClsEmpleadoDom> ObtenerTodo()
        {
            return _repositorioBase.ObtenerTodo();
        }
    }
}
