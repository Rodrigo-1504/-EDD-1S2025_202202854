using System.Security.Cryptography;
using System.Text;

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
            var usuarioQuemado = new Usuarios(-1, "", "", "", 0, "");
            
            var genesisBlock = new NodoBlockChain(0, usuarioQuemado, "0000");
            Chain.Insert(0, genesisBlock);
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
            string prevHash = Chain[0].Hash;
            user.contrasenia = EncriptarSHA256(user.contrasenia);
            var newBlock = new NodoBlockChain(index, user, prevHash);
            Chain.Insert(0, newBlock);
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
            string contraseñaEncriptada = EncriptarSHA256(contraseña);

            foreach(var nodo in Chain)
            {
                if((nodo.Data.correo == correo) && (nodo.Data.contrasenia == contraseñaEncriptada))
                {
                    return nodo.Data;
                }
            }
            return null;
        }

        private static string EncriptarSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        
        public void Imprimir()
        {
            for (int i = Chain.Count - 1; i >= 0; i--)
            {
                var nodo = Chain[i];
                string contraseniaAcortada = nodo.Data.contrasenia.Length > 8 
                    ? nodo.Data.contrasenia.Substring(0, 8) + "..." 
                    : nodo.Data.contrasenia;
                    
                Console.WriteLine($"ID: {nodo.Data.id}, Nombre: {nodo.Data.nombres}, Apellido: {nodo.Data.apellidos}, Correo: {nodo.Data.correo}, Edad: {nodo.Data.edades}, Contraseña: {contraseniaAcortada}");
            }
        }

        public string GenerarDot()
        {
            if(Chain.Count == 0)
            {
                return "digraph G{\n\tnode[shape=record, style=filled, fontname=\"Arial\"];}";
            }

            string graphviz = "digraph {\n";
            graphviz += "\tnode[shape=record, style=filled];\n";

            for(int i = 0; i < Chain.Count; i++)
            {
                NodoBlockChain block = Chain[i];
                Usuarios user = block.Data;

                string hashShort = block.Hash.Substring(0, 6);
                string prevHashShort = block.PreviousHash.Length >= 6 ? block.PreviousHash.Substring(0, 6) : block.PreviousHash;

                graphviz += $"\tBlock{i} [label=\"{{ INDEX: {block.Index} | TIMESTAP: {block.TimeStap} | ID: {user.id} | Nombre: {user.nombres} | Contraseña: {user.contrasenia} | HASH: {hashShort} | PrevHASH: {prevHashShort} }}\"]\n;";             

                if(i > 0)
                {
                    graphviz += $"\tBlock{i} -> Block{i-1};\n";
                }
            }

            graphviz += "}\n";
            return graphviz;
        }

    }
}