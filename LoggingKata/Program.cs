using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;


namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            var allRestaurants = lines.Select(parser.Parse).ToArray();

            ITrackable finalRestaurant1 = null;
            ITrackable finalRestaurant2 = null;
            double finalDistance = 0;

            ITrackable locA = null;
            ITrackable locB = null;
            var corA = new GeoCoordinate(0, 0);
            var corB = new GeoCoordinate(0, 0);


            for (int i = 0; i < allRestaurants.Length; i++)
            {
                locA = allRestaurants[i];
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                for (int i2 = 0; i2 < allRestaurants.Length; i2++)
                {
                    locB = allRestaurants[i2];
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    var testDistance = corA.GetDistanceTo(corB);

                    if (testDistance > finalDistance)
                    {
                        finalDistance = testDistance;
                        finalRestaurant1 = allRestaurants[i];
                        finalRestaurant2 = allRestaurants[i2];
                    }
                }
            }

            var num = (1609.344);
            double? v = finalDistance / num;
            Console.WriteLine();
            Console.WriteLine($"The distance between {finalRestaurant1.Name} and {finalRestaurant2.Name} is {finalDistance} miles apart.");
        }
    }
}
