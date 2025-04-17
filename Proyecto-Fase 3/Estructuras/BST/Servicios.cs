namespace DS
{
    public class Servicios
    {
        //Características
        public int id{ get; set; }
        public int id_Repuesto { get; set; }
        public int id_Vehiculo { get; set; }
        public string detalles { get; set; }
        public double costo { get; set; }

        //Constructor
        public Servicios(int ID, int id_Repair, int id_Vehicle, string details, double cost)
        {
            id = ID;
            id_Repuesto = id_Repair;
            id_Vehiculo = id_Vehicle;
            detalles = details;
            costo = cost;
        }
    }
}