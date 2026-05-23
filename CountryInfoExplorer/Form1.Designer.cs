namespace CountryInfoExplorer;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        // ── Form ─────────────────────────────────────────────────────────
        this.Text = "Country Information Explorer";
        this.Size = new System.Drawing.Size(1020, 700);
        this.MinimumSize = new System.Drawing.Size(900, 600);
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
        this.Font = new System.Drawing.Font("Segoe UI", 9.5f);

        // ── Title label ──────────────────────────────────────────────────
        lblTitle = new System.Windows.Forms.Label
        {
            Text = "  Country Information Explorer",
            Font = new System.Drawing.Font("Segoe UI", 16f, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.FromArgb(30, 80, 162),
            AutoSize = false,
            Height = 48,
            Dock = System.Windows.Forms.DockStyle.Top,
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
            BackColor = System.Drawing.Color.White,
            Padding = new System.Windows.Forms.Padding(16, 0, 0, 0)
        };

        // ── Search panel ─────────────────────────────────────────────────
        pnlSearch = new System.Windows.Forms.Panel
        {
            Height = 52,
            Dock = System.Windows.Forms.DockStyle.Top,
            BackColor = System.Drawing.Color.White,
            Padding = new System.Windows.Forms.Padding(16, 8, 16, 8)
        };

        lblSearchPrompt = new System.Windows.Forms.Label
        {
            Text = "Search by:",
            Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold),
            AutoSize = true,
            Location = new System.Drawing.Point(16, 15)
        };

        cboSearchType = new System.Windows.Forms.ComboBox
        {
            DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList,
            Width = 155,
            Height = 28,
            Font = new System.Drawing.Font("Segoe UI", 9.5f),
            Location = new System.Drawing.Point(92, 11)
        };
        cboSearchType.Items.AddRange(new object[]
        {
            "Name", "Full Name", "Capital City", "Region",
            "Subregion", "Language", "Currency", "Country Code", "Translation"
        });
        cboSearchType.SelectedIndex = 0;

        txtCountryName = new System.Windows.Forms.TextBox
        {
            PlaceholderText = "e.g. Germany, Brazil, Japan…",
            Width = 310,
            Height = 28,
            Font = new System.Drawing.Font("Segoe UI", 10f),
            Location = new System.Drawing.Point(256, 11)
        };

        btnSearch = new System.Windows.Forms.Button
        {
            Text = "Search",
            Width = 90,
            Height = 28,
            Location = new System.Drawing.Point(574, 11),
            BackColor = System.Drawing.Color.FromArgb(30, 80, 162),
            ForeColor = System.Drawing.Color.White,
            FlatStyle = System.Windows.Forms.FlatStyle.Flat,
            Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold),
            Cursor = System.Windows.Forms.Cursors.Hand
        };
        btnSearch.FlatAppearance.BorderSize = 0;

        btnClear = new System.Windows.Forms.Button
        {
            Text = "Clear",
            Width = 75,
            Height = 28,
            Location = new System.Drawing.Point(672, 11),
            FlatStyle = System.Windows.Forms.FlatStyle.Flat,
            Cursor = System.Windows.Forms.Cursors.Hand,
            BackColor = System.Drawing.Color.FromArgb(200, 210, 220),
            FlatAppearance = { BorderSize = 0 }
        };

        pnlSearch.Controls.AddRange(new System.Windows.Forms.Control[]
            { lblSearchPrompt, cboSearchType, txtCountryName, btnSearch, btnClear });

        cboSearchType.SelectedIndexChanged += CboSearchType_SelectedIndexChanged;

        // ── Status bar ───────────────────────────────────────────────────
        statusStrip = new System.Windows.Forms.StatusStrip { BackColor = System.Drawing.Color.White };
        tsslStatus = new System.Windows.Forms.ToolStripStatusLabel
            { Text = "Ready — enter a country name and press Search." };
        statusStrip.Items.Add(tsslStatus);

        // ── Main split container ─────────────────────────────────────────
        splitMain = new System.Windows.Forms.SplitContainer
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            SplitterWidth = 6,
            BackColor = System.Drawing.Color.FromArgb(210, 220, 235)
        };

        // ── LEFT panel ───────────────────────────────────────────────────
        picFlag = new System.Windows.Forms.PictureBox
        {
            Height = 170,
            Dock = System.Windows.Forms.DockStyle.Top,
            SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
            BackColor = System.Drawing.Color.FromArgb(240, 244, 248),
            BorderStyle = System.Windows.Forms.BorderStyle.None
        };

        lblMatchesHeader = new System.Windows.Forms.Label
        {
            Text = "  RESULTS",
            Height = 26,
            Dock = System.Windows.Forms.DockStyle.Top,
            Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Bold),
            ForeColor = System.Drawing.Color.DimGray,
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
            BackColor = System.Drawing.Color.FromArgb(240, 244, 248)
        };

        lstMatches = new System.Windows.Forms.ListBox
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            Font = new System.Drawing.Font("Segoe UI", 10f),
            BorderStyle = System.Windows.Forms.BorderStyle.None,
            BackColor = System.Drawing.Color.White,
            ItemHeight = 26
        };

        splitMain.Panel1.Controls.Add(lstMatches);
        splitMain.Panel1.Controls.Add(lblMatchesHeader);
        splitMain.Panel1.Controls.Add(picFlag);
        splitMain.Panel1.BackColor = System.Drawing.Color.White;

        // ── RIGHT panel: detail table ─────────────────────────────────────
        pnlDetails = new System.Windows.Forms.Panel
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
            AutoScroll = true,
            BackColor = System.Drawing.Color.White,
            Padding = new System.Windows.Forms.Padding(20, 16, 20, 16)
        };

        // heading inside details
        lblDetailHeading = new System.Windows.Forms.Label
        {
            Text = "Select a country from the list to see details.",
            Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Italic),
            ForeColor = System.Drawing.Color.Gray,
            Dock = System.Windows.Forms.DockStyle.Top,
            Height = 36,
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
            BackColor = System.Drawing.Color.White
        };

        // TableLayoutPanel for field/value rows
        tblDetails = new System.Windows.Forms.TableLayoutPanel
        {
            Dock = System.Windows.Forms.DockStyle.Top,
            AutoSize = true,
            ColumnCount = 2,
            RowCount = 11,
            BackColor = System.Drawing.Color.White,
            CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None,
            Visible = false
        };
        tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
            System.Windows.Forms.SizeType.Absolute, 145));
        tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
            System.Windows.Forms.SizeType.Percent, 100));

        string[] captions = {
            "Official Name:", "Capital:", "Region:", "Subregion:",
            "Population:", "Area (km²):", "Languages:", "Currencies:",
            "Timezones:", "Continents:", "Google Maps:"
        };

        lblOfficialNameVal = new System.Windows.Forms.Label();
        lblCapitalVal      = new System.Windows.Forms.Label();
        lblRegionVal       = new System.Windows.Forms.Label();
        lblSubregionVal    = new System.Windows.Forms.Label();
        lblPopulationVal   = new System.Windows.Forms.Label();
        lblAreaVal         = new System.Windows.Forms.Label();
        lblLanguagesVal    = new System.Windows.Forms.Label();
        lblCurrenciesVal   = new System.Windows.Forms.Label();
        lblTimezonesVal    = new System.Windows.Forms.Label();
        lblContinentsVal   = new System.Windows.Forms.Label();
        lnkMaps            = new System.Windows.Forms.LinkLabel();

        System.Windows.Forms.Control[] valControls = {
            lblOfficialNameVal, lblCapitalVal, lblRegionVal, lblSubregionVal,
            lblPopulationVal,   lblAreaVal,    lblLanguagesVal, lblCurrenciesVal,
            lblTimezonesVal,    lblContinentsVal, lnkMaps
        };

        for (int i = 0; i < captions.Length; i++)
        {
            var cap = new System.Windows.Forms.Label
            {
                Text = captions[i],
                Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(70, 70, 70),
                AutoSize = false,
                Width = 140,
                Height = 28,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            var val = valControls[i];
            val.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            val.ForeColor = System.Drawing.Color.FromArgb(25, 25, 25);
            if (val is System.Windows.Forms.Label lbl)
            {
                lbl.AutoSize = true;
                lbl.MaximumSize = new System.Drawing.Size(500, 0);
                lbl.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            }
            else if (val is System.Windows.Forms.LinkLabel lnk)
            {
                lnk.AutoSize = true;
                lnk.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            }
            tblDetails.Controls.Add(cap, 0, i);
            tblDetails.Controls.Add(val, 1, i);
        }

        pnlDetails.Controls.Add(tblDetails);
        pnlDetails.Controls.Add(lblDetailHeading);
        splitMain.Panel2.Controls.Add(pnlDetails);

        // ── Assemble form ────────────────────────────────────────────────
        this.Controls.Add(splitMain);
        this.Controls.Add(statusStrip);
        this.Controls.Add(pnlSearch);
        this.Controls.Add(lblTitle);

        // ── Events ───────────────────────────────────────────────────────
        btnSearch.Click += BtnSearch_Click;
        btnClear.Click  += BtnClear_Click;
        txtCountryName.KeyDown += TxtCountryName_KeyDown;
        lstMatches.SelectedIndexChanged += LstMatches_SelectedIndexChanged;
        lnkMaps.LinkClicked += LnkMaps_LinkClicked;
        this.Load += Form1_Load;
        this.Shown += Form1_Shown;
    }

    #endregion

    // Controls
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.Panel pnlSearch;
    private System.Windows.Forms.Label lblSearchPrompt;
    private System.Windows.Forms.ComboBox cboSearchType;
    private System.Windows.Forms.TextBox txtCountryName;
    private System.Windows.Forms.Button btnSearch;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.PictureBox picFlag;
    private System.Windows.Forms.Label lblMatchesHeader;
    private System.Windows.Forms.ListBox lstMatches;
    private System.Windows.Forms.Panel pnlDetails;
    private System.Windows.Forms.Label lblDetailHeading;
    private System.Windows.Forms.TableLayoutPanel tblDetails;
    private System.Windows.Forms.Label lblOfficialNameVal;
    private System.Windows.Forms.Label lblCapitalVal;
    private System.Windows.Forms.Label lblRegionVal;
    private System.Windows.Forms.Label lblSubregionVal;
    private System.Windows.Forms.Label lblPopulationVal;
    private System.Windows.Forms.Label lblAreaVal;
    private System.Windows.Forms.Label lblLanguagesVal;
    private System.Windows.Forms.Label lblCurrenciesVal;
    private System.Windows.Forms.Label lblTimezonesVal;
    private System.Windows.Forms.Label lblContinentsVal;
    private System.Windows.Forms.LinkLabel lnkMaps;
    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
}
