using Gtk;
using Structures;

namespace Interfaces2
{
    public class Opciones : Window
    {
        // Singleton para la ventana de opciones de administrador
        private static Opciones _instance;

        public static Opciones Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Opciones();
                }
                return _instance;
            }
        }

        // Constructor
        public Opciones() : base("Options")
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
            Button bulkUploadButton = CreateButton("Cargas Masivas", goCargas);
            Button gestionEntidades = CreateButton("Gestion de Entidades", goGestion);
            Button actualizacionRepuestos = CreateButton("Actualizacion de Repuestos", goActualizacionR);
            Button visualizarRepuestos = CreateButton("Visualizacion de Repuestos", goVisualizacionR);
            Button generarServicios = CreateButton("Generar Servicios", goServicios);
            Button controlLogueo = CreateButton("Control de Logueo", goControl);
            Button generarReportes = CreateButton("GenerarReportes", goReportes);

            // Agregar botones al contenedor
            container.PackStart(bulkUploadButton, true, true, 0);
            container.PackStart(gestionEntidades, true, true, 0);
            container.PackStart(actualizacionRepuestos, true, true, 0);
            container.PackStart(visualizarRepuestos, true, true, 0);
            container.PackStart(generarServicios, true, true, 0);            
            container.PackStart(controlLogueo, true, true, 0);
            container.PackStart(generarReportes, true, true, 0);

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
        private void goCargas(object sender, EventArgs e)
        {
            OpenWindow(CargaMasiva.Instance);
        }

        private void goGestion(object sender, EventArgs e)
        {
            OpenWindow(Gestion.Instance);
        }

        private void goActualizacionR(object sender, EventArgs e)
        {
            OpenWindow(ActualizacionRepuestos.Instance);
        }

        private void goVisualizacionR(object sender, EventArgs e)
        {
            OpenWindow(VisualizarRepuestos.Instance);
        }

        private void goServicios(object sender, EventArgs e)
        {
            OpenWindow(GenerarServicios.Instance);
        }

        private void goControl(object sender, EventArgs e)
        {
            //OpenWindow(ControlLogueo.Instance);
        }

        private void goReportes(object sender, EventArgs e)
        {
            OpenWindow(Login.Instance);
            ListaSimple listaUsuarios = ListaSimple.Instance;
            ListaDoble listaVehiculos = ListaDoble.Instance;
            ArbolBST listaServicios = ArbolBST.Instance;
            ArbolAVL listaRepuestos = ArbolAVL.Instance;

            string dotLista = listaUsuarios.graphvizLista();
            string dotDoble = listaVehiculos.graphvizDoble();
            string dotBST = listaServicios.graphvizBST();
            string dotAVL = listaRepuestos.graphvizAVL();
            

            try
            {
                Dot_Png.Convertidor.generarArchivoDot("Lista Simple", dotLista);
                Dot_Png.Convertidor.ConvertirDot_a_Png("Lista Simple.dot");

                Dot_Png.Convertidor.generarArchivoDot("Lista Doble", dotDoble);
                Dot_Png.Convertidor.ConvertirDot_a_Png("Lista Doble.dot");

                Dot_Png.Convertidor.generarArchivoDot("BST", dotBST);
                Dot_Png.Convertidor.ConvertirDot_a_Png("BST.dot");

                Dot_Png.Convertidor.generarArchivoDot("AVL", dotAVL);
                Dot_Png.Convertidor.ConvertirDot_a_Png("AVL.dot");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error Reportes: " + ex.Message);
            }

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