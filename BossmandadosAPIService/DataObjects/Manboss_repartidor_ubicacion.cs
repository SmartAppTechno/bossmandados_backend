using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_repartidor_ubicacion
    {
        public int Id { get; set; }
        public int Repartidor { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime Hora { get; set; }
        public int Mandado { get; set; }
    }
}