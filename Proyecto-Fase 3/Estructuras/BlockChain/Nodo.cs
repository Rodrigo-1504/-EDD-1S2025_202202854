using System.Security.Cryptography;
using System.Text;


namespace DS
{
    public class NodoBlockChain
    {
        public int Index { get; set; }
        public  DateTime TimeStap { get; set; }
        public Usuarios Data { get; set; }
        public int Nonce { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }

        public NodoBlockChain(int index, Usuarios user, string prevHash)
        {
            Index = index;
            TimeStap = DateTime.UtcNow;
            Data = user;
            Nonce = 0;
            PreviousHash = prevHash;
            Hash = CalcularHash();
        }

        private string GenerarHash()
        {
            string rawdata = $"{Index}{TimeStap:o}{SerializeUsuario(Data)}{Nonce}{PreviousHash}";
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawdata));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private string CalcularHash()
        {
            string hash;
            do
            {
                hash = GenerarHash();
                Nonce++;
            }while(!hash.StartsWith("0000"));
            return hash;
        }

        private string SerializeUsuario(Usuarios usuario)
        {

            if(usuario == null) return "genesisBlock";
            return $"{usuario.id}{usuario.nombres}{usuario.apellidos}{usuario.correo}{usuario.edades}{usuario.contrasenia}";
        }
    }
}