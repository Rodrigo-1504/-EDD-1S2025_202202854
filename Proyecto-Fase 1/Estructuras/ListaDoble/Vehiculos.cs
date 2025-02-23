namespace List
{
    public class Vehiculos
    {
        //Características del Vehiculo
        public int id{ get; set; }
        public int id_user{ get; set; }
        public string marca{ get; set; }
        public int modelo{ get; set; }
        public string placa{ get; set; }

        //Constrcutor
        public Vehiculos(int Id, int idUsuario, string Marca, int Modelo, string Placa)
        {
            id = Id;
            id_user = idUsuario;
            marca = Marca;
            modelo = Modelo;
            placa = Placa;
        }
    }
}