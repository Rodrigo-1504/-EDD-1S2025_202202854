using Gtk;
using Structures;

namespace Interfaces2
{
    public class cancelarFactura : Window
    {
        private ArbolB listaFacturas = ArbolB.Instance;
        private ArbolBST listaServicios = ArbolBST.Instance;
        private ListaDoble listaVehiculos = ListaDoble.Instance;

        private Label idLabel, ordenLabel, totalLabel;
        private Label ordenLabel2, totalLabel2;
        private Entry idEntry;

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

        public cancelarFactura() : base("Cancel bill")
        {
            try
            {
                SetDefaultSize(500, 400);
                SetPosition(WindowPosition.Center);
                VBox mainContainer = CreateMainContainer();
                Add(mainContainer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inicializando la ventana: " + ex.Message);
            }
        }

        private VBox CreateMainContainer()
        {
            VBox container = new VBox { BorderWidth = 20, Spacing = 10 };
            HBox dataUsers = CreateDataRepairsContainer();
            Button updateButton = CreateButton("Pagar", pagarFactura, 5, 5);
            Button backButton = CreateButton("Regresar", goBack, 5, 5);

            container.PackStart(dataUsers, true, true, 0);
            container.PackStart(updateButton, true, true, 0);
            container.PackStart(backButton, true, true, 0);

            return container;
        }

        private HBox CreateDataRepairsContainer()
        {
            HBox container = new HBox();
            VBox labels = CreateLabelsContainer();
            VBox labels2 = CreateDynamicLabelsContainer();
            VBox entries = CreateEntriesContainer();
            container.PackStart(labels, true, true, 0);
            container.PackStart(labels2, true, true, 0);
            container.PackStart(entries, true, true, 0);
            return container;
        }

        private VBox CreateLabelsContainer()
        {
            VBox container = new VBox { BorderWidth = 20, Spacing = 10 };
            idLabel = new Label("Id") { MarginTop = 15, MarginBottom = 20 };
            ordenLabel = new Label("Orden") { MarginBottom = 20 };
            totalLabel = new Label("Total") { MarginBottom = 20 };
            container.PackStart(idLabel, false, false, 0);
            container.PackStart(ordenLabel, false, false, 0);
            container.PackStart(totalLabel, false, false, 0);
            return container;
        }

        private VBox CreateDynamicLabelsContainer()
        {
            VBox container = new VBox { BorderWidth = 20, Spacing = 10 };
            idEntry = new Entry { MarginTop = 15, MarginBottom = 5 };
            ordenLabel2 = new Label("") { MarginBottom = 20 };
            totalLabel2 = new Label("") { MarginBottom = 20 };
            container.PackStart(idEntry, false, false, 0);
            container.PackStart(ordenLabel2, false, false, 0);
            container.PackStart(totalLabel2, false, false, 0);
            return container;
        }

        private VBox CreateEntriesContainer()
        {
            VBox container = new VBox { BorderWidth = 20, Spacing = 10 };
            Button searchButton = CreateButton("Buscar", buscarFactura, 5, 5);
            container.PackStart(searchButton, false, false, 0);
            return container;
        }

        private Button CreateButton(string label, EventHandler handler, int marginTop, int marginBottom)
        {
            Button button = new Button(label) { MarginTop = marginTop, MarginBottom = marginBottom };
            button.Clicked += handler;
            return button;
        }

        private void goBack(object sender, EventArgs e)
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
                Console.WriteLine("Error al regresar: " + ex.Message);
            }
        }

        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            ((Window)sender).Hide();
            args.RetVal = true;
        }

        private void buscarFactura(object sender, EventArgs e)
        {
            try
            {
                var factura = listaFacturas.Buscar(Convert.ToInt32(idEntry.Text));
                if (factura == null) return;
                var servicio = listaServicios.Buscar(factura.id_Servicio);
                if (servicio == null) return;
                var vehiculo = listaVehiculos.BuscarVehiculo(servicio.servicios.id_Vehiculo);
                if (vehiculo == null || vehiculo.ID_Usuario != ManejoSesion.CurrentUserId) return;
                ordenLabel2.Text = factura.id_Servicio.ToString();
                totalLabel2.Text = factura.total.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar la factura: " + ex.Message);
            }
        }

        private void pagarFactura(object sender, EventArgs e)
        {
            try
            {
                var factura = listaFacturas.Buscar(Convert.ToInt32(idEntry.Text));
                if (factura == null) throw new Exception("Factura no encontrada");
                var servicio = listaServicios.Buscar(factura.id_Servicio);
                var vehiculo = listaVehiculos.BuscarVehiculo(servicio.servicios.id_Vehiculo);
                if (vehiculo == null || vehiculo.ID_Usuario != ManejoSesion.CurrentUserId)
                {
                    throw new Exception("Esta factura no pertenece a tu usuario");
                }
                listaFacturas.Eliminar(factura.id);
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Factura pagada exitosamente");
                md.Run();
                md.Destroy();
                idEntry.Text = "";
                ordenLabel2.Text = "";
                totalLabel2.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar el pago: " + ex.Message);
            }
            listaFacturas.ImprimirEnOrden();
        }
    }
}
