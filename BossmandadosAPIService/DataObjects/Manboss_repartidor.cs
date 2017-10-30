using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_repartidor
    {
        public int Id { get; set; }
        public int Repartidor { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double Rating { get; set; }
        public double Efectivo { get; set; }
        public int Estado { get; set; }

    }
}