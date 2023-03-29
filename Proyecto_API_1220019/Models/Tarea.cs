using System;
using System.Collections.Generic;

namespace Proyecto_API_1220019.Models;

public partial class Tarea
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaEntrega { get; set; }

    public int IdClase { get; set; }

    public virtual Clase IdClaseNavigation { get; set; } = null!;
}
