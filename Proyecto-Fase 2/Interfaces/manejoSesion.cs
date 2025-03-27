using System;
using Structures;

namespace Interfaces2
{
    public static class manejoSesion
    {
        public static int currentUserId { get; set; }
        public static string currentUserMail { get; set; }
        public static bool isAdmin { get; set; }
        public static DateTime LoginTime {get; private set; }


        public static void Login(int userId, string email, bool admin)
        {
            currentUserId = userId;
            currentUserMail = email;
            isAdmin = admin;
            LoginTime = DateTime.Now;

            //REGISTRAR EL INICIO DE SESION
            LogAccess("Entrada");
        }


        public static void Logout()
        {
            LogAccess("Salida");

            //LIMPIAR DATOS
            currentUserId = 0;
            currentUserMail = null;
            isAdmin = false;
        }

        public static void LogAccess(string action)
        {

        }
    }
}