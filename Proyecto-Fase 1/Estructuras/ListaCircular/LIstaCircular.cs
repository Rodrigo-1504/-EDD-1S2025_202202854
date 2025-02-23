using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe class ListaCircular
    {

        //Instanciar
        private static ListaCircular _instance;

        public static ListaCircular Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ListaCircular();
                }
                return _instance;
            }
        }

        //Características de la lista Circular
        private NodoCircular* cabeza;
        private NodoCircular* cola;

        //Constructor
        public ListaCircular()
        {
            cabeza = null;
        }

        ~ListaCircular()
        {
            if(cabeza == null) return;

            if(cabeza != null)
            {
                NodoCircular* temp = cabeza;
                while(temp->siguiente != cabeza)
                {
                    /*COMO FUNCIONA ESTO: 
                    NO PODEMOS ELIMINAR EL PRIMER NODO (CABEZA) PORQUE EL ULTIMO NODO ESTA ENLAZADO A ÉL
                    Y LA FORMA MAS SENCILLA DE HACERLO ENTONCES ES, BORRAR LOS DEMAS NODOS QUE NO SEAN LA CABEZA
                    Y ASÍ SI VAMOS BORRANDO, SOLO NOS QUEDARÍA LA CABEZA, PERO SIN EL ULTIMO NODO ENLAZANDOLO*/
                    NodoCircular* temp2 = temp->siguiente;
                    Marshal.FreeHGlobal((IntPtr)temp);
                    temp  = temp2;
                }
                Marshal.FreeHGlobal((IntPtr)temp); //Borrando la cabeza
            }
        }

        public void agregarRepuestos(Repuestos replace)
        {
            /* LA FORMA DE INGRESAR NODOS ES SIMILAR A LA LISTA SIMPLE, CON LA UNICA DIFERENCIA
            QUE EL ULTIMO NODO ESTARÁ ENLAZADO AL PRIMERO NODO*/

            //creacion del nodo
            NodoCircular* newRepuesto = (NodoCircular*)Marshal.AllocHGlobal(sizeof(NodoCircular));
            newRepuesto->repuesto = replace;

            //Agregando el nodo a la lista
            if(cabeza == null)
            {
                cabeza = newRepuesto;
                cabeza->siguiente = cabeza; //Enlazandose a si mismo
            }
            else
            {
                NodoCircular* temp = cabeza;
                while (temp->siguiente != cabeza)
                {
                    temp = temp->siguiente;
                }

                //Reposicionando el puntero, (antes apuntaba a la cabeza, ahora apunta el siguiente nodo)
                temp->siguiente = newRepuesto;
                //El puntero siguiente del nuevo nodo apunta a la cabeza
                newRepuesto->siguiente = cabeza;
            }

        }

        public void eliminarRepuesto(int id)
        {
            // Lista vacía
            if (cabeza == null) return;

            NodoCircular* temp = cabeza;
            NodoCircular* anterior = null;

            // Recorrer la lista circular
            do
            {
                if (temp->repuesto.id == id)
                {
                    // Si el nodo a eliminar es la cabeza
                    if (temp == cabeza)
                    {
                        if (cabeza->siguiente == cabeza) // Solo hay un nodo
                        {
                            cabeza = null;
                        }
                        else
                        {
                            // Encontrar el último nodo
                            NodoCircular* ultimo = cabeza;
                            while (ultimo->siguiente != cabeza)
                            {
                                ultimo = ultimo->siguiente;
                            }

                            cabeza = cabeza->siguiente; // La nueva cabeza es el siguiente nodo
                            ultimo->siguiente = cabeza; // El último nodo apunta a la nueva cabeza
                        }
                    }
                    else
                    {
                        // Enlazar el nodo anterior con el siguiente nodo
                        anterior->siguiente = temp->siguiente;
                    }

                    // Liberar la memoria del nodo eliminado
                    Marshal.FreeHGlobal((IntPtr)temp);
                    return;
                }

                anterior = temp;
                temp = temp->siguiente;
            } while (temp != cabeza);
        }

        public Repuestos buscarRepuesto(int id)
        {
            if(cabeza == null) return null;
            
            NodoCircular* temp = cabeza;
            
            //Ciclo Do-While para no caer en un bucle, ya que esta es una lista circular
            do
            {
                if(temp->repuesto.id == id)
                {
                    return temp->repuesto;
                }
                temp = temp->siguiente;
            }while(temp != cabeza);

            return null;
        }

        public bool actualizarRepuesto(int id, string repuesto, string detalles, int costo)
        {
            Repuestos buscar = buscarRepuesto(id);

            if(buscar != null)
            {
                buscar.repuesto = repuesto;
                buscar.detalles = detalles;
                buscar.costo = costo;

                return true;
            }

            return false;
        }
        
    }
}
