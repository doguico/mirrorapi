using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Mirror.v1;
using Google.Apis.Mirror.v1.Data;

namespace MirrorAPI
{
    public class Locations
    {
        /// <summary>
        /// Print information about the latest known location for the current user.
        /// </summary>
        /// <param name='service'>
        /// Authorized Mirror service.
        /// </param>
        public static void PrintLatestLocation(MirrorService service)
        {
            try
            {
                Location location = service.Locations.Get("latest").Fetch();

                Console.WriteLine("Location recorded on: " + location.Timestamp);
                Console.WriteLine("  > Lat: " + location.Latitude);
                Console.WriteLine("  > Long: " + location.Longitude);
                Console.WriteLine("  > Accuracy: " + location.Accuracy + " meters");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        /// <summary>
        /// Print information about all the known locations for the current user.
        /// </summary>
        /// <param name="service">Authorized Mirror service.</param>
        public static void PrintAllLocations(MirrorService service)
        {
            try
            {
                LocationsListResponse locations = service.Locations.List().Fetch();

                foreach (Location location in locations.Items)
                {
                    Console.WriteLine("Location recorded on: " + location.Timestamp);
                    Console.WriteLine("  > Lat: " + location.Latitude);
                    Console.WriteLine("  > Long: " + location.Longitude);
                    Console.WriteLine("  > Accuracy: " + location.Accuracy + " meters");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

    }
}
