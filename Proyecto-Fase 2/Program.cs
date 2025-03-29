using System;
using Gtk;
using Structures;

namespace Interfaces2
{
    public static class Program
    {
        [STAThread] // Atributo importante para aplicaciones GTK en Windows
        public static void Main(string[] args)
        {
            try
            {
                InitializeApplication();
                CreateAdminUser();
                RunApplication();
            }
            catch (Exception ex)
            {
                HandleCriticalError(ex);
            }
        }

        private static void InitializeApplication()
        {
            Application.Init();
            
            // Configurar el manejador de excepciones no controladas
            GLib.ExceptionManager.UnhandledException += OnUnhandledException;
            
            var loginWindow = Login.Instance;
            loginWindow.DeleteEvent += OnWindowDelete;
            loginWindow.ShowAll();
        }

        private static void CreateAdminUser()
        {
            try
            {
                var adminUser = new Usuarios(
                    ID: 0,
                    names: "Admin",
                    lastnames: "Admin",
                    mail: "admin@usac.com",
                    password: "admin123",
                    edad: 20);

                ListaSimple.Instance.AgregarUsuarios(adminUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear usuario admin: {ex.Message}");
                // Considerar mostrar un mensaje al usuario o registrar en logs
            }
        }

        private static void RunApplication()
        {
            Application.Run();
        }

        private static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            try
            {
                ((Window)sender).Hide();
                args.RetVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cerrar ventana: {ex.Message}");
            }
        }

        private static void OnUnhandledException(GLib.UnhandledExceptionArgs args)
        {
            var exception = (Exception)args.ExceptionObject;
            Console.WriteLine($"Excepción no controlada: {exception}");
            HandleCriticalError(exception);
            
            // Opcional: Forzar cierre de la aplicación para errores graves
            if (args.IsTerminating)
            {
                Environment.Exit(1);
            }
        }

        private static void HandleCriticalError(Exception ex)
        {
            Console.WriteLine($"Error crítico: {ex}");
            
            // Mostrar mensaje de error al usuario
            try
            {
                Application.Init(); // Asegurar que GTK está inicializado
                using (var dialog = new MessageDialog(
                    null,
                    DialogFlags.Modal,
                    MessageType.Error,
                    ButtonsType.Ok,
                    $"Error crítico: {ex.Message}"))
                {
                    dialog.Run();
                }
            }
            catch
            {
                // Fallback si no se puede mostrar el diálogo
                Console.WriteLine("No se pudo mostrar el diálogo de error");
            }
            
            Environment.Exit(1); // Salir con código de error
        }
    }
}