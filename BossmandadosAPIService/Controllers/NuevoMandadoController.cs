using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using BossmandadosAPIService.DataObjects;
using BossmandadosAPIService.Models;
using System;
using System.Collections.Generic;

namespace BossmandadosAPIService.Controllers {
    [MobileAppController]
    public class NuevoMandadoController : ApiController {
        [HttpPost]
        public async Task<List<Manboss_servicio>> TipoMandado() {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                List<Manboss_servicio> servicios = null;
                try {
                    string query = "SELECT * FROM manboss_servicios";
                    servicios = await context.Manboss_servicios.SqlQuery(query).ToListAsync();
                }
                catch {
                }
                return servicios;
            }
        }
        [HttpPost]
        public async Task<double> CalcularPrecio(List<Manboss_mandados_ruta> ruta) {
            
            double tiempo = 30;
            double distancia = 20; // cobro.Distancia;
            double tarifa_dinamica = 1;
            double costo = (tiempo + distancia) * tarifa_dinamica;

            return costo;
        }

        [HttpPost]
        public async Task<Manboss_mandados> CrearMandado(List<Manboss_mandados_ruta> ruta, Manboss_cliente cliente) {
            double costo = await CalcularPrecio(ruta);
            return null;
        }
    }
}
