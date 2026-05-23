// =============================================================================
// CountryModel.cs — Data Models (the "shape" of API data)
// =============================================================================
// This file defines C# classes that match the JSON structure returned by the
// REST Countries API (https://restcountries.com/v3.1/).
//
// When Newtonsoft.Json deserializes the API response, it reads each JSON field
// and copies its value into the matching C# property.
//
// Example JSON snippet the API returns:
//   {
//     "name": { "common": "Germany", "official": "Federal Republic of Germany" },
//     "capital": ["Berlin"],
//     "population": 83240525
//   }
//
// The [JsonProperty("fieldName")] attribute tells the deserializer which JSON
// key maps to which C# property. This is needed when the JSON key uses
// camelCase (like "googleMaps") but we want a different C# name.
// =============================================================================

using System.Collections.Generic;
using Newtonsoft.Json;

namespace CountryInfoExplorer.Models
{
    // Holds the name of a country in two forms:
    //   Common   = the everyday name  (e.g. "Germany")
    //   Official = the formal name    (e.g. "Federal Republic of Germany")
    public class CountryName
    {
        [JsonProperty("common")]
        public string Common { get; set; } = string.Empty;

        [JsonProperty("official")]
        public string Official { get; set; } = string.Empty;
    }

    // Holds URLs to the country's flag image in different formats.
    // We use the PNG URL to download and display the flag in a PictureBox.
    public class CountryFlags
    {
        [JsonProperty("png")]
        public string Png { get; set; } = string.Empty;   // URL to PNG image

        [JsonProperty("svg")]
        public string Svg { get; set; } = string.Empty;   // URL to SVG image (not used here)

        [JsonProperty("alt")]
        public string Alt { get; set; } = string.Empty;   // text description of the flag
    }

    // Represents one currency used by a country.
    // A country can have multiple currencies (see the Currencies dictionary in Country).
    public class CountryCurrency
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;   // e.g. "Euro"

        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty; // e.g. "€"
    }

    // Holds map links for the country.
    public class CountryMaps
    {
        [JsonProperty("googleMaps")]
        public string GoogleMaps { get; set; } = string.Empty; // URL to Google Maps
    }

    // The main country model. One instance of this class = one country from the API.
    // Every field here matches a JSON field in the API response.
    public class Country
    {
        // Name contains both the common name and the official name
        [JsonProperty("name")]
        public CountryName Name { get; set; } = new();

        // Capital is a List because some countries have more than one capital city
        // (e.g. South Africa has three). Nullable (?) because some territories have none.
        [JsonProperty("capital")]
        public List<string>? Capital { get; set; }

        // The broad world region, e.g. "Europe", "Asia", "Africa"
        [JsonProperty("region")]
        public string Region { get; set; } = string.Empty;

        // A more specific area within the region, e.g. "Western Europe"
        [JsonProperty("subregion")]
        public string Subregion { get; set; } = string.Empty;

        // Population count as a whole number (long supports very large numbers)
        [JsonProperty("population")]
        public long Population { get; set; }

        // Total area in square kilometres
        [JsonProperty("area")]
        public double Area { get; set; }

        // A dictionary of languages: key = language code (e.g. "deu"), value = language name (e.g. "German")
        // Dictionary? is nullable because some territories have no official language recorded.
        [JsonProperty("languages")]
        public Dictionary<string, string>? Languages { get; set; }

        // A dictionary of currencies: key = currency code (e.g. "EUR"), value = currency details
        [JsonProperty("currencies")]
        public Dictionary<string, CountryCurrency>? Currencies { get; set; }

        // Flag image URLs
        [JsonProperty("flags")]
        public CountryFlags Flags { get; set; } = new();

        // Map links
        [JsonProperty("maps")]
        public CountryMaps Maps { get; set; } = new();

        // List of timezone strings, e.g. ["UTC+01:00", "UTC+02:00"]
        [JsonProperty("timezones")]
        public List<string>? Timezones { get; set; }

        // List of continents the country belongs to, e.g. ["Europe"]
        [JsonProperty("continents")]
        public List<string>? Continents { get; set; }

        // Whether the country is internationally recognised as independent
        [JsonProperty("independent")]
        public bool? Independent { get; set; }

        // ── Computed display properties ────────────────────────────────────
        // These are not from the JSON. They are helper properties that format
        // the raw data into a display-ready string for the UI labels.
        // The "=>" syntax is a C# "expression body" — a compact way to write
        // a property that just returns a calculated value.

        // Returns the capital cities joined by commas, or "N/A" if none
        public string CapitalDisplay => Capital != null && Capital.Count > 0
            ? string.Join(", ", Capital)
            : "N/A";

        // Returns all language names joined by commas, or "N/A" if unknown
        public string LanguagesDisplay => Languages != null
            ? string.Join(", ", Languages.Values)
            : "N/A";

        // Returns all currencies formatted as "Euro (€), Dollar ($)", or "N/A"
        public string CurrenciesDisplay
        {
            get
            {
                if (Currencies == null) return "N/A";
                var parts = new List<string>();
                // kv = key-value pair: kv.Key = "EUR", kv.Value = CountryCurrency object
                foreach (var kv in Currencies)
                    parts.Add($"{kv.Value.Name} ({kv.Value.Symbol})");
                return string.Join(", ", parts);
            }
        }

        // Returns all timezones joined by commas, or "N/A" if unknown
        public string TimezonesDisplay => Timezones != null
            ? string.Join(", ", Timezones)
            : "N/A";

        // Returns all continents joined by commas, or "N/A" if unknown
        public string ContinentsDisplay => Continents != null
            ? string.Join(", ", Continents)
            : "N/A";
    }
}
