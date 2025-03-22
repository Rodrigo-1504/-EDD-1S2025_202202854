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
        //avl.agregarRepuestos(new Repuestos(50, "VENTANA", "ROTA", 20.00));
        //avl.agregarRepuestos(new Repuestos(25, "VENTANA", "ROTA", 20.00));
        //avl.agregarRepuestos(new Repuestos(40, "VENTANA", "ROTA", 20.00));

        Console.WriteLine("RECORRIDO ENORDEN: ");
        avl.RecorridoEnOrden();

    }
}