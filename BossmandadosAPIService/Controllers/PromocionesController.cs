using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using BossmandadosAPIService.DataObjects;
using BossmandadosAPIService.Models;
using System;
using System.Collections.Generic;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class PromocionesController : ApiController
    {
        [HttpPost]
        public async Task<List<Manboss_promociones>> Promociones()
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_promociones";
                    var result = await context.Manboss_promociones.SqlQuery(query).ToListAsync();
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