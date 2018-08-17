using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using System.Threading.Tasks;
using BossmandadosAPIService.DataObjects;
using BossmandadosAPIService.Models;
using System;
using System.Configuration;

namespace BossmandadosAPIService.Controllers {
    [MobileAppController]
    public class ClienteController : ApiController {
        [HttpPost]
        public async Task<Manboss_cliente> Cliente(int ClienteID) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {

                    var query = "SELECT * FROM dbo.manboss_clientes WHERE Id=" + ClienteID;
                    var result = await context.Manboss_clientes.SqlQuery(query).FirstAsync();
                    return result;

                }
                catch (Exception ex) {
                }
                return null;
            }
        }
        [HttpPost]
        public async Task<Manboss_cliente> Login(string correo) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {
                    var query = "SELECT * FROM dbo.manboss_clientes WHERE correo = '" + correo + "'";
                    var result = await context.Manboss_clientes.SqlQuery(query).FirstAsync();
                    return result;
                    
                }
                catch(Exception ex){
                    String error = ex.ToString();
                    return null;
                }
            }
        }
        [HttpPost]
        public async Task<Manboss_cliente> CrearCliente(string correo, string nombre, string telefono, string direccion, string red_social) {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext()) {
                try {
                    var query = "INSERT INTO manboss_clientes (nombre, correo, telefono, red_social, direccion)" +
                        "VALUES ('" + nombre + "','" + correo + "','" + telefono + "','" + red_social + "','" + direccion + "')";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);
                }
                catch {
                }
                return await Login(correo);
            }
        }

        [HttpPost]
        public async Task<Manboss_cliente> Crear_facebook(string correo, string nombre, string red_social)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "INSERT INTO manboss_clientes (nombre, correo, red_social)" +
                        "VALUES ('" + nombre + "','" + correo + "','" + red_social + "')";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);
                }
                catch
                {
                }
                return await Login(correo);
            }
        }

        [HttpPost]
        public async Task<Manboss_cliente> Crear_correo(string correo, string nombre, string red_social,string contrasenia,string hash)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "INSERT INTO manboss_clientes (nombre, correo, red_social, contrasenia,hash)" +
                        "VALUES ('" + nombre + "','" + correo + "','" + red_social + "','" + contrasenia + "','" + hash + "')";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);
                }
                catch
                {
                }
                return await Login(correo);
            }
        }

        [HttpPost]
        public async Task<Manboss_cliente> Registrar_cliente(string id,string telefono,string direccion,string latitud, string longitud)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "UPDATE manboss_clientes SET telefono = '" + telefono + "', direccion = '" + direccion + "', latitud = '"
                        + latitud + "', longitud = '" + longitud + "'  WHERE id ='" + id + "'";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);
                }
                catch
                {
                }
                return await Get_cliente(id,"get_cliente");
            }
        }

        [HttpPost]
        public async Task<Manboss_cliente> Get_cliente(string id,string get)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "SELECT * FROM dbo.manboss_clientes WHERE id = '" + id + "'";
                    var result = await context.Manboss_clientes.SqlQuery(query).FirstAsync();
                    return result;
                }
                catch
                {
                    return null;
                }
            }
        }

    }
}
