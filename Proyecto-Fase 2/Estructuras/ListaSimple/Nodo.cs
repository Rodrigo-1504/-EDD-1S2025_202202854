namespace Structures
{
    public class Nodo
    {
        public Usuarios usuarios;
        public Nodo siguiente;

        public Nodo(Usuarios users)
        {
            usuarios = users;
            siguiente = null;
        }
    }
}