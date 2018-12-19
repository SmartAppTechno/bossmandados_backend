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
    public class DireccionesController : ApiController
    {
        [HttpPost]
        public async Task<List<Manboss_direcciones>> Direcciones(int clienteID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_clientes_direcciones WHERE Cliente =" + clienteID;
                    var result = await context.Manboss_direcciones.SqlQuery(query).ToListAsync();
                    return result;

                }
                catch (Exception ex)
                {
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<Manboss_direcciones> CrearDireccion(int cliente,string direccion,float latitud,float longitud)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {  
                try
                {
                    string query = "INSERT INTO dbo.manboss_clientes_direcciones (cliente,direccion,latitud,longitud) VALUES (" + cliente + ",'" + direccion + "'," + latitud + "," + longitud + ")";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);
                }
                catch (Exception ex)
                {
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<Manboss_direcciones> GetDireccion(int direccionID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_clientes_direcciones WHERE Id =" + direccionID;
                    var result = await context.Manboss_direcciones.SqlQuery(query).FirstAsync();
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