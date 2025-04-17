namespace DS
{
    public class BlockChain
    {
        
        //INSTANCIAR
        private static BlockChain? _instance;
        public static BlockChain Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new BlockChain();
                }
                return _instance;
            }
        }

        public List<NodoBlockChain> Chain { get; private set; }

        public BlockChain()
        {
            Chain = new List<NodoBlockChain>();
            CrearBloqueGenesis();
        }

        public void addUser(Usuarios user)
        {
            if(Chain.Count == 0)
            {
                throw new InvalidOperationException("Debe existir el bloque Genesis");
            }

            if(BuscarUsuarioID(user.id) != null)
            {
                throw new Exception("ID del usuario ya existe");
            }

            int index = Chain[0].Index + 1;
            string? prevHash = Chain[0].Hash;
            var newBlock = new NodoBlockChain(index, user, prevHash);
            Chain.Add(newBlock);
        }

        public Usuarios? BuscarUsuarioID(int id)
        {
            foreach(var nodo in Chain)
            {
                if(nodo.Data.id == id)
                {
                    return nodo.Data;
                }
            }
            return null;
        }

        public Usuarios? BuscarUsuario(string correo, string contraseña)
        {
            foreach(var nodo in Chain)
            {
                if((nodo.Data.correo == correo) && (nodo.Data.contrasenia == contraseña))
                {
                    return nodo.Data;
                }
            }
            return null;
        }

        private void CrearBloqueGenesis()
        {
            var genesisBlock = new NodoBlockChain(
                index: 0,
                user: null,
                prevHash: "0000"
            );

            Chain.Add(genesisBlock);
        }

        public void Imprimir()
        {
            foreach(var nodo in Chain)
            {
                Console.WriteLine($"ID: {nodo.Data.id}, Nombre: {nodo.Data.nombres}, Apellido: {nodo.Data.apellidos}, Correo: {nodo.Data.correo}, Edad: {nodo.Data.edades}, Contraseña: {nodo.Data.contrasenia}");
            }
        }

    }
}