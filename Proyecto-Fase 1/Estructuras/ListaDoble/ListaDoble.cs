using System;
using System.Runtime.InteropServices;


/*PARA AGREGAR LOS VEHICULOOS VAMOS A REALIZAR LO SIGUIENTE: 
    UNA LISTA DOBLE ES SIMILAR A UNA LISTA SIMPLE, CON LA DIFERENCIA QUE
    LA LISTA DOBLE TIENE 2 PUNTEROS
    LO QUE COMPONE AL NODO FUE HECHO EN Nodo.cs EN ESTA CARPETA
    Y LO QUE COMPONE A LOS DATOS DEL NODO FUE HECHO EN Vehiculos.cs*/


namespace List
{
    public unsafe class ListaDoble
    {
        //Instanciar
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

        //Caracteristicas de la Lista Doble
        private NodoDoble* cabeza;

        //Constructor
        public ListaDoble()
        {
            cabeza = null;
        }

        //Destructor
        ~ListaDoble()
        {
            while(cabeza != null)
            {
                NodoDoble* temp = cabeza;
                cabeza = cabeza->siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);       
            }
        }

        public void agregarVehiculos(Vehiculos vehicles)
        {
            /*PARA INGRESAR NODOS A UNA LISTA DOBLE REVISAMOS LO SIGUIENTE:
                - SI ES LA CABEZA, EL APUNTADOR SIGUIENTE DEBE APUNTAR A OTRO NODO, PERO EL ANTERIOR A NULL
                - SI NO ES LA CABEZA, TENEMOS QUE DAR USO DE LOS 2 APUNTADORES*/

            //creación del Nodo en el heap
            NodoDoble* newVehicle = (NodoDoble*)Marshal.AllocHGlobal(sizeof(NodoDoble));
            newVehicle->vehiculo = vehicles;
            newVehicle->siguiente = null;
            newVehicle->anterior = null;

            //Meter el nodo a la lista
            if(cabeza == null)
            {
                cabeza = newVehicle;
            }
            else
            {
                NodoDoble* temp = cabeza;
                while(temp->siguiente != null)
                {
                    temp = temp->siguiente;
                }
                temp->siguiente = newVehicle;
                newVehicle->anterior = temp;
            }
        }

        public void eliminarVehiculo(int id)
        {
            /*PARA ELIMINAR UN NODO ES SIMILAR QUE CON LA LISTA SIMPLE
            CON LA DIFERENCIA DE QUE AHORA HAY QUE DARLE USO AL PUNTERO 
            ANTERIOR DEL NODO SIGUIENTE*/

            //Lista vacia
            if(cabeza == null) return;

            //El nodo a eliminar es la cabeza de la lista
            if(cabeza->vehiculo.id == id)
            {
                NodoDoble* temp = cabeza;
                cabeza = cabeza->siguiente;
                if(cabeza != null)
                {
                    cabeza->anterior = null;
                }

                Marshal.FreeHGlobal((IntPtr)temp);
                return;
            }

            NodoDoble* tempActual = cabeza;
            while(tempActual != null)
            {
                if(tempActual->vehiculo.id == id)
                {
                    //Redirigiendo los Apuntadores
                    if(tempActual->anterior != null)
                    {
                        //El apuntador siguiente del nodo anteior = apuntador siguiente del nodo actual
                        tempActual->anterior->siguiente = tempActual->siguiente;
                    }

                    if(tempActual->siguiente != null)
                    {
                        tempActual->siguiente->anterior = tempActual->anterior;
                    }
                    Marshal.FreeHGlobal((IntPtr)tempActual);
                    return;
                }

                tempActual = tempActual->siguiente;
            }
        }

        public Vehiculos buscarVehiculo(int id)
        {
            NodoDoble* temp = cabeza;
            while(temp != null)
            {
                if(temp->vehiculo.id == id)
                {
                    return temp->vehiculo;
                }
                temp = temp->siguiente;
            }

            return null;
        }

        public void buscarVehiculoUsuario(int idUser)
        {
            NodoDoble* temp = cabeza;
            while(temp != null)
            {
                if(temp->vehiculo.ID_Usuario == idUser)
                {
                    Console.WriteLine($"Carro: {temp->vehiculo.marca}");
                }
                temp = temp->siguiente;
            }
            
        }

        public bool actualizarVehiculo(int id, int id_User, string marcaNueva, int modeloNuevo, string placaNueva)
        {
            Vehiculos vehiculoBuscado = buscarVehiculo(id);
            if(vehiculoBuscado != null)
            {
                vehiculoBuscado.ID_Usuario = id_User;
                vehiculoBuscado.marca = marcaNueva;
                vehiculoBuscado.modelo = modeloNuevo;
                vehiculoBuscado.placa = placaNueva;

                return true;
            }

            return false;
        }

        public void imprimirListaDoble()
        {
            NodoDoble* temp = cabeza;
            while(temp != null)
            {
                Console.WriteLine($"ID: {temp->vehiculo.id}, ID_Usuario: {temp->vehiculo.ID_Usuario}, Marca: {temp->vehiculo.marca}, Modelo: {temp->vehiculo.modelo}, Placa: {temp->vehiculo.placa}");
                temp = temp->siguiente;
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

            NodoDoble* temp = cabeza;
            int index = 0;

            //Creando los nodos
            while(temp != null)
            {
                graphviz += $"\t\t\tn{index} [label = \"{{ID: {temp->vehiculo.id} \\n ID_USUARIOS: {temp->vehiculo.ID_Usuario} \\n Marca: {temp->vehiculo.marca} \\n Modelo: {temp->vehiculo.modelo} \\n Placa: {temp->vehiculo.placa}}}\"];\n";
                temp = temp->siguiente;
                index++;
            }

            temp = cabeza;
            for(int i = 0; temp != null; i++)
            {
                if(temp->siguiente != null)
                {
                    graphviz += $"\t\t\tn{i}->n{i+1};\n";
                }

                if(temp->anterior != null)
                {
                    graphviz += $"\t\t\tn{i}->n{i-1};\n";
                }
                
                temp = temp->siguiente;
            }

            graphviz += "\t\t}\n";
            graphviz += "}\n";            
            return graphviz;
        }

    }
}