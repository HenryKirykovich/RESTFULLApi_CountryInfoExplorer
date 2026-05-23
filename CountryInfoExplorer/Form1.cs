// =============================================================================
// Form1.cs — Application Logic (the "brains" of the form)
// =============================================================================
// This file contains all the event handlers and helper methods.
// It works together with Form1.Designer.cs, which holds the visual layout.
//
// How these two files connect:
//   Form1.Designer.cs  →  creates controls, sets their look, wires up events
//   Form1.cs           →  contains the methods that run when events fire
//
// The "partial" keyword on the class allows one class to be split across
// multiple files. Both files together make up the complete Form1 class.
// =============================================================================

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

// Form1 inherits from Form, which means it IS a Windows Form window.
// "partial" means the class is split between this file and Form1.Designer.cs.
public partial class Form1 : Form
{
    // _countryService handles all communication with the REST Countries API.
    // It is "readonly" because we create it once and never replace it.
    private readonly CountryService _countryService = new();

    // Stores the list of countries returned by the last search.
    // We keep this in memory so when the user clicks a name in the list,
    // we can look up the full Country object without making another API call.
    private List<Country> _currentResults = new();

    // Constructor — called once when the form is first created.
    // InitializeComponent() builds the entire UI (defined in Form1.Designer.cs).
    public Form1()
    {
        InitializeComponent();
    }

    // ═══════════════════════════════════════════════════════════════════════
    // SEARCH — methods that handle starting a search
    // ═══════════════════════════════════════════════════════════════════════

    // Called when the user clicks the "Search" button.
    // "async void" is the correct signature for event handlers that do async work.
    private async void BtnSearch_Click(object sender, EventArgs e)
    {
        await ExecuteSearchAsync();
    }

    // Called when the user presses a key while the text box is focused.
    // We check if it was the Enter key, and if so, trigger the search.
    private async void TxtCountryName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true; // prevents the "ding" sound on Enter
            await ExecuteSearchAsync();
        }
    }

    // The main search method. Both BtnSearch_Click and TxtCountryName_KeyDown call this.
    // "async Task" means this method can pause (await) while waiting for the API response,
    // without freezing the entire UI.
    private async Task ExecuteSearchAsync()
    {
        // Read what the user typed and remove leading/trailing spaces
        string query = txtCountryName.Text.Trim();

        // Don't search if the box is empty
        if (string.IsNullOrWhiteSpace(query))
        {
            ShowStatus("Please enter a search term.");
            return;
        }

        // Convert the dropdown selection (0, 1, 2...) to a SearchMode enum value
        var mode = (SearchMode)cboSearchType.SelectedIndex;

        SetSearching(true); // disable controls and show "Searching…"
        ClearDetails();     // wipe the detail panel while we wait

        try
        {
            // Call the API — this line pauses (awaits) until the response arrives.
            // During the wait the UI stays responsive; the user can still move the window.
            _currentResults = await _countryService.SearchAsync(mode, query);

            // BeginUpdate/EndUpdate prevents the list from repainting after every item add.
            // Without them, adding many items would cause visible flickering.
            lstMatches.BeginUpdate();
            lstMatches.Items.Clear();
            foreach (var c in _currentResults)
                lstMatches.Items.Add(c.Name.Common);
            lstMatches.EndUpdate();

            ShowStatus($"Found {_currentResults.Count} result(s) for \"{query}\".");

            // If there is exactly one result, auto-select it so details appear immediately
            if (lstMatches.Items.Count == 1)
                lstMatches.SelectedIndex = 0;
        }
        catch (CountryApiException ex)
        {
            // CountryApiException is our custom exception for expected API errors
            // (e.g. "no results found", "network error"). Show a friendly message.
            lstMatches.Items.Clear();
            _currentResults.Clear();
            ShowStatus($"Error: {ex.Message}", isError: true);
            MessageBox.Show(ex.Message, "Search Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            // Catch any other unexpected error so the app doesn't crash
            ShowStatus("Unexpected error. See details.", isError: true);
            MessageBox.Show($"Unexpected error:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            // finally runs whether the search succeeded or failed.
            // Always re-enable the controls so the user can try again.
            SetSearching(false);
        }
    }

    // ═══════════════════════════════════════════════════════════════════════
    // SEARCH TYPE DROPDOWN — updates the placeholder hint when mode changes
    // ═══════════════════════════════════════════════════════════════════════

    // Each search mode has a different placeholder hint so the user knows
    // what format to type. For example, "Region" shows "e.g. Europe, Asia…"
    private static readonly string[] PlaceholderHints =
    {
        "e.g. Germany, Brazil, Japan…",         // 0 = Name
        "e.g. Aruba, United States…",            // 1 = Full Name
        "e.g. Tallinn, Paris, Tokyo…",            // 2 = Capital City
        "e.g. Europe, Asia, Africa…",             // 3 = Region
        "e.g. Northern Europe, Caribbean…",       // 4 = Subregion
        "e.g. Spanish, French, Arabic…",          // 5 = Language
        "e.g. USD, EUR, cop…",                    // 6 = Currency
        "e.g. DE, DEU, 276, co…",                 // 7 = Country Code
        "e.g. Alemania, Allemagne…"               // 8 = Translation
    };

    // Called when the user picks a different option in the "Search by:" dropdown.
    // Updates the text box placeholder and clears any previous text.
    private void CboSearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = cboSearchType.SelectedIndex;
        if (idx >= 0 && idx < PlaceholderHints.Length)
            txtCountryName.PlaceholderText = PlaceholderHints[idx];
        txtCountryName.Clear();
        txtCountryName.Focus(); // move the cursor into the text box automatically
    }

    // ═══════════════════════════════════════════════════════════════════════
    // LIST SELECTION — show details when user clicks a country name
    // ═══════════════════════════════════════════════════════════════════════

    // Called whenever the selected item in the country list changes.
    // Looks up the Country object and fills in the detail panel + loads the flag.
    private async void LstMatches_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = lstMatches.SelectedIndex;

        // -1 means nothing is selected (e.g. list was just cleared)
        if (idx < 0 || idx >= _currentResults.Count) return;

        var country = _currentResults[idx]; // get the full country object
        PopulateDetails(country);            // fill the right panel with text
        await LoadFlagAsync(country.Flags.Png); // download and show the flag image
    }

    // ═══════════════════════════════════════════════════════════════════════
    // POPULATE DETAILS — fill the right panel with a country's information
    // ═══════════════════════════════════════════════════════════════════════

    // Takes a Country object and updates every label in the detail table.
    // Called from LstMatches_SelectedIndexChanged each time a new country is clicked.
    private void PopulateDetails(Country c)
    {
        // Update the heading at the top with the country's common name
        lblDetailHeading.Text      = c.Name.Common;
        lblDetailHeading.Font      = new Font("Segoe UI", 14f, FontStyle.Bold);
        lblDetailHeading.ForeColor = Color.FromArgb(30, 80, 162); // dark blue

        // Fill each value label from the Country object's properties.
        // These properties are defined in CountryModel.cs.
        lblOfficialNameVal.Text = c.Name.Official;
        lblCapitalVal.Text      = c.CapitalDisplay;       // "N/A" if no capital
        lblRegionVal.Text       = c.Region;
        lblSubregionVal.Text    = string.IsNullOrWhiteSpace(c.Subregion) ? "N/A" : c.Subregion;
        lblPopulationVal.Text   = c.Population.ToString("N0"); // "N0" = thousand separators e.g. 83,200,000
        lblAreaVal.Text         = c.Area > 0 ? c.Area.ToString("N0") : "N/A";
        lblLanguagesVal.Text    = c.LanguagesDisplay;
        lblCurrenciesVal.Text   = c.CurrenciesDisplay;
        lblTimezonesVal.Text    = c.TimezonesDisplay;
        lblContinentsVal.Text   = c.ContinentsDisplay;

        // Set up the Google Maps link (or show "N/A" if not available)
        if (!string.IsNullOrWhiteSpace(c.Maps.GoogleMaps))
        {
            lnkMaps.Text = "Open in Google Maps";
            lnkMaps.Tag  = c.Maps.GoogleMaps; // store the URL in Tag for use when clicked
        }
        else
        {
            lnkMaps.Text = "N/A";
            lnkMaps.Tag  = null;
        }

        tblDetails.Visible = true; // show the table (it was hidden before any selection)
    }

    // ═══════════════════════════════════════════════════════════════════════
    // FLAG LOADING — download and display the country flag image
    // ═══════════════════════════════════════════════════════════════════════

    // Downloads the flag PNG from the URL provided by the API and shows it in picFlag.
    // Runs asynchronously so the UI doesn't freeze while the image downloads.
    private async Task LoadFlagAsync(string flagUrl)
    {
        picFlag.Image = null; // clear the previous flag immediately
        if (string.IsNullOrWhiteSpace(flagUrl)) return;

        try
        {
            // Download the image as raw bytes
            byte[] data = await _countryService.DownloadFlagAsync(flagUrl);

            // MemoryStream lets us treat the byte array as a file stream
            // so Image.FromStream() can decode it into a Bitmap
            using var ms = new MemoryStream(data);
            picFlag.Image = Image.FromStream(ms);
        }
        catch
        {
            // Flag loading is optional — if it fails, we just leave the box empty.
            // We don't show an error message because the rest of the data is still useful.
            picFlag.Image = null;
        }
    }

    // ═══════════════════════════════════════════════════════════════════════
    // CLEAR BUTTON — reset the form back to its initial state
    // ═══════════════════════════════════════════════════════════════════════

    // Resets everything: clears the text box, the results list, the detail panel,
    // and the flag image, then puts the cursor back in the search box.
    private void BtnClear_Click(object sender, EventArgs e)
    {
        txtCountryName.Clear();
        lstMatches.Items.Clear();
        _currentResults.Clear();
        ClearDetails();
        picFlag.Image = null;
        ShowStatus("Ready \u2014 enter a country name and press Search.");
        txtCountryName.Focus();
    }

    // Resets the detail panel to its "nothing selected" state.
    // Called at the start of each new search and when Clear is clicked.
    private void ClearDetails()
    {
        lblDetailHeading.Text      = "Select a country from the list to see details.";
        lblDetailHeading.Font      = new Font("Segoe UI", 11f, FontStyle.Italic);
        lblDetailHeading.ForeColor = Color.Gray;
        tblDetails.Visible         = false; // hide the table until a country is selected

        // Clear all value labels
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

    // ═══════════════════════════════════════════════════════════════════════
    // GOOGLE MAPS LINK — open the country in the browser
    // ═══════════════════════════════════════════════════════════════════════

    // Called when the user clicks the "Open in Google Maps" link.
    // We stored the URL in lnkMaps.Tag when PopulateDetails() ran.
    private void LnkMaps_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (lnkMaps.Tag is string url && !string.IsNullOrWhiteSpace(url))
        {
            // Security check: only open http/https URLs to prevent malicious links.
            // Uri.TryCreate parses the string into a Uri object so we can check its scheme.
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeHttp))
            {
                // UseShellExecute = true tells Windows to open the URL in the default browser
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
        }
    }

    // ═══════════════════════════════════════════════════════════════════════
    // HELPER METHODS — small utilities used by other methods above
    // ═══════════════════════════════════════════════════════════════════════

    // Form_Load fires when the form finishes loading but before it is shown.
    // Nothing to do here currently, but the designer wires it up and it's
    // good practice to keep it in case we need it later.
    private void Form1_Load(object sender, EventArgs e) { }

    // Form_Shown fires the first time the form becomes visible on screen.
    // We set the splitter position here because the split container's actual
    // pixel size is only known after the form is shown (not during Load).
    private void Form1_Shown(object sender, EventArgs e)
    {
        splitMain.Panel1MinSize = 230; // left panel cannot be narrower than 230px
        splitMain.Panel2MinSize = 380; // right panel cannot be narrower than 380px
        try { splitMain.SplitterDistance = 290; } // start with a 290px left panel
        catch { /* if 290 is invalid for the current size, use the default */ }
    }

    // Enables or disables the search controls while an API call is in progress.
    // "searching = true"  → disable controls, show wait cursor, show "Searching…"
    // "searching = false" → re-enable everything
    private void SetSearching(bool searching)
    {
        btnSearch.Enabled      = !searching;
        txtCountryName.Enabled = !searching;
        btnClear.Enabled       = !searching;
        this.Cursor = searching ? Cursors.WaitCursor : Cursors.Default;
        if (searching) ShowStatus("Searching\u2026");
    }

    // Updates the status bar text at the bottom of the form.
    // isError = true turns the text red so the user notices it is an error.
    private void ShowStatus(string message, bool isError = false)
    {
        tsslStatus.ForeColor = isError ? Color.Firebrick : SystemColors.ControlText;
        tsslStatus.Text = message;
    }

    // Called automatically when the form is being closed.
    // We dispose the CountryService to free the HttpClient it holds.
    // Always call base.OnFormClosing() so the form actually closes.
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _countryService.Dispose();
        base.OnFormClosing(e);
    }
}
