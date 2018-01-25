using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_mandados_cobro
    {
        public int Id { get; set; }
        public int Mandado { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public DateTime TiempoInicio { get; set; }
        public DateTime? TiempoFin { get; set; }
        public double Distancia { get; set; }
        public double Tiempo { get; set; }
    }
}