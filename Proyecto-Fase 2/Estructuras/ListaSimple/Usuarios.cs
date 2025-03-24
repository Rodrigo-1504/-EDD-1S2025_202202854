namespace Structures
{
    public class Usuarios
    {
        //CARATERÍSTICAS DEL USUARIO
        public int id{ get; set; }
        public string nombres{ get; set; }
        public string apellidos{ get; set; }
        public string correo{ get; set; }
        public string contrasenia { get; set; }
        public int edades { get; set; }
        
        //CONSTRUCTOR
        public Usuarios(int ID, string names, string lastnames, string mail, string password, int edad)
        {
            id = ID;
            nombres = names;
            apellidos = lastnames;
            correo = mail;
            contrasenia = password;
            edades = edad;
        }
    }
}