namespace List{
    public unsafe struct Nodo
    {
        //Características de un Nodo
        public Usuarios usuarios;
        public Nodo* siguiente;

        //Constructor
        public Nodo(Usuarios users)
        {
            usuarios = users;
            siguiente = null;
        }
    }
}