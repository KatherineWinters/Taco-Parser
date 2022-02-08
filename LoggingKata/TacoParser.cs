using System;

namespace LoggingKata
{
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("Error: The line contained fewer than 3 elements.");

                return null; 
            }

            double latitude = Convert.ToDouble(cells[0]);

            double longitude = Convert.ToDouble(cells[1]);

            string name = (cells[2]);

            var restaurant = new TacoBell(latitude, longitude, name);

            return restaurant;

        }
    }
}