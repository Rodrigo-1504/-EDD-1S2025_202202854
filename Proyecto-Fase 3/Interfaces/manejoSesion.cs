using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace Interfaces3
{
    public static class ManejoSesion
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new DateTimeConverter() }
        };

        public static int CurrentUserId { get; private set; }
        public static string CurrentUserMail { get; private set; }
        public static bool IsAdmin { get; private set; }
        public static DateTime LoginTime { get; private set; }
        public static string LogFilePath { get; } = Path.Combine("Reportes", "accesos.json");

        public static void Login(int userId, string email, bool admin)
        {
            CurrentUserId = userId;
            CurrentUserMail = email ?? throw new ArgumentNullException(nameof(email));
            IsAdmin = admin;
            LoginTime = DateTime.Now;

            LogAccess("Inicio de sesion");
        }

        public static void Logout()
        {
            if (string.IsNullOrEmpty(CurrentUserMail))
            {
                Console.WriteLine("No hay sesión activa para cerrar.");
                return;
            }

            try
            {
                LogAccess("Cierre de sesion");
                Console.WriteLine($"Sesión cerrada para {CurrentUserMail}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar cierre de sesión: {ex.Message}");
            }
            finally
            {
                ClearSession(); // Siempre limpia la sesión
            }
        }

        private static void ClearSession()
        {
            CurrentUserId = 0;
            CurrentUserMail = null;
            IsAdmin = false;
        }

        public static void LogAccess(string action)
        {
            try
            {
                if (string.IsNullOrEmpty(action))
                    throw new ArgumentException("La acción no puede estar vacía", nameof(action));

                EnsureLogDirectoryExists();

                var logEntry = new AccessLog
                {
                    Usuario = CurrentUserMail,
                    Accion = action,
                    Fecha = DateTime.Now
                };

                var logs = GetExistingLogs();
                logs.Add(logEntry);

                SaveLogsToFile(logs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar acceso: {ex.Message}");
                // Considerar agregar un sistema de reintentos o notificación
            }
        }

        private static void EnsureLogDirectoryExists()
        {
            var directory = Path.GetDirectoryName(LogFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static List<AccessLog> GetExistingLogs()
        {
            if (!File.Exists(LogFilePath))
                return new List<AccessLog>();

            try
            {
                string jsonData = File.ReadAllText(LogFilePath);
                return JsonSerializer.Deserialize<List<AccessLog>>(jsonData, _jsonOptions) 
                    ?? new List<AccessLog>();
            }
            catch (JsonException)
            {
                // Si el archivo está corrupto, crear uno nuevo
                File.Delete(LogFilePath);
                return new List<AccessLog>();
            }
        }

        private static void SaveLogsToFile(List<AccessLog> logs)
        {
            string jsonString = JsonSerializer.Serialize(logs, _jsonOptions);
            File.WriteAllText(LogFilePath, jsonString);
        }

        public static IReadOnlyList<AccessLog> GetAccessLogs()
        {
            try
            {
                return GetExistingLogs().AsReadOnly();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener logs: {ex.Message}");
                return new List<AccessLog>().AsReadOnly();
            }
        }

        public class AccessLog
        {
            public string Usuario { get; set; }
            public string Accion { get; set; }
            public DateTime Fecha { get; set; }
        }

        private class DateTimeConverter : JsonConverter<DateTime>
        {
            private const string Format = "yyyy-MM-dd HH:mm:ss.ff";

            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.ParseExact(reader.GetString(), Format, CultureInfo.InvariantCulture);
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
            }
        }
    }
}