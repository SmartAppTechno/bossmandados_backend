using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using BossmandadosAPIService.DataObjects;
using System.Threading.Tasks;
using BossmandadosAPIService.Models;
using System;

namespace BossmandadosAPIService.Controllers {
    [MobileAppController]
    public class PerfilController : ApiController {
        [HttpPost]
        public async Task<Manboss_repartidor> Repartidor(int RepartidorID, bool MetodoRepartidor) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {

                    var query = "SELECT * FROM dbo.manboss_repartidores WHERE Repartidor = " + RepartidorID;
                    var result = await context.Manboss_repartidores.SqlQuery(query).FirstAsync();
                    return result;

                }
                catch (Exception ex) { }
                return null;
            }
        }

        [HttpPost]
        public async Task<double> ActualizarRaiting(int RepartidorID, bool MetodoRaiting) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {

                    var query = "SELECT * FROM dbo.manboss_repartidores_calificaciones WHERE Repartidor = " + RepartidorID;
                    var calificaciones = await context.Manboss_repartidores_calificaciones.SqlQuery(query).ToListAsync();

                    int n = 0;
                    double suma = 0;

                    foreach (Manboss_repartidores_calificacion calificacion in calificaciones) {
                        n++;
                        suma += calificacion.Calificacion;
                    }

                    double promedio = 0;
                    if (n != 0) {
                        promedio = suma / n;
                    }

                    query = "UPDATE dbo.manboss_repartidores SET Rating = " + promedio + " WHERE Id = " + RepartidorID;
                    query = query.Replace(',', '.');
                    int noOfRowUpdated = context.Database.ExecuteSqlCommand(query);
                    return promedio;
                }
                catch (Exception ex) { }
                return 0;
            }
        }

    }
}
