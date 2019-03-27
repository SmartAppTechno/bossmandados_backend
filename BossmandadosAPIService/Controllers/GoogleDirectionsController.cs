using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using BossmandadosAPIService.DataObjects;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Common;
using BossmandadosAPIService.App_Start;
using Microsoft.Azure.Mobile.Server.Config;
using BossmandadosAPIService.Models;
using System.Web.Http;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class GoogleDirectionsController : ApiController
    {
        private DirectionsRequest SetUpRequest(ref List<Manboss_mandados_ruta> ubicaciones)
        {
            if (ubicaciones.Count < 2)
            {
                return null;
            }
            Manboss_mandados_ruta origin = ubicaciones.First<Manboss_mandados_ruta>(), destination = ubicaciones.Last<Manboss_mandados_ruta>();
            string[] waypoints;

            ubicaciones.RemoveAt(0);
            ubicaciones.RemoveAt(ubicaciones.Count - 1);

            if (ubicaciones.Count > 0)
            {
                waypoints = new string[ubicaciones.Count];
                int i = 0;
                foreach (var m in ubicaciones)
                {
                    waypoints[i] = m.Latitud.ToString() + "," + m.Longitud.ToString();
                    i++;
                }
            }
            else
            {
                waypoints = null;
            }

            DirectionsRequest request = new DirectionsRequest()
            {
                Origin = origin.Latitud.ToString() + "," + origin.Longitud.ToString(),
                Destination = destination.Latitud.ToString() + "," + destination.Longitud.ToString(),
                Waypoints = waypoints, 
                ApiKey = GoogleDirectionsConfig.SIGNING_KEY
            };

            return request;
        }

        // POST: GoogleDirections/GetPolyline
        [HttpPost]
        public async Task<string> GetPolyline(int MandadoID)
        {
            using (BossmandadosAPIContext context = new BossmandadosAPIContext())
            {
                try
                {
                    var query = "SELECT * FROM dbo.manboss_mandados_rutas WHERE Mandado = " + MandadoID + " AND Terminado = 0";
                    var ubicaciones = await context.Manboss_mandados_rutas.SqlQuery(query).ToListAsync();
                    DirectionsRequest directionsRequest = SetUpRequest(ref ubicaciones);

                    if (directionsRequest == null)
                    {
                        return null;
                    }

                    DirectionsResponse directions = await GoogleMaps.Directions.QueryAsync(directionsRequest);

                    return SetUpResponse(directions);
                }
                catch
                {
                    return null;
                }
            }
        }

        private string SetUpResponse(DirectionsResponse response)
        {
            if (response.Status != DirectionsStatusCodes.OK)
            {
                throw new Exception("Failed to receive Google Directions response: " + response.StatusStr);
            }

            string polyline =  response.Routes.First<Route>().OverviewPath.GetRawPointsData();

            return polyline;
        }
            
    }
}