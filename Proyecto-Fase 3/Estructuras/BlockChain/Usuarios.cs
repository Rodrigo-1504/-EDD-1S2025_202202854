namespace DS
{
    public class Usuarios
    {
        //CARATERÍSTICAS DEL USUARIO
        public int id{ get; set; }
        public string nombres{ get; set; }
        public string apellidos{ get; set; }
        public string correo{ get; set; }
        public int edades { get; set; }
        public string contrasenia { get; set; }
        
        //CONSTRUCTOR
        public Usuarios(int ID, string names, string lastnames, string mail, int edad, string password)
        {
            id = ID;
            nombres = names;
            apellidos = lastnames;
            correo = mail;
            edades = edad;
            contrasenia = password;
        }
    }
}