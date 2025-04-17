namespace DS
{
    public class Subnodo
    {
        public int valor { get; set; }
        public Subnodo siguiente { get; set; }

        public Subnodo(int val)
        {
            valor = val;
            siguiente = null;
        }
    }
}