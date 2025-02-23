namespace List
{
    public class Facturas
    {
        //Características de las Facturas
        public int id { get; set; }
        public int id_Orden { get; set; }
        public double total { get; set; }

        //Constructor
        public Facturas(int ID, int ID_Orden, double Total)
        {
            id = ID;
            id_Orden = ID_Orden;
            total = Total;
        }
    }
}