using System;
namespace WebPageCarbonFootprint
{
    public class WebsiteCarbonGetResponse
    {
        public string url { get; set; }
        public string green { get; set; }
        public int bytes { get; set; }
        public float cleanerThan { get; set; }
        public Stats statistics { get; set; }
    }
    public class Stats
    {
        public float adjustedBytes { get; set; }
        public float energy { get; set; }
        public CO2Types co2 { get; set; }
    }
    public class CO2Types
    {
        public Measurements grid { get; set; }
        public Measurements renewable { get; set; }
    }
    public class Measurements
    {
        public float grams { get; set; }
        public float litres { get; set; }
    }
}
