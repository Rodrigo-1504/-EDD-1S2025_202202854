using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe class Pila
    {
        //Instanciar
        private static Pila _instance;

        public static Pila Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Pila();
                }
                return _instance;
            }
        }

        //Características de la Pila
        private NodoPila* tope;

        public Pila()
        {
            tope = null;
        }

        ~Pila()
        {
            while(tope != null)
            {
                NodoPila* temp = tope;
                tope = tope->abajo;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }

        //LIFO
        public void agregarFactura(Facturas bill)
        {
            /*AL MOMENTO DE INGRESAR UN NODO A UNA PILA, SIEMPRE EL ULTIMO NODO A INGRESAR SERÁ EL TOPE DE LA PILA*/
            NodoPila* newBill = (NodoPila*)Marshal.AllocHGlobal(sizeof(NodoPila));
            newBill->factura = bill;
            newBill->abajo = tope;
            tope = newBill;
        }

        public Facturas eliminarFactura()
        {
            if(tope == null) return null;

            NodoPila* temp = tope;
            Facturas factura = temp->factura;

            tope = tope->abajo;

            Marshal.FreeHGlobal((IntPtr)temp);
            return factura;

        }

        public void imprimir()
        {
            NodoPila* temp = tope;
            while(temp != null)
            {
                Console.WriteLine($"ID: {temp->factura.id}, ID_Orden: {temp->factura.id_Orden}, Total: {temp->factura.total}");
                temp = temp->abajo;
            }
        }
        
        public string graphvizPila()
        {
            if(tope == null)
            {
                return "digraph G {\n\tnode[shape=record];\n\tNULL[label = \"{NULL}\"];\n}\n";
            }


            //Estructura inicial del graphviz
            string graphviz = "digraph {\n";
            graphviz += "\tnode[shape=record];\n";
            graphviz += "\tgraph[pencolor=transparent];\n";
            //graphviz += "\trankdir=LR;\n";
            graphviz += "\tsubgraph cluster_0{\n";
            graphviz += "\t\tlabel = \"Pila\";\n";

            NodoPila* temp = tope;
            int index = 0;


            //Creando los nodos
            while(temp != null)
            {
                graphviz += $"\t\t\tn{index} [label = \"{{ID : {temp->factura.id} \\n ID_Orden: {temp->factura.id_Orden} \\n Total: {temp->factura.total}}}\"];\n";
                temp = temp->abajo;
                index++;
            }

            //Conectar los nodos
            //Regresando al principio de la lista
            temp = tope;
            for(int i = 0; temp != null && temp->abajo != null; i++)
            {
                graphviz += $"\t\t\tn{i} -> n{i+1};\n";
                temp = temp->abajo;
            }

            graphviz += "\t\t}\n";
            graphviz += "}\n";
            return graphviz;
        }

    }
}