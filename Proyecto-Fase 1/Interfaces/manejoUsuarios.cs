using Gtk;
using List;
using System;

namespace Interfaces
{
    public class manejoUsuarios : Window
    {
        // Instancia de la lista de usuarios
        private ListaSimple listaUsuarios = ListaSimple.Instance;
        private ListaDoble listaVehiculos = ListaDoble.Instance;

        // Labels y Entries
        private Label idLabel, nameLabel, lastnameLabel, mailLabel;
        private Label nameLabel2, lastnameLabel2, mailLabel2;
        private Entry idEntry, nameEntry, lastnameEntry, mailEntry;

        // Singleton para la ventana de manejo de usuarios
        private static manejoUsuarios _instance;

        public static manejoUsuarios Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new manejoUsuarios();
                }
                return _instance;
            }
        }

        // Constructor
        public manejoUsuarios() : base("Users management")
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
            HBox dataUsers = CreateDataUsersContainer();
            Button updateButton = CreateButton("Actualizar", editarUsuario, 5, 5);
            Button deleteButton = CreateButton("Eliminar", eliminarUsuario, 5, 5);
            Button backButton = CreateButton("Regresar", goBack, 5, 5);

            // Agregar widgets al contenedor principal
            container.PackStart(dataUsers, true, true, 0);
            container.PackStart(updateButton, true, true, 0);
            container.PackStart(deleteButton, true, true, 0);
            container.PackStart(backButton, true, true, 0);

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

        // Método para crear el contenedor de labels estáticos
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

            container.PackStart(idLabel, false, false, 0);
            container.PackStart(nameLabel, false, false, 0);
            container.PackStart(lastnameLabel, false, false, 0);
            container.PackStart(mailLabel, false, false, 0);

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

            idEntry = new Entry { MarginBottom = 5 };
            nameLabel2 = new Label("") { MarginBottom = 20 };
            lastnameLabel2 = new Label("") { MarginBottom = 20 };
            mailLabel2 = new Label("") { MarginBottom = 20 };

            container.PackStart(idEntry, false, false, 0);
            container.PackStart(nameLabel2, false, false, 0);
            container.PackStart(lastnameLabel2, false, false, 0);
            container.PackStart(mailLabel2, false, false, 0);

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
            Button searchButton = CreateButton("Buscar", buscarUsuario, 5, 5);
            nameEntry = new Entry { MarginBottom = 5 };
            lastnameEntry = new Entry { MarginBottom = 5 };
            mailEntry = new Entry { MarginBottom = 5 };

            container.PackStart(searchButton, false, false, 0);
            container.PackStart(nameEntry, false, false, 0);
            container.PackStart(lastnameEntry, false, false, 0);
            container.PackStart(mailEntry, false, false, 0);

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

        // Método para buscar un usuario
        private void buscarUsuario(object sender, EventArgs e)
        {
            Usuarios usuarioBuscado = listaUsuarios.BuscarUsuario(Convert.ToInt32(idEntry.Text));



            if (usuarioBuscado != null)
            {
                nameLabel2.Text = usuarioBuscado.nombres;
                lastnameLabel2.Text = usuarioBuscado.apellidos;
                mailLabel2.Text = usuarioBuscado.correo;
                Console.WriteLine("Vehiculo del usuario: ");
                listaVehiculos.buscarVehiculoUsuario(usuarioBuscado.id);
            }
        }

        // Método para editar un usuario
        private void editarUsuario(object sender, EventArgs e)
        {
            Usuarios usuarioEditar = listaUsuarios.BuscarUsuario(Convert.ToInt32(idEntry.Text));

            if (usuarioEditar != null)
            {
                listaUsuarios.actualizarUsuario(
                    usuarioEditar.id,
                    nameEntry.Text,
                    lastnameEntry.Text,
                    mailEntry.Text
                );
            }

            Console.WriteLine("\n--NUEVA LISTA---");
            listaUsuarios.imprimirLista();
        }

        // Método para eliminar un usuario
        private void eliminarUsuario(object sender, EventArgs e)
        {
            Usuarios usuarioEliminar = listaUsuarios.BuscarUsuario(Convert.ToInt32(idEntry.Text));

            if (usuarioEliminar != null)
            {
                listaUsuarios.EliminarUsuario(usuarioEliminar.id);
            }

            Console.WriteLine("\n---NUEVA LISTA---");
            listaUsuarios.imprimirLista();
        }
    }
}