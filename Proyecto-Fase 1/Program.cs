using Gtk;
using Interfaces;
using List;
using Dot_Png;

class Program
{
    public static void Main(string[] args)
    {
      
        try
        {
            //Inicializar Gtk
            Application.Init();
            
            //Creacion de una ventana
            inicioSesion iniciar = inicioSesion.Instance;

            //Que todas las ventanas tengan la misma lista simple
            ListaSimple listaUsuarios = ListaSimple.Instance;

            //Agregar al admin
            listaUsuarios.AgregarUsuarios(new Usuarios(0, "Rodrigo Alejandro", "Tahuite Soria", "root@gmail.com", "root123"));

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
        ((Window)sender).Hide();
        args.RetVal = true;
    }
}
//Cualquier caso Ctrl + c para matar el programa