// =============================================================================
// Program.cs — Application Entry Point
// =============================================================================
// This is the very first file that runs when the application starts.
// It sets up global error handling and then launches the main form.
//
// Every Windows Forms application needs exactly one entry point like this.
// =============================================================================

namespace CountryInfoExplorer;

static class Program
{
    // [STAThread] is required by Windows Forms.
    // STA = Single-Threaded Apartment — Windows UI controls must run on an STA thread.
    [STAThread]
    static void Main()
    {
        // Catch any unhandled exceptions on the UI thread and show a message
        // instead of crashing silently. This is a safety net for unexpected bugs.
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (s, e) =>
            MessageBox.Show(e.Exception.ToString(), "Crash", MessageBoxButtons.OK, MessageBoxIcon.Error);

        // Catch fatal exceptions from non-UI threads (background threads, async code)
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            MessageBox.Show(e.ExceptionObject?.ToString(), "Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);

        // ApplicationConfiguration.Initialize() applies settings from the project config:
        // high-DPI support, visual styles, and other modern Windows look-and-feel settings.
        ApplicationConfiguration.Initialize();

        // Create the main form and start the Windows message loop.
        // Application.Run() blocks here until the user closes the form.
        Application.Run(new Form1());
    }
}