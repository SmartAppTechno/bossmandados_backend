using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using BossmandadosAPIService.DataObjects;
using BossmandadosAPIService.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        public async Task<Manboss_mandados> CrearMandado(string ruta, string nuevo_mandado) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {
                    List<Manboss_mandados_ruta> arr_rutas = JsonConvert.DeserializeObject<List<Manboss_mandados_ruta>>(ruta);
                    Manboss_mandados mandado = JsonConvert.DeserializeObject<Manboss_mandados>(nuevo_mandado);
                    string query = "INSERT INTO manboss_mandados " +
                        "(estado,cliente,total,fecha,tipo_pago,cuenta_pendiente) " +
                        "VALUES (" + mandado.Estado + "," + mandado.Cliente + "," + mandado.Total + "," + mandado.Fecha.ToString() +
                        "," + mandado.Tipo_pago + "," + mandado.Cuenta_pendiente + ")";
                    await context.Database.ExecuteSqlCommandAsync(query);
                    context.SaveChanges();
                    foreach(Manboss_mandados_ruta r in arr_rutas) {
                        query = "INSERT INTO Manboss_ruta " +
                        "(mandado,servicio,latitud,longitud,calle,numero,comentarios,direccion,terminado) " +
                        "VALUES (" + mandado.Id + "," + r.Servicio + "," + r.Latitud + "," + r.Longitud +
                        "," + r.Calle + "," + r.Numero + "," + r.Comentarios + "," + r.Direccion + "," + r.Terminado + ")";
                        await context.Database.ExecuteSqlCommandAsync(query);
                    }
                }
                catch {
                }
                return null;
            }
        }
    }
}
