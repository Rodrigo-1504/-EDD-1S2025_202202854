using Gtk;
using Structures;

namespace Interfaces2
{
    public class OpcionesUsuario : Window
    {
        // Singleton para la ventana de OpcionesUsuario de administrador
        private static OpcionesUsuario _instance;

        public static OpcionesUsuario Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OpcionesUsuario();
                }
                return _instance;
            }
        }

        // Constructor
        public OpcionesUsuario() : base("Users Options")
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
            Button bulkUploadButton = CreateButton("Insertar Vehiculo", insertVehicles);
            Button gestionEntidades = CreateButton("Visualizacion de Servicios", seeServices);
            Button actualizacionRepuestos = CreateButton("Visualizacion de Facturas", seeBills);
            Button visualizarRepuestos = CreateButton("Cancelar Facturas", cancelBill);
            Button regresar = CreateButton("Regresar", goBack);

            // Agregar botones al contenedor
            container.PackStart(bulkUploadButton, true, true, 0);
            container.PackStart(gestionEntidades, true, true, 0);
            container.PackStart(actualizacionRepuestos, true, true, 0);
            container.PackStart(visualizarRepuestos, true, true, 0);
            container.PackStart(regresar, true, true, 0);

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
        private void insertVehicles(object sender, EventArgs e)
        {
            OpenWindow(insertarVehiculo.Instance);
        }

        private void seeServices(object sender, EventArgs e)
        {
            try
            {
                OpenWindow(verServicios.Instance);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
        }

        private void seeBills(object sender, EventArgs e)
        {
            OpenWindow(verFacturas.Instance);
        }

        private void cancelBill(object sender, EventArgs e)
        {
            OpenWindow(cancelarFactura.Instance);
        }

        private void goBack(object sender, EventArgs e)
        {
            OpenWindow(Login.Instance);
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