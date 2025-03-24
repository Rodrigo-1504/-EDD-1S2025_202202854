using System;
using System.Runtime.InteropServices;

namespace Structures
{
    public class ListaDoble
    {
        
        ListaSimple ls = ListaSimple.Instance;
        
        //INSTANCIAR
        private static ListaDoble _instance;
        public static ListaDoble Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ListaDoble();
                }
                return _instance;
            }
        }

        //CARACTERÍSTICAS DE LA LISTA DOBLE
        private NodoDoble cabeza;

        //CONSTRUCTOR
        public ListaDoble()
        {
            cabeza = null;
        }

        public void AgregarVehiculos(Vehiculos veh)
        {
            //CREANDO NODO
            NodoDoble newVehicle = new NodoDoble(veh);

            //BUSCAR QUE EL VEHICULO QUE SE VAYA A INGRESAR NO ESTE YA EN LA LISTA
            if(BuscarVehiculo(veh.id) != null)
            {
                Console.WriteLine("Vehiculo ya existente");
                return;
            }

            //VERIFICAR QUE EL USUARIO EXISTA
            if(ls.BuscarId(veh.ID_Usuario) == null)
            {
                Console.WriteLine("El usuario no existe");
                return;
            }

            //SI LA LISTA ESTA VACIA, AGREGAR NODO A LA CABEZA
            if(cabeza == null)
            {
                cabeza = newVehicle;
            }
            //AGREGAR EL NODO EN OTRA PARTE DE LA LISTA
            else
            {
                NodoDoble temporal = cabeza; //NODO TEMPORAL QUE RECORRE LA LISTA
                while(temporal.siguiente != null)
                {
                    temporal = temporal.siguiente;
                }
                temporal.siguiente = newVehicle; //APUNTADOR SIGUIENTE (VACIO) AHORA ESTARÁ APUNTANDO AL NUEVO OBJETO
                newVehicle.anterior = temporal; //APUNTADOR ANTERIOR, APUNTARÁ A SU ANTERIOR
            }
        }

        public void EliminarVehiculo(int id)
        {
            //LISTA VACIA
            if(cabeza == null) return;

            //NODO A ELIMINAR = CABEZA
            if(cabeza.vehiculo.id == id)
            {
                cabeza = cabeza.siguiente;
                if(cabeza != null)
                {
                    cabeza.anterior = null;
                }
                return;
            }

            //RECORRER LA LISTA PARA ELIMINAR EL NODO
            NodoDoble temporal = cabeza;
            while(temporal != null)
            {
                if(temporal.vehiculo.id == id)
                {
                    if(temporal.anterior != null)
                    {
                        temporal.anterior.siguiente = temporal.siguiente;
                    }

                    if(temporal.siguiente != null)
                    {
                        temporal.siguiente.anterior = temporal.anterior;
                    }

                    return;
                }
                temporal = temporal.siguiente;
            }
        }

        public Vehiculos BuscarVehiculo(int id)
        {
            NodoDoble temporal = cabeza;
            while(temporal != null)
            {
                if(temporal.vehiculo.id == id)
                {
                    return temporal.vehiculo;
                }
                temporal = temporal.siguiente;
            }
            return null;
        }

        public void Imprimir()
        {
            NodoDoble temporal = cabeza;
            while(temporal != null)
            {
                Console.WriteLine($"ID: {temporal.vehiculo.id}, ID_Usuario: {temporal.vehiculo.ID_Usuario}, Marca: {temporal.vehiculo.marca}, Modelo: {temporal.vehiculo.modelo}, Placa: {temporal.vehiculo.placa}");
                temporal = temporal.siguiente;
            }
        }

        public string graphvizDoble()
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
            graphviz += "\t\tlabel = \"Lista Doble\";\n";

            NodoDoble temp = cabeza;
            int index = 0;

            //Creando los nodos
            while(temp != null)
            {
                graphviz += $"\t\t\tn{index} [label = \"{{ID: {temp.vehiculo.id} \\n ID_USUARIOS: {temp.vehiculo.ID_Usuario} \\n Marca: {temp.vehiculo.marca} \\n Modelo: {temp.vehiculo.modelo} \\n Placa: {temp.vehiculo.placa}}}\"];\n";
                temp = temp.siguiente;
                index++;
            }

            temp = cabeza;
            for(int i = 0; temp != null; i++)
            {
                if(temp.siguiente != null)
                {
                    graphviz += $"\t\t\tn{i}.n{i+1};\n";
                }

                if(temp.anterior != null)
                {
                    graphviz += $"\t\t\tn{i}.n{i-1};\n";
                }
                
                temp = temp.siguiente;
            }

            graphviz += "\t\t}\n";
            graphviz += "}\n";            
            return graphviz;
        }
    }
}