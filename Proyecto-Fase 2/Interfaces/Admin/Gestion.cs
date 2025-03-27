using Gtk;
using Structures;

namespace Interfaces2
{
    public class Gestion : Window
    {
        // Instancias de las listas
        private ListaSimple listaUsuarios = ListaSimple.Instance;
        private ListaDoble listaVehiculos = ListaDoble.Instance;

        // Labels y Entries para usuarios
        private Label idLabel, nameLabel, lastnameLabel, mailLabel, ageLabel;
        private Label nameLabel2, lastnameLabel2, mailLabel2, ageLabel2;
        private Entry idEntry;

        // Labels y Entries para vehículos
        private Label idVLabel, idUserLabel, marcaLabel, modeloLabel, placaLabel;
        private Label idUserLabel2, marcaLabel2, modeloLabel2, placaLabel2;
        private Entry idVEntry;

        // ComboBox para elegir entre usuarios y vehículos
        private ComboBoxText opciones = new ComboBoxText();

        // Singleton para la ventana de manejo de usuarios
        private static Gestion _instance;

        public static Gestion Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Gestion();
                }
                return _instance;
            }
        }

        // Constructor
        public Gestion() : base("Users and Vehicles management")
        {
            // Configuración de la ventana
            SetDefaultSize(500, 400);
            SetPosition(WindowPosition.Center);

            // Configurar el ComboBox
            opciones.AppendText("Usuario");
            opciones.AppendText("Vehiculo");
            opciones.Active = 1;

            // Crear y configurar los contenedores
            VBox usuarioContainer = CreateUsuarioContainer();
            VBox vehiculoContainer = CreateVehiculoContainer();

            // Mostrar solo el contenedor de usuarios al inicio
            usuarioContainer.Show();
            vehiculoContainer.Hide();

            // Manejar el cambio de opción en el ComboBox
            opciones.Changed += (sender, e) =>
            {
                usuarioContainer.Hide();
                vehiculoContainer.Hide();

                switch (opciones.ActiveText)
                {
                    case "Usuario":
                        usuarioContainer.Show();
                        vehiculoContainer.Hide();
                        break;
                    case "Vehiculo":
                        vehiculoContainer.Show();
                        usuarioContainer.Hide();
                        break;
                    default:
                        usuarioContainer.Show();
                        vehiculoContainer.Hide();
                        break;
                }
            };

            // Crear y configurar el contenedor principal
            VBox mainContainer = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            Button deleteButton = CreateButton("Eliminar", eliminar, 5, 5);
            Button backButton = CreateButton("Regresar", goBack, 5, 5);

            mainContainer.PackStart(opciones, false, false, 0);
            mainContainer.PackStart(usuarioContainer, false, false, 0);
            mainContainer.PackStart(vehiculoContainer, false, false, 0);
            mainContainer.PackStart(deleteButton, false, false, 0);
            mainContainer.PackStart(backButton, false, false, 0);

            Add(mainContainer);
        }

        // Método para crear el contenedor de usuarios
        private VBox CreateUsuarioContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            // Crear y configurar los campos de entrada para usuarios
            HBox dataUsers = CreateDataUsersContainer();
            container.PackStart(dataUsers, true, true, 0);

            return container;
        }

        // Método para crear el contenedor de vehículos
        private VBox CreateVehiculoContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            // Crear y configurar los campos de entrada para vehículos
            HBox dataVehiculos = CreateDataVehiculosContainer();
            container.PackStart(dataVehiculos, true, true, 0);

            return container;
        }

        // Método para crear el contenedor de datos de usuarios
        private HBox CreateDataUsersContainer()
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

        // Método para crear el contenedor de datos de vehículos
        private HBox CreateDataVehiculosContainer()
        {
            HBox container = new HBox();

            // Crear y configurar los labels
            VBox labels = CreateVehiculoLabelsContainer();
            // Crear y configurar los labels dinámicos
            VBox labels2 = CreateVehiculoDynamicLabelsContainer();
            // Crear y configurar los campos de entrada
            VBox entries = CreateVehiculoEntriesContainer();

            // Agregar widgets al contenedor
            container.PackStart(labels, true, true, 0);
            container.PackStart(labels2, true, true, 0);
            container.PackStart(entries, true, true, 0);

            return container;
        }

        // Método para crear el contenedor de labels estáticos de usuarios
        private VBox CreateLabelsContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idLabel = new Label("Id") { MarginTop = 15, MarginBottom = 20 };
            nameLabel = new Label("Nombres") { MarginBottom = 20 };
            lastnameLabel = new Label("Apellidos") { MarginBottom = 20 };
            mailLabel = new Label("Correo") { MarginBottom = 20 };
            ageLabel = new Label("Edad") {MarginBottom = 20};

            container.PackStart(idLabel, false, false, 0);
            container.PackStart(nameLabel, false, false, 0);
            container.PackStart(lastnameLabel, false, false, 0);
            container.PackStart(mailLabel, false, false, 0);
            container.PackStart(ageLabel, false, false, 0);

            return container;
        }

        // Método para crear el contenedor de labels dinámicos de usuarios
        private VBox CreateDynamicLabelsContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idEntry = new Entry { MarginTop = 15, MarginBottom = 5 };
            nameLabel2 = new Label("") { MarginBottom = 20 };
            lastnameLabel2 = new Label("") { MarginBottom = 20 };
            mailLabel2 = new Label("") { MarginBottom = 20 };
            ageLabel2 = new Label("") { MarginBottom = 20 };

            container.PackStart(idEntry, false, false, 0);
            container.PackStart(nameLabel2, false, false, 0);
            container.PackStart(lastnameLabel2, false, false, 0);
            container.PackStart(mailLabel2, false, false, 0);
            container.PackStart(ageLabel2, false, false, 0);

            return container;
        }

        // Método para crear el contenedor de campos de entrada de usuarios
        private VBox CreateEntriesContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };
            Button searchButton = CreateButton("Buscar", buscarUsuario, 5, 5);
            container.PackStart(searchButton, false, false, 0);

            return container;
        }

        // Método para crear el contenedor de labels estáticos de vehículos
        private VBox CreateVehiculoLabelsContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idVLabel = new Label("Id") { MarginTop = 15, MarginBottom = 20 };
            idUserLabel = new Label("Id_Usuario") { MarginBottom = 20 };
            marcaLabel = new Label("Marca") { MarginBottom = 20 };
            modeloLabel = new Label("Modelo") { MarginBottom = 20 };
            placaLabel = new Label("Placa") { MarginBottom = 20 };

            container.PackStart(idVLabel, false, false, 0);
            container.PackStart(idUserLabel, false, false, 0);
            container.PackStart(marcaLabel, false, false, 0);
            container.PackStart(modeloLabel, false, false, 0);
            container.PackStart(placaLabel, false, false, 0);

            return container;
        }

        // Método para crear el contenedor de labels dinámicos de vehículos
        private VBox CreateVehiculoDynamicLabelsContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idVEntry = new Entry { MarginTop = 15, MarginBottom = 5 };
            idUserLabel2 = new Label("") { MarginBottom = 20 };
            marcaLabel2 = new Label("") { MarginBottom = 20 };
            modeloLabel2 = new Label("") { MarginBottom = 20 };
            placaLabel2 = new Label("") { MarginBottom = 20 };

            container.PackStart(idVEntry, false, false, 0);
            container.PackStart(idUserLabel2, false, false, 0);
            container.PackStart(marcaLabel2, false, false, 0);
            container.PackStart(modeloLabel2, false, false, 0);
            container.PackStart(placaLabel2, false, false, 0);

            return container;
        }

        // Método para crear el contenedor de campos de entrada de vehículos
        private VBox CreateVehiculoEntriesContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };
            Button searchButton = CreateButton("Buscar", buscarVehiculo, 5, 5);
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
        private void buscarUsuario(object sender, EventArgs e)
        {
            Usuarios usuarioBuscado = listaUsuarios.BuscarId(Convert.ToInt32(idEntry.Text));

            if (usuarioBuscado != null)
            {
                nameLabel2.Text = usuarioBuscado.nombres;
                lastnameLabel2.Text = usuarioBuscado.apellidos;
                mailLabel2.Text = usuarioBuscado.correo;
                ageLabel2.Text = usuarioBuscado.edades.ToString();
            }
        }

        // Método para buscar un vehículo
        private void buscarVehiculo(object sender, EventArgs e)
        {
            Vehiculos vehiculoBuscado = listaVehiculos.BuscarVehiculo(Convert.ToInt32(idVEntry.Text));

            if (vehiculoBuscado != null)
            {
                idUserLabel2.Text = vehiculoBuscado.ID_Usuario.ToString();
                marcaLabel2.Text = vehiculoBuscado.marca;
                modeloLabel2.Text = vehiculoBuscado.modelo.ToString();
                placaLabel2.Text = vehiculoBuscado.placa;
            }
        }

        // Método para eliminar un usuario o vehículo
        private void eliminar(object sender, EventArgs e)
        {
            switch (opciones.ActiveText)
            {
                case "Usuario":
                    eliminarUsuario();
                    break;
                case "Vehiculo":
                    eliminarVehiculo();
                    break;
                default:
                    Console.WriteLine("Opción no válida");
                    break;
            }
        }

        // Método para eliminar un usuario
        private void eliminarUsuario()
        {
            Usuarios usuarioEliminar = listaUsuarios.BuscarId(Convert.ToInt32(idEntry.Text));

            if (usuarioEliminar != null)
            {
                listaUsuarios.EliminarUsuario(usuarioEliminar.id);
            }

            Console.WriteLine("\n---NUEVA LISTA DE USUARIOS---");
            listaUsuarios.Imprimir();

            /*Console.WriteLine("\n---NUEVA LISTA DE VEHICULOS---");
            listaVehiculos.Imprimir();*/
        }

        // Método para eliminar un vehículo
        private void eliminarVehiculo()
        {
            Vehiculos vehiculoEliminar = listaVehiculos.BuscarVehiculo(Convert.ToInt32(idVEntry.Text));

            if (vehiculoEliminar != null)
            {
                listaVehiculos.EliminarVehiculo(vehiculoEliminar.id);
            }

            Console.WriteLine("\n---NUEVA LISTA---");
            listaVehiculos.Imprimir();
        }
    }
}