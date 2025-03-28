namespace Structures
{
    public class NodoB
    {
        public static int orden = 5;
        public static int max_Claves = orden - 1;
        public static int min_Claves = (orden/2) - 1;

        public List<Facturas> claves { get; set; }
        public List<NodoB> hijos { get; set; }
        public bool hoja { get; set; }

        public NodoB()
        {
            claves = new List<Facturas>(max_Claves);
            hijos = new List<NodoB>(orden);
            hoja = true;
        }

        public bool Lleno()
        {
            return claves.Count >= max_Claves;
        }

        public bool MinimoClaves()
        {
            return claves.Count >= min_Claves;
        }
    }
}