using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BossmandadosAPIService.DataObjects
{
    public class Manboss_chat_mensaje
    {
        public int Id { get; set; }
        public int Chat { get; set; }
        public string Mensaje { get; set; }
    }
}