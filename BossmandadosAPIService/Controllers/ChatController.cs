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
    public class ChatController : ApiController
    {
        [HttpPost]
        public async Task<int> Chat(int MandadoID, int RepartidorID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                int ChatID = 0;
                try
                {

                    var query = "INSERT INTO manboss_chat (mandado,repartidor) VALUES (" + MandadoID + "," + RepartidorID + ")";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);
                    query = "SELECT * FROM manboss_chat WHERE mandado = " + MandadoID + " AND repartidor = " + RepartidorID;
                    var result = await context.Manboss_chat.SqlQuery(query).FirstAsync();
                    ChatID = result.Id;

                }
                catch (Exception ex)
                {
                }
                return ChatID;
            }
        }
        [HttpPost]
        public async Task<bool> Mensaje(int ChatID, string Mensaje)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "INSERT INTO manboss_chat_mensajes (chat,mensaje) VALUES (" + ChatID + ",'" + Mensaje + "')";
                    int row = await context.Database.ExecuteSqlCommandAsync(query);
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
        }

        [HttpPost]
        public async Task<List<Manboss_chat_mensaje>> Conversacion(int MandadoID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "SELECT * FROM dbo.manboss_chat WHERE Mandado = " + MandadoID;
                    var aux = await context.Manboss_chat.SqlQuery(query).FirstAsync();
                    query = "SELECT * FROM dbo.manboss_chat_mensajes WHERE Chat = " + aux.Id;
                    var result = await context.Manboss_chat_mensajes.SqlQuery(query).ToListAsync();
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
