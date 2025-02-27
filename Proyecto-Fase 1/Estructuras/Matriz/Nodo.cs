namespace List
{
    public unsafe struct NodoMatriz
    {
        public int id;

        public NodoMatriz* siguiente;
        public NodoMatriz* anterior;

        public NodoInterno* acceso;
    }
}