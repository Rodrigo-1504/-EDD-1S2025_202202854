using System;
using System.Net;
using System.Runtime.InteropServices;
using Pango;

namespace Structures
{
    public class ListaSimple
    {
        //INSTANCIAR
        private static ListaSimple _instance;
        public static ListaSimple Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ListaSimple();
                }
                return _instance;
            }
        }

        //CABEZA DE LA LISTA
        private Nodo cabeza;

        
        //CONSTRUCTOR
        public ListaSimple()
        {
            cabeza = null;
        }


        //INSERTAR NODOS A LA LISTA
        public void AgregarUsuarios(Usuarios users)
        {
            //CREACION DEL NODO
            Nodo newNodo = new Nodo(users);

            //BUSCAR QUE EL ID DEL NODO NO EXISTA, SI EXISTE NO SE AGREGA EL NODO A LA LISTA
            if(BuscarId(users.id) != null)
            {
                Console.WriteLine("Ya existe el ID");
                return;
            }

            //LISTA VACIA
            if(cabeza == null)
            {
                cabeza = newNodo;
            }
            //RECORRERÁ LA LISTA, HASTA ENCONTRAR VACIO, AHÍ ES DONDE SE AGREGARÁ EL NODO
            else
            {
                Nodo temporal = cabeza; //NODO TEMPORAL PARA RECORRER LA LISTA
                while(temporal.siguiente != null)
                {
                    temporal = temporal.siguiente;
                }

                temporal.siguiente = newNodo;
            }
        }

        //ELIMINAR NODO DE LA LISTA
        public void EliminarUsuario(int id)
        {
            //LISTA VACIA
            if(cabeza == null) return;

            //NODO A ELIMINAR = CABEZA
            if(cabeza.usuarios.id == id)
            {
                cabeza = cabeza.siguiente;
                return;
            }

            //RECORRER LA LISTA HASTA ENCONTRAR EL NODO A ELIMINAR
            Nodo anterior = cabeza;
            Nodo temporal = cabeza.siguiente;

            while(temporal != null)
            {
                if(temporal.usuarios.id == id)
                {
                    anterior.siguiente = temporal.siguiente;
                    return;
                }
                
                anterior = temporal;
                temporal = temporal.siguiente;
            }

        }

        public Nodo BuscarUsuario(String correo, String contraseña)
        {
            Nodo temporal = cabeza;
            while(temporal != null)
            {
                if(temporal.usuarios.correo == correo && temporal.usuarios.contraseña == contraseña)
                {
                    return temporal;
                }
                temporal = temporal.siguiente;
            }
            return null;
        }

        public Nodo BuscarId(int id)
        {
            Nodo temporal = cabeza;
            while(temporal != null)
            {
                if(temporal.usuarios.id == id)
                {
                    return temporal;
                }
                temporal = temporal.siguiente;
            }
            return null;
        }

        public void Imprimir()
        {
            Nodo temporal = cabeza;
            while(temporal != null)
            {
                Console.WriteLine($"ID: {temporal.usuarios.id}, Nombre: {temporal.usuarios.nombres}, Apellido: {temporal.usuarios.apellidos}, Correo: {temporal.usuarios.correo}, Contraseña: {temporal.usuarios.contraseña}");
                temporal = temporal.siguiente;
            }
        }

        public string graphvizLista()
        {
            if(cabeza == null)
            {
                return "digraph G {\n\tnode[shape=record];\n\tNULL[label = \"{NULL}\"];\n}\n";
            }


            //ESTRUCTURA INICIAL DEL GRAPHVIZ
            string graphviz = "digraph {\n";
            graphviz += "\tnode[shape=record];\n";
            graphviz += "\tgraph[pencolor=transparent];\n";
            graphviz += "\trankdir=LR;\n";
            graphviz += "\tsubgraph cluster_0{\n";
            graphviz += "\t\tlabel = \"Lista Simple\";\n";

            Nodo temp = cabeza;
            int index = 0;


            //CREANDO LOS NODOS
            while(temp != null)
            {
                graphviz += $"\t\t\tn{index} [label = \"{{ID : {temp.usuarios.id} \\n Nombres: {temp.usuarios.nombres} {temp.usuarios.apellidos} \\n Correo: {temp.usuarios.correo}}}\"];\n";
                temp = temp.siguiente;
                index++;
            }

            //CONECTAR LOS NODOS
            //REGRESANDO AL PRINCIPIO DE LA LISTA
            temp = cabeza;
            for(int i = 0; temp != null && temp.siguiente != null; i++)
            {
                graphviz += $"\t\t\tn{i} -> n{i+1};\n";
                temp = temp.siguiente;
            }

            graphviz += "\t\t}\n";
            graphviz += "}\n";
            return graphviz;
        }
    }
}