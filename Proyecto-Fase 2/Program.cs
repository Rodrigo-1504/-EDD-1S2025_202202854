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
    }

    public static void OnWindowDelete(object sender, DeleteEventArgs args)
    {
        ((Window)sender).Hide();
        args.RetVal = true;
    }
}