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
        
        
        //TAREA 4
        /*
        ListaCircular listaRepuestos = ListaCircular.Instance;

        listaRepuestos.agregarRepuestos(new Repuestos(1, "Pastillas de Freno", "Cambio de las pastillas de freno", 20.5));
        listaRepuestos.agregarRepuestos(new Repuestos(2, "Bateria", "Cambio de la bateria del carro", 100.75));
        listaRepuestos.agregarRepuestos(new Repuestos(3, "Llantas", "Llantas nuevas, se hizo cambio de las 4 llantas", 575.80));
        
        listaRepuestos.imprimirListaCircular();
        string dotCircular = listaRepuestos.graphvizCircular();
        Dot_Png.Convertidor.generarArchivoDot("Lista Circular", dotCircular);
        Dot_Png.Convertidor.ConvertirDot_a_Png("Lista Circular.dot");
        */
    }
    
    //Funcion para terminar el programa
    public static void OnWindowDelete(object sender, DeleteEventArgs args)
    {
        ((Window)sender).Hide();
        args.RetVal = true;
    }
}

//Cualquier caso Ctrl + c para matar el programa