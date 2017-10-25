using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Hash { get; set; }
        public int Rol { get; set; }
    }
}