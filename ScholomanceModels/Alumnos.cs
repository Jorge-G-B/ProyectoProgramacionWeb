using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScholomanceModels
{
    public class Alumnos
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public int Edad { get; set; }

        public string Email { get; set; } = null!;
    }
}
