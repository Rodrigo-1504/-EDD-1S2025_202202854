namespace DS
{
    public class SubNodo
    {
        public int valor { get; set; }
        public SubNodo? siguiente { get; set; }

        public SubNodo(int val)
        {
            valor = val;
            siguiente = null;
        }
    }
}