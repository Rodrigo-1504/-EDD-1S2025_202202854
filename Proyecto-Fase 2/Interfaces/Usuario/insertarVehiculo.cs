using Gtk;
using Structures;
using System;

namespace Interfaces2
{
    public class insertarVehiculo : Window
    {
        // Instancias de las listas
        ListaDoble listaVehiculos = ListaDoble.Instance;

        // Entradas de texto
        private Entry idEntry, marcaEntry, modeloEntry, placaEntry;

        // Singleton para la ventana de generar servicios
        private static insertarVehiculo _instance;

        public static insertarVehiculo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new insertarVehiculo();
                }
                return _instance;
            }
        }

        // Constructor
        public insertarVehiculo() : base("new Vehicles")
        {
            // Configuración de la ventana
            SetDefaultSize(500, 300);
            SetPosition(WindowPosition.Center);

            // Crear y configurar el contenedor principal
            VBox mainContainer = CreateMainContainer();
            Add(mainContainer);
        }

        // Método para crear el contenedor principal
        private VBox CreateMainContainer()
        {
            try{
                VBox container = new VBox
                {
                    BorderWidth = 20,
                    Spacing = 10
                };
                
                // Crear y configurar los campos de entrada
                HBox inputFields = CreateInputFieldsContainer();
                Button saveButton = CreateButton("Guardar", guardarVehiculo, 5, 5);
                Button backButton = CreateButton("Regresar", goBack, 5, 5);

                // Agregar widgets al contenedor principal
                container.PackStart(inputFields, true, true, 0);
                container.PackStart(saveButton, true, true, 0);
                container.PackStart(backButton, true, true, 0);

                return container;
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                return null;
            }
        }

        // Método para crear el contenedor de campos de entrada
        private HBox CreateInputFieldsContainer()
        {
            HBox container = new HBox();

            // Crear y configurar los labels
            VBox labels = CreateLabelsContainer();
            // Crear y configurar los campos de entrada
            VBox entries = CreateEntriesContainer();

            // Agregar widgets al contenedor
            container.PackStart(labels, true, true, 0);
            container.PackStart(entries, true, true, 0);

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

            Label idLabel = new Label("Id") { MarginTop = 15, MarginBottom = 20 };
            Label marcaLabel = new Label("Marca") { MarginBottom = 20 };
            Label modeloLabel = new Label("Modelo") { MarginBottom = 20 };
            Label placaLabel = new Label("Placa") { MarginBottom = 20 };

            container.PackStart(idLabel, false, false, 0);
            container.PackStart(marcaLabel, false, false, 0);
            container.PackStart(modeloLabel, false, false, 0);
            container.PackStart(placaLabel, false, false, 0);

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

            idEntry = new Entry { MarginBottom = 5 };
            marcaEntry = new Entry { MarginBottom = 5 };
            modeloEntry = new Entry { MarginBottom = 5 };
            placaEntry = new Entry { MarginBottom = 5 };

            container.PackStart(idEntry, false, false, 0);
            container.PackStart(marcaEntry, false, false, 0);
            container.PackStart(modeloEntry, false, false, 0);
            container.PackStart(placaEntry, false, false, 0);

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
            OpcionesUsuario opciones = OpcionesUsuario.Instance;
            opciones.DeleteEvent += OnWindowDelete;
            opciones.ShowAll();
            this.Hide();
        }

        // Método para manejar el evento de cierre de la ventana
        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            ((Window)sender).Hide();
            args.RetVal = true;
        }

        // Método para generar un servicio
        private void guardarVehiculo(object sender, EventArgs e)
        {
            try
            {
                listaVehiculos.AgregarVehiculos(new Vehiculos(
                    Convert.ToInt32(idEntry.Text),
                    manejoSesion.currentUserId,
                    marcaEntry.Text,
                    Convert.ToInt32(modeloEntry.Text),
                    placaEntry.Text
                ));

                listaVehiculos.Imprimir();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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
    }
}