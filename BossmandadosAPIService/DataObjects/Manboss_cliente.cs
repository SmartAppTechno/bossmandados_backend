using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Red_social { get; set; }
        public string Direccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public string Contrasenia { get; set; }
        public string Hash { get; set; }
    }
}