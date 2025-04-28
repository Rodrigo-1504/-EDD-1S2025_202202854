using System.Security.Cryptography;
using System.Text;

namespace DS
{
    public class NodoMerkle
    {
        public string? Hash { get; set; }
        public NodoMerkle? izquierda { get; set; }
        public NodoMerkle? derecha { get; set; }
        public Facturas? facturas { get; set; }

        //NODOS PRINCIPALES
        public NodoMerkle(Facturas factura)
        {
            facturas = factura;
            Hash = factura.getHash();
            izquierda = null;
            derecha = null;
        }

        //NODOS HIJOS
        public NodoMerkle(NodoMerkle left, NodoMerkle right)
        {
            facturas = null;
            izquierda = left;
            derecha = right;
            Hash = CalcularHash(left.Hash, right.Hash);
        }


        private string CalcularHash(string leftHash, string rightHash)
        {
            string combined = leftHash + (rightHash ?? leftHash);
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                StringBuilder builder = new StringBuilder();
                foreach(byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}