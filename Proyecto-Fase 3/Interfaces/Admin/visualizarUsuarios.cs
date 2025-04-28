using Gtk;
using DS;
using System;
using Pango;

namespace Interfaces3
{
    public class visualizarUsuarios : Window
    {
        // Instancias de las listas
        BlockChain listaUsuarios = BlockChain.Instance;
        
        // Entradas de texto
        private Entry idEntry;
        private Label nombreLabel2, apellidoLabel2, correoLabel2, edadLabel2, contraseñaLabel2;
        
        // Singleton para la ventana de generar servicios
        private static visualizarUsuarios _instance;

        public static visualizarUsuarios Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new visualizarUsuarios();
                }
                return _instance;
            }
        }

        // Constructor
        public visualizarUsuarios() : base("Insertar Usuarios")
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
                Button saveButton = CreateButton("Buscar", buscarUsuario, 5, 5);
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
                VBox entries = CreateDynamicLabels();

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
        private VBox CreateDynamicLabels()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            try
            {
                idEntry = new Entry { MarginBottom = 20 };
                nombreLabel2 = new Label { MarginBottom = 20 };
                apellidoLabel2 = new Label { MarginBottom = 20 };
                correoLabel2 = new Label { MarginBottom = 20 };
                edadLabel2 = new Label { MarginBottom = 20 };
                contraseñaLabel2 = new Label { MarginBottom = 20 };

                container.PackStart(idEntry, false, false, 0);
                container.PackStart(nombreLabel2, false, false, 0);
                container.PackStart(apellidoLabel2, false, false, 0);
                container.PackStart(correoLabel2, false, false, 0);
                container.PackStart(edadLabel2, false, false, 0);
                container.PackStart(contraseñaLabel2, false, false, 0);
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
        private void buscarUsuario(object sender, EventArgs e)
        {
            try
            {
                // Validar campos vacíos
                if (string.IsNullOrWhiteSpace(idEntry.Text))
                {
                    ShowErrorMessage("El campo de ID es necesario.");
                    return;
                }

                // Buscar usuarios
                var buscarUsuario = listaUsuarios.BuscarUsuarioID(Convert.ToInt32(idEntry.Text));
                if (buscarUsuario == null)
                {
                    ShowErrorMessage("El usuario especificado no existe.");
                    return;
                }

                if (buscarUsuario.id  < 1)
                {
                    ShowErrorMessage("El usuario especificado no se puede ver.");
                    return;
                }

                nombreLabel2.Text = buscarUsuario.nombres;
                apellidoLabel2.Text = buscarUsuario.apellidos;
                correoLabel2.Text = buscarUsuario.correo;
                edadLabel2.Text = buscarUsuario.edades.ToString();
                contraseñaLabel2.Text = buscarUsuario.contrasenia;

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