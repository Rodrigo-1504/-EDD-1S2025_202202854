namespace Structures
{
    public class Facturas
    {
        public int id { get; set; }
        public int id_Servicio { get; set; }
        public double total { get; set; }


        public Facturas(int ID, int Id_Services, double Total)
        {
            id = ID;
            id_Servicio = Id_Services;
            total = Total;
        }
    }
}