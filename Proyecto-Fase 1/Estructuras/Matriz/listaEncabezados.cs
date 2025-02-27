using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe class listaEncabezado
    {
        public NodoMatriz* primero;
        public NodoMatriz* ultimo;
        public string tipo;
        public int size;


        public listaEncabezado(string tipo)
        {
            primero = null;
            ultimo = null;
            this.tipo = tipo;
            size = 0;
        }

        ~listaEncabezado()
        {
            if(primero == null) return;

            while(primero != null)
            {
                NodoMatriz* temp = primero;
                primero = primero->siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }

        public void insertar_nodoMatriz(int id)
        {
            NodoMatriz* newHeader = (NodoMatriz*)Marshal.AllocHGlobal(sizeof(NodoMatriz));
            
            if(newHeader == null)throw new InvalidOperationException("No se pudo asignar memoria para el nuevo nodo");

            newHeader->id = id;
            newHeader->siguiente = null;
            newHeader->anterior = null;
            newHeader->acceso = null;

            size++;

            if(primero == null)
            {
                primero = newHeader;
                ultimo = newHeader;
            }
            else
            {
                if(newHeader->id < primero->id)
                {
                    newHeader->siguiente = primero;
                    primero->anterior = newHeader;
                    primero = newHeader;
                }
                else if(newHeader->id > ultimo->id)
                {
                    ultimo->siguiente = newHeader;
                    newHeader->anterior = ultimo;
                    ultimo = newHeader;
                }
                else
                {
                    NodoMatriz* temp = primero;
                    while(temp != null)
                    {
                        if(newHeader->id < temp->id)   
                        {
                            newHeader->siguiente = temp;
                            newHeader->anterior = temp->anterior;
                            temp->anterior->siguiente = newHeader;
                            temp->anterior = newHeader;
                            break;
                        }
                        else
                        {
                            temp = temp->siguiente;
                        }
                    }
                }
            }
        }

        public void imprimirEncabezados()
        {
            if(primero == null)
            {
                Console.WriteLine("Lista vacia");
                return;
            }

            NodoMatriz* temp = primero;
            while(temp != null)
            {
                Console.WriteLine("Encabezado " + tipo + " " + Convert.ToInt32(temp->id));
                temp = temp->siguiente;
            }
        }

        public NodoMatriz* obtenerEncabezado(int id)
        {
            NodoMatriz* temp = primero;
            while(temp != null)
            {
                if(id == temp->id) return temp;
                temp = temp->siguiente;
            }

            return null;
        }
    }
}