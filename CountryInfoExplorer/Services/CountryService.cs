// =============================================================================
// CountryService.cs — API Communication Layer
// =============================================================================
// This file is responsible for talking to the REST Countries API.
// It sends HTTP requests, reads the JSON response, and returns Country objects.
//
// Why a separate service file?
//   Keeping API logic here (away from Form1.cs) is called "separation of concerns".
//   The form just calls SearchAsync() and gets back a list — it doesn't need to
//   know anything about HTTP or JSON. This makes both files easier to read and test.
// =============================================================================

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CountryInfoExplorer.Models;
using Newtonsoft.Json;

namespace CountryInfoExplorer.Services
{
    // SearchMode tells SearchAsync() which API endpoint to use.
    // Each value corresponds to one dropdown option in the form.
    public enum SearchMode
    {
        Name,        // search by common name       → /name/{query}
        FullName,    // exact full name match        → /name/{query}?fullText=true
        Capital,     // search by capital city       → /capital/{query}
        Region,      // search by world region       → /region/{query}
        Subregion,   // search by subregion          → /subregion/{query}
        Language,    // search by spoken language    → /lang/{query}
        Currency,    // search by currency code/name → /currency/{query}
        Code,        // search by country code       → /alpha/{query}
        Translation  // search by translated name    → /translation/{query}
    }

    // CountryApiException is our own custom exception class.
    // We throw it whenever something goes wrong with the API call so that
    // Form1.cs can catch it and show the user a friendly error message.
    // It inherits from Exception — the base class for all C# exceptions.
    public class CountryApiException : Exception
    {
        public CountryApiException(string message) : base(message) { }
        public CountryApiException(string message, Exception inner) : base(message, inner) { }
    }

    // CountryService handles all HTTP communication with the REST Countries API.
    // It implements IDisposable so we can call Dispose() to free the HttpClient
    // when the form closes (see OnFormClosing in Form1.cs).
    public class CountryService : IDisposable
    {
        // HttpClient is the .NET class used to make HTTP requests (like fetch in JS).
        // It is "static" so all instances of CountryService share ONE HttpClient.
        // Reusing a single HttpClient is best practice — creating one per request
        // can exhaust the system's available sockets.
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://restcountries.com/v3.1/"), // all URLs start here
            Timeout     = TimeSpan.FromSeconds(15)                    // fail if no response in 15s
        };

        // Static constructor — runs once when the class is first used.
        // We add a request header here so every request tells the server we want JSON.
        static CountryService()
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        // ─────────────────────────────────────────────────────────────────────
        // SearchAsync — the main public method called from Form1.cs
        // ─────────────────────────────────────────────────────────────────────
        // Validates the input, builds the correct API URL based on the search mode,
        // then calls FetchCountriesAsync() to do the actual HTTP request.
        public async Task<List<Country>> SearchAsync(SearchMode mode, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new CountryApiException("Please enter a search term.");

            // Remove extra spaces and reject suspiciously long input
            var sanitized = query.Trim();
            if (sanitized.Length > 100)
                throw new CountryApiException("Search term is too long.");

            // Uri.EscapeDataString() converts special characters so they are safe
            // to put in a URL. For example, spaces become %20, & becomes %26.
            string url = mode switch
            {
                SearchMode.Name        => $"name/{Uri.EscapeDataString(sanitized)}",
                SearchMode.FullName    => $"name/{Uri.EscapeDataString(sanitized)}?fullText=true",
                SearchMode.Capital     => $"capital/{Uri.EscapeDataString(sanitized)}",
                SearchMode.Region      => $"region/{Uri.EscapeDataString(sanitized)}",
                SearchMode.Subregion   => $"subregion/{Uri.EscapeDataString(sanitized)}",
                SearchMode.Language    => $"lang/{Uri.EscapeDataString(sanitized)}",
                SearchMode.Currency    => $"currency/{Uri.EscapeDataString(sanitized)}",
                SearchMode.Code        => $"alpha/{Uri.EscapeDataString(sanitized)}",
                SearchMode.Translation => $"translation/{Uri.EscapeDataString(sanitized)}",
                _ => throw new CountryApiException("Unknown search mode.")
            };

            return await FetchCountriesAsync(url, sanitized);
        }

        // ─────────────────────────────────────────────────────────────────────
        // FetchCountriesAsync — sends the HTTP GET and parses the response
        // ─────────────────────────────────────────────────────────────────────
        // This is "private" because only SearchAsync inside this class calls it.
        // "static" means it doesn't use any instance fields — just the shared _httpClient.
        private static async Task<List<Country>> FetchCountriesAsync(string url, string term)
        {
            HttpResponseMessage response;
            try
            {
                // GetAsync sends the HTTP GET request and awaits the server's response.
                // The full URL is BaseAddress + url, e.g. https://restcountries.com/v3.1/name/germany
                response = await _httpClient.GetAsync(url);
            }
            catch (HttpRequestException ex)
            {
                // HttpRequestException means a network-level failure (no internet, DNS error, etc.)
                throw new CountryApiException(
                    "Network error: unable to reach the REST Countries API. Check your internet connection.", ex);
            }
            catch (TaskCanceledException)
            {
                // TaskCanceledException is thrown when the request exceeds the 15-second timeout
                throw new CountryApiException("The request timed out. Please try again.");
            }

            // HTTP 404 = "Not Found" — the API uses this to mean "no matching country"
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new CountryApiException($"No results found for \"{term}\".");

            // Any other non-success status (5xx server error, etc.)
            if (!response.IsSuccessStatusCode)
                throw new CountryApiException(
                    $"API returned an error: {(int)response.StatusCode} {response.ReasonPhrase}");

            // Read the response body as a string (this is the raw JSON text)
            string json = await response.Content.ReadAsStringAsync();

            List<Country>? countries;
            try
            {
                // Most endpoints return a JSON array: [ {...}, {...} ]
                // But /alpha/{code} returns a single object: { ... }
                // We detect which by checking the first non-whitespace character.
                if (json.TrimStart().StartsWith('{'))
                {
                    // Single object — wrap it in a list so we always return List<Country>
                    var single = JsonConvert.DeserializeObject<Country>(json);
                    countries = single != null ? new List<Country> { single } : null;
                }
                else
                {
                    // JSON array — deserialize directly to List<Country>
                    countries = JsonConvert.DeserializeObject<List<Country>>(json);
                }
            }
            catch (JsonException ex)
            {
                // JsonException means the JSON was malformed or didn't match our model
                throw new CountryApiException("Failed to parse the API response.", ex);
            }

            if (countries == null || countries.Count == 0)
                throw new CountryApiException($"No data returned for \"{term}\".");

            // Sort the results alphabetically by common name so the list is easy to read
            countries.Sort((a, b) =>
                string.Compare(a.Name.Common, b.Name.Common, StringComparison.OrdinalIgnoreCase));
            return countries;
        }

        // ─────────────────────────────────────────────────────────────────────
        // DownloadFlagAsync — downloads the flag image as raw bytes
        // ─────────────────────────────────────────────────────────────────────
        // Form1.cs calls this after selecting a country.
        // Returns a byte[] which is then converted to an Image in Form1.cs.
        public async Task<byte[]> DownloadFlagAsync(string flagUrl)
        {
            if (string.IsNullOrWhiteSpace(flagUrl))
                throw new CountryApiException("Flag URL is not available.");

            // Security check: only allow downloads from the two known flag CDN domains.
            // This prevents someone from tricking the app into downloading from arbitrary URLs.
            if (!Uri.TryCreate(flagUrl, UriKind.Absolute, out var uri) ||
                (uri.Host != "flagcdn.com" && uri.Host != "upload.wikimedia.org"))
            {
                throw new CountryApiException("Invalid flag URL.");
            }

            try
            {
                // GetByteArrayAsync downloads the file and returns it as a byte array (raw bytes).
                return await _httpClient.GetByteArrayAsync(flagUrl);
            }
            catch (Exception ex)
            {
                throw new CountryApiException("Could not load the flag image.", ex);
            }
        }

        // Dispose() frees the HttpClient when the form closes.
        // Called from Form1.OnFormClosing().
        public void Dispose() => _httpClient.Dispose();
    }
}
