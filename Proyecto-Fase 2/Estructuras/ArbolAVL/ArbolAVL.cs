using System;
using System.Runtime.ExceptionServices;

namespace Structures
{
    public class ArbolAVL
    {


        //INSTANCIAR
        private static ArbolAVL _instance;
        public static ArbolAVL Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ArbolAVL();
                }
                return _instance;
            }
        }

        //CARACTERÍSTICAS
        public NodoAVL raiz;

        public ArbolAVL()
        {
            raiz = null;
        }

        //INSERTAR E INSERTAR RECURSIVAMENTE
        public void agregarRepuestos(Repuestos repuesto)
        {
            if(Buscar(repuesto.id) != null)
            {
                Console.WriteLine("El id del repuesto ya existe");
                return;
            }
            
            raiz = insertarRecursivamente(raiz, repuesto);         
        }

        private NodoAVL insertarRecursivamente(NodoAVL nodo, Repuestos repuesto)
        {
            if(nodo == null)
            {
                return new NodoAVL(repuesto);
            }
            
            //INSERTAR EL NODO
            if(repuesto.id < nodo.repuestos.id)
            {
                nodo.izquierda = insertarRecursivamente(nodo.izquierda, repuesto);
            }
            else if(repuesto.id > nodo.repuestos.id)
            {
                nodo.derecha = insertarRecursivamente(nodo.derecha, repuesto);
            }
            else 
            {
                return nodo;
            }

            //ACTUALIZAR ALTURAS
            nodo.height = Math.Max(Altura(nodo.izquierda), Altura(nodo.derecha)) + 1;

            //OBTENER EL FACTOR DE EQUILIBRIO
            int equilibrio = FactorEquilibrio(nodo);

            //REALIZAR LAS ROTACIONES NECESARIAS

            /*
                *VALORES PERMITIDOS
                * 0 -> SUBARBOL IZQUIERDO A LA MISMA ALTURA QUE EL SUBARBOL DERECHO
                * 1 -> SUBARBOL IZQUIERDO MAYOR AL SUBARBOL DERECHO
                * -1 -> SUBARBOL IZQUIERDO MENOR AL SUBARBOL DERECHO
            */

            /*
                *CASOS DE DESBALANCEO
                1. 
                                6
                        5               8
                    3               7       9
                1    

                EL DESEQUILIBRIO OCURRE EN EL NODO 5, PORQUE? ALTURA DEL NODO 1 = 0; ALTURA DEL NODO 3 = 1; ALTURA DEL NODO 5 = 2;
                ENTONCES CONSIDERAMOS QUE EL SUBARBOL IZQUIERDO DEL NODO 5 ESTA MÁS PESADO, Y ES AHÍ DONDE HAY QUE REALIZAR UNA ROTACION DERECHA


                2. 
                                6
                        5               8
                    3               7       9
                        1    

                EL DESEQUILIBRIO OCURRE NUEVAMENTE EN EL NODO 5, NUEVAMENTE PORQUE ES DE ALTURA 2
                ENTONCES PRIMERO SE REALIZA UNA ROTACION SIMPLE A LA IZQUIERDA Y LUEGO OTRA A LA DERECHA (DOBLE IZQUIERDA)

                3. 
                                6
                        5               8
                    3               7       9
                                                10

                SIMILAR AL CASO 1, PERO AHORA CON EL SUBARBOL DERECHO, SE REALIZAR UNA ROTACIÓN A LA IZQUIERDA

                4. 
                                6
                        5               8
                    3              7          10
                                            9       

                SIMILAR AL CASO 2, SE REALIZAR UNA ROTACIÓN A LA DERECHA Y LUEGO OTRA A LA IZQUIERDA (DOBLE DERECHA)

            */


            if (equilibrio > 1 && repuesto.id < nodo.izquierda.repuestos.id) // CASO 1 (LL)
            {
                //Console.WriteLine("Rotacion derecha (LL)");
                return rotacionDerecha(nodo);
            }
            else if (equilibrio > 1 && repuesto.id > nodo.izquierda.repuestos.id) // CASO 2 (LR)
            {
                nodo.izquierda = rotacionIzquierda(nodo.izquierda);
                //Console.WriteLine("Rotacion izquierda-derecha (LR)");
                return rotacionDerecha(nodo);
            }
            else if (equilibrio < -1 && repuesto.id > nodo.derecha.repuestos.id) // CASO 3 (RR)
            {
                //Console.WriteLine("Rotacion izquierda (RR)");
                return rotacionIzquierda(nodo);
            }
            else if (equilibrio < -1 && repuesto.id < nodo.derecha.repuestos.id) // CASO 4 (RL)
            {
                nodo.derecha = rotacionDerecha(nodo.derecha);
                //Console.WriteLine("Rotacion derecha-izquierda (RL)");
                return rotacionIzquierda(nodo);
            }

            return nodo;
        }

        //OBTENER LA ALTURA DEL NODO
        private int Altura(NodoAVL nodo)
        {
            if(nodo == null)
            {
                return -1;
            }
            return nodo.height;
        }

        private int FactorEquilibrio(NodoAVL nodo)
        {
            if(nodo == null)
            {
                return -1;
            }
            return Altura(nodo.izquierda) - Altura(nodo.derecha);
        }

        private NodoAVL rotacionDerecha(NodoAVL y)
        {
            NodoAVL x = y.izquierda;
            NodoAVL T2 = x.derecha;

            x.derecha = y;
            y.izquierda = T2;

            y.height = Math.Max(Altura(y.izquierda), Altura(y.derecha)) + 1;
            x.height = Math.Max(Altura(x.izquierda), Altura(x.derecha)) + 1;

            return x;
        }

        private NodoAVL rotacionIzquierda(NodoAVL x)
        {
            NodoAVL y = x.derecha;
            NodoAVL T2 = y.izquierda;

            y.izquierda = x;
            x.derecha = T2;

            x.height = Math.Max(Altura(x.izquierda), Altura(x.derecha)) + 1;
            y.height = Math.Max(Altura(y.izquierda), Altura(y.derecha)) + 1;

            return y;
        }

        public NodoAVL Buscar(int id)
        {
            return Buscar(raiz, id);
        }

        private NodoAVL Buscar(NodoAVL nodo, int id)
        {
            if(nodo == null) return null;
            
            if(id == nodo.repuestos.id)
            {
                return nodo;
            }


            if(id < nodo.repuestos.id)
            {
                return Buscar(nodo.izquierda, id);
            }
            
            return Buscar(nodo.derecha, id);
        }

        public void Actualizar(int id, String repuesto, String detalles, double Costo)
        {
            
            NodoAVL nodoActualizar = Buscar(id);
            
            if(nodoActualizar != null)
            {
                nodoActualizar.repuestos.repuesto = repuesto;
                nodoActualizar.repuestos.detalles = detalles;
                nodoActualizar.repuestos.costo = Costo;
            }
        }

        public void RecorridoPreOrden()
        {
            //RAIZ - IZQUIERDA - DERECHA
            RecorridoPreOrdenRecursivo(raiz);
        }

        private void RecorridoPreOrdenRecursivo(NodoAVL nodo)
        {
            if(nodo != null)
            {
                Console.WriteLine($"ID: {nodo.repuestos.id}, Repuesto: {nodo.repuestos.repuesto}, Detalles: {nodo.repuestos.detalles}, Costo: {nodo.repuestos.costo}");
                RecorridoPreOrdenRecursivo(nodo.izquierda);
                RecorridoPreOrdenRecursivo(nodo.derecha);
            }
        }

        public void RecorridoEnOrden()
        {
            //IZQUIERDA - RAIZ - DERECHA
            RecorridoEnOrdenRecursivo(raiz);
        }

        private void RecorridoEnOrdenRecursivo(NodoAVL nodo)
        {
            if(nodo != null)
            {
                RecorridoEnOrdenRecursivo(nodo.izquierda);
                Console.WriteLine($"ID: {nodo.repuestos.id}, Repuesto: {nodo.repuestos.repuesto}, Detalles: {nodo.repuestos.detalles}, Costo: {nodo.repuestos.costo}");
                RecorridoEnOrdenRecursivo(nodo.derecha);
            }
        }

        public void RecorridoPostOrden()
        {
            //IZQUIERDA - DERECHA - RAIZ
            RecorridoPostOrdenRecursivo(raiz);
        }

        private void RecorridoPostOrdenRecursivo(NodoAVL nodo)
        {
            if(nodo != null)
            {
                RecorridoPostOrdenRecursivo(nodo.izquierda);
                RecorridoPostOrdenRecursivo(nodo.derecha);
                Console.WriteLine($"ID: {nodo.repuestos.id}, Repuesto: {nodo.repuestos.repuesto}, Detalles: {nodo.repuestos.detalles}, Costo: {nodo.repuestos.costo}");
            }
        }


        public string graphvizAVL()
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

            graphviz += graphvizAVLRecursivo(raiz);
            
            graphviz += "\t\t}\n";
            graphviz += "}\n";            
            return graphviz;
        }

        private string graphvizAVLRecursivo(NodoAVL nodo)
        {
            string graphviz = "";

            if (nodo != null)
            {
                // Agregar la relación con el hijo izquierdo
                if (nodo.izquierda != null)
                {
                    graphviz += $"\t\"{nodo.repuestos.id}\" -> \"{nodo.izquierda.repuestos.id}\";\n";
                }

                // Agregar la relación con el hijo derecho
                if (nodo.derecha != null)
                {
                    graphviz += $"\t\"{nodo.repuestos.id}\" -> \"{nodo.derecha.repuestos.id}\";\n";
                }

                // Recorrer los subárboles
                graphviz += graphvizAVLRecursivo(nodo.izquierda);
                graphviz += graphvizAVLRecursivo(nodo.derecha);
            }

            return graphviz;
        }

    }
}