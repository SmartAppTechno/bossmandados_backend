using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_repartidores_calificacion
    {
        public int Id { get; set; }
        public int Mandado { get; set; }
        public int Cliente { get; set; }
        public int Repartidor { get; set; }
        public double Calificacion { get; set; }
        public string Comentarios { get; set; }
    }
}