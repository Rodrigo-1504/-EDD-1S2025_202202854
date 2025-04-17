using System.Text;

namespace DS
{
    public class NodoGrafo
    {
        public int Indice { get; set; }
        public NodoGrafo? siguiente { get; set; }
        public NodoGrafo? anterior { get; set; }
        public Subnodo Lista { get; set; }

        public void agregar(int val)
        {
            Subnodo nuevoNodo = new Subnodo(val);
            if(Lista == null)
            {
                Lista = nuevoNodo;
            }
            else
            {
                Subnodo aux = Lista;
                while(aux.siguiente != null)
                {
                    aux = aux.siguiente;
                }
                aux.siguiente = nuevoNodo;
            }
        }

        public void Imprimir()
        {
            Subnodo aux = Lista;
            while(aux != null)
            {
                Console.Write($"{aux.valor}");
                aux = aux.siguiente;
            }
        }

        public string ObtenerCadena()
        {
            StringBuilder sb = new StringBuilder();
            Subnodo aux = Lista;
            while(aux != null)
            {
                sb.Append($"{aux.valor}");
                aux = aux.siguiente;
            }
            return sb.ToString();
        }
    }
}