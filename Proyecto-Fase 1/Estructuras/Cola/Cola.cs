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

        public void imprimir()
        {
            NodoCola* temp = cabeza;

            while(temp != null)
            {
                Console.WriteLine($"ID: {temp->services.id}, ID_Respuesto: {temp->services.id_Repuesto}, ID_Vehiculo: {temp->services.id_Vehiculo}, Detalles: {temp->services.detalles}, Costo: {temp->services.costo}");
                temp = temp->siguiente;
            }
        }
    }
}