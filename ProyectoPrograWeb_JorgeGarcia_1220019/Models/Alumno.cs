using System;
using System.Collections.Generic;

namespace ProyectoPrograWeb_JorgeGarcia_1220019.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Edad { get; set; }

    public string Email { get; set; } = null!;
}
