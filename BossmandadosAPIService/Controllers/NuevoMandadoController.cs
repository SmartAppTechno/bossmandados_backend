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
        public async Task<List<Manboss_servicio>> TipoMandado(string tiposMandado) {
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
        public List<string> TiposDePago(string tiposPago) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {
                    return new List<string> {
                        "Efectivo", "Tarjeta"
                    };
                }
                catch {
                    return null;
                }
            }
        }

        [HttpPost]
        public async Task<Manboss_mandados> CrearMandado(List<Manboss_mandados_ruta> ruta, Manboss_mandados mandado) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {
                    string query = "INSERT INTO manboss_mandados " +
                        "(estado,cliente,total,fecha,tipo_pago,cuenta_pendiente) " +
                        "VALUES (" + mandado.Estado + "," + mandado.Cliente + "," + mandado.Total + "," + mandado.Fecha.ToString() +
                        "," + mandado.Tipo_pago + "," + mandado.Cuenta_pendiente + ")";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);

                    foreach(Manboss_mandados_ruta r in ruta) {
                        query = "INSERT INTO Manboss_ruta " +
                        "(mandado,servicio,latitud,longitud,calle,numero,comentarios,terminado) " +
                        "VALUES (" + mandado.Id + "," + r.Servicio + "," + r.Latitud + "," + r.Longitud +
                        "," + r.Calle + "," + r.Numero + "," + r.Comentarios + "," + r.Terminado + ")";
                        row = await context.Database.ExecuteSqlCommandAsync(query);
                    }
                }
                catch {
                }
                return null;
            }
        }
    }
}
