
namespace List
{
    public unsafe struct NodoCola
    {
        //Características del Nodo Cola
        public Servicios services;
        public NodoCola* siguiente;

        //Constrcutor
        public NodoCola(Servicios servicios)
        {
            services = servicios;
            siguiente = null;
        }
    }
}