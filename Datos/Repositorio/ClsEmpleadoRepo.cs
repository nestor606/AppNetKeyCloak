using Datos.Contexto;
using Datos.Mappers;
using Dominio.Interface.Repositorio;
using Dominio.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorio
{
    public class ClsEmpleadoRepo : IRepositorioBase<ClsEmpleadoDom, int>
    {
        private ApplicationsContext _Context;

        public ClsEmpleadoRepo(ApplicationsContext context)
        {
            _Context = context;
        }

        public void Actualizar(ClsEmpleadoDom entidad)
        {
            var query = _Context.Empleado.Where(x => x.ID == entidad.ID).FirstOrDefault();
            
            if (query != null) {

                query.FechaActualizacion = DateTime.Now;
                _Context.Entry(query).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _Context.SaveChanges();
            }
        }

        public void Eliminar(int id)
        {
            var query = _Context.Empleado.Where(x => x.ID == id).FirstOrDefault();

            if (query != null)
            { 
                _Context.Empleado.Remove(query);
            }

        }

        public ClsEmpleadoDom Insertar(ClsEmpleadoDom tntidad)
        {
            _Context.Empleado.Add(tntidad.Map());
            _Context.SaveChanges();
            return tntidad;
        }

        public ClsEmpleadoDom ListarID(int id)
        {
            var query = _Context.Empleado.Where(x => x.ID == id).FirstOrDefault();

            return query.Map();

        }

        public List<ClsEmpleadoDom> ObtenerTodo()
        {
            
            return _Context.Empleado.ToList().Map();
        }

        public void salvarTodo()
        {
            throw new NotImplementedException();
        }
    
    }
}
