using System.Collections.Generic;
using Newtonsoft.Json;

namespace CountryInfoExplorer.Models
{
    public class CountryName
    {
        [JsonProperty("common")]
        public string Common { get; set; } = string.Empty;

        [JsonProperty("official")]
        public string Official { get; set; } = string.Empty;
    }

    public class CountryFlags
    {
        [JsonProperty("png")]
        public string Png { get; set; } = string.Empty;

        [JsonProperty("svg")]
        public string Svg { get; set; } = string.Empty;

        [JsonProperty("alt")]
        public string Alt { get; set; } = string.Empty;
    }

    public class CountryCurrency
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }

    public class CountryMaps
    {
        [JsonProperty("googleMaps")]
        public string GoogleMaps { get; set; } = string.Empty;
    }

    public class Country
    {
        [JsonProperty("name")]
        public CountryName Name { get; set; } = new();

        [JsonProperty("capital")]
        public List<string>? Capital { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; } = string.Empty;

        [JsonProperty("subregion")]
        public string Subregion { get; set; } = string.Empty;

        [JsonProperty("population")]
        public long Population { get; set; }

        [JsonProperty("area")]
        public double Area { get; set; }

        [JsonProperty("languages")]
        public Dictionary<string, string>? Languages { get; set; }

        [JsonProperty("currencies")]
        public Dictionary<string, CountryCurrency>? Currencies { get; set; }

        [JsonProperty("flags")]
        public CountryFlags Flags { get; set; } = new();

        [JsonProperty("maps")]
        public CountryMaps Maps { get; set; } = new();

        [JsonProperty("timezones")]
        public List<string>? Timezones { get; set; }

        [JsonProperty("continents")]
        public List<string>? Continents { get; set; }

        [JsonProperty("independent")]
        public bool? Independent { get; set; }

        // Computed helpers
        public string CapitalDisplay => Capital != null && Capital.Count > 0
            ? string.Join(", ", Capital)
            : "N/A";

        public string LanguagesDisplay => Languages != null
            ? string.Join(", ", Languages.Values)
            : "N/A";

        public string CurrenciesDisplay
        {
            get
            {
                if (Currencies == null) return "N/A";
                var parts = new List<string>();
                foreach (var kv in Currencies)
                    parts.Add($"{kv.Value.Name} ({kv.Value.Symbol})");
                return string.Join(", ", parts);
            }
        }

        public string TimezonesDisplay => Timezones != null
            ? string.Join(", ", Timezones)
            : "N/A";

        public string ContinentsDisplay => Continents != null
            ? string.Join(", ", Continents)
            : "N/A";
    }
}
