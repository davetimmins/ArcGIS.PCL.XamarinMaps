using ArcGIS.ServiceModel.Common;
using System;
using Xamarin.Forms.Maps;

namespace ArcGIS.ServiceModel
{
    public static class PositionExtensions
    {
        /// <summary>
        /// Convert a Position into an ArcGIS point
        /// </summary>
        /// <param name="position"></param>
        /// <returns>An ArcGIS point with a spatial reference of WGS84</returns>
        public static Point ToPoint(this Position position)
        {
            return new Point
            {
                SpatialReference = SpatialReference.WGS84,
                X = position.Longitude,
                Y = position.Latitude
            };
        }
    }
}
