namespace Circular
{
    public class Repuestos
    {
        //Características del Repuesto
        public int id{ get; set; }
        public string repuesto{ get; set; }
        public string detalles{ get; set; }
        public int costo{ get; set; }

        //Constructor
        public Repuestos(int ID, string Repuesto, string Detalles, int Costo)
        {
            id = ID;
            repuesto = Repuesto;
            detalles = Detalles;
            costo = Costo;
        }
    }
}