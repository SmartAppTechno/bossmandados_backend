using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using BossmandadosAPIService.DataObjects;
using System.Threading.Tasks;
using BossmandadosAPIService.Models;
using System;
using System.Collections.Generic;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class MandadosActivosController : ApiController
    {
        [HttpPost]
        public async Task<List<Manboss_mandados>> Mandados(int RepartidorID, int estado)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_mandados WHERE Estado = " + estado + " AND Repartidor = " + RepartidorID;
                    var result = await context.Manboss_mandados.SqlQuery(query).ToListAsync();
                    return result;

                }
                catch (Exception ex) {
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<List<Manboss_mandados_ruta>> Ruta(int MandadoID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_mandados_rutas WHERE Mandado = " + MandadoID;
                    var result = await context.Manboss_mandados_rutas.SqlQuery(query).ToListAsync();
                    return result;

                }
                catch (Exception ex)
                { 
                }
                return null;
            }
        }
        
    }
}
