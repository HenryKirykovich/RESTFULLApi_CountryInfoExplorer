using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CountryInfoExplorer.Models;
using Newtonsoft.Json;

namespace CountryInfoExplorer.Services
{
    public enum SearchMode
    {
        Name,
        FullName,
        Capital,
        Region,
        Subregion,
        Language,
        Currency,
        Code,
        Translation
    }

    public class CountryApiException : Exception
    {
        public CountryApiException(string message) : base(message) { }
        public CountryApiException(string message, Exception inner) : base(message, inner) { }
    }

    public class CountryService : IDisposable
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://restcountries.com/v3.1/"),
            Timeout = TimeSpan.FromSeconds(15)
        };

        static CountryService()
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        /// <summary>
        /// Searches countries using the selected endpoint mode.
        /// Sanitizes the query and maps SearchMode to the correct REST Countries v3.1 endpoint.
        /// </summary>
        public async Task<List<Country>> SearchAsync(SearchMode mode, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new CountryApiException("Please enter a search term.");

            var sanitized = query.Trim();
            if (sanitized.Length > 100)
                throw new CountryApiException("Search term is too long.");

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

        // ── Internal fetch helper ────────────────────────────────────────────
        // Executes the HTTP GET, handles 404 / network errors, and deserializes
        // the response (handles both JSON array and single-object responses).
        private static async Task<List<Country>> FetchCountriesAsync(string url, string term)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(url);
            }
            catch (HttpRequestException ex)
            {
                throw new CountryApiException(
                    "Network error: unable to reach the REST Countries API. Check your internet connection.", ex);
            }
            catch (TaskCanceledException)
            {
                throw new CountryApiException("The request timed out. Please try again.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new CountryApiException($"No results found for \"{term}\".");

            if (!response.IsSuccessStatusCode)
                throw new CountryApiException(
                    $"API returned an error: {(int)response.StatusCode} {response.ReasonPhrase}");

            string json = await response.Content.ReadAsStringAsync();

            List<Country>? countries;
            try
            {
                // The /alpha/{code} endpoint returns a single object, not an array
                if (json.TrimStart().StartsWith('{'))
                {
                    var single = JsonConvert.DeserializeObject<Country>(json);
                    countries = single != null ? new List<Country> { single } : null;
                }
                else
                {
                    countries = JsonConvert.DeserializeObject<List<Country>>(json);
                }
            }
            catch (JsonException ex)
            {
                throw new CountryApiException("Failed to parse the API response.", ex);
            }

            if (countries == null || countries.Count == 0)
                throw new CountryApiException($"No data returned for \"{term}\".");

            countries.Sort((a, b) =>
                string.Compare(a.Name.Common, b.Name.Common, StringComparison.OrdinalIgnoreCase));
            return countries;
        }

        /// <summary>
        /// Downloads a flag image and returns it as a byte array.
        /// </summary>
        public async Task<byte[]> DownloadFlagAsync(string flagUrl)
        {
            if (string.IsNullOrWhiteSpace(flagUrl))
                throw new CountryApiException("Flag URL is not available.");

            if (!Uri.TryCreate(flagUrl, UriKind.Absolute, out var uri) ||
                (uri.Host != "flagcdn.com" && uri.Host != "upload.wikimedia.org"))
            {
                throw new CountryApiException("Invalid flag URL.");
            }

            try
            {
                return await _httpClient.GetByteArrayAsync(flagUrl);
            }
            catch (Exception ex)
            {
                throw new CountryApiException("Could not load the flag image.", ex);
            }
        }

        public void Dispose() => _httpClient.Dispose();
    }
}
