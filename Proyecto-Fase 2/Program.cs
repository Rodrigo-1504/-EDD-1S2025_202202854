using System;
using System.Runtime;
using Gtk;
using Structures;
using Interfaces2;

class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Application.Init();

            Login inicio = Login.Instance;

            ListaSimple ls = ListaSimple.Instance;
            ls.AgregarUsuarios(new Usuarios(0, "Admin", "Admin", "admin@usac.com", "admin123", 20));

            inicio.DeleteEvent += OnWindowDelete;

            inicio.ShowAll();

            Application.Run();
        }
        catch(Exception e)
        {
            Console.WriteLine("Error en Program.cs: " + e.Message);
        }

        /*ArbolB listaFacturas = ArbolB.Instance;
        listaFacturas.Insertar(new Facturas(1, 50, 30.0));
        listaFacturas.Insertar(new Facturas(2, 25, 40.0));
        listaFacturas.Insertar(new Facturas(3, 30, 50.0));
        listaFacturas.Insertar(new Facturas(4, 60, 60.0));
        listaFacturas.Insertar(new Facturas(5, 70, 70.0));

        listaFacturas.ImprimirEnOrden();
        */
    }

    public static void OnWindowDelete(object sender, DeleteEventArgs args)
    {
        ((Window)sender).Hide();
        args.RetVal = true;
    }
}