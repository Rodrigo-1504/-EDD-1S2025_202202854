namespace List
{
    public unsafe struct NodoInterno
    {
        public int id;
        public string detalles;
        public int coordenadaX;
        public int coordenadaY;

        public NodoInterno* arriba;
        public NodoInterno* abajo;
        public NodoInterno* izquierda;
        public NodoInterno* derecha;
    }
}