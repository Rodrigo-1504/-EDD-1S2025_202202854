namespace List
{
    public unsafe struct NodoPila
    {
        //Características del Nodo Pila
        public Facturas factura;
        public NodoPila* abajo;


        //Constructor
        public NodoPila(Facturas facturas)
        {
            factura = facturas;
            abajo = null;
        }
    }
}