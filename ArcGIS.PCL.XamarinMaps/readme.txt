This library has extensions for the ArcGIS.PCL package for converting ArcGIS features into types compatible with the Xamarin.Forms.Maps library.

All extension methods are in the root namespace ArcGIS.ServiceModel

Example usage for getting points from an ArcGIS Server service as Pins

var gateway = new PortalGateway (
	"http://sampleserver3.arcgisonline.com/ArcGIS", 
	new ArcGIS.ServiceModel.Serializers.JsonDotNetSerializer ());

var query = new Query ("Earthquakes/EarthquakesFromLastSevenDays/MapServer/0".AsEndpoint()) {
	Where = "magnitude > 5.5",
	OutFields = new List<string> { "magnitude" }
};

// ToOutputAsGeographic() ensures that the query will return features with a spatial reference of WGS84
// you could also just set the OutputSpatialReference in the query to SpatialReference.WGS84
var points = await gateway.Query<Point> (query.ToOutputAsGeographic());

// call the extension method for the response
return points.ToPins (PinType.Place, "magnitude");