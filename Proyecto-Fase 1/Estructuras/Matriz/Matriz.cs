using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe class Matriz
    {
        public int capa;
        public listaEncabezado filas;
        public listaEncabezado columnas;

        private static Matriz _instance;

        public static Matriz Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Matriz(0);
                }
                return _instance;
            }
        }


        public Matriz(int capa)
        {
            this.capa = capa;
            filas = new listaEncabezado("filas");
            columnas = new listaEncabezado("columnas");
        }

        ~Matriz()
        {
            NodoMatriz* Xfila = filas.primero;
            while(Xfila != null)
            {
                NodoInterno* interno = Xfila->acceso;
                while(interno != null)
                {
                    NodoInterno* temp = interno;
                    interno = interno->derecha;
                    if(temp != null)
                    {
                        Marshal.FreeHGlobal((IntPtr)temp);
                    }
                }

                NodoMatriz* tempFila = Xfila;
                Xfila = Xfila->siguiente;
                if(tempFila != null)
                {
                    Marshal.FreeHGlobal((IntPtr)tempFila);
                }
            }

            NodoMatriz* Xcolumna = columnas.primero;
            while(Xcolumna != null)
            {
                NodoInterno* interno = Xcolumna->acceso;
                while(interno != null)
                {
                    NodoInterno* temp = interno;
                    interno = interno->abajo;
                    if(temp != null)
                    {
                        Marshal.FreeHGlobal((IntPtr)temp);
                    }
                }

                NodoMatriz* tempColumna = Xcolumna;
                Xcolumna = Xcolumna->siguiente;
                if(tempColumna != null)
                {
                    Marshal.FreeHGlobal((IntPtr)tempColumna);
                }
            }
        }

        public void insertar(int posX, int posY, string detalles)
        {
            NodoInterno* newNodo = (NodoInterno*)Marshal.AllocHGlobal(sizeof(NodoInterno));
            newNodo->id = 1;
            newNodo->detalles = detalles;
            newNodo->coordenadaX = posX;
            newNodo->coordenadaY = posY;
            newNodo->arriba = null;
            newNodo->abajo = null;
            newNodo->izquierda = null;
            newNodo->derecha = null;

            NodoMatriz* nodoX = filas.obtenerEncabezado(posX);
            NodoMatriz* nodoY = columnas.obtenerEncabezado(posY);

            if(nodoX == null)
            {
                filas.insertar_nodoMatriz(posX);
                nodoX = filas.obtenerEncabezado(posX);
            }

            if(nodoY == null)
            {
                columnas.insertar_nodoMatriz(posY);
                nodoY = columnas.obtenerEncabezado(posY);
            }

            if(nodoX == null || nodoY == null)
            {
                throw new InvalidOperationException("Error al crear los encabezados");
            }

            if(nodoX->acceso == null)
            {
                nodoX->acceso = newNodo;
            }
            else
            {
                NodoInterno* temp = nodoX->acceso;
                while(temp != null)
                {
                    if(newNodo->coordenadaY < temp->coordenadaY)
                    {
                        newNodo->derecha = temp;
                        newNodo->izquierda = temp->izquierda;
                        temp->izquierda->derecha = newNodo;
                        temp->izquierda = newNodo;
                        break;
                    }
                    else if(newNodo->coordenadaX == temp->coordenadaX && newNodo->coordenadaY == temp->coordenadaY)
                    {
                        break;
                    }
                    else
                    {
                        if(temp->derecha == null)
                        {
                            temp->derecha = newNodo;
                            newNodo->izquierda = temp;
                            break;
                        }
                        else
                        {
                            temp = temp->derecha;
                        }
                    }
                }
            }

            if(nodoY->acceso == null)
            {
                nodoY->acceso = newNodo;
            }
            else
            {
                NodoInterno* temp2 = nodoY->acceso;
                while(temp2 != null)
                {
                    if(newNodo->coordenadaX < temp2->coordenadaX)
                    {
                        newNodo->abajo = temp2;
                        newNodo->arriba = temp2->arriba;
                        temp2->arriba->abajo = newNodo;
                        temp2->arriba = newNodo;
                        break;
                    }
                    else if(newNodo->coordenadaX == temp2->coordenadaX && newNodo->coordenadaY == temp2->coordenadaY)
                    {
                        break;
                    }

                    else
                    {
                        if(temp2 == null)
                        {
                            temp2->abajo = newNodo;
                            newNodo->arriba = temp2;
                            break;
                        }
                        else
                        {
                            temp2 = temp2->abajo;
                        }
                    }
                }
            }
        }

        public void mostrarMatriz()
        {
            NodoMatriz* Ycolumna = columnas.primero;
            Console.Write("\t");

            while(Ycolumna != null)
            {
                Console.Write("R" + Ycolumna->id + "\t");
                Ycolumna = Ycolumna->siguiente;
            }
            Console.WriteLine();

            NodoMatriz* Xfila = filas.primero;
            while(Xfila != null)
            {
                Console.Write("V" + Xfila->id + "\t");

                NodoInterno* interno = Xfila->acceso;
                NodoMatriz* YcolumnaIteracion = columnas.primero;

                while(YcolumnaIteracion != null)
                {
                    if(interno != null && interno->coordenadaY == YcolumnaIteracion->id)
                    {
                        Console.Write(interno->detalles + "\t");
                        interno = interno->derecha;
                    }
                    else
                    {
                        Console.Write("\t");
                    }
                    YcolumnaIteracion = YcolumnaIteracion->siguiente;
                }
                Console.WriteLine();
                Xfila = Xfila->siguiente;
            }
        }

        public string graphvizMatriz()
        {
            string graphviz = "digraph G {\n";
            graphviz += "\tnode[shape=record];\n";
            graphviz += "\tgraph[pencolor=transparent];\n";
            graphviz += "\trankdir=TB;\n"; // Top to Bottom para mejor visualización de la matriz
            graphviz += "\tsubgraph cluster_0{\n";
            graphviz += "\t\tlabel = \"Matriz\";\n";

            // Crear nodos para las filas y columnas
            NodoMatriz* Xfila = filas.primero;
            while (Xfila != null)
            {
                graphviz += $"\t\tf{Xfila->id} [label = \"Fila {Xfila->id}\"];\n";
                Xfila = Xfila->siguiente;
            }

            NodoMatriz* Ycolumna = columnas.primero;
            while (Ycolumna != null)
            {
                graphviz += $"\t\tc{Ycolumna->id} [label = \"Columna {Ycolumna->id}\"];\n";
                Ycolumna = Ycolumna->siguiente;
            }

            // Conectar filas y columnas
            Xfila = filas.primero;
            while (Xfila != null)
            {
                NodoInterno* interno = Xfila->acceso;
                while (interno != null)
                {
                    graphviz += $"\t\tn{interno->coordenadaX}_{interno->coordenadaY} [label = \"{interno->detalles}\"];\n";
                    graphviz += $"\t\tf{interno->coordenadaX} -> n{interno->coordenadaX}_{interno->coordenadaY};\n";
                    graphviz += $"\t\tc{interno->coordenadaY} -> n{interno->coordenadaX}_{interno->coordenadaY};\n";
                    interno = interno->derecha;
                }
                Xfila = Xfila->siguiente;
            }

            graphviz += "\t}\n";
            graphviz += "}\n";
            return graphviz;
        }
    }
}