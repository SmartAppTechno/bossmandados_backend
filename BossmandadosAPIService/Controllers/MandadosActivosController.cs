using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using BossmandadosAPIService.DataObjects;
using System.Threading.Tasks;
using BossmandadosAPIService.Models;
using System;
using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class MandadosActivosController : ApiController
    {
        [HttpPost]
        public async Task<List<Manboss_mandados>> Mandados(int RepartidorID, int estado) {
            List<Manboss_mandados> mandados = null;
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {

                    var query = "SELECT * FROM dbo.manboss_mandados WHERE Estado = " + estado + " AND Repartidor = " + RepartidorID;
                    var result = await context.Manboss_mandados.SqlQuery(query).ToListAsync();
                    mandados = result;
                }
                catch  {
                }
                return mandados;
            }
        }

        [HttpPost]
        public async Task<List<Manboss_mandados_ruta>> Ruta(int MandadoID) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {
                    var query = "SELECT * FROM dbo.manboss_mandados_rutas WHERE Mandado = " + MandadoID + " AND Terminado = 0";
                    var result = await context.Manboss_mandados_rutas.SqlQuery(query).ToListAsync();
                    return result;
                }
                catch {
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> SetMandado(int MandadoID, int Estado)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "UPDATE dbo.manboss_mandados SET Estado = " + Estado + " WHERE Id = " + MandadoID;
                    int row = await context.Database.ExecuteSqlCommandAsync(query);

                    if (Estado == 3)
                    {
                        row = await AgregarCobro(MandadoID, context);
                    }
                    else if (Estado == 4)
                    {
                        row = await FinalizarCobro(MandadoID, context);
                    }

                    if (row != 0)
                    {
                        return true;
                    }

                }
                catch (Exception ex)
                {
                }
                return false;
            }
        }

        [HttpPost]
        public async Task<bool> CompletarPunto(int RutaID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "UPDATE dbo.manboss_mandados_rutas SET Terminado = " + 1 + " WHERE Id = " + RutaID;
                    int row = await context.Database.ExecuteSqlCommandAsync(query);

                    if (row != 0)
                    {
                        return true;
                    }

                }
                catch (Exception ex)
                {
                }
                return false;
            }
        }

        private async static Task<int> AgregarCobro(int MandadoID, BossmandadosAPIContext context)
        {
            int row = 0;
            try
            {
                //Conseguir Latitud y Longitud del repartidor

                string query = "SELECT * FROM dbo.manboss_mandados WHERE id = " + MandadoID;
                Manboss_mandados mandado = await context.Manboss_mandados.SqlQuery(query).FirstAsync();
                query = "SELECT * FROM dbo.manboss_repartidores WHERE id = " + mandado.Repartidor;
                Manboss_repartidor result = await context.Manboss_repartidores.SqlQuery(query).FirstAsync();



                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string lat = result.Latitud.ToString().Replace(',', '.');
                string lon = result.Longitud.ToString().Replace(',', '.');
                query = "INSERT INTO dbo.manboss_mandados_cobros (mandado,latitud,longitud,tiempoInicio,distancia,tiempo) VALUES (" + MandadoID + "," + lat + "," + lon + ",'" + sqlFormattedDate + "',37,0)";
                row = await context.Database.ExecuteSqlCommandAsync(query);
            }
            catch (Exception e)
            {
                string aux = e.Message;
            }
            return row;
        }
        private async static Task<int> FinalizarCobro(int MandadoID, BossmandadosAPIContext context)
        {
            int row = 0;
            try
            {



                DateTime fin = DateTime.Now;
                string sqlFormattedDate = fin.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string query = "UPDATE dbo.manboss_mandados_cobros SET tiempoFin = '" + sqlFormattedDate  +"'  WHERE mandado = " + MandadoID;
                row = await context.Database.ExecuteSqlCommandAsync(query);

                query = "SELECT * FROM dbo.manboss_mandados_cobros WHERE Mandado = " + MandadoID;
                Manboss_mandados_cobro cobro = await context.Manboss_mandados_cobros.SqlQuery(query).FirstAsync();

                DateTime inicio = cobro.TiempoInicio;
                double tiempo = (fin - inicio).TotalSeconds / 60;
                double distancia = cobro.Distancia;
                double tarifa_dinamica = 1;
                double costo = (tiempo + distancia) * tarifa_dinamica;

                costo = Math.Round(costo * 2) / 2;

                string total = costo.ToString().Replace(',', '.');

                query = "UPDATE dbo.manboss_mandados SET total = " + costo + " WHERE id = " + MandadoID;
                row = await context.Database.ExecuteSqlCommandAsync(query);
            }
            catch (Exception e)
            {
            }
            return row;
        }
    }

}
