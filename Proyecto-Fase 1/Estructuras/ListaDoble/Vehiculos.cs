namespace DoubleList
{
    public class Vehiculos
    {
        //Características del Vehiculo
        public int id{ get; set; }
        public int id_user{ get; set; }
        public string marca{ get; set; }
        public string modelo{ get; set; }
        public string placa{ get; set; }

        //Constrcutor
        public Vehiculos(int Id, int idUsuario, string Marca, string Modelo, string Placa)
        {
            id = Id;
            id_user = idUsuario;
            marca = Marca;
            modelo = Modelo;
            placa = Placa;
        }
    }
}