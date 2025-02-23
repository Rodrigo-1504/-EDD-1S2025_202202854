using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe class Pila
    {
        //Instanciar
        private static Pila _instance;

        public static Pila Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Pila();
                }
                return _instance;
            }
        }

        //Características de la Pila
        private NodoPila* tope;

        public Pila()
        {
            tope = null;
        }

        ~Pila()
        {
            while(tope != null)
            {
                NodoPila* temp = tope;
                tope = tope->abajo;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }

        //LIFO
        public void agregarFactura(Facturas bill)
        {
            /*AL MOMENTO DE INGRESAR UN NODO A UNA PILA, SIEMPRE EL ULTIMO NODO A INGRESAR SERÁ EL TOPE DE LA PILA*/
            NodoPila* newBill = (NodoPila*)Marshal.AllocHGlobal(sizeof(NodoPila));
            newBill->factura = bill;
            newBill->abajo = tope;
            tope = newBill;
        }

        public Facturas eliminarFactura()
        {
            if(tope == null) return null;

            NodoPila* temp = tope;
            Facturas factura = temp->factura;

            tope = tope->abajo;

            Marshal.FreeHGlobal((IntPtr)temp);
            return factura;

        }

        public void imprimir()
        {
            NodoPila* temp = tope;
            while(temp != null)
            {
                Console.WriteLine($"ID: {temp->factura.id}, ID_Orden: {temp->factura.id_Orden}, Total: {temp->factura.total}");
                temp = temp->abajo;
            }
        }

    }
}