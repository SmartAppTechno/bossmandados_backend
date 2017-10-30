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
        public async Task<Manboss_usuario> PostLogin(string correo, string password)
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
