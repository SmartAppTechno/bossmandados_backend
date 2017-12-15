using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_comision
    {
        public int Id { get; set; }
        public int Mandado { get; set; }
        public int Repartidor { get; set; }
        public double Comision { get; set; }
    }
}