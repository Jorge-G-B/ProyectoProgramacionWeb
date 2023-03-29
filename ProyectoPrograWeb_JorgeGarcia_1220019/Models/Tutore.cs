using System;
using System.Collections.Generic;

namespace ProyectoPrograWeb_JorgeGarcia_1220019.Models;

public partial class Tutore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;
}
