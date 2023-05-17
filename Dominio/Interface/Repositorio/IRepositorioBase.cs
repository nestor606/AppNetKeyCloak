using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Repositorio
{
    public interface IRepositorioBase<TEntidad,TEntidadID>:
        IActualizar<TEntidad>,IAgregar<TEntidad>,IEliminar<TEntidadID>,IListar<TEntidad,TEntidadID>,ISalvarTodo
    {
    }
}
