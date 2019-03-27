using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects {
    public class Manboss_servicio {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Ubicaciones { get; set; }
        public double Tarifa_base_ex { get; set; }
        public double Costo_minuto_ex { get; set; }
        public double Costo_km_ex { get; set; }
        public double Tarifa_base_co { get; set; }
        public double Costo_minuto_co { get; set; }
        public double Costo_km_co { get; set; }
        public string Foto { get; set; }
    }
}