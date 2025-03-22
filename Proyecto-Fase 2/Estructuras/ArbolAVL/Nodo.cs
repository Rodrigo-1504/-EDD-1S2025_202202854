namespace Structures
{
    public class NodoAVL
    {
        //CARACTERÍSTICAS DEL NODO
        public Repuestos repuestos;
        public NodoAVL izquierda;
        public NodoAVL derecha;
        public int height;


        //CONSTRUCTOR
        public NodoAVL(Repuestos repuesto)
        {
            repuestos = repuesto;
            izquierda = null;
            derecha = null;
            height = 0;
        }
    }
}