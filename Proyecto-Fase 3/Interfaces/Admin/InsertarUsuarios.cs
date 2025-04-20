using Gtk;
using DS;
using System;
using Pango;

namespace Interfaces3
{
    public class InsertarUsuarios : Window
    {
        // Instancias de las listas
        BlockChain listaUsuarios = BlockChain.Instance;
        
        // Entradas de texto
        private Entry idEntry, nombreEntry, apellidoEntry, correoEntry, edadEntry, contraseñaEntry;
        
        // Singleton para la ventana de generar servicios
        private static InsertarUsuarios _instance;

        public static InsertarUsuarios Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InsertarUsuarios();
                }
                return _instance;
            }
        }

        // Constructor
        public InsertarUsuarios() : base("Insertar Usuarios")
        {
            try
            {
                // Configuración de la ventana
                SetDefaultSize(500, 300);
                SetPosition(WindowPosition.Center);

                // Crear y configurar el contenedor principal
                VBox mainContainer = CreateMainContainer();
                Add(mainContainer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar la ventana: {ex.Message}");
                ShowErrorMessage("Error al inicializar la ventana. Por favor reinicie la aplicación.");
            }
        }

        // Método para crear el contenedor principal
        private VBox CreateMainContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            try
            {
                // Crear y configurar los campos de entrada
                HBox inputFields = CreateInputFieldsContainer();
                Button saveButton = CreateButton("Guardar", generarUsuario, 5, 5);
                Button backButton = CreateButton("Regresar", goBack, 5, 5);

                // Agregar widgets al contenedor principal
                container.PackStart(inputFields, true, true, 0);
                container.PackStart(saveButton, true, true, 0);
                container.PackStart(backButton, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la interfaz: {ex.Message}");
                ShowErrorMessage("Error al crear la interfaz gráfica.");
            }

            return container;
        }

        // Método para crear el contenedor de campos de entrada
        private HBox CreateInputFieldsContainer()
        {
            HBox container = new HBox();

            try
            {
                // Crear y configurar los labels
                VBox labels = CreateLabelsContainer();
                // Crear y configurar los campos de entrada
                VBox entries = CreateEntriesContainer();

                // Agregar widgets al contenedor
                container.PackStart(labels, true, true, 0);
                container.PackStart(entries, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear campos de entrada: {ex.Message}");
            }

            return container;
        }

        // Método para crear el contenedor de labels
        private VBox CreateLabelsContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            try
            {
                Label idLabel = new Label("Id") { MarginTop = 15, MarginBottom = 20 };
                Label nombreLabel = new Label("Nombres") { MarginBottom = 20 };
                Label apellidoLabel = new Label("Apellidos") { MarginBottom = 20 };
                Label correoLabel = new Label("Correo") { MarginBottom = 20 };
                Label edadLabel = new Label("Edad") { MarginBottom = 20 };
                Label contraseñaLabel = new Label("Contraseña") { MarginBottom = 20 };

                container.PackStart(idLabel, false, false, 0);
                container.PackStart(nombreLabel, false, false, 0);
                container.PackStart(apellidoLabel, false, false, 0);
                container.PackStart(correoLabel, false, false, 0);
                container.PackStart(edadLabel, false, false, 0);
                container.PackStart(contraseñaLabel, false, false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear labels: {ex.Message}");
            }

            return container;
        }

        // Método para crear el contenedor de campos de entrada
        private VBox CreateEntriesContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            try
            {
                idEntry = new Entry { MarginBottom = 5 };
                nombreEntry = new Entry { MarginBottom = 5 };
                apellidoEntry = new Entry { MarginBottom = 5 };
                correoEntry = new Entry { MarginBottom = 5 };
                edadEntry = new Entry { MarginBottom = 5 };
                contraseñaEntry = new Entry { MarginBottom = 5 };

                container.PackStart(idEntry, false, false, 0);
                container.PackStart(nombreEntry, false, false, 0);
                container.PackStart(apellidoEntry, false, false, 0);
                container.PackStart(correoEntry, false, false, 0);
                container.PackStart(edadEntry, false, false, 0);
                container.PackStart(contraseñaEntry, false, false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear campos de entrada: {ex.Message}");
            }

            return container;
        }

        // Método para crear un botón con márgenes y manejador de eventos
        private Button CreateButton(string label, EventHandler handler, int marginTop, int marginBottom)
        {
            Button button = new Button(label)
            {
                MarginTop = marginTop,
                MarginBottom = marginBottom
            };
            button.Clicked += handler;
            return button;
        }

        // Método para manejar el evento de clic en el botón "Regresar"
        private void goBack(object sender, EventArgs e)
        {
            try
            {
                Opciones opciones = Opciones.Instance;
                opciones.DeleteEvent += OnWindowDelete;
                opciones.ShowAll();
                this.Hide();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al regresar: {ex.Message}");
                ShowErrorMessage("Error al intentar regresar al menú principal.");
            }
        }

        // Método para manejar el evento de cierre de la ventana
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

        // Método para generar un servicio
        private void generarUsuario(object sender, EventArgs e)
        {
            try
            {
                // Validar campos vacíos
                if (string.IsNullOrWhiteSpace(idEntry.Text) || 
                    string.IsNullOrWhiteSpace(nombreEntry.Text) || 
                    string.IsNullOrWhiteSpace(apellidoEntry.Text) || 
                    string.IsNullOrWhiteSpace(correoEntry.Text) || 
                    string.IsNullOrWhiteSpace(edadEntry.Text) ||
                    string.IsNullOrWhiteSpace(contraseñaEntry.Text))
                {
                    ShowErrorMessage("Todos los campos son obligatorios.");
                    return;
                }

                // Buscar usuarios
                var buscarUsuario = listaUsuarios.BuscarUsuarioID(Convert.ToInt32(idEntry.Text));
                if (buscarUsuario != null)
                {
                    ShowErrorMessage("El usuario especificado ya existe.");
                    return;
                }

                // Convertir valores
                int id = Convert.ToInt32(idEntry.Text);
                string name = nombreEntry.Text;
                string lastname = apellidoEntry.Text;
                string mail = correoEntry.Text;
                int age = Convert.ToInt32(edadEntry.Text);
                string password = contraseñaEntry.Text;

                // Agregar Usuario
                listaUsuarios.addUser(new Usuarios(id, name, lastname, mail, age, password));

                Console.WriteLine("\n--- LISTA DE USUARIOS---");
                listaUsuarios.Imprimir();

                LimpiarCampos();
            }
            catch (FormatException)
            {
                ShowErrorMessage("Los campos numéricos deben contener valores válidos.");
            }
            catch (OverflowException)
            {
                ShowErrorMessage("Los valores numéricos son demasiado grandes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar usuario: {ex.Message}");
            }
        }


        //Limpiar campos
        private void LimpiarCampos()
        {
            idEntry.Text = "";
            nombreEntry.Text = "";
            apellidoEntry.Text = "";
            correoEntry.Text = "";
            edadEntry.Text = "";
            contraseñaEntry.Text = "";
        }

        // Método para mostrar un mensaje de error
        private void ShowErrorMessage(string message)
        {
            try
            {
                using (MessageDialog errorDialog = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Error,
                    ButtonsType.Ok,
                    message))
                {
                    errorDialog.Run();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al mostrar mensaje de error: {ex.Message}");
            }
        }
    }
}