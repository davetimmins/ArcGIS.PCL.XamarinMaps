using ArcGIS.ServiceModel.Common;
using ArcGIS.ServiceModel.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Maps;

namespace ArcGIS.ServiceModel
{
    public static class FeatureExtensions
    {
        /// <summary>
        /// Convert a query response containing point features into Pins that can be added to a Xamarin Map control.
        /// You should make sure that the point features are in the WGS84 spatial reference when calling this.
        /// The easiest way to do this is to specify the OutputSpatialReference when fetching the features. There is also an extension method for the Query operation called <see cref="ToOutputAsGeographic"/> to check this.
        /// </summary>
        /// <param name="points">The query response containing features to convert</param>
        /// <param name="pinType">The pin type to set for the Pins</param>
        /// <param name="labelField">A field in the attributes of the features to use as the label of the Pin</param>
        /// <returns>The list of converted Pins or null if no features are passed in or they have null geometries</returns>
        /// <exception cref="InvalidOperationException">Throws an exception if non geographic features are passed in</exception>
        public static List<Pin> ToPins(this QueryResponse<Point> points,
            PinType pinType = PinType.Generic, String labelField = "")
        {
            if (points == null || points.Features == null || !points.Features.Any() || points.Features.Any(p => p.Geometry == null)) return null;

            points.Features.First().Geometry.SpatialReference = points.SpatialReference;
            return points.Features.ToList().ToPins(pinType, labelField);
        }

        /// <summary>
        /// Convert a set of ArcGIS point features into Pins that can be added to a Xamarin Map control.
        /// You should make sure that the point features are in the WGS84 spatial reference when calling this.
        /// The easiest way to do this is to specify the OutputSpatialReference when fetching the features. There is also an extension method for the Query operation called <see cref="ToOutputAsGeographic"/> to check this.
        /// </summary>
        /// <param name="points">The features to convert</param>
        /// <param name="pinType">The pin type to set for the Pins</param>
        /// <param name="labelField">A field in the attributes of the features to use as the label of the Pin</param>
        /// <returns>The list of converted Pins or null if no features are passed in or they have null geometries</returns>
        /// <exception cref="InvalidOperationException">Throws an exception if non geographic features are passed in</exception>
        public static List<Pin> ToPins(this List<Feature<Point>> points,
            PinType pinType = PinType.Generic, String labelField = "")
        {
            if (points == null || !points.Any() || points.Any(p => p.Geometry == null)) return null;

            var checkSR = points.First().Geometry.SpatialReference;
            if (checkSR != null && checkSR != SpatialReference.WGS84)
                throw new InvalidOperationException("Only features with a spatial reference of WGS84 are supported.");

            return points.Select(point =>
            {
                return new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(point.Geometry.Y, point.Geometry.X),
                    Label = (String.IsNullOrWhiteSpace(labelField) || point.Attributes == null || !point.Attributes.ContainsKey(labelField))
                        ? "" : point.Attributes[labelField].ToString()
                };
            }).ToList();
        }
    }
}
