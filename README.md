<img align="right" height="120" src="https://raw.githubusercontent.com/davetimmins/ArcGIS.PCL/master/gateway.png">

# ArcGIS.PCL.XamarinMaps

[![Build status](https://ci.appveyor.com/api/projects/status/27swdq5ie969aekk)](https://ci.appveyor.com/project/davetimmins/arcgis-pcl-xamarinmaps) [![NuGet Status](http://img.shields.io/nuget/v/ArcGIS.PCL.XamarinMaps.svg?style=flat)](https://www.nuget.org/packages/ArcGIS.PCL.XamarinMaps/) 

This library has extensions for the [ArcGIS.PCL](https://github.com/davetimmins/ArcGIS.PCL) package for converting ArcGIS features into types compatible with the Xamarin.Forms.Maps library.

All extension methods are in the root namespace `ArcGIS.ServiceModel`

Example usage for getting points from an ArcGIS Server service as Pins

```csharp

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
```

### Installation
If you have [NuGet](http://nuget.org) installed, the easiest way to get started is to install via NuGet:

    PM> Install-Package ArcGIS.PCL.XamarinMaps

Of course you can also get the code from this site.

### Icon

Icon made by [Freepik](http://www.freepik.com) from [www.flaticon.com](http://www.flaticon.com/free-icon/triangle-of-triangles_32915)
           
