using Gtk;
using DS;
using System;

namespace Interfaces3
{
    public class Login : Window
    {
        // Singleton para la ventana de inicio de sesión
        private static Login _instance;
        private Entry mailEntry, passwordEntry;

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

        public Login() : base("Inicio de Sesión") // Nombre más descriptivo
        {
            try
            {
                ConfigureWindow();
                Add(CreateMainContainer());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar ventana: {ex.Message}");
                ShowErrorMessage("Error al inicializar la ventana de login");
            }
        }

        private void ConfigureWindow()
        {
            SetDefaultSize(400, 250);
            SetPosition(WindowPosition.Center);
        }

        private VBox CreateMainContainer()
        {
            var mainContainer = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            try
            {
                mainContainer.PackStart(CreateEntryContainer("Correo:", out mailEntry), true, true, 0);
                mainContainer.PackStart(CreateEntryContainer("Contraseña:", out passwordEntry), true, true, 0);
                
                var loginButton = new Button("Iniciar Sesión") { Sensitive = true };
                loginButton.Clicked += OnLoginButtonClicked;
                mainContainer.PackStart(loginButton, true, true, 0);

                // Configurar campo de contraseña
                passwordEntry.Visibility = false; // Ocultar caracteres
                passwordEntry.InvisibleChar = '•'; // Carácter de reemplazo
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear interfaz: {ex.Message}");
            }

            return mainContainer;
        }

        private VBox CreateEntryContainer(string labelText, out Entry entry)
        {
            var container = new VBox { Spacing = 5 };
            entry = new Entry();

            container.PackStart(new Label(labelText), false, false, 0);
            container.PackStart(entry, true, true, 0);

            return container;
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
                {
                    ShowErrorMessage("Por favor complete todos los campos");
                    return;
                }

                var listaUsuarios = BlockChain.Instance;
                var user = listaUsuarios.BuscarUsuario(mailEntry.Text, passwordEntry.Text);

                if (user == null)
                {
                    ShowErrorMessage("Credenciales incorrectas");
                    return;
                }

                HandleSuccessfulLogin(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en login: {ex.Message}");
                ShowErrorMessage("Error durante el proceso de autenticación");
            }
        }

        private void HandleSuccessfulLogin(Usuarios user)
        {
            try
            {
                if (IsAdminUser(user))
                {
                    ManejoSesion.Login(user.id, user.correo, true);
                    ShowWindow(Opciones.Instance);
                }
                else
                {
                    ManejoSesion.Login(user.id, user.correo, false);
                    //ShowWindow(OpcionesUsuario.Instance);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al manejar login exitoso: {ex.Message}");
                throw;
            }
            
        }

        private bool IsAdminUser(Usuarios user)
        {
            return user.correo.Equals("admin@usac.com", StringComparison.Ordinal) && 
                   user.contrasenia.Equals("admin123", StringComparison.Ordinal);
        }

        private void ShowWindow(Window window)
        {
            window.DeleteEvent += OnWindowDelete;
            window.ShowAll();
            Hide();
        }

        private void ShowErrorMessage(string message)
        {
            try
            {
                using (var dialog = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Error,
                    ButtonsType.Ok,
                    message))
                {
                    dialog.Run();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al mostrar mensaje: {ex.Message}");
            }
        }

        private static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            try
            {
                ((Window)sender).Hide();
                args.RetVal = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cerrar ventana: {ex.Message}");
            }
        }
    }
}