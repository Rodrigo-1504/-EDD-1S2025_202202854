using Gtk;
using DS;

namespace Interfaces3
{
    public class OpcionesUsuario : Window
    {
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

        public OpcionesUsuario() : base("Users Options")
        {
            try
            {
                SetDefaultSize(300, 400);
                SetPosition(WindowPosition.Center);
                VBox buttonsContainer = CreateButtonsContainer();
                Add(buttonsContainer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inicializando la ventana: " + ex.Message);
            }
        }

        private VBox CreateButtonsContainer()
        {
            VBox container = new VBox { BorderWidth = 20, Spacing = 10 };

            try
            {
                Button bulkUploadButton = CreateButton("Visualizacion Vehiculo", visualizarVehiculo);
                Button gestionEntidades = CreateButton("Visualizacion de Servicios", seeServices);
                Button actualizacionRepuestos = CreateButton("Visualizacion de Facturas", seeBills);
                Button regresar = CreateButton("Regresar", goBack);

                container.PackStart(bulkUploadButton, true, true, 0);
                container.PackStart(gestionEntidades, true, true, 0);
                container.PackStart(actualizacionRepuestos, true, true, 0);
                container.PackStart(regresar, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creando botones: " + ex.Message);
            }

            return container;
        }

        private Button CreateButton(string label, EventHandler handler)
        {
            Button button = new Button(label) { MarginBottom = 5 };
            button.Clicked += handler;
            return button;
        }

        private void visualizarVehiculo(object sender, EventArgs e)
        {
            //OpenWindow(InsertarVehiculo.Instance);
        }

        private void seeServices(object sender, EventArgs e)
        {
            try
            {
                OpenWindow(visualizarServicio.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error abriendo la ventana de servicios: " + ex.Message);
            }
        }

        private void seeBills(object sender, EventArgs e)
        {
            try
            {
                //OpenWindow(verFacturas.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error abriendo la ventana de facturas: " + ex.Message);
            }
        }

        private void goBack(object sender, EventArgs e)
        {
            try
            {
                ManejoSesion.Logout();
                OpenWindow(Login.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error regresando a la ventana de login: " + ex.Message);
            }
        }

        private void OpenWindow(Window window)
        {
            try
            {
                window.DeleteEvent += OnWindowDelete;
                window.ShowAll();
                this.Hide();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la ventana: " + ex.Message);
            }
        }

        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            ((Window)sender).Hide();
            args.RetVal = true;
        }
    }
}