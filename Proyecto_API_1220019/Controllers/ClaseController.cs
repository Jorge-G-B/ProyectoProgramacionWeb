using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Transactions;
using Proyecto_API_1220019.Models;

namespace Proyecto_API_1220019.Controllers
{
    [ApiController]
    [Route("Clase")]
    public class ClaseController : Controller
    {
        [Route("ObtenerListaClases")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScholomanceModels.Clase>>> GetList()
        {
            try
            {
                EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                IEnumerable<ScholomanceModels.Clase> clases = _EscuelawebContext.Clases.Select(s =>
                new ScholomanceModels.Clase
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    IdProfesor = s.IdProfesor
                }
                ).ToList();
                return Ok(clases);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Route("ObtenerClase")]
        [HttpGet]
        public ScholomanceModels.Clase GetClase(int id)
        {
            EscuelawebContext _EscuelawebContext = new EscuelawebContext();
            ScholomanceModels.Clase alumno = _EscuelawebContext.Clases.Select(s =>
            new ScholomanceModels.Clase
            {
                Id = s.Id,
                Nombre = s.Nombre,
                IdProfesor = s.IdProfesor
            }
            ).FirstOrDefault(s => s.Id == id);
            return alumno;
        }

        [Route("CrearClase")]
        [HttpPost]
        public async void Create(ScholomanceModels.Clase clase)
        {
            try
            {
                EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                Models.Clase _clase = new Models.Clase
                {
                    Nombre = clase.Nombre,
                    IdProfesor = clase.IdProfesor
                };
                _EscuelawebContext.Clases.Add(_clase);
                await _EscuelawebContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        [Route("EliminarClase")]
        [HttpDelete]
        public async void Delete(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                    var clase = await _EscuelawebContext.Clases.FindAsync(id);
                    if (clase != null)
                    {
                        _EscuelawebContext.Clases.Remove(clase);
                    }
                    await _EscuelawebContext.SaveChangesAsync();
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    // Revertir la transacción
                    transaction.Dispose();
                    throw;
                }
            }
        }

        [Route("EditarClase")]
        [HttpPut]
        public async void Edit(ScholomanceModels.Clase clase)
        {
            try
            {
                EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                Models.Clase _clase = new Models.Clase
                {
                    Id = clase.Id,
                    Nombre = clase.Nombre,
                    IdProfesor = clase.IdProfesor
                };
                _EscuelawebContext.Update(_clase);
                await _EscuelawebContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

