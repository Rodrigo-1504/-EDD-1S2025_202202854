namespace Structures
{
    public class NodoDoble
    {
        //Características del Nodo
        public Vehiculos vehiculo;
        public NodoDoble anterior;
        public NodoDoble siguiente;

        //Constructor
        public NodoDoble(Vehiculos vehicule)
        {
            vehiculo = vehicule;
            anterior = null;
            siguiente = null;
        }
    }
}