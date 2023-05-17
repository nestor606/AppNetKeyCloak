using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface
{
    public interface IActualizar<TEntidad>
    {
        void Actualizar(TEntidad entidad);

    }
}
