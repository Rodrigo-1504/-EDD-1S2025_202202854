using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace DS
{
    public class Facturas
    {
        public int id { get; set; }
        public int id_Servicio { get; set; }
        public double total { get; set; }
        public DateTime fechas { get; set; }
        public string metodoPago { get; set; }

        public Facturas(int ID, int Id_Services, double Total, DateTime fecha, string pago)
        {
            id = ID;
            id_Servicio = Id_Services;
            total = Total;
            fechas = fecha;
            metodoPago = pago;
        }

        public string getHash()
        {
            string data = JsonConvert.SerializeObject(this);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        /*
        //Metodo de pago
        private static string MetodoPago()
        {
            string [] metodos = {"Transferencia", "Efectivo", "Tarjeta"};
            Random random = new Random();
            int indiceAleatorio = random.Next(metodos.Length);
            return metodos[indiceAleatorio];
        }
        */
    }
}