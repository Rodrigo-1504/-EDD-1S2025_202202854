using System;
using System.Runtime;
using Gtk;
using Structures;

class Program
{
    public static void Main(string[] args)
    {
        ArbolBST bst = ArbolBST.Instance;

        bst.agregarServicios(new Servicios(3, 1, 1, "Limpieza", 25.00));
        bst.agregarServicios(new Servicios(4, 1, 1, "Limpieza", 25.00));
        bst.agregarServicios(new Servicios(1, 1, 1, "Limpieza", 25.00));
        bst.agregarServicios(new Servicios(2, 1, 1, "Limpieza", 25.00));
        bst.agregarServicios(new Servicios(7, 1, 1, "Limpieza", 25.00));

        string dotBST = bst.graphvizBST();
        Dot_Png.Convertidor.generarArchivoDot("BST", dotBST);
        Dot_Png.Convertidor.ConvertirDot_a_Png("BST.dot");

    }
}