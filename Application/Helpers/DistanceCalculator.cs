using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evacuation.Application.Helpers
{
    public class DistanceCalculator
    {


        private const double EarthRadiusKm = 6371;

        public static double CalculateDistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            //Haversine
            try
            {
                if (double.IsNaN(lat1) || double.IsNaN(lon1) || double.IsNaN(lat2) || double.IsNaN(lon2))
                {
                    return double.NaN;
                }

                double dLat = ToRad(lat2 - lat1);
                double dLon = ToRad(lon2 - lon1);

                double a =
                    Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                return EarthRadiusKm * c; // กิโลเมตร
            }
            catch
            {
                // กัน error หลุด (เช่น input แปลกจาก external system)
                return double.NaN;
            }
        }

        //การแปลงองศา → เรเดียน
        private static double ToRad(double degree) => degree * Math.PI / 180;




        public static double CalculateEtaMinutes(double distanceKm, double speedKmPerHour)
        {
            if (speedKmPerHour <= 0)
                return double.NaN;

            double etaMinutes = distanceKm / speedKmPerHour * 60;
            return etaMinutes;
        }




    }




}
