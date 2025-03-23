namespace Structures
{
    public class NodoBST
    {
        //CARACTERÍSTICAS DEL NODO
        public Servicios servicios;
        public NodoBST izquierda;
        public NodoBST derecha;

        //CONSTRUCTOR
        public NodoBST(Servicios servicio)
        {
            servicios = servicio;
            izquierda = null;
            derecha = null;
        }
    }
}