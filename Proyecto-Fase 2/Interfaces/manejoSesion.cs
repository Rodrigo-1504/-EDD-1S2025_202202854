using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Structures;

namespace Interfaces2
{
    public static class manejoSesion
    {
        public static int currentUserId { get; set; }
        public static string currentUserMail { get; set; }
        public static bool isAdmin { get; set; }
        public static DateTime LoginTime { get; private set; }
        public static readonly string logFilePath = "Reportes/accesos.json";

        public static void Login(int userId, string email, bool admin)
        {
            currentUserId = userId;
            currentUserMail = email;
            isAdmin = admin;
            LoginTime = DateTime.Now;

            // Registrar el inicio de sesión
            LogAccess("Entrada");
        }

        public static void Logout()
        {
            // Registrar el cierre de sesión
            LogAccess("Salida");

            // Limpiar datos
            currentUserId = 0;
            currentUserMail = null;
            isAdmin = false;
        }

        public static void LogAccess(string action)
        {
            try
            {
                // Crear directorio si no existe
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

                // Crear objeto de log
                var logEntry = new
                {
                    usuario = currentUserMail,
                    accion = action,
                    fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff")
                };

                // Leer logs existentes
                List<object> logs = new List<object>();
                if (File.Exists(logFilePath))
                {
                    string jsonData = File.ReadAllText(logFilePath);
                    logs = JsonSerializer.Deserialize<List<object>>(jsonData) ?? new List<object>();
                }

                // Agregar nuevo log
                logs.Add(logEntry);

                // Escribir archivo JSON
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(logs, options);
                File.WriteAllText(logFilePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar log de acceso: {ex.Message}");
            }
        }

        // Método para obtener todos los logs (para el administrador)
        public static List<Dictionary<string, string>> GetAccessLogs()
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    string jsonData = File.ReadAllText(logFilePath);
                    return JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonData) ?? new List<Dictionary<string, string>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer logs de acceso: {ex.Message}");
            }
            return new List<Dictionary<string, string>>();
        }
    }
}