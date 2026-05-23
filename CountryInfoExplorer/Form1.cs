using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CountryInfoExplorer.Models;
using CountryInfoExplorer.Services;

namespace CountryInfoExplorer;

public partial class Form1 : Form
{
    private readonly CountryService _countryService = new();
    private List<Country> _currentResults = new();

    public Form1()
    {
        InitializeComponent();
    }

    // ── Search ───────────────────────────────────────────────────────────────

    private async void BtnSearch_Click(object sender, EventArgs e)
    {
        await ExecuteSearchAsync();
    }

    private async void TxtCountryName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            await ExecuteSearchAsync();
        }
    }

    private async Task ExecuteSearchAsync()
    {
        string query = txtCountryName.Text.Trim();
        if (string.IsNullOrWhiteSpace(query))
        {
            ShowStatus("Please enter a search term.");
            return;
        }

        var mode = (SearchMode)cboSearchType.SelectedIndex;
        SetSearching(true);
        ClearDetails();

        try
        {
            _currentResults = await _countryService.SearchAsync(mode, query);

            lstMatches.BeginUpdate();
            lstMatches.Items.Clear();
            foreach (var c in _currentResults)
                lstMatches.Items.Add(c.Name.Common);
            lstMatches.EndUpdate();

            ShowStatus($"Found {_currentResults.Count} result(s) for \"{query}\".");

            if (lstMatches.Items.Count == 1)
                lstMatches.SelectedIndex = 0;
        }
        catch (CountryApiException ex)
        {
            lstMatches.Items.Clear();
            _currentResults.Clear();
            ShowStatus($"Error: {ex.Message}", isError: true);
            MessageBox.Show(ex.Message, "Search Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            ShowStatus("Unexpected error. See details.", isError: true);
            MessageBox.Show($"Unexpected error:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetSearching(false);
        }
    }

    // ── Search-type selector ─────────────────────────────────────────────────

    private static readonly string[] PlaceholderHints =
    {
        "e.g. Germany, Brazil, Japan…",         // Name
        "e.g. Aruba, United States…",            // Full Name
        "e.g. Tallinn, Paris, Tokyo…",            // Capital City
        "e.g. Europe, Asia, Africa…",             // Region
        "e.g. Northern Europe, Caribbean…",       // Subregion
        "e.g. Spanish, French, Arabic…",          // Language
        "e.g. USD, EUR, cop…",                    // Currency
        "e.g. DE, DEU, 276, co…",                 // Country Code
        "e.g. Alemania, Allemagne…"               // Translation
    };

    private void CboSearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = cboSearchType.SelectedIndex;
        if (idx >= 0 && idx < PlaceholderHints.Length)
            txtCountryName.PlaceholderText = PlaceholderHints[idx];
        txtCountryName.Clear();
        txtCountryName.Focus();
    }

    // ── List selection ────────────────────────────────────────────────────────

    private async void LstMatches_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = lstMatches.SelectedIndex;
        if (idx < 0 || idx >= _currentResults.Count) return;

        var country = _currentResults[idx];
        PopulateDetails(country);
        await LoadFlagAsync(country.Flags.Png);
    }

    // ── Populate details ──────────────────────────────────────────────────────

    // Populates all detail labels on the right panel from a Country object.
    private void PopulateDetails(Country c)
    {
        lblDetailHeading.Text = c.Name.Common;
        lblDetailHeading.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
        lblDetailHeading.ForeColor = Color.FromArgb(30, 80, 162);

        lblOfficialNameVal.Text = c.Name.Official;
        lblCapitalVal.Text      = c.CapitalDisplay;
        lblRegionVal.Text       = c.Region;
        lblSubregionVal.Text    = string.IsNullOrWhiteSpace(c.Subregion) ? "N/A" : c.Subregion;
        lblPopulationVal.Text   = c.Population.ToString("N0");
        lblAreaVal.Text         = c.Area > 0 ? c.Area.ToString("N0") : "N/A";
        lblLanguagesVal.Text    = c.LanguagesDisplay;
        lblCurrenciesVal.Text   = c.CurrenciesDisplay;
        lblTimezonesVal.Text    = c.TimezonesDisplay;
        lblContinentsVal.Text   = c.ContinentsDisplay;

        if (!string.IsNullOrWhiteSpace(c.Maps.GoogleMaps))
        {
            lnkMaps.Text = "Open in Google Maps";
            lnkMaps.Tag  = c.Maps.GoogleMaps;
        }
        else
        {
            lnkMaps.Text = "N/A";
            lnkMaps.Tag  = null;
        }

        tblDetails.Visible = true;
    }

    // ── Flag loading ─────────────────────────────────────────────────────────

    private async Task LoadFlagAsync(string flagUrl)
    {
        picFlag.Image = null;
        if (string.IsNullOrWhiteSpace(flagUrl)) return;

        try
        {
            byte[] data = await _countryService.DownloadFlagAsync(flagUrl);
            using var ms = new MemoryStream(data);
            picFlag.Image = Image.FromStream(ms);
        }
        catch
        {
            // Flag loading is optional; silently ignore failures
            picFlag.Image = null;
        }
    }

    // ── Clear ────────────────────────────────────────────────────────────────

    private void BtnClear_Click(object sender, EventArgs e)
    {
        txtCountryName.Clear();
        lstMatches.Items.Clear();
        _currentResults.Clear();
        ClearDetails();
        picFlag.Image = null;
        ShowStatus("Ready — enter a country name and press Search.");
        txtCountryName.Focus();
    }

    private void ClearDetails()
    {
        lblDetailHeading.Text      = "Select a country from the list to see details.";
        lblDetailHeading.Font      = new Font("Segoe UI", 11f, FontStyle.Italic);
        lblDetailHeading.ForeColor = Color.Gray;
        tblDetails.Visible         = false;

        lblOfficialNameVal.Text = string.Empty;
        lblCapitalVal.Text      = string.Empty;
        lblRegionVal.Text       = string.Empty;
        lblSubregionVal.Text    = string.Empty;
        lblPopulationVal.Text   = string.Empty;
        lblAreaVal.Text         = string.Empty;
        lblLanguagesVal.Text    = string.Empty;
        lblCurrenciesVal.Text   = string.Empty;
        lblTimezonesVal.Text    = string.Empty;
        lblContinentsVal.Text   = string.Empty;
        lnkMaps.Text            = string.Empty;
        lnkMaps.Tag             = null;
    }

    // ── Maps link ────────────────────────────────────────────────────────────

    private void LnkMaps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (lnkMaps.Tag is string url && !string.IsNullOrWhiteSpace(url))
        {
            // Validate URL scheme before opening
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private void Form1_Load(object sender, EventArgs e) { }

    private void Form1_Shown(object sender, EventArgs e)
    {
        splitMain.Panel1MinSize = 230;
        splitMain.Panel2MinSize = 380;
        try { splitMain.SplitterDistance = 290; }
        catch { /* default position is fine */ }
    }

    private void SetSearching(bool searching)
    {
        btnSearch.Enabled       = !searching;
        txtCountryName.Enabled  = !searching;
        btnClear.Enabled        = !searching;
        this.Cursor = searching ? Cursors.WaitCursor : Cursors.Default;
        if (searching) ShowStatus("Searching…");
    }

    private void ShowStatus(string message, bool isError = false)
    {
        tsslStatus.ForeColor = isError ? Color.Firebrick : SystemColors.ControlText;
        tsslStatus.Text = message;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _countryService.Dispose();
        base.OnFormClosing(e);
    }
}
