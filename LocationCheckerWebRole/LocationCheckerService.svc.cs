using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using LocationCheckerWebRole.GeocodeService;

namespace LocationCheckerWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : ILocationCheckerService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string Getlocation(string address)
        {
            string results = "";
            string key = "AlCGGWAhH4rz2Yt6CF58CIT-zurP_98yC90p8IvKayCE_tqGTxGT-m85bZO06U-J";

            try
            {
                //Create the geocode request, set the access key and the address to query
                GeocodeRequest geocodeRequest = new GeocodeRequest();
                geocodeRequest.Credentials = new GeocodeService.Credentials();
                geocodeRequest.Credentials.ApplicationId = key;
                geocodeRequest.Query = address;

                //Create a filter to return only high confidence results
                ConfidenceFilter[] filters = new ConfidenceFilter[1];
                filters[0] = new ConfidenceFilter();
                filters[0].MinimumConfidence = Confidence.High;

                //Apply the filter to the request
                GeocodeOptions options = new GeocodeOptions();
                options.Filters = filters;
                geocodeRequest.Options = options;

                //Make the request
                GeocodeServiceClient client = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
                GeocodeResponse response = client.Geocode(geocodeRequest);

                if (response.Results.Length > 0)
                {
                    results = String.Format("Success: {0}:{1}",
                        response.Results[0].Locations[0].Latitude,
                        response.Results[0].Locations[0].Longitude);
                }
                else
                {
                    results = "No Results Found";
                }
            }
            catch (Exception e)
            {
                results = "Geocoding Error: " + e.Message;
            }
            return results;
        }
    }
}
