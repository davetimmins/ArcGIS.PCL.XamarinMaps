using ArcGIS.ServiceModel.Common;
using ArcGIS.ServiceModel.Operation;
using System;

namespace ArcGIS.ServiceModel
{
    public static class OperationExtensions
    {
        /// <summary>
        /// Makes sure that the OutputSpatialReference is set to WGS84 for the Query object
        /// </summary>
        /// <param name="query">Query object to modify</param>
        /// <returns>The modified query object with it's OutputSpatialReference set to WGS84</returns>
        public static Query ToOutputAsGeographic(this Query query)
        {
            query.OutputSpatialReference = SpatialReference.WGS84;
            return query;
        }
    }
}
