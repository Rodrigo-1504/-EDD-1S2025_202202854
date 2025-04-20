using Gtk;
using DS;
using System;

namespace Interfaces3
{
    public class GenerarServicios : Window
    {
        // Instancias de las listas
        private readonly ArbolBST listasServicios = ArbolBST.Instance;
        //private readonly ArbolB listaFacturas = ArbolB.Instance;
        private readonly ArbolAVL listaRepuestos = ArbolAVL.Instance;
        
        // Entradas de texto
        private Entry idEntry, replacementEntry, idCarEntry, detailsEntry, costEntry;
        private int idFactura = 0;
        
        // Singleton para la ventana de generar servicios
        private static GenerarServicios _instance;

        public static GenerarServicios Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GenerarServicios();
                }
                return _instance;
            }
        }

        // Constructor
        public GenerarServicios() : base("Services")
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
                Button saveButton = CreateButton("Guardar", generarServicio, 5, 5);
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
                Label idReplacement = new Label("Id Repuesto") { MarginBottom = 20 };
                Label idVehiculo = new Label("Id Vehiculo") { MarginBottom = 20 };
                Label details = new Label("Detalles") { MarginBottom = 20 };
                Label cost = new Label("Costo") { MarginBottom = 20 };

                container.PackStart(idLabel, false, false, 0);
                container.PackStart(idReplacement, false, false, 0);
                container.PackStart(idVehiculo, false, false, 0);
                container.PackStart(details, false, false, 0);
                container.PackStart(cost, false, false, 0);
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
                replacementEntry = new Entry { MarginBottom = 5 };
                idCarEntry = new Entry { MarginBottom = 5 };
                detailsEntry = new Entry { MarginBottom = 5 };
                costEntry = new Entry { MarginBottom = 5 };

                container.PackStart(idEntry, false, false, 0);
                container.PackStart(replacementEntry, false, false, 0);
                container.PackStart(idCarEntry, false, false, 0);
                container.PackStart(detailsEntry, false, false, 0);
                container.PackStart(costEntry, false, false, 0);
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
        private void generarServicio(object sender, EventArgs e)
        {
            try
            {
                // Validar campos vacíos
                if (string.IsNullOrWhiteSpace(idEntry.Text) || 
                    string.IsNullOrWhiteSpace(replacementEntry.Text) || 
                    string.IsNullOrWhiteSpace(idCarEntry.Text) || 
                    string.IsNullOrWhiteSpace(detailsEntry.Text) || 
                    string.IsNullOrWhiteSpace(costEntry.Text))
                {
                    ShowErrorMessage("Todos los campos son obligatorios.");
                    return;
                }

                // Buscar repuesto
                NodoAVL buscarRepuesto = listaRepuestos.Buscar(Convert.ToInt32(replacementEntry.Text));
                if (buscarRepuesto == null)
                {
                    ShowErrorMessage("El repuesto especificado no existe.");
                    return;
                }

                // Convertir valores
                int id = Convert.ToInt32(idEntry.Text);
                int idRepuesto = Convert.ToInt32(replacementEntry.Text);
                int idVehiculo = Convert.ToInt32(idCarEntry.Text);
                string detalles = detailsEntry.Text;
                double costoServicio = Convert.ToDouble(costEntry.Text);
                double costoRepuesto = buscarRepuesto.repuestos.costo;
                double total = costoServicio + costoRepuesto;

                // Agregar servicio
                listasServicios.agregarServicios(new Servicios(
                    id, idRepuesto, idVehiculo, detalles, costoServicio
                ));

                Console.WriteLine("\n--- LISTA DE SERVICIOS---");
                listasServicios.RecorridoEnOrden();

                // Generar factura
                /*
                idFactura++;
                listaFacturas.Insertar(new Facturas(idFactura, id, total));

                Console.WriteLine("\n--- LISTA DE FACTURAS ---");
                listaFacturas.ImprimirEnOrden();
                */
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
                Console.WriteLine($"Error al generar servicio: {ex.Message}");
                ShowErrorMessage("Ocurrió un error al generar el servicio.");
                //listaFacturas.VerificarIntegridadCompleta();
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