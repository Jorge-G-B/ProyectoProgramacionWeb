using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Transactions;
using Proyecto_API_1220019.Models;

namespace Proyecto_API_1220019.Controllers
{
    [ApiController]
    [Route("Alumnos")]
    public class AlumnoController : Controller
    {
        [Route("ObtenerListaAlumnos")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScholomanceModels.Alumnos>>> GetList()
        {
            try
            {
                EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                IEnumerable<ScholomanceModels.Alumnos> alumnos = _EscuelawebContext.Alumnos.Select(s =>
                new ScholomanceModels.Alumnos
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    Edad = s.Edad,
                    Email = s.Email
                }
                ).ToList();
                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Route("ObtenerAlumno")]
        [HttpGet]
        public ScholomanceModels.Alumnos GetAlumno(int id)
        {
            EscuelawebContext _EscuelawebContext = new EscuelawebContext();
            ScholomanceModels.Alumnos alumno = _EscuelawebContext.Alumnos.Select(s =>
            new ScholomanceModels.Alumnos
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Apellido = s.Apellido,
                Edad = s.Edad,
                Email = s.Email
            }
            ).FirstOrDefault(s => s.Id == id);
            return alumno;
        }

        [Route("CrearAlumno")]
        [HttpPost]
        public async void Create(ScholomanceModels.Alumnos alumno)
        {
            try
            {
                EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                Models.Alumno _alumno = new Models.Alumno
                {
                    Nombre = alumno.Nombre,
                    Apellido = alumno.Apellido,
                    Edad = alumno.Edad,
                    Email = alumno.Email
                };
                _EscuelawebContext.Alumnos.Add(_alumno);
                await _EscuelawebContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        [Route("EliminarAlumno")]
        [HttpDelete]
        public async void Delete(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                    var alumno = await _EscuelawebContext.Alumnos.FindAsync(id);
                    if (alumno != null)
                    {
                        _EscuelawebContext.Alumnos.Remove(alumno);
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

        [Route("EditarAlumno")]
        [HttpPut]
        public async void Edit(ScholomanceModels.Alumnos alumno)
        {
            try
            {
                EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                Models.Alumno _alumno = new Models.Alumno
                {
                    Id = alumno.Id,
                    Nombre = alumno.Nombre,
                    Apellido = alumno.Apellido,
                    Edad = alumno.Edad,
                    Email = alumno.Email
                };
                _EscuelawebContext.Update(_alumno);
                await _EscuelawebContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
