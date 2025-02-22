using Gtk;
using Interfaces;

class Program
{
    public static void Main(string[] args)
    {
        
        try
        {
            //Inicializar Gtk
            Application.Init();

            //Creacion de una ventana
            inicioSesion iniciar = new inicioSesion();

            //Terminar el programa al cerrar la aplicación
            iniciar.DeleteEvent += OnWindowDelete;

            //Mostrar ventana
            iniciar .ShowAll();

            //Ejecutar en bucle el programa, osea que no se cierre
            Application.Run();
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
    
    //Funcion para terminar el programa
    public static void OnWindowDelete(object sender, DeleteEventArgs args)
    {
        Application.Quit();
        args.RetVal = true;
    }
}