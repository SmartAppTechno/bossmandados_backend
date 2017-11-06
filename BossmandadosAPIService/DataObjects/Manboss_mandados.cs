using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_mandados
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Cliente { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }
        public int Tipo_pago { get; set; }
        public int Cuenta_pendiente { get; set; }
        public int Repartidor { get; set; }
        public DateTime Tiempo_trayecto { get; set; }
        public DateTime Tiempo_total { get; set; }
    }
}