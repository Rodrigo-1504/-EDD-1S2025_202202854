using Gtk;
using Structures;

namespace Interfaces2
{
    public class cancelarFactura : Window
    {
        // Instancia de la lista de usuarios
        private ArbolB listaFacturas = ArbolB.Instance;
        private ArbolBST listaServicios = ArbolBST.Instance;
        private ListaDoble listaVehiculos = ListaDoble.Instance;

        // Labels y Entries para Repuestos
        private Label idLabel, ordenLabel, totalLabel;
        private Label ordenLabel2, totalLabel2;
        private Entry idEntry;

        // Singleton para la ventana de manejo de usuarios
        private static cancelarFactura _instance;

        public static cancelarFactura Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new cancelarFactura();
                }
                return _instance;
            }
        }

        // Constructor
        public cancelarFactura() : base("Cancel bill")
        {
            // Configuración de la ventana
            SetDefaultSize(500, 400);
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

            // Crear y configurar los campos de entrada y botones
            HBox dataUsers = CreateDataRepairsContainer();
            Button updateButton = CreateButton("Pagar", pagarFactura, 5, 5);
            Button backButton = CreateButton("Regresar", goBack, 5, 5);

            // Agregar widgets al contenedor principal
            container.PackStart(dataUsers, true, true, 0);
            container.PackStart(updateButton, true, true, 0);
            container.PackStart(backButton, true, true, 0);

            return container;
        }

        // Método para crear el contenedor de datos de usuarios
        private HBox CreateDataRepairsContainer()
        {
            HBox container = new HBox();

            // Crear y configurar los labels
            VBox labels = CreateLabelsContainer();
            // Crear y configurar los labels dinámicos
            VBox labels2 = CreateDynamicLabelsContainer();
            // Crear y configurar los campos de entrada
            VBox entries = CreateEntriesContainer();

            // Agregar widgets al contenedor
            container.PackStart(labels, true, true, 0);
            container.PackStart(labels2, true, true, 0);
            container.PackStart(entries, true, true, 0);

            return container;
        }

        // Método para crear el contenedor de labels estáticos
        private VBox CreateLabelsContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idLabel = new Label("Id") { MarginTop = 15, MarginBottom = 20 };
            ordenLabel = new Label("Orden") { MarginBottom = 20 };
            totalLabel = new Label("Total") { MarginBottom = 20 };

            container.PackStart(idLabel, false, false, 0);
            container.PackStart(ordenLabel, false, false, 0);
            container.PackStart(totalLabel, false, false, 0);

            return container;
        }

        // Método para crear el contenedor de labels dinámicos
        private VBox CreateDynamicLabelsContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idEntry = new Entry { MarginTop = 15, MarginBottom = 5 };
            ordenLabel2 = new Label("") { MarginBottom = 20 };
            totalLabel2 = new Label("") { MarginBottom = 20 };

            container.PackStart(idEntry, false, false, 0);
            container.PackStart(ordenLabel2, false, false, 0);
            container.PackStart(totalLabel2, false, false, 0);

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
            Button searchButton = CreateButton("Buscar", buscarFactura, 5, 5);

            container.PackStart(searchButton, false, false, 0);

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

        // Método para buscar un usuario
        private void buscarFactura(object sender, EventArgs e)
        {
            try{    
                var factura = listaFacturas.Buscar(Convert.ToInt32(idEntry.Text));
                if(factura == null)
                {
                    return;
                }

                var servicio = listaServicios.Buscar(factura.id_Servicio);
                if(servicio == null)
                {
                    return;
                }

                var vehiculo = listaVehiculos.BuscarVehiculo(servicio.servicios.id_Vehiculo);
                if(vehiculo == null)
                {
                    return;
                }

                if(vehiculo.ID_Usuario != manejoSesion.currentUserId)
                {
                    return;
                }

                ordenLabel2.Text = factura.id_Servicio.ToString();
                totalLabel2.Text = factura.total.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void pagarFactura(object sender, EventArgs e)
        {
            try
            {
                var factura = listaFacturas.Buscar(Convert.ToInt32(idEntry.Text));
                
                // Verificar nuevamente que pertenece al usuario (seguridad adicional)
                var servicio = listaServicios.Buscar(factura.id_Servicio);
                var vehiculo = listaVehiculos.BuscarVehiculo(servicio.servicios.id_Vehiculo);
        
                if (vehiculo == null || vehiculo.ID_Usuario != manejoSesion.currentUserId)
                {
                    Console.WriteLine("Esta factura no pertenece a tu usuario");
                    return;
                }
                
                // Eliminar la factura (simulando pago)
                if (factura != null)
                {
                    listaFacturas.Eliminar(factura.id);

                    MessageDialog md = new MessageDialog(this, 
                        DialogFlags.Modal, 
                        MessageType.Info, 
                        ButtonsType.Ok, 
                        "Factura pagada exitosamente");
                    md.Run();
                    md.Destroy();

                    // Limpiar los campos
                    idEntry.Text = "";
                    ordenLabel2.Text = "";
                    totalLabel2.Text = "";
                }
                else
                {
                    Console.WriteLine("Error al procesar el pago");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            listaFacturas.ImprimirEnOrden();
        }
    }
}