using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using BossmandadosAPIService.DataObjects;
using BossmandadosAPIService.Models;
using System;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class ClienteController : ApiController
    {
        [HttpPost]
        public async Task<Manboss_cliente> Cliente(int ClienteID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_clientes WHERE Id = " + ClienteID;
                    var result = await context.Manboss_clientes.SqlQuery(query).FirstAsync();
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
