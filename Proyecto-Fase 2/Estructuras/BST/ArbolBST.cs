using System;
using System.Runtime.ExceptionServices;

namespace Structures
{
    public class ArbolBST
    {
        ListaDoble listaVehiculos = ListaDoble.Instance;
        ArbolAVL listaRepuestos = ArbolAVL.Instance;

        //INSTANCIAR
        private static ArbolBST _instance;
        public static ArbolBST Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ArbolBST();
                }
                return _instance;
            }
        }

        //CARACTERÍSTICAS
        private NodoBST raiz;

        public ArbolBST()
        {
            raiz = null;
        }

        //INSERTAR E INSERTAR RECURSIVAMENTE
        public void agregarServicios(Servicios servicio)
        {
            if(Buscar(servicio.id) != null)
            {
                Console.WriteLine("El id del servicio ya existe");
                return;
            }

            if(listaVehiculos.BuscarVehiculo(servicio.id_Vehiculo) == null)
            {
                Console.WriteLine("El vehiculo no existe");
                return;
            }

            if(listaRepuestos.Buscar(servicio.id_Repuesto) == null)
            {
                Console.WriteLine("El repuesto no existe");
                return;
            }

            raiz = insertarRecursivamente(raiz, servicio);         
        }

        private NodoBST insertarRecursivamente(NodoBST nodo, Servicios servicio)
        {
            if(nodo == null)
            {
                return new NodoBST(servicio);
            }
            
            //INSERTAR EL NODO
            if(servicio.id < nodo.servicios.id)
            {
                nodo.izquierda = insertarRecursivamente(nodo.izquierda, servicio);
            }
            else if(servicio.id > nodo.servicios.id)
            {
                nodo.derecha = insertarRecursivamente(nodo.derecha, servicio);
            }
            return nodo;
        }

        public NodoBST Buscar(int id)
        {
            return BuscarRecursivamente(raiz, id);
        }

        private NodoBST BuscarRecursivamente(NodoBST nodo, int id)
        {
            if(nodo == null) return null;
            
            if(id == nodo.servicios.id)
            {
                return nodo;
            }


            if(id < nodo.servicios.id)
            {
                return BuscarRecursivamente(nodo.izquierda, id);
            }
            
            return BuscarRecursivamente(nodo.derecha, id);
        }

        public void RecorridoPreOrden()
        {
            RecorridoPreOrdenRecursivo(raiz);
        }

        private void RecorridoPreOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                Console.WriteLine(nodo.servicios.id + " ");
                RecorridoPreOrdenRecursivo(nodo.izquierda);
                RecorridoPreOrdenRecursivo(nodo.derecha);
            }
        }

        public void RecorridoEnOrden()
        {
            RecorridoEnOrdenRecursivo(raiz);
        }

        private void RecorridoEnOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                RecorridoEnOrdenRecursivo(nodo.izquierda);
                Console.WriteLine(nodo.servicios.id + " ");
                RecorridoEnOrdenRecursivo(nodo.derecha);
            }
        }

        public void RecorridoPostOrden()
        {
            RecorridoPostOrdenRecursivo(raiz);
        }

        private void RecorridoPostOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                RecorridoPostOrdenRecursivo(nodo.izquierda);
                RecorridoPostOrdenRecursivo(nodo.derecha);
                Console.WriteLine(nodo.servicios.id + " ");
            }
        }


        public string graphvizBST()
        {
            if(raiz == null)
            {
                return "digraph G {\n\tnode[shape=record];\n\tNULL[label = \"{NULL}\"];\n}\n";
            }

            string graphviz = "";
            graphviz += "digraph AVL{\n";
            graphviz += "\tnode[shape=circle];\n";
            graphviz += "\tgraph[pencolor=transparent];\n";
            graphviz += "\tsubgraph cluster_0{\n";
            graphviz += "\t\tlabel = \"Arbol AVL\";\n";

            graphviz += graphvizBSTRecursivo(raiz);
            
            graphviz += "\t\t}\n";
            graphviz += "}\n";            
            return graphviz;
        }

        private string graphvizBSTRecursivo(NodoBST nodo)
        {
            string graphviz = "";

            if (nodo != null)
            {
                // Agregar la relación con el hijo izquierdo
                if (nodo.izquierda != null)
                {
                    graphviz += $"\t\"{nodo.servicios.id}\" -> \"{nodo.izquierda.servicios.id}\";\n";
                }

                // Agregar la relación con el hijo derecho
                if (nodo.derecha != null)
                {
                    graphviz += $"\t\"{nodo.servicios.id}\" -> \"{nodo.derecha.servicios.id}\";\n";
                }

                // Recorrer los subárboles
                graphviz += graphvizBSTRecursivo(nodo.izquierda);
                graphviz += graphvizBSTRecursivo(nodo.derecha);
            }

            return graphviz;
        }

    }
}
