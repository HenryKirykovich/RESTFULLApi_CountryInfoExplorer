# Country Information Explorer

A Windows Forms application (.NET 8) that fetches and displays detailed country data using the [REST Countries API v3.1](https://restcountries.com).

## Features

- Search countries by **Name**, **Full Name**, **Capital City**, **Region**, **Subregion**, **Language**, **Currency**, **Country Code**, or **Translation**
- Displays: official name, capital, region, subregion, population, area, languages, currencies, timezones, continents, and flag
- Clickable Google Maps link for each country
- Full error handling for network failures and invalid queries

## Tech Stack

- C# / .NET 8 Windows Forms
- `HttpClient` for REST API calls
- `Newtonsoft.Json` for JSON deserialization

## How to Run

```bash
cd CountryInfoExplorer
dotnet run
```

Or open `CountryInfoExplorer.sln` in Visual Studio and press **F5**.

## API

Uses [restcountries.com/v3.1](https://restcountries.com) — free, no API key required.
