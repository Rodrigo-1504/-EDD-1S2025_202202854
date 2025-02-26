using Gtk;
using Interfaces;
using List;

namespace Interfaces
{
    public class OpcionesAdmin : Window
    {
        // Singleton para la ventana de opciones de administrador
        private static OpcionesAdmin _instance;

        public static OpcionesAdmin Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OpcionesAdmin();
                }
                return _instance;
            }
        }

        // Constructor
        public OpcionesAdmin() : base("Options")
        {
            // Configuración de la ventana
            SetDefaultSize(300, 400);
            SetPosition(WindowPosition.Center);

            // Crear y configurar el contenedor de botones
            VBox buttonsContainer = CreateButtonsContainer();
            Add(buttonsContainer);
        }

        // Método para crear el contenedor de botones
        private VBox CreateButtonsContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            // Crear y configurar los botones
            Button bulkUploadButton = CreateButton("Cargas Masivas", goBulkUpload);
            Button manualEntryButton = CreateButton("Ingreso Individual", goManualEntry);
            Button manageUsersButton = CreateButton("Manejo de Usuarios", goManagementUser);
            Button generateServiceButton = CreateButton("Generar Servicios", goService);
            Button cancelBillButton = CreateButton("Cancelar Facturas", goCancelBill);
            Button exitButton = CreateButton("Salir", goBack);

            // Agregar botones al contenedor
            container.PackStart(bulkUploadButton, true, true, 0);
            container.PackStart(manualEntryButton, true, true, 0);
            container.PackStart(manageUsersButton, true, true, 0);
            container.PackStart(generateServiceButton, true, true, 0);
            container.PackStart(cancelBillButton, true, true, 0);
            container.PackStart(exitButton, true, true, 0);

            return container;
        }

        // Método para crear un botón con un manejador de eventos
        private Button CreateButton(string label, EventHandler handler)
        {
            Button button = new Button(label)
            {
                MarginBottom = 5
            };
            button.Clicked += handler;
            return button;
        }

        // Métodos para manejar los eventos de clic en los botones
        private void goBulkUpload(object sender, EventArgs e)
        {
            OpenWindow(cargaMasiva.Instance);
        }

        private void goManualEntry(object sender, EventArgs e)
        {
            OpenWindow(ingresoManual.Instance);
        }

        private void goManagementUser(object sender, EventArgs e)
        {
            OpenWindow(manejoUsuarios.Instance);
        }

        private void goService(object sender, EventArgs e)
        {
            OpenWindow(generarServicios.Instance);
        }

        private void goCancelBill(object sender, EventArgs e)
        {
            OpenWindow(cancelarFacturas.Instance);
        }

        private void goBack(object sender, EventArgs e)
        {
            OpenWindow(inicioSesion.Instance);
            ListaSimple listaUsuarios = ListaSimple.Instance;
            ListaDoble listaVehiculos = ListaDoble.Instance;
            ListaCircular listaRepuestos = ListaCircular.Instance;
            Cola listaServicios = Cola.Instance;
            Pila listaFacturas = Pila.Instance;

            string dotLista = listaUsuarios.graphvizLista();
            string dotDoble = listaVehiculos.graphvizDoble();
            string dotCircular = listaRepuestos.graphvizCircular();
            string dotCola = listaServicios.graphvizCola();
            string dotPila = listaFacturas.graphvizPila();
            

            Dot_Png.Convertidor.generarArchivoDot("Lista Simple", dotLista);
            Dot_Png.Convertidor.ConvertirDot_a_Png("Lista Simple.dot");

            Dot_Png.Convertidor.generarArchivoDot("Lista Doble", dotDoble);
            Dot_Png.Convertidor.ConvertirDot_a_Png("Lista Doble.dot");

            Dot_Png.Convertidor.generarArchivoDot("Lista Circular", dotCircular);
            Dot_Png.Convertidor.ConvertirDot_a_Png("Lista Circular.dot");

            Dot_Png.Convertidor.generarArchivoDot("Cola", dotCola);
            Dot_Png.Convertidor.ConvertirDot_a_Png("Cola.dot");

            Dot_Png.Convertidor.generarArchivoDot("Pila", dotPila);
            Dot_Png.Convertidor.ConvertirDot_a_Png("Pila.dot");
        }

        // Método para abrir una ventana y ocultar la actual
        private void OpenWindow(Window window)
        {
            window.DeleteEvent += OnWindowDelete;
            window.ShowAll();
            this.Hide();
        }

        // Método para manejar el evento de cierre de la ventana
        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            ((Window)sender).Hide();
            args.RetVal = true;
        }
    }
}