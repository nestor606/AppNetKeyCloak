using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface
{
    public interface IListar<TEntidad, TEntidadID>
    {
        TEntidad ListarID(TEntidadID id);
        List<TEntidad> ObtenerTodo(); 
    }
}
