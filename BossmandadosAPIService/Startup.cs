using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BossmandadosAPIService.Startup))]

namespace BossmandadosAPIService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}