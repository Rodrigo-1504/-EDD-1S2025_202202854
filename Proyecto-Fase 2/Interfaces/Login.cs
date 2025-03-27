using Gtk;
using Structures;
using Interfaces2;

namespace Interfaces2
{
    public class Login : Window
    {
        // Singleton para la ventana de inicio de sesión
        private static Login _instance;

        public static Login Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Login();
                }
                return _instance;
            }
        }

        // Campos para las entradas de texto
        private Entry mailEntry, passwordEntry;

        // Constructor
        public Login() : base("Login")
        {
            // Configuración de la ventana
            SetDefaultSize(400, 250);
            SetPosition(WindowPosition.Center);

            // Crear y configurar el contenedor principal
            VBox mainContainer = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            // Crear y configurar contenedores para correo y contraseña
            VBox mailContainer = CreateEntryContainer("Correo:", out mailEntry);
            VBox passwordContainer = CreateEntryContainer("Contraseña:", out passwordEntry);

            // Botón de inicio de sesión
            Button loginButton = new Button("Login");
            loginButton.Clicked += OnLoginButtonClicked;

            // Agregar widgets al contenedor principal
            mainContainer.PackStart(mailContainer, true, true, 0);
            mainContainer.PackStart(passwordContainer, true, true, 0);
            mainContainer.PackStart(loginButton, true, true, 0);

            // Añadir el contenedor principal a la ventana
            Add(mainContainer);
        }

        // Método para crear un contenedor con una etiqueta y una entrada de texto
        private VBox CreateEntryContainer(string labelText, out Entry entry)
        {
            VBox container = new VBox
            {
                Spacing = 5
            };

            Label label = new Label(labelText);
            entry = new Entry();

            container.PackStart(label, false, false, 0);
            container.PackStart(entry, true, true, 0);

            return container;
        }

        // Método para manejar el evento de clic en el botón de inicio de sesión
        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Instanciar la lista de usuarios
            ListaSimple listaUsuarios = ListaSimple.Instance;

            // Buscar al usuario por correo y contraseña
            Usuarios user = listaUsuarios.BuscarUsuario(mailEntry.Text, passwordEntry.Text);

            if (user != null)
            {
                if(user.correo == "admin@usac.com" && user.contrasenia == "admin123")
                {
                    // Mostrar la ventana de opciones de administrador
                    Opciones opciones = Opciones.Instance;
                    opciones.DeleteEvent += OnWindowDelete;
                    opciones.ShowAll();
                    this.Hide();
                }
                else
                {
                    manejoSesion.Login(user.id, user.correo, false);
                    OpcionesUsuario opciones2 = OpcionesUsuario.Instance;
                    opciones2.DeleteEvent += OnWindowDelete;
                    opciones2.ShowAll();
                    this.Hide();
                }
                
            }
            else
            {
                // Usuario no encontrado
                ShowErrorMessage("Error, correo o contraseña incorrectas");
            }
        }

        // Método para mostrar un mensaje de error
        private void ShowErrorMessage(string message)
        {
            MessageDialog errorDialog = new MessageDialog(
                this,
                DialogFlags.Modal,
                MessageType.Info,
                ButtonsType.Ok,
                message
            );

            errorDialog.Run();
            errorDialog.Destroy();
        }

        // Método para manejar el evento de cierre de la ventana
        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            Application.Quit();
            args.RetVal = true;
        }
    }
}