namespace List{
    public class Usuarios
    {
        //Características del Usuario
        public int id{ get; set; }
        public string nombres{ get; set; }
        public string apellidos{ get; set; }
        public string correo{ get; set; }
        public string contraseña { get; set; }
        
        //Constructor
        public Usuarios(int ID, string names, string lastnames, string mail, string password)
        {
            id = ID;
            nombres = names;
            apellidos = lastnames;
            correo = mail;
            contraseña = password;
        }
    }
}