using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using BossmandadosAPIService.DataObjects;
using BossmandadosAPIService.Models;
using System;
using System.Collections.Generic;

namespace BossmandadosAPIService.Controllers {
    [MobileAppController]
    public class ComisionController : ApiController {

        [HttpPost]
        public async Task<List<Manboss_comision>> Comisiones(int RepartidorID) {
            List<Manboss_comision> comisiones = null;
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {
                    var query = "SELECT * FROM dbo.manboss_comisiones WHERE repartidor = " + RepartidorID;
                    var result = await context.Manboss_comisiones.SqlQuery(query).ToListAsync();
                    comisiones = result;

                }
                catch (Exception ex) {
                }
                return comisiones;
            }
        }
        [HttpPost]
        public async Task<bool> Agregar(int RepartidorID, int MandadoID) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                string query = string.Empty;
                try {
                    string val = await GetComision(MandadoID, context);

                    query = "INSERT INTO manboss_comisiones (mandado,repartidor,comision) VALUES (" + MandadoID + "," + RepartidorID + "," + val + ")";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);

                }
                catch (Exception ex) {
                    return false;
                }
                return true;
            }
        }
        [HttpPost]
        public async Task<List<Manboss_comision>> Filtrar(int RepartidorID, int Day, int Month, int Year) {
            string query = "SELECT manboss_comisiones.Id,manboss_comisiones.mandado,manboss_comisiones.repartidor,manboss_comisiones.comision" +
                " FROM manboss_comisiones INNER JOIN manboss_mandados ON manboss_comisiones.mandado = manboss_mandados.id" +
                " WHERE manboss_comisiones.repartidor = " + RepartidorID + " ";

            if (Year != 0) {
                query += "AND Year(manboss_mandados.fecha) = " + Year.ToString() + " ";
                if (Month != 0) {
                    query += "AND Month(manboss_mandados.fecha) = " + Month.ToString() + " ";
                    if (Day != 0) {
                        query += "AND Day(manboss_mandados.fecha) = " + Day.ToString() + " ";
                    }
                }
            }

            List<Manboss_comision> comisiones = null;
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {
                    var result = await context.Manboss_comisiones.SqlQuery(query).ToListAsync();
                    comisiones = result;

                }
                catch (Exception ex) {
                }
                return comisiones;
            }
        }
        private async Task<string> GetComision(int MandadoID, BossmandadosAPIContext context) {
            string query = "SELECT * FROM dbo.manboss_mandados WHERE id = " + MandadoID;
            Manboss_mandados result = await context.Manboss_mandados.SqlQuery(query).FirstAsync();

            double porcentaje = 0.65;

            double val = result.Total * porcentaje;
            val = Math.Round(val / 2) * 2;
            string ans = val.ToString();
            ans = ans.Replace(',', '.');
            return ans;
        }
    }
}
