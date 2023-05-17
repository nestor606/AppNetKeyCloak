using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interface;

namespace Aplicacion.Interfaces
{
    public interface IServicioBase<TEntidad,TEntidadID>:
        IAgregar<TEntidad>,IActualizar<TEntidad>,IEliminar<TEntidadID>,IListar<TEntidad,TEntidadID>
    {
    }
}
