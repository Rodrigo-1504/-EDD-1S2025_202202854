using Gtk;
using Structures;

namespace Interfaces2
{
    public class ActualizacionRepuestos : Window
    {
        // Instancia de la lista de usuarios
        private ArbolAVL listaRepuestos = ArbolAVL.Instance;

        // Labels y Entries para Repuestos
        private Label idLabel, repuestoLabel, detallesLabel, costoLabel;
        private Label repuestoLabel2, detallesLabel2, costoLabel2;
        private Entry idEntry, repuestoEntry, detallesEntry, costoEntry;

        // Singleton para la ventana de manejo de usuarios
        private static ActualizacionRepuestos _instance;

        public static ActualizacionRepuestos Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ActualizacionRepuestos();
                }
                return _instance;
            }
        }

        // Constructor
        public ActualizacionRepuestos() : base("Repairs management")
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
            Button updateButton = CreateButton("Actualizar", editarRepuesto, 5, 5);
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
            repuestoLabel = new Label("Repuestos") { MarginBottom = 20 };
            detallesLabel = new Label("Detalles") { MarginBottom = 20 };
            costoLabel = new Label("Costo") { MarginBottom = 20 };

            container.PackStart(idLabel, false, false, 0);
            container.PackStart(repuestoLabel, false, false, 0);
            container.PackStart(detallesLabel, false, false, 0);
            container.PackStart(costoLabel, false, false, 0);

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
            repuestoLabel2 = new Label("") { MarginBottom = 20 };
            detallesLabel2 = new Label("") { MarginBottom = 20 };
            costoLabel2 = new Label("") { MarginBottom = 20 };

            container.PackStart(idEntry, false, false, 0);
            container.PackStart(repuestoLabel2, false, false, 0);
            container.PackStart(detallesLabel2, false, false, 0);
            container.PackStart(costoLabel2, false, false, 0);

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
            Button searchButton = CreateButton("Buscar", buscarRepuesto, 5, 5);
            repuestoEntry = new Entry { MarginBottom = 5 };
            detallesEntry = new Entry { MarginBottom = 5 };
            costoEntry = new Entry { MarginBottom = 5 };

            container.PackStart(searchButton, false, false, 0);
            container.PackStart(repuestoEntry, false, false, 0);
            container.PackStart(detallesEntry, false, false, 0);
            container.PackStart(costoEntry, false, false, 0);

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
            Opciones opciones = Opciones.Instance;
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
        private void buscarRepuesto(object sender, EventArgs e)
        {
            NodoAVL repuestoBuscado = listaRepuestos.Buscar(Convert.ToInt32(idEntry.Text));

            if (repuestoBuscado != null)
            {
                repuestoLabel2.Text = repuestoBuscado.repuestos.repuesto;
                detallesLabel2.Text = repuestoBuscado.repuestos.detalles;
                costoLabel2.Text = repuestoBuscado.repuestos.costo.ToString();
            }
        }

        // Método para editar un usuario
        private void editarRepuesto(object sender, EventArgs e)
        {
            NodoAVL repuestoBuscado = listaRepuestos.Buscar(Convert.ToInt32(idEntry.Text));

            if (repuestoBuscado != null)
            {
                listaRepuestos.Actualizar(
                    repuestoBuscado.repuestos.id,
                    repuestoEntry.Text, 
                    detallesEntry.Text, 
                    Convert.ToDouble(costoEntry.Text)
                );
            }

            Console.WriteLine("\n--NUEVA LISTA---");
            listaRepuestos.RecorridoEnOrden();
        }

    }
}