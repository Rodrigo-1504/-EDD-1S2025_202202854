using Gtk;
using Structures;
using System;

namespace Interfaces2
{
    public class InsertarVehiculo : Window // Cambiado a PascalCase según convenciones
    {
        // Instancias de las listas
        private readonly ListaDoble listaVehiculos = ListaDoble.Instance;

        // Entradas de texto
        private Entry idEntry, marcaEntry, modeloEntry, placaEntry;

        // Singleton para la ventana
        private static InsertarVehiculo _instance;

        public static InsertarVehiculo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InsertarVehiculo();
                }
                return _instance;
            }
        }

        // Constructor
        public InsertarVehiculo() : base("Nuevo Vehículo") // Mejorado título
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
                Console.WriteLine($"Error al inicializar ventana: {ex.Message}");
                ShowErrorMessage("Error al inicializar la ventana de vehículos");
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
                Button saveButton = CreateButton("Guardar", GuardarVehiculo, 5, 5);
                Button backButton = CreateButton("Regresar", GoBack, 5, 5);

                // Agregar widgets al contenedor principal
                container.PackStart(inputFields, true, true, 0);
                container.PackStart(saveButton, true, true, 0);
                container.PackStart(backButton, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear interfaz: {ex.Message}");
                ShowErrorMessage("Error al crear la interfaz");
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
                Label marcaLabel = new Label("Marca") { MarginBottom = 20 };
                Label modeloLabel = new Label("Modelo") { MarginBottom = 20 };
                Label placaLabel = new Label("Placa") { MarginBottom = 20 };

                container.PackStart(idLabel, false, false, 0);
                container.PackStart(marcaLabel, false, false, 0);
                container.PackStart(modeloLabel, false, false, 0);
                container.PackStart(placaLabel, false, false, 0);
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
                marcaEntry = new Entry { MarginBottom = 5 };
                modeloEntry = new Entry { MarginBottom = 5 };
                placaEntry = new Entry { MarginBottom = 5 };

                container.PackStart(idEntry, false, false, 0);
                container.PackStart(marcaEntry, false, false, 0);
                container.PackStart(modeloEntry, false, false, 0);
                container.PackStart(placaEntry, false, false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear entradas: {ex.Message}");
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
        private void GoBack(object sender, EventArgs e)
        {
            try
            {
                OpcionesUsuario opciones = OpcionesUsuario.Instance;
                opciones.DeleteEvent += OnWindowDelete;
                opciones.ShowAll();
                this.Hide();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al regresar: {ex.Message}");
                ShowErrorMessage("Error al intentar regresar al menú");
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

        // Método para guardar un vehículo
        private void GuardarVehiculo(object sender, EventArgs e)
        {
            try
            {
                // Validar campos vacíos
                if (string.IsNullOrWhiteSpace(idEntry.Text) || 
                    string.IsNullOrWhiteSpace(marcaEntry.Text) || 
                    string.IsNullOrWhiteSpace(modeloEntry.Text) || 
                    string.IsNullOrWhiteSpace(placaEntry.Text))
                {
                    ShowErrorMessage("Todos los campos son obligatorios");
                    return;
                }

                // Validar formato numérico
                if (!int.TryParse(idEntry.Text, out int id))
                {
                    ShowErrorMessage("El ID debe ser un número válido");
                    return;
                }

                if (!int.TryParse(modeloEntry.Text, out int modelo))
                {
                    ShowErrorMessage("El modelo debe ser un número válido");
                    return;
                }

                // Crear y agregar vehículo
                var nuevoVehiculo = new Vehiculos(
                    id,
                    ManejoSesion.CurrentUserId,
                    marcaEntry.Text,
                    modelo,
                    placaEntry.Text
                );

                listaVehiculos.AgregarVehiculos(nuevoVehiculo);
                listaVehiculos.Imprimir();

                // Mostrar confirmación y limpiar campos
                ShowErrorMessage("Vehículo registrado correctamente");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar vehículo: {ex.Message}");
                ShowErrorMessage("Error al registrar el vehículo");
            }
        }

        private void LimpiarCampos()
        {
            idEntry.Text = "";
            marcaEntry.Text = "";
            modeloEntry.Text = "";
            placaEntry.Text = "";
        }

        // Método para mostrar un mensaje de error
        private void ShowErrorMessage(string message)
        {
            try
            {
                using (MessageDialog dialog = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Info,
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
    }
}