using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_direcciones
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public string Alias { get; set; }
        public string Direccion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}