using Aplicacion.Interfaces;
using Dominio.Modelo;
using Microsoft.AspNetCore.Mvc;
using DTO.DTOs;
using DTO.Mappers;
using static Aplicacion.Utilidadades.MensajesBase;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : Controller
    {
        private readonly IServicioBase<ClsEmpleadoDom, int> _db;

        public EmpleadoController(IServicioBase<ClsEmpleadoDom, int> db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<List<ClsEmpleadoDTO>> ObtenerTodo()
        {
            return Ok(_db.ObtenerTodo());
        }

        [HttpGet("{id}")]
        public ActionResult<List<ClsEmpleadoDTO>> ListarID(int id) {

            return Ok(_db.ListarID(id));
        }
        [HttpPost]
        public ActionResult Insertar([FromBody] ClsEmpleadoDTO entidad) {

            _db.Insertar(entidad.Map());
            return Ok(Satisfactorio.Insertado.GetEnumDescription());

        }

    }
}
