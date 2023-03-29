using System;
using System.Collections.Generic;

namespace ProyectoPrograWeb_JorgeGarcia_1220019.Models;

public partial class Profesore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Especialidad { get; set; } = null!;

    public virtual ICollection<Clase> Clases { get; } = new List<Clase>();
}
