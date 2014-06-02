using ArcGIS.ServiceModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Maps;

namespace ArcGIS.ServiceModel.Extensions
{
    public static class PinExtensions
    {
        /// <summary>
        /// Convert pins into ArcGIS Point features. 
        /// The converted list will have a spatial reference of WGS84 and include attributes for Address, Label and Type
        /// </summary>
        /// <param name="pins">Pins to convert</param>
        /// <returns>The list of converted points</returns>
        public static List<Feature<Point>> ToFeatures(this List<Pin> pins)
        {
            return pins.Select(pin =>
            {
                return new Feature<Point>
                {
                    Geometry = new Point
                    {
                        X = pin.Position.Longitude,
                        Y = pin.Position.Latitude,
                        SpatialReference = SpatialReference.WGS84
                    },
                    Attributes = new Dictionary<String, object>
                    {                
                        { "Address", pin.Address },
                        { "Label", pin.Label },
                        { "Type", pin.Type.ToString() },
                    }
                };
            }).ToList();
        }
    }
}
