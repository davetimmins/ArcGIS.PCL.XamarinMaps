using System;
using Xamarin.Forms.Maps;

namespace ArcGIS.ServiceModel
{
    public static class MathUtils
    {
        public enum LengthUnit { Mile, NauticalMile, Kilometer, Meter }

        const double Miles2Kilometers = 1.609344;
        const double Miles2Meters = 1609.344;
        const double Miles2Nautical = 0.8684;

        /// <summary>
        /// Converts degrees to Radians.
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static double ToRadian(double degree)
        {
            return (degree * Math.PI / 180.0);
        }

        /// <summary>
        /// To degrees from a radian value.
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static double ToDegree(double radian)
        {
            return (radian / Math.PI * 180.0);
        }

        /// <summary>
        /// Convert dd.ddddd to dddmm.mmmm
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double d2dm(double d)
        {
            int degree = ((int)d);
            double minute = (d - degree) * 60;
            double dm = degree * 100 + minute;
            return dm;
        }

        /// <summary>
        /// Calculate the distance between the two positions
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <param name="unit"></param>
        /// <returns>Return distance in Kilometer, Meter, or NauticalMile</returns>
        public static double Distance(Position position1, Position position2, LengthUnit unit = LengthUnit.Meter)
        {
            var deltaLong = position1.Longitude - position2.Longitude;
            var distance = Math.Sin(ToRadian(position1.Latitude)) * Math.Sin(ToRadian(position2.Latitude)) +
                           Math.Cos(ToRadian(position1.Latitude)) * Math.Cos(ToRadian(position2.Latitude)) * Math.Cos(ToRadian(deltaLong));

            // distance is in miles
            distance = Math.Acos(distance);
            distance = ToDegree(distance) * 60 * 1.1515;

            if (unit == LengthUnit.Kilometer)
                distance = distance * Miles2Kilometers;  // distance in km
            else if (unit == LengthUnit.Meter)
                distance = distance * Miles2Meters;      // distance in meter
            else if (unit == LengthUnit.NauticalMile)
                distance = distance * Miles2Nautical;    // distance in nautical mile

            if (double.IsNaN(distance))
                return 0;

            return (distance);
        }

        /// <summary>
        /// Calculates the bearing between the two positions.
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <returns>The bearing value from 0 to 360.</returns>
        public static double Bearing(Position position1, Position position2)
        {
            double lat1 = ToRadian(position1.Latitude);
            double lat2 = ToRadian(position2.Latitude);

            double deltaLong = ToRadian((position2.Longitude - position1.Longitude));

            double y = Math.Sin(deltaLong) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(deltaLong);

            return (ToDegree(Math.Atan2(y, x)) + 360) % 360;
        }
    }
}

