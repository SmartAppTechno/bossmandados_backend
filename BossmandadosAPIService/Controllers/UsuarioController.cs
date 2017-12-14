using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using BossmandadosAPIService.DataObjects;
using BossmandadosAPIService.Models;
using System;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class UsuarioController : ApiController
    {
        [HttpPost]
        public async Task<Manboss_usuario> GetUsuario(int UsuarioID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_usuarios WHERE Id = " + UsuarioID;
                    var result = await context.Manboss_usuarios.SqlQuery(query).FirstAsync();
                    return result;

                }
                catch (Exception ex) { }
                return null;
            }
        }
    }
}
