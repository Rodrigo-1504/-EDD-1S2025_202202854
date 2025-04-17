namespace DS
{
    public class Repuestos
    {
        //CARACTERÍSTICAS DEL REPUESTO
        public int id { get; set; }
        public string repuesto { get; set; }
        public string detalles { get; set; }
        public double costo { get; set; }

        //CONSTRUCTOR
        public Repuestos(int ID, string Repuesto, string Detalles, double Costo)
        {
            id = ID;
            repuesto = Repuesto;
            detalles = Detalles;
            costo = Costo;
        }
    }
}