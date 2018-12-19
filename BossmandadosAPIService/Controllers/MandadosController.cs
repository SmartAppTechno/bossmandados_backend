﻿using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using BossmandadosAPIService.DataObjects;
using System.Threading.Tasks;
using BossmandadosAPIService.Models;
using System;
using System.Collections.Generic;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class MandadosController : ApiController
    {
        [HttpPost]
        public async Task<Manboss_mandados> Mandados(int MandadoID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_mandados WHERE Id = " + MandadoID;
                    var result = await context.Manboss_mandados.SqlQuery(query).FirstAsync();
                    return result;

                }
                catch (Exception ex) {
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<Manboss_mandados> Mandados_cliente(int clienteID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {

                    var query = "SELECT * FROM dbo.manboss_mandados WHERE cliente = " + clienteID;
                    var result = await context.Manboss_mandados.SqlQuery(query).FirstAsync();
                    return result;

                }
                catch (Exception ex)
                {
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<List<Manboss_mandados_ruta>> Ruta(int MandadoID,string ruta)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "SELECT * FROM dbo.manboss_mandados_rutas WHERE Mandado = " + MandadoID;
                    var result = await context.Manboss_mandados_rutas.SqlQuery(query).ToListAsync();
                    return result;
                }
                catch
                {
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<List<Manboss_repartidor_ubicacion>> Ubicaciones(int MandadoID,string ubicacion)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "SELECT * FROM dbo.manboss_repartidores_ubicaciones WHERE Mandado = " + MandadoID;
                    var result = await context.Manboss_repartidores_ubicacion.SqlQuery(query).ToListAsync();
                    return result;
                }
                catch
                {
                }
                return null;
            }
        }



    }
}
