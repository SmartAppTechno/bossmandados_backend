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
        public string Direccion { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
    }
}