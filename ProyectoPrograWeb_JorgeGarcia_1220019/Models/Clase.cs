using System;
using System.Collections.Generic;

namespace ProyectoPrograWeb_JorgeGarcia_1220019.Models;

public partial class Clase
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdProfesor { get; set; }

    public virtual Profesore IdProfesorNavigation { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; } = new List<Tarea>();
}
