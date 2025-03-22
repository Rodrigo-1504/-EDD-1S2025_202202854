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
        private NodoAVL raiz;

        public ArbolAVL()
        {
            raiz = null;
        }

        //INSERTAR E INSERTAR RECURSIVAMENTE
        public void agregarRepuestos(Repuestos repuesto)
        {
            if(raiz != null)
            {
                raiz = insertarRecursivamente(raiz, repuesto);
            }
            else
            {
                raiz = new NodoAVL(repuesto);
            }
            
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


            if(equilibrio > 1 && repuesto.id < nodo.izquierda.repuestos.id) //CASO 1
            {
                return rotacionDerecha(nodo);
            }
            else if(equilibrio > 1 && repuesto.id > nodo.izquierda.repuestos.id) //CASO 2
            {
                nodo.izquierda = rotacionIzquierda(nodo.izquierda);
                return rotacionDerecha(nodo);
            }
            else if(equilibrio < -1 && repuesto.id > nodo.derecha.repuestos.id) //CASO 3
            {
                return rotacionIzquierda(nodo);
            }
            else if(equilibrio < -1 && repuesto.id < nodo.derecha.repuestos.id) //CASO 4
            {
                nodo.derecha = rotacionDerecha(nodo.derecha);
                return rotacionIzquierda(nodo);
            }

            return nodo;
        }

        //OBTENER LA ALTURA DEL NODO
        private int Altura(NodoAVL nodo)
        {
            if(nodo == null)
            {
                return 0;
            }
            return nodo.height;
        }

        private int FactorEquilibrio(NodoAVL nodo)
        {
            if(nodo == null)
            {
                return 0;
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

        public void RecorridoEnOrden()
        {
            RecorridoEnOrdenRecursivo(raiz);
        }

        private void RecorridoEnOrdenRecursivo(NodoAVL nodo)
        {
            if(nodo != null)
            {
                RecorridoEnOrdenRecursivo(nodo.izquierda);
                Console.WriteLine(nodo.repuestos.id + " ");
                RecorridoEnOrdenRecursivo(nodo.derecha);
            }
        }

    }
}