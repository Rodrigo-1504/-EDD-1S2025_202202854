using System.Text;

namespace DS
{
    public class NodoPrincipal
    {
        public int Indice { get; set; }
        public NodoPrincipal? siguiente { get; set; }
        public NodoPrincipal? anterior { get; set; }
        public SubNodo? Lista { get; set; }

        public void agregarSubNodo(int val)
        {
            //SE AGREGAR EL SUBNODO AL NODO PRINCIPAL CREANDO ASÍ LA LISTA DE LISTAS
            SubNodo nuevoNodo = new SubNodo(val);
            if(Lista == null)
            {
                Lista = nuevoNodo;
            }
            else
            {
                SubNodo aux = Lista;
                while(aux.siguiente != null)
                {
                    aux = aux.siguiente;
                }
                aux.siguiente = nuevoNodo;
            }
        }

        public void Imprimir()
        {
            SubNodo aux = Lista;
            while(aux != null)
            {
                Console.Write($"{aux.valor}");
                aux = aux.siguiente;
            }
        }
    }
}