using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using BossmandadosAPIService.DataObjects;
using System.Threading.Tasks;
using BossmandadosAPIService.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class RepartidorController : ApiController
    {
        // POST api/Login
        [HttpPost]
        public async Task<Manboss_usuario> Login(string correo, string password)
        {   
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_usuarios WHERE Rol = 2 AND Correo = '" + correo +
                        "' ";
                    var result = await context.Manboss_usuarios.SqlQuery(query).FirstAsync();
                    string hash = result.Hash;
                    password = Encrypt(password + hash).ToLower();

                    if (result.Contrasenia.Equals(password))
                    {
                        return result;
                    }

                }
                catch(Exception ex) { }
                return null;
            }
        }

        [HttpPost]
        public async Task<bool> Estado(bool estado, int RepartidorID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "UPDATE dbo.manboss_repartidores SET Estado = " + Convert.ToInt16(estado) + " WHERE Id = " + RepartidorID;
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

        [HttpPost]
        public async Task<bool> CantidadEfectivo(double efectivo, int RepartidorID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "UPDATE dbo.manboss_repartidores SET Efectivo = " + efectivo + " WHERE Id = " + RepartidorID;
                    query = query.Replace(',', '.');
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
        [HttpPost]
        public async Task<bool> Ubicacion(double Latitud, double Longitud, int RepartidorID)
        {
            string lat = Latitud.ToString().Replace(',', '.');
            string lon = Longitud.ToString().Replace(',', '.');
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    DateTime myDateTime = DateTime.Now;
                    string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    var query = "INSERT INTO dbo.manboss_repartidores_ubicaciones (repartidor,latitud,longitud,hora) VALUES (" + RepartidorID + "," + lat + "," + lon + ",'" + sqlFormattedDate + "')";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);
                    query = "UPDATE dbo.manboss_repartidores SET latitud = " + lat + ", longitud = " + lon + "WHERE Id = " + RepartidorID;
                    row = await context.Database.ExecuteSqlCommandAsync(query);
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

        private static string Encrypt(string inputString)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
