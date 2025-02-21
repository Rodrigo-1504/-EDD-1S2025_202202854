using System;
using System.Runtime.InteropServices;

/* PARA AGREGAR USUARIOS VAMOS A REALIZAR LOS SIGUIENTES PASOS: 
            UNA LISTA SE COMPONE DE NODOS, Y LOS NODOS POR SU PARTE SE COMPONEN DE 2 COSAS:
            - UN PUNTERO
            - DATO DEL PUNTERO
            
            EL DATO DEL PUNTERO FUE HECHO CON Usuarios.cs
            Y LO QUE COMPONE AL NODO SE HIZO CON Nodo.cs
*/

namespace List
{
    public unsafe class ListaSimple
    {
        //Características de la Lista
        private Nodo* cabeza;

        //Constructor
        public ListaSimple()
        {
            cabeza = null;
        }

        //Destructor
        ~ListaSimple()
        {
            while(cabeza != null)
            {
                Nodo* temp = cabeza;
                cabeza = cabeza->siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }

        public void AgregarUsuarios(Usuarios user)
        {
            /*PARA INGRESAR NODOS A UNA LISTA SIMPLE PRIMERO REVISAMOS SI EL PRIMER NODO (CABEZA) ESTA OCUPADA, SI LO ESTA
            INGRESAMOS EL NODO EN EL SIGUIENTE PUNTERO, PERO SÍ NO ESTA OCUPADO, EL NODO A INGRESAR SERÁ EL PRIMERO NODO*/

            //Creacion del Nodo en el heap
            Nodo* newUser = (Nodo*)Marshal.AllocHGlobal(sizeof(Nodo));
            newUser->usuarios = user;
            newUser->siguiente = null;

            //Meter el nodo a la lista
            if(cabeza == null)
            {
                cabeza = newUser;
            }
            else
            {
                //Creación de un nodo temporal que recorrerá la lista
                Nodo* temp = cabeza;
                while(temp->siguiente != null)
                {
                    temp = temp->siguiente;
                }

                temp->siguiente = newUser;
            }
            
        }

        //Eliminar un nodo
        public void EliminarUsuario(int id)
        {
            /*PARA ELIMINAR UN NODO DEBEMOS PRIMERO ENCONTRAR EL NODO QUE DESEAMOS ELIMINAR
            SEGUNDO, COLOCAR EL PUNTERO QUE APUNTA AL NODO A ELIMINAR, Y COLOCARLO EN EL SIGUIENTE NODO*/

            //Lista Vacia
            if(cabeza == null) return;

            //El nodo a eliminar es la cabeza del nodo
            if(cabeza->usuarios.id == id)
            {
                Nodo* temp = cabeza;
                cabeza = cabeza->siguiente; //El nodo cabeza será el siguiente nodo
                Marshal.FreeHGlobal((IntPtr)temp); //Libereamos la memoria, osea eliminamos el nodo
                return;
            }

            //El nodo a elimianr no cumple con ninguno de los casos anteriores
            Nodo* tempAnterior = cabeza;
            Nodo* tempActual = cabeza->siguiente;

            while(tempActual != null)
            {
                if(tempActual->usuarios.id == id)
                {
                    tempAnterior->siguiente = tempActual->siguiente; //Enlazando el nodo anteior con el nodo siguiente, dejando sin enlazar al nodo actual
                    Marshal.FreeHGlobal((IntPtr)tempActual);
                    return;
                }

                tempAnterior = tempActual;
                tempActual = tempActual->siguiente;
            }
        }


        public Usuarios BuscarUsuario(int id)
        {
            Nodo* temp = cabeza;

            while(temp != null)
            {
                if(temp->usuarios.id == id)
                {
                    return temp->usuarios;
                }
                temp = temp->siguiente;
            }

            return null;
        }

        public bool actualizarUsuario(int id, string nombres, string apellidos, string correo)
        {
            Usuarios usuario = BuscarUsuario(id);

            if(usuario != null){

                usuario.nombres = nombres;
                usuario.apellidos = apellidos;
                usuario.correo = correo;

                return true;
            }

            return false;
        }

        //Metodo para imprimir, prueba de que si funciona el código
        public void imprimirLista()
        {
            Nodo *temp = cabeza;

            while(temp != null)
            {
                Console.WriteLine($"ID: {temp->usuarios.id}, Nombre: {temp->usuarios.nombres}, Apellidos: {temp->usuarios.apellidos}, Correo: {temp->usuarios.correo}");
                temp = temp->siguiente;
            }
        }
    }
}