using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using BossmandadosAPIService.DataObjects;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using GoogleMapsApi.Entities.Common;
using BossmandadosAPIService.App_Start;
using Microsoft.Azure.Mobile.Server.Config;

namespace BossmandadosAPIService.Controllers
{
    [MobileAppController]
    public class GoogleDirectionsController : Controller
    {

        // GET: GoogleDirections
        public ActionResult Index()
        {
            return View();
        }

        private DirectionsRequest SetUpRequest(ref List<string> ubicaciones)
        {
            if (ubicaciones.Count < 2)
            {
                return null;
            }
            string originString = ubicaciones.First<string>(), destinationString = ubicaciones.Last<string>();
            string[] waypoints;

            ubicaciones.RemoveAt(0);
            ubicaciones.RemoveAt(ubicaciones.Count - 1);

            if (ubicaciones.Count > 0)
            {
                waypoints = ubicaciones.ToArray();
            }
            else
            {
                waypoints = null;
            }

            DirectionsRequest request = new DirectionsRequest()
            {
                Origin = originString,
                Destination = destinationString,
                Waypoints = waypoints,
                SigningKey = GoogleDirectionsConfig.SIGNING_KEY
            };

            return request;
        }
        [HttpPost]
        public async Task<Manboss_polyline_direcciones> GetPolyline(List<string> ubicaciones)
        {
            try
            {
                DirectionsRequest directionsRequest = SetUpRequest(ref ubicaciones);

                if (directionsRequest == null)
                {
                    return null;
                }

                DirectionsResponse directions = await GoogleMaps.Directions.QueryAsync(directionsRequest);

                return SetUpResponse(directions);
            }
            catch (Exception e)
            {
                //TODO: logging when request fails
                return null;
            }
        }

        public Manboss_polyline_direcciones SetUpResponse(DirectionsResponse response)
        {
            if (response.Status != DirectionsStatusCodes.OK)
            {
                throw new Exception("Failed to receive Google Directions response: " + response.StatusStr);
            }

            Manboss_polyline_direcciones polyline = new Manboss_polyline_direcciones();
            polyline.Serial = response.Routes.First<Route>().OverviewPath.GetRawPointsData();

            return polyline;
        }
            
    }
}