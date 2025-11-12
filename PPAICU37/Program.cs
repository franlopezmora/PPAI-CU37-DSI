namespace PPAICU37
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Inicializar la base de datos antes de iniciar la aplicación
            DbInit.EnsureDatabase();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new PantallaCerrarOrden());
        }
    }
}