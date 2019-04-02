using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_mandados_ruta
    {
        public int Id { get; set; }
        public int Mandado { get; set; }
        public int Servicio { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Calle { get; set; }
        public int? Numero { get; set; }
        public string Comentarios { get; set; }
        public int? Tamanio { get; set; }
        public double? Peso { get; set; }
        public int Terminado { get; set; }
        public string Direccion { get; internal set; }
    }
}