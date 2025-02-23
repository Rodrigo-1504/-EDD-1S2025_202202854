//PODRIAMOS USAR EL MISMO NODO QUE USAMOS PARA LA LISTA SIMPLE, PERO LO HAREMOS DE NUEVO :)

namespace List
{
    public unsafe struct NodoCircular
    {
        //Características del Nodo
        public Repuestos repuesto;
        public NodoCircular* siguiente;

        //Constructor
        public NodoCircular(Repuestos replace)
        {
            repuesto = replace;
            siguiente = null;
        }
    }

}