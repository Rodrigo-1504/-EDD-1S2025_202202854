using System;
using System.Runtime;
using Gtk;
using Structures;

class Program
{
    public static void Main(string[] args)
    {
        ArbolAVL avl = ArbolAVL.Instance;

        avl.agregarRepuestos(new Repuestos(10, "VENTANA", "ROTA", 20.00));
        avl.agregarRepuestos(new Repuestos(30, "VENTANA", "ROTA", 20.00));
        avl.agregarRepuestos(new Repuestos(20, "VENTANA", "ROTA", 20.00));
        avl.agregarRepuestos(new Repuestos(50, "VENTANA", "ROTA", 20.00));
        avl.agregarRepuestos(new Repuestos(25, "VENTANA", "ROTA", 20.00));
        avl.agregarRepuestos(new Repuestos(40, "VENTANA", "ROTA", 20.00));

        //Console.WriteLine("RECORRIDO ENORDEN: ");
        //avl.RecorridoEnOrden();

        try
        {
            string dotAVL = avl.graphvizAVL();
            Dot_Png.Convertidor.generarArchivoDot("AVL", dotAVL);
            Dot_Png.Convertidor.ConvertirDot_a_Png("AVL.dot");
            Console.WriteLine("Se logro");
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }

        //Console.WriteLine("RECORRIDO POSTORDEN: ");
        //avl.RecorridoPostOrden();

    }
}