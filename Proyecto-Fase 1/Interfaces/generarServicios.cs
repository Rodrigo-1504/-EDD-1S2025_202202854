using Gtk;
using List;
using System;

namespace Interfaces
{
    public class generarServicios : Window
    {
        // Instancias de las listas
        private ListaDoble listaVehiculos = ListaDoble.Instance;
        private ListaCircular listaRepuestos = ListaCircular.Instance;
        private Cola listaServicios = Cola.Instance;
        private Pila listaFacturas = Pila.Instance;
        private Matriz bitacora = Matriz.Instance;

        // Entradas de texto
        private Entry idEntry, replacementEntry, idCarEntry, detailsEntry, costEntry;

        // Singleton para la ventana de generar servicios
        private static generarServicios _instance;

        public static generarServicios Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new generarServicios();
                }
                return _instance;
            }
        }

        // Constructor
        public generarServicios() : base("Services")
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
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            // Crear y configurar los campos de entrada
            HBox inputFields = CreateInputFieldsContainer();
            Button saveButton = CreateButton("Guardar", generarServicio, 5, 5);
            Button backButton = CreateButton("Regresar", goBack, 5, 5);

            // Agregar widgets al contenedor principal
            container.PackStart(inputFields, true, true, 0);
            container.PackStart(saveButton, true, true, 0);
            container.PackStart(backButton, true, true, 0);

            return container;
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
            Label idReplacement = new Label("Id Repuesto") { MarginBottom = 20 };
            Label idVehiculo = new Label("Id Vehiculo") { MarginBottom = 20 };
            Label details = new Label("Detalles") { MarginBottom = 20 };
            Label cost = new Label("Costo") { MarginBottom = 20 };

            container.PackStart(idLabel, false, false, 0);
            container.PackStart(idReplacement, false, false, 0);
            container.PackStart(idVehiculo, false, false, 0);
            container.PackStart(details, false, false, 0);
            container.PackStart(cost, false, false, 0);

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
            replacementEntry = new Entry { MarginBottom = 5 };
            idCarEntry = new Entry { MarginBottom = 5 };
            detailsEntry = new Entry { MarginBottom = 5 };
            costEntry = new Entry { MarginBottom = 5 };

            container.PackStart(idEntry, false, false, 0);
            container.PackStart(replacementEntry, false, false, 0);
            container.PackStart(idCarEntry, false, false, 0);
            container.PackStart(detailsEntry, false, false, 0);
            container.PackStart(costEntry, false, false, 0);

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
            OpcionesAdmin opciones = OpcionesAdmin.Instance;
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
        private void generarServicio(object sender, EventArgs e)
        {
            try
            {
                Repuestos buscarRepuesto = listaRepuestos.buscarRepuesto(Convert.ToInt32(replacementEntry.Text));
                Vehiculos buscarVehiculo = listaVehiculos.buscarVehiculo(Convert.ToInt32(idCarEntry.Text));

                if (buscarRepuesto != null && buscarVehiculo != null)
                {
                    listaServicios.agregarServicios(new Servicios(
                        Convert.ToInt32(idEntry.Text),
                        buscarRepuesto.id,
                        buscarVehiculo.id,
                        detailsEntry.Text,
                        Convert.ToInt32(costEntry.Text)
                    ));

                    Console.WriteLine("\n---LISTA DE SERVICIOS--");
                    listaServicios.imprimir();

                    double costoServicio = Convert.ToDouble(costEntry.Text);
                    double costoRepuesto = buscarRepuesto.costo;

                    double total = costoServicio + costoRepuesto;

                    int idFactura = Convert.ToInt32(idEntry.Text);

                    listaFacturas.agregarFactura(new Facturas(idFactura, idFactura, total));

                    Console.WriteLine("\n---FACTURAS---");
                    listaFacturas.imprimir();

                    bitacora.insertar(buscarVehiculo.id, buscarRepuesto.id, detailsEntry.Text);
                    bitacora.mostrarMatriz();

                }
                else
                {
                    ShowErrorMessage("El repuesto o el vehículo no fueron encontrados");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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