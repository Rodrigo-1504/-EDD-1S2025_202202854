using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe class Cola
    {

        //Instanciar
        private static Cola _instance;

        public static Cola Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Cola();
                }
                return _instance;
            }
        }

        //PARA EL TOP 5
        public struct IdContador
        {
            public int id;
            public int count;
        }

        //Características
        private NodoCola* cabeza;
        private NodoCola* cola;

        //Consctructor
        public Cola()
        {
            cabeza = null;
            cola = null;
        }

        //Destructor
        ~Cola()
        {
            while(cabeza != null)
            {
                NodoCola* temp = cabeza;
                cabeza = cabeza->siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }

        public void agregarServicios(Servicios servicios)
        {

            /*LA COLA ES MUY SIMILAR A LA LISTA SIMPLE, CON LA CONDICIÓN QUE LA COLA TIENE UN NODO FINAL*/

            NodoCola* newService = (NodoCola*)Marshal.AllocHGlobal(sizeof(NodoCola));
            newService->services = servicios;
            newService->siguiente = null;


            if(cabeza == null)
            {
                //Agregamos el primer nodo, que será tanto la cabeza como la cola
                cabeza = newService;
                cola = newService;
            }
            else
            {   
                //Meteremos los datos siempre al final, luego de la cola
                cola->siguiente = newService;
                cola = newService;
            }
        }

        //FIFO
        public Servicios eliminarServicio()
        {
            if(cabeza == null) return null;

            //Eliminaremos el primer elemento de la cola, por lo que debemos mover la cabeza al siguiente nodo, y mostraremos el valor
            NodoCola* temp = cabeza;
            Servicios servicios = temp->services; //Mostrar los datos del nodo que vamos a eliminar

            cabeza = cabeza->siguiente;

            if(cabeza == null)
            {
                cola = null;
            }
            Marshal.FreeHGlobal((IntPtr)temp);
            return servicios;
        }

        public Servicios buscarServicio(int id)
        {
            NodoCola* temp = cabeza;
            while(temp != null)
            {
                if(temp->services.id == id)
                {
                    return temp->services;
                }
                temp = temp->siguiente;
            }
            return null;
        }

        public void ContarServicios(IdContador* contador, int* size, int id)
        {
            bool found = false;
            for(int i = 0; i < *size; i++)
            {
                if(contador[i].id == id)
                {
                    contador[i].count++;
                    found = true;
                    break;
                }
            }

            if(!found)
            {
                contador[*size].id = id;
                contador[*size].count = 1;
                (*size)++;
            }
        }

        public static void ordenarTop5(IdContador* contador, int size)
        {
            for(int i = 0; i < size - 1; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if(contador[i].count < contador[j].count)
                    {
                        IdContador temp = contador[i];
                        contador[i] = contador[j];
                        contador[j] = temp;
                    }
                }
            }
        }

        public void imprimir()
        {
            NodoCola* temp = cabeza;

            while(temp != null)
            {
                Console.WriteLine($"ID: {temp->services.id}, ID_Respuesto: {temp->services.id_Repuesto}, ID_Vehiculo: {temp->services.id_Vehiculo}, Detalles: {temp->services.detalles}, Costo: {temp->services.costo}");
                temp = temp->siguiente;
            }
        }

        public string graphvizCola()
        {
            if(cabeza == null)
            {
                return "digraph G {\n\tnode[shape=record];\n\tNULL[label = \"{NULL}\"];\n}\n";
            }


            //Estructura inicial del graphviz
            string graphviz = "digraph {\n";
            graphviz += "\tnode[shape=record];\n";
            graphviz += "\tgraph[pencolor=transparent];\n";
            graphviz += "\trankdir=LR;\n";
            graphviz += "\tsubgraph cluster_0{\n";
            graphviz += "\t\tlabel = \"Cola\";\n";

            NodoCola* temp = cabeza;
            int index = 0;


            //Creando los nodos
            while(temp != null)
            {
                graphviz += $"\t\t\tn{index} [label = \"{{Servicio: {index+1} \\n ID: {temp->services.id} \\n Id Repuesto: {temp->services.id_Repuesto} \\n Id Vehiculo: {temp->services.id_Vehiculo} \\n Detalles: {temp->services.detalles} \\n Costo: {temp->services.costo}}}\"];\n";
                temp = temp->siguiente;
                index++;
            }

            //Conectar los nodos
            //Regresando al principio de la lista
            temp = cabeza;
            for(int i = 0; temp != null && temp->siguiente != null; i++)
            {
                graphviz += $"\t\t\tn{i} -> n{i+1};\n";
                temp = temp->siguiente;
            }

            graphviz += "\t\t}\n";
            graphviz += "}\n";
            return graphviz;
        }
    }
}