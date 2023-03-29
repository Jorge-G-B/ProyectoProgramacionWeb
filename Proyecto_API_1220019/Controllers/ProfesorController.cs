using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Transactions;
using Proyecto_API_1220019.Models;

namespace Proyecto_API_1220019.Controllers
{
        [ApiController]
        [Route("Profesores")]
        public class ProfesoreController : Controller
        {
            [Route("ObtenerListaDeProfesores")]
            [HttpGet]
            public async Task<IEnumerable<ScholomanceModels.Profesores>> GetList()
            {
                EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                IEnumerable<ScholomanceModels.Profesores> Profesores = _EscuelawebContext.Profesores.Select(s =>
                new ScholomanceModels.Profesores
                {
                     Id = s.Id,
                     Nombre = s.Nombre,
                     Apellido = s.Apellido,
                     Email = s.Email,
                     Especialidad = s.Especialidad,
                }
                ).ToList();
                return Profesores;
            }

            [Route("ObtenerProfesor")]
            [HttpGet]
            public ScholomanceModels.Profesores GetProfesor(int id)
            {
                EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                ScholomanceModels.Profesores profesor = _EscuelawebContext.Profesores.Select(s =>
                new ScholomanceModels.Profesores
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Apellido = s.Apellido,
                    Email = s.Email,
                    Especialidad = s.Especialidad
                }
                ).FirstOrDefault(s => s.Id == id);
                return profesor;
            }

            [Route("CrearProfesor")]
            [HttpPost]
            public async void Create(ScholomanceModels.Profesores profesor)
            {
                try
                {
                    EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                    Models.Profesore _profesor = new Models.Profesore
                    {
                        Nombre = profesor.Nombre,
                        Apellido = profesor.Apellido,
                        Email = profesor.Email,
                        Especialidad = profesor.Especialidad
                    };
                    _EscuelawebContext.Profesores.Add(_profesor);
                    await _EscuelawebContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }

            [Route("EliminarProfesor")]
            [HttpDelete]
            public async void Delete(int id)
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                        var profesor = await _EscuelawebContext.Profesores.FindAsync(id);
                        if (profesor != null)
                        {
                            _EscuelawebContext.Profesores.Remove(profesor);
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

            [Route("EditarProfesor")]
            [HttpPut]
            public async void Edit(ScholomanceModels.Profesores profesor)
            {
                try
                {
                    EscuelawebContext _EscuelawebContext = new EscuelawebContext();
                    Models.Profesore _profesor = new Models.Profesore
                    {
                        Id = profesor.Id,
                        Nombre = profesor.Nombre,
                        Apellido = profesor.Apellido,
                        Email = profesor.Email,
                        Especialidad = profesor.Especialidad
                    };
                    _EscuelawebContext.Update(_profesor);
                    await _EscuelawebContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
        }
}
