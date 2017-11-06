using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class UsuarioController : ApiController
    {
        // GET api/Usuario
        public string Get()
        {
            return "Hello from custom controller!";
        }
    }
}
