// =============================================================================
// Form1.Designer.cs — Visual Layout of the Application Window
// =============================================================================
// This file describes HOW the form looks: all buttons, labels, text boxes, etc.
// It is called a "Designer" file because Visual Studio normally generates it
// automatically when you drag-and-drop controls in the Design View.
//
// IMPORTANT RULES for this file (so Visual Studio Designer can open it):
//   1. Every control is created with a plain "new ControlType();" — no { } initializers
//   2. Every property is set on its own line: this.btn.Text = "Search";
//   3. SuspendLayout() must be called before configuring, ResumeLayout() after
//   4. No loops or conditions inside InitializeComponent()
// =============================================================================

namespace CountryInfoExplorer;

partial class Form1
{
    // Holds references to all components so they can be freed when the form closes.
    // Visual Studio requires this field to be named exactly "components".
    private System.ComponentModel.IContainer components = null;

    // Dispose() is automatically called when the form is destroyed.
    // It frees memory and system handles used by the controls.
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing); // always call the parent class Dispose too
    }

    #region Windows Form Designer generated code

    // InitializeComponent() is called once from the Form1 constructor.
    // It creates every control, sets all visual properties, and wires up events.
    // Think of it as "build the entire UI from scratch".
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        // ── Step 1: Create every control object ───────────────────────────
        // We just call "new" here. All properties are configured further below.
        // This pattern is REQUIRED by the Visual Studio WinForms Designer.

        this.lblTitle           = new System.Windows.Forms.Label();
        this.pnlSearch          = new System.Windows.Forms.Panel();
        this.lblSearchPrompt    = new System.Windows.Forms.Label();
        this.cboSearchType      = new System.Windows.Forms.ComboBox();
        this.txtCountryName     = new System.Windows.Forms.TextBox();
        this.btnSearch          = new System.Windows.Forms.Button();
        this.btnClear           = new System.Windows.Forms.Button();
        this.splitMain          = new System.Windows.Forms.SplitContainer();
        this.picFlag            = new System.Windows.Forms.PictureBox();
        this.lblMatchesHeader   = new System.Windows.Forms.Label();
        this.lstMatches         = new System.Windows.Forms.ListBox();
        this.pnlDetails         = new System.Windows.Forms.Panel();
        this.lblDetailHeading   = new System.Windows.Forms.Label();
        this.tblDetails         = new System.Windows.Forms.TableLayoutPanel();

        // Caption labels — the LEFT column of the detail table (bold field names).
        // They are always visible and never change content.
        this.lblCapOfficialName = new System.Windows.Forms.Label();
        this.lblCapCapital      = new System.Windows.Forms.Label();
        this.lblCapRegion       = new System.Windows.Forms.Label();
        this.lblCapSubregion    = new System.Windows.Forms.Label();
        this.lblCapPopulation   = new System.Windows.Forms.Label();
        this.lblCapArea         = new System.Windows.Forms.Label();
        this.lblCapLanguages    = new System.Windows.Forms.Label();
        this.lblCapCurrencies   = new System.Windows.Forms.Label();
        this.lblCapTimezones    = new System.Windows.Forms.Label();
        this.lblCapContinents   = new System.Windows.Forms.Label();
        this.lblCapMaps         = new System.Windows.Forms.Label();

        // Value labels — the RIGHT column of the detail table (data from the API).
        // These are filled in by PopulateDetails() when a country is selected.
        this.lblOfficialNameVal = new System.Windows.Forms.Label();
        this.lblCapitalVal      = new System.Windows.Forms.Label();
        this.lblRegionVal       = new System.Windows.Forms.Label();
        this.lblSubregionVal    = new System.Windows.Forms.Label();
        this.lblPopulationVal   = new System.Windows.Forms.Label();
        this.lblAreaVal         = new System.Windows.Forms.Label();
        this.lblLanguagesVal    = new System.Windows.Forms.Label();
        this.lblCurrenciesVal   = new System.Windows.Forms.Label();
        this.lblTimezonesVal    = new System.Windows.Forms.Label();
        this.lblContinentsVal   = new System.Windows.Forms.Label();
        this.lnkMaps            = new System.Windows.Forms.LinkLabel();

        // Status strip — the thin bar at the bottom showing messages like "Found 5 results"
        this.statusStrip        = new System.Windows.Forms.StatusStrip();
        this.tsslStatus         = new System.Windows.Forms.ToolStripStatusLabel();

        // ── Step 2: Suspend layout on all containers ───────────────────────
        // SuspendLayout() tells Windows: "don't redraw yet, I'm still configuring."
        // Without this, every property change would cause a screen repaint (slow & flickery).
        // We call ResumeLayout() at the very end to paint everything at once.
        ((System.ComponentModel.ISupportInitialize)this.splitMain).BeginInit();
        this.splitMain.Panel1.SuspendLayout();
        this.splitMain.Panel2.SuspendLayout();
        this.splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.picFlag).BeginInit();
        this.pnlSearch.SuspendLayout();
        this.pnlDetails.SuspendLayout();
        this.tblDetails.SuspendLayout();
        this.statusStrip.SuspendLayout();
        this.SuspendLayout();

        // ══════════════════════════════════════════════════════════════════
        // Step 3: Configure each control's properties
        // ══════════════════════════════════════════════════════════════════

        // ── Title Label ────────────────────────────────────────────────────
        // The blue banner at the very top of the form showing the app name.
        this.lblTitle.Text      = "  Country Information Explorer";
        this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 16f, System.Drawing.FontStyle.Bold);
        this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 80, 162);   // dark blue text
        this.lblTitle.AutoSize  = false;
        this.lblTitle.Height    = 48;
        this.lblTitle.Dock      = System.Windows.Forms.DockStyle.Top;           // stretches to full width
        this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.lblTitle.BackColor = System.Drawing.Color.White;
        this.lblTitle.Padding   = new System.Windows.Forms.Padding(16, 0, 0, 0);

        // ── Search Panel ────────────────────────────────────────────────────
        // A white horizontal bar below the title. It holds all the search controls.
        this.pnlSearch.Height    = 52;
        this.pnlSearch.Dock      = System.Windows.Forms.DockStyle.Top;
        this.pnlSearch.BackColor = System.Drawing.Color.White;
        this.pnlSearch.Padding   = new System.Windows.Forms.Padding(16, 8, 16, 8);

        // ── "Search by:" label ──────────────────────────────────────────────
        // A small bold label telling the user what the dropdown is for.
        this.lblSearchPrompt.Text     = "Search by:";
        this.lblSearchPrompt.Font     = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblSearchPrompt.AutoSize = true;
        this.lblSearchPrompt.Location = new System.Drawing.Point(16, 15);

        // ── Search Type Dropdown (ComboBox) ─────────────────────────────────
        // Lets the user choose HOW to search: by Name, Capital, Language, etc.
        // DropDownList means the user can only SELECT an option, not type in it.
        this.cboSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboSearchType.Width         = 155;
        this.cboSearchType.Height        = 28;
        this.cboSearchType.Font          = new System.Drawing.Font("Segoe UI", 9.5f);
        this.cboSearchType.Location      = new System.Drawing.Point(92, 11);
        // Fill the dropdown with all available search options
        this.cboSearchType.Items.AddRange(new object[]
        {
            "Name", "Full Name", "Capital City", "Region",
            "Subregion", "Language", "Currency", "Country Code", "Translation"
        });
        this.cboSearchType.SelectedIndex = 0; // default: search by "Name"

        // ── Search Text Box ──────────────────────────────────────────────────
        // The input field where the user types a country name (or capital, etc.)
        // PlaceholderText is the grey hint that disappears when the user starts typing.
        this.txtCountryName.PlaceholderText = "e.g. Germany, Brazil, Japan…";
        this.txtCountryName.Width           = 310;
        this.txtCountryName.Height          = 28;
        this.txtCountryName.Font            = new System.Drawing.Font("Segoe UI", 10f);
        this.txtCountryName.Location        = new System.Drawing.Point(256, 11);

        // ── Search Button ────────────────────────────────────────────────────
        // The blue "Search" button. Clicking it (or pressing Enter) runs the search.
        this.btnSearch.Text      = "Search";
        this.btnSearch.Width     = 90;
        this.btnSearch.Height    = 28;
        this.btnSearch.Location  = new System.Drawing.Point(574, 11);
        this.btnSearch.BackColor = System.Drawing.Color.FromArgb(30, 80, 162);  // blue background
        this.btnSearch.ForeColor = System.Drawing.Color.White;
        this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSearch.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.btnSearch.Cursor    = System.Windows.Forms.Cursors.Hand;           // pointer cursor on hover
        this.btnSearch.FlatAppearance.BorderSize = 0;                           // remove the default border

        // ── Clear Button ─────────────────────────────────────────────────────
        // The grey "Clear" button. Clicking it resets the form to its initial state.
        this.btnClear.Text      = "Clear";
        this.btnClear.Width     = 75;
        this.btnClear.Height    = 28;
        this.btnClear.Location  = new System.Drawing.Point(672, 11);
        this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnClear.Cursor    = System.Windows.Forms.Cursors.Hand;
        this.btnClear.BackColor = System.Drawing.Color.FromArgb(200, 210, 220); // light grey
        this.btnClear.FlatAppearance.BorderSize = 0;

        // ── Flag Picture Box ─────────────────────────────────────────────────
        // Displays the country's flag image at the top of the left panel.
        // The image is downloaded from the REST Countries API and shown here.
        // SizeMode.Zoom scales the image to fit the box without cropping.
        this.picFlag.Height      = 170;
        this.picFlag.Dock        = System.Windows.Forms.DockStyle.Top;
        this.picFlag.SizeMode    = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.picFlag.BackColor   = System.Drawing.Color.FromArgb(240, 244, 248);
        this.picFlag.BorderStyle = System.Windows.Forms.BorderStyle.None;

        // ── "RESULTS" Header Label ───────────────────────────────────────────
        // A tiny header label above the country list saying "RESULTS".
        this.lblMatchesHeader.Text      = "  RESULTS";
        this.lblMatchesHeader.Height    = 26;
        this.lblMatchesHeader.Dock      = System.Windows.Forms.DockStyle.Top;
        this.lblMatchesHeader.Font      = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold);
        this.lblMatchesHeader.ForeColor = System.Drawing.Color.DimGray;
        this.lblMatchesHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.lblMatchesHeader.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);

        // ── Results List Box ─────────────────────────────────────────────────
        // Shows the matching country names after a search.
        // Clicking a country here populates the detail panel on the right.
        this.lstMatches.Dock        = System.Windows.Forms.DockStyle.Fill;
        this.lstMatches.Font        = new System.Drawing.Font("Segoe UI", 10f);
        this.lstMatches.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.lstMatches.BackColor   = System.Drawing.Color.White;
        this.lstMatches.ItemHeight  = 26;

        // ── Detail Heading Label ─────────────────────────────────────────────
        // Shows the selected country's name in large bold text in the right panel.
        // Before any selection it shows a grey placeholder message.
        this.lblDetailHeading.Text      = "Select a country from the list to see details.";
        this.lblDetailHeading.Font      = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Italic);
        this.lblDetailHeading.ForeColor = System.Drawing.Color.Gray;
        this.lblDetailHeading.Dock      = System.Windows.Forms.DockStyle.Top;
        this.lblDetailHeading.Height    = 36;
        this.lblDetailHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.lblDetailHeading.BackColor = System.Drawing.Color.White;

        // ── Details Table Layout Panel ───────────────────────────────────────
        // A TableLayoutPanel is like a two-column table: left column = field names,
        // right column = values loaded from the API.
        // It is hidden (Visible = false) until a country is actually selected.
        this.tblDetails.Dock            = System.Windows.Forms.DockStyle.Top;
        this.tblDetails.AutoSize        = true;
        this.tblDetails.ColumnCount     = 2;
        this.tblDetails.RowCount        = 11;
        this.tblDetails.BackColor       = System.Drawing.Color.White;
        this.tblDetails.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
        this.tblDetails.Visible         = false;
        // Left column: fixed width of 145px for the bold field name labels
        this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
            System.Windows.Forms.SizeType.Absolute, 145F));
        // Right column: takes all remaining width for the value labels
        this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
            System.Windows.Forms.SizeType.Percent, 100F));
        // 11 rows, each auto-sized to fit the content inside
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));

        // ── Caption Labels (left column) ─────────────────────────────────────
        // These are the bold labels like "Official Name:", "Capital:", etc.
        // Their text never changes — they are just column headers for each row.
        this.lblCapOfficialName.Text      = "Official Name:";
        this.lblCapOfficialName.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapOfficialName.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapOfficialName.AutoSize  = false;
        this.lblCapOfficialName.Width     = 140;
        this.lblCapOfficialName.Height    = 28;
        this.lblCapOfficialName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapCapital.Text      = "Capital:";
        this.lblCapCapital.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapCapital.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapCapital.AutoSize  = false;
        this.lblCapCapital.Width     = 140;
        this.lblCapCapital.Height    = 28;
        this.lblCapCapital.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapRegion.Text      = "Region:";
        this.lblCapRegion.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapRegion.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapRegion.AutoSize  = false;
        this.lblCapRegion.Width     = 140;
        this.lblCapRegion.Height    = 28;
        this.lblCapRegion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapSubregion.Text      = "Subregion:";
        this.lblCapSubregion.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapSubregion.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapSubregion.AutoSize  = false;
        this.lblCapSubregion.Width     = 140;
        this.lblCapSubregion.Height    = 28;
        this.lblCapSubregion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapPopulation.Text      = "Population:";
        this.lblCapPopulation.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapPopulation.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapPopulation.AutoSize  = false;
        this.lblCapPopulation.Width     = 140;
        this.lblCapPopulation.Height    = 28;
        this.lblCapPopulation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapArea.Text      = "Area (km\u00b2):";
        this.lblCapArea.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapArea.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapArea.AutoSize  = false;
        this.lblCapArea.Width     = 140;
        this.lblCapArea.Height    = 28;
        this.lblCapArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapLanguages.Text      = "Languages:";
        this.lblCapLanguages.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapLanguages.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapLanguages.AutoSize  = false;
        this.lblCapLanguages.Width     = 140;
        this.lblCapLanguages.Height    = 28;
        this.lblCapLanguages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapCurrencies.Text      = "Currencies:";
        this.lblCapCurrencies.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapCurrencies.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapCurrencies.AutoSize  = false;
        this.lblCapCurrencies.Width     = 140;
        this.lblCapCurrencies.Height    = 28;
        this.lblCapCurrencies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapTimezones.Text      = "Timezones:";
        this.lblCapTimezones.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapTimezones.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapTimezones.AutoSize  = false;
        this.lblCapTimezones.Width     = 140;
        this.lblCapTimezones.Height    = 28;
        this.lblCapTimezones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapContinents.Text      = "Continents:";
        this.lblCapContinents.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapContinents.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapContinents.AutoSize  = false;
        this.lblCapContinents.Width     = 140;
        this.lblCapContinents.Height    = 28;
        this.lblCapContinents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.lblCapMaps.Text      = "Google Maps:";
        this.lblCapMaps.Font      = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
        this.lblCapMaps.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
        this.lblCapMaps.AutoSize  = false;
        this.lblCapMaps.Width     = 140;
        this.lblCapMaps.Height    = 28;
        this.lblCapMaps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        // ── Value Labels (right column) ──────────────────────────────────────
        // These labels display the actual data from the API.
        // They start empty and are filled in by PopulateDetails() in Form1.cs.
        // MaximumSize prevents very long text from stretching off screen.
        this.lblOfficialNameVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblOfficialNameVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblOfficialNameVal.AutoSize    = true;
        this.lblOfficialNameVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblOfficialNameVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        this.lblCapitalVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblCapitalVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblCapitalVal.AutoSize    = true;
        this.lblCapitalVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblCapitalVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        this.lblRegionVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblRegionVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblRegionVal.AutoSize    = true;
        this.lblRegionVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblRegionVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        this.lblSubregionVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblSubregionVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblSubregionVal.AutoSize    = true;
        this.lblSubregionVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblSubregionVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        this.lblPopulationVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblPopulationVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblPopulationVal.AutoSize    = true;
        this.lblPopulationVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblPopulationVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        this.lblAreaVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblAreaVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblAreaVal.AutoSize    = true;
        this.lblAreaVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblAreaVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        this.lblLanguagesVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblLanguagesVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblLanguagesVal.AutoSize    = true;
        this.lblLanguagesVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblLanguagesVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        this.lblCurrenciesVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblCurrenciesVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblCurrenciesVal.AutoSize    = true;
        this.lblCurrenciesVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblCurrenciesVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        this.lblTimezonesVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblTimezonesVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblTimezonesVal.AutoSize    = true;
        this.lblTimezonesVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblTimezonesVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        this.lblContinentsVal.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lblContinentsVal.ForeColor   = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lblContinentsVal.AutoSize    = true;
        this.lblContinentsVal.MaximumSize = new System.Drawing.Size(500, 0);
        this.lblContinentsVal.Margin      = new System.Windows.Forms.Padding(0, 4, 0, 4);

        // ── Google Maps Link Label ───────────────────────────────────────────
        // LinkLabel looks like a hyperlink. Clicking it opens Google Maps in the browser.
        // We use LinkLabel instead of a plain Label specifically for the blue underlined look.
        this.lnkMaps.Font      = new System.Drawing.Font("Segoe UI", 9.5f);
        this.lnkMaps.ForeColor = System.Drawing.Color.FromArgb(25, 25, 25);
        this.lnkMaps.AutoSize  = true;
        this.lnkMaps.Margin    = new System.Windows.Forms.Padding(0, 4, 0, 4);

        // ── Status Strip ─────────────────────────────────────────────────────
        // The thin bar at the very bottom of the form.
        // Shows messages to the user like "Found 5 results" or error text.
        this.statusStrip.BackColor = System.Drawing.Color.White;
        this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
        {
            this.tsslStatus
        });
        // The label item inside the status strip
        this.tsslStatus.Name = "tsslStatus";
        this.tsslStatus.Text = "Ready \u2014 enter a country name and press Search.";

        // ── Split Container ──────────────────────────────────────────────────
        // Divides the form content area into two resizable panels:
        //   Panel1 (left):  flag image + country list
        //   Panel2 (right): country detail table
        // The user can drag the divider left/right to resize both panels.
        this.splitMain.Dock          = System.Windows.Forms.DockStyle.Fill;
        this.splitMain.SplitterWidth = 6;
        this.splitMain.BackColor     = System.Drawing.Color.FromArgb(210, 220, 235); // divider color

        // ── Details Panel ────────────────────────────────────────────────────
        // A scrollable panel on the right side of the splitter.
        // AutoScroll = true means a scrollbar appears automatically if content is too tall.
        this.pnlDetails.Dock       = System.Windows.Forms.DockStyle.Fill;
        this.pnlDetails.AutoScroll = true;
        this.pnlDetails.BackColor  = System.Drawing.Color.White;
        this.pnlDetails.Padding    = new System.Windows.Forms.Padding(20, 16, 20, 16);

        // ══════════════════════════════════════════════════════════════════
        // Step 4: Build the control tree
        // Add each control to its parent container (like nesting HTML elements).
        // ══════════════════════════════════════════════════════════════════

        // Search panel contains: label, dropdown, text box, two buttons
        this.pnlSearch.Controls.Add(this.btnClear);
        this.pnlSearch.Controls.Add(this.btnSearch);
        this.pnlSearch.Controls.Add(this.txtCountryName);
        this.pnlSearch.Controls.Add(this.cboSearchType);
        this.pnlSearch.Controls.Add(this.lblSearchPrompt);

        // Detail table: add each caption+value pair, row by row
        // Controls.Add(control, column, row) — column 0 = left, column 1 = right
        this.tblDetails.Controls.Add(this.lblCapOfficialName, 0, 0);
        this.tblDetails.Controls.Add(this.lblOfficialNameVal, 1, 0);
        this.tblDetails.Controls.Add(this.lblCapCapital,      0, 1);
        this.tblDetails.Controls.Add(this.lblCapitalVal,      1, 1);
        this.tblDetails.Controls.Add(this.lblCapRegion,       0, 2);
        this.tblDetails.Controls.Add(this.lblRegionVal,       1, 2);
        this.tblDetails.Controls.Add(this.lblCapSubregion,    0, 3);
        this.tblDetails.Controls.Add(this.lblSubregionVal,    1, 3);
        this.tblDetails.Controls.Add(this.lblCapPopulation,   0, 4);
        this.tblDetails.Controls.Add(this.lblPopulationVal,   1, 4);
        this.tblDetails.Controls.Add(this.lblCapArea,         0, 5);
        this.tblDetails.Controls.Add(this.lblAreaVal,         1, 5);
        this.tblDetails.Controls.Add(this.lblCapLanguages,    0, 6);
        this.tblDetails.Controls.Add(this.lblLanguagesVal,    1, 6);
        this.tblDetails.Controls.Add(this.lblCapCurrencies,   0, 7);
        this.tblDetails.Controls.Add(this.lblCurrenciesVal,   1, 7);
        this.tblDetails.Controls.Add(this.lblCapTimezones,    0, 8);
        this.tblDetails.Controls.Add(this.lblTimezonesVal,    1, 8);
        this.tblDetails.Controls.Add(this.lblCapContinents,   0, 9);
        this.tblDetails.Controls.Add(this.lblContinentsVal,   1, 9);
        this.tblDetails.Controls.Add(this.lblCapMaps,         0, 10);
        this.tblDetails.Controls.Add(this.lnkMaps,            1, 10);

        // Details panel (right side): heading at top, then the table below
        // Note: with DockStyle.Top controls, the LAST one added ends up at the TOP visually
        this.pnlDetails.Controls.Add(this.tblDetails);
        this.pnlDetails.Controls.Add(this.lblDetailHeading);

        // Left panel of the splitter: flag on top, "RESULTS" header, then list fills the rest
        this.splitMain.Panel1.Controls.Add(this.lstMatches);        // fills remaining space
        this.splitMain.Panel1.Controls.Add(this.lblMatchesHeader);  // docks to top
        this.splitMain.Panel1.Controls.Add(this.picFlag);           // docks to top
        this.splitMain.Panel1.BackColor = System.Drawing.Color.White;

        // Right panel of the splitter: the detail panel
        this.splitMain.Panel2.Controls.Add(this.pnlDetails);

        // ── Step 5: Wire up event handlers ──────────────────────────────────
        // Connect user actions (clicks, key presses, selections) to the methods
        // in Form1.cs that handle them. The "new System.EventHandler(...)" syntax
        // is required by the Visual Studio Designer.
        this.cboSearchType.SelectedIndexChanged += new System.EventHandler(this.CboSearchType_SelectedIndexChanged);
        this.btnSearch.Click                    += new System.EventHandler(this.BtnSearch_Click);
        this.btnClear.Click                     += new System.EventHandler(this.BtnClear_Click);
        this.txtCountryName.KeyDown             += new System.Windows.Forms.KeyEventHandler(this.TxtCountryName_KeyDown);
        this.lstMatches.SelectedIndexChanged    += new System.EventHandler(this.LstMatches_SelectedIndexChanged);
        this.lnkMaps.LinkClicked                += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkMaps_LinkClicked);
        this.Load                               += new System.EventHandler(this.Form1_Load);
        this.Shown                              += new System.EventHandler(this.Form1_Shown);

        // ── Step 6: Add controls to the form itself ──────────────────────────
        // DockStyle.Fill must be added FIRST so it fills the space left by Docked.Top controls.
        // DockStyle.Top controls stack from bottom to top in the order they are added here.
        this.Controls.Add(this.splitMain);    // fills the remaining center area
        this.Controls.Add(this.statusStrip);  // docks to the bottom automatically
        this.Controls.Add(this.pnlSearch);    // docks to the top (search bar)
        this.Controls.Add(this.lblTitle);     // docks to the top (title banner — appears first)

        // ── Step 7: Set the form's own properties ────────────────────────────
        this.Text                = "Country Information Explorer";    // window title bar
        this.Size                = new System.Drawing.Size(1020, 700);
        this.MinimumSize         = new System.Drawing.Size(900, 600); // can't resize smaller
        this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.BackColor           = System.Drawing.Color.FromArgb(240, 244, 248);
        this.Font                = new System.Drawing.Font("Segoe UI", 9.5f);
        // AutoScaleMode.Font makes all controls scale correctly on high-DPI screens
        this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);

        // ── Step 8: Resume layout — draw everything now ──────────────────────
        // These calls match the SuspendLayout() calls from Step 2.
        // After ResumeLayout(), Windows renders the fully built form at once.
        this.tblDetails.ResumeLayout(false);
        this.tblDetails.PerformLayout();
        this.pnlDetails.ResumeLayout(false);
        this.pnlDetails.PerformLayout();
        this.splitMain.Panel1.ResumeLayout(false);
        this.splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.splitMain).EndInit();
        this.splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.picFlag).EndInit();
        this.pnlSearch.ResumeLayout(false);
        this.pnlSearch.PerformLayout();
        this.statusStrip.ResumeLayout(false);
        this.statusStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    // ── Control field declarations ────────────────────────────────────────────
    // Every control used in the form must be declared as a private field here.
    // Visual Studio requires them to be in the Designer file (not Form1.cs).
    private System.Windows.Forms.Label          lblTitle;
    private System.Windows.Forms.Panel          pnlSearch;
    private System.Windows.Forms.Label          lblSearchPrompt;
    private System.Windows.Forms.ComboBox       cboSearchType;
    private System.Windows.Forms.TextBox        txtCountryName;
    private System.Windows.Forms.Button         btnSearch;
    private System.Windows.Forms.Button         btnClear;
    private System.Windows.Forms.PictureBox     picFlag;
    private System.Windows.Forms.Label          lblMatchesHeader;
    private System.Windows.Forms.ListBox        lstMatches;
    private System.Windows.Forms.Panel          pnlDetails;
    private System.Windows.Forms.Label          lblDetailHeading;
    private System.Windows.Forms.TableLayoutPanel tblDetails;
    // Caption labels (left column — field names)
    private System.Windows.Forms.Label          lblCapOfficialName;
    private System.Windows.Forms.Label          lblCapCapital;
    private System.Windows.Forms.Label          lblCapRegion;
    private System.Windows.Forms.Label          lblCapSubregion;
    private System.Windows.Forms.Label          lblCapPopulation;
    private System.Windows.Forms.Label          lblCapArea;
    private System.Windows.Forms.Label          lblCapLanguages;
    private System.Windows.Forms.Label          lblCapCurrencies;
    private System.Windows.Forms.Label          lblCapTimezones;
    private System.Windows.Forms.Label          lblCapContinents;
    private System.Windows.Forms.Label          lblCapMaps;
    // Value labels (right column — data from the API)
    private System.Windows.Forms.Label          lblOfficialNameVal;
    private System.Windows.Forms.Label          lblCapitalVal;
    private System.Windows.Forms.Label          lblRegionVal;
    private System.Windows.Forms.Label          lblSubregionVal;
    private System.Windows.Forms.Label          lblPopulationVal;
    private System.Windows.Forms.Label          lblAreaVal;
    private System.Windows.Forms.Label          lblLanguagesVal;
    private System.Windows.Forms.Label          lblCurrenciesVal;
    private System.Windows.Forms.Label          lblTimezonesVal;
    private System.Windows.Forms.Label          lblContinentsVal;
    private System.Windows.Forms.LinkLabel      lnkMaps;
    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.StatusStrip    statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
}
