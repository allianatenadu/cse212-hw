// Models the USGS GeoJSON earthquake feed format:
// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php

public class FeatureCollection
{
    public List<Feature> Features { get; set; } = [];
}

public class Feature
{
    public EarthquakeProperties Properties { get; set; } = new();
}

public class EarthquakeProperties
{
    public string Place { get; set; } = "";
    public double? Mag { get; set; }
}