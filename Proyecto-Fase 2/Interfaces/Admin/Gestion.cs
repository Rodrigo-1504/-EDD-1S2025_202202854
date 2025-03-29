using Gtk;
using Structures;
using System;

namespace Interfaces2
{
    public class Gestion : Window
    {
        // Instancias de las listas
        private readonly ListaSimple listaUsuarios = ListaSimple.Instance;
        private readonly ListaDoble listaVehiculos = ListaDoble.Instance;

        // Labels y Entries para usuarios
        private Label idLabel, nameLabel, lastnameLabel, mailLabel, ageLabel;
        private Label nameLabel2, lastnameLabel2, mailLabel2, ageLabel2;
        private Entry idEntry;

        // Labels y Entries para vehículos
        private Label idVLabel, idUserLabel, marcaLabel, modeloLabel, placaLabel;
        private Label idUserLabel2, marcaLabel2, modeloLabel2, placaLabel2;
        private Entry idVEntry;

        // ComboBox para elegir entre usuarios y vehículos
        private readonly ComboBoxText opciones = new ComboBoxText();

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
            try
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
                    try
                    {
                        usuarioContainer.Hide();
                        vehiculoContainer.Hide();

                        switch (opciones.ActiveText)
                        {
                            case "Usuario":
                                usuarioContainer.Show();
                                break;
                            case "Vehiculo":
                                vehiculoContainer.Show();
                                break;
                            default:
                                usuarioContainer.Show();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al cambiar opción: {ex.Message}");
                        ShowErrorMessage("Error al cambiar entre usuarios y vehículos");
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar la ventana: {ex.Message}");
                ShowErrorMessage("Error al inicializar la ventana de gestión");
            }
        }

        // Método para crear el contenedor de usuarios
        private VBox CreateUsuarioContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            try
            {
                HBox dataUsers = CreateDataUsersContainer();
                container.PackStart(dataUsers, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear contenedor de usuarios: {ex.Message}");
            }

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

            try
            {
                HBox dataVehiculos = CreateDataVehiculosContainer();
                container.PackStart(dataVehiculos, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear contenedor de vehículos: {ex.Message}");
            }

            return container;
        }

        // Método para crear el contenedor de datos de usuarios
        private HBox CreateDataUsersContainer()
        {
            HBox container = new HBox();

            try
            {
                VBox labels = CreateLabelsContainer();
                VBox labels2 = CreateDynamicLabelsContainer();
                VBox entries = CreateEntriesContainer();

                container.PackStart(labels, true, true, 0);
                container.PackStart(labels2, true, true, 0);
                container.PackStart(entries, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear datos de usuarios: {ex.Message}");
            }

            return container;
        }

        // Método para crear el contenedor de datos de vehículos
        private HBox CreateDataVehiculosContainer()
        {
            HBox container = new HBox();

            try
            {
                VBox labels = CreateVehiculoLabelsContainer();
                VBox labels2 = CreateVehiculoDynamicLabelsContainer();
                VBox entries = CreateVehiculoEntriesContainer();

                container.PackStart(labels, true, true, 0);
                container.PackStart(labels2, true, true, 0);
                container.PackStart(entries, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear datos de vehículos: {ex.Message}");
            }

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

            try
            {
                idLabel = new Label("Id") { MarginTop = 15, MarginBottom = 20 };
                nameLabel = new Label("Nombres") { MarginBottom = 20 };
                lastnameLabel = new Label("Apellidos") { MarginBottom = 20 };
                mailLabel = new Label("Correo") { MarginBottom = 20 };
                ageLabel = new Label("Edad") { MarginBottom = 20 };

                container.PackStart(idLabel, false, false, 0);
                container.PackStart(nameLabel, false, false, 0);
                container.PackStart(lastnameLabel, false, false, 0);
                container.PackStart(mailLabel, false, false, 0);
                container.PackStart(ageLabel, false, false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear labels de usuario: {ex.Message}");
            }

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

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear labels dinámicos de usuario: {ex.Message}");
            }

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

            try
            {
                Button searchButton = CreateButton("Buscar", buscarUsuario, 5, 5);
                container.PackStart(searchButton, false, false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear entradas de usuario: {ex.Message}");
            }

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

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear labels de vehículo: {ex.Message}");
            }

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

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear labels dinámicos de vehículo: {ex.Message}");
            }

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

            try
            {
                Button searchButton = CreateButton("Buscar", buscarVehiculo, 5, 5);
                container.PackStart(searchButton, false, false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear entradas de vehículo: {ex.Message}");
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
                ShowErrorMessage("Error al intentar regresar al menú principal");
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

        // Método para buscar un usuario
        private void buscarUsuario(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idEntry.Text))
                {
                    ShowErrorMessage("El campo ID no puede estar vacío");
                    return;
                }

                int id = Convert.ToInt32(idEntry.Text);
                Usuarios usuarioBuscado = listaUsuarios.BuscarId(id);

                if (usuarioBuscado != null)
                {
                    nameLabel2.Text = usuarioBuscado.nombres;
                    lastnameLabel2.Text = usuarioBuscado.apellidos;
                    mailLabel2.Text = usuarioBuscado.correo;
                    ageLabel2.Text = usuarioBuscado.edades.ToString();
                }
                else
                {
                    ShowErrorMessage("Usuario no encontrado");
                }
            }
            catch (FormatException)
            {
                ShowErrorMessage("El ID debe ser un número válido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar usuario: {ex.Message}");
                ShowErrorMessage("Error al buscar usuario");
            }
        }

        // Método para buscar un vehículo
        private void buscarVehiculo(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idVEntry.Text))
                {
                    ShowErrorMessage("El campo ID no puede estar vacío");
                    return;
                }

                int id = Convert.ToInt32(idVEntry.Text);
                Vehiculos vehiculoBuscado = listaVehiculos.BuscarVehiculo(id);

                if (vehiculoBuscado != null)
                {
                    idUserLabel2.Text = vehiculoBuscado.ID_Usuario.ToString();
                    marcaLabel2.Text = vehiculoBuscado.marca;
                    modeloLabel2.Text = vehiculoBuscado.modelo.ToString();
                    placaLabel2.Text = vehiculoBuscado.placa;
                }
                else
                {
                    ShowErrorMessage("Vehículo no encontrado");
                }
            }
            catch (FormatException)
            {
                ShowErrorMessage("El ID debe ser un número válido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar vehículo: {ex.Message}");
                ShowErrorMessage("Error al buscar vehículo");
            }
        }

        // Método para eliminar un usuario o vehículo
        private void eliminar(object sender, EventArgs e)
        {
            try
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
                        ShowErrorMessage("Opción no válida");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar: {ex.Message}");
                ShowErrorMessage("Error al intentar eliminar");
            }
        }

        // Método para eliminar un usuario
        private void eliminarUsuario()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idEntry.Text))
                {
                    ShowErrorMessage("El campo ID no puede estar vacío");
                    return;
                }

                int id = Convert.ToInt32(idEntry.Text);
                Usuarios usuarioEliminar = listaUsuarios.BuscarId(id);

                if (usuarioEliminar != null)
                {
                    listaUsuarios.EliminarUsuario(usuarioEliminar.id);
                    Console.WriteLine("\n---NUEVA LISTA DE USUARIOS---");
                    listaUsuarios.Imprimir();
                    ShowErrorMessage("Usuario eliminado correctamente");
                    
                    // Limpiar campos
                    idEntry.Text = "";
                    nameLabel2.Text = "";
                    lastnameLabel2.Text = "";
                    mailLabel2.Text = "";
                    ageLabel2.Text = "";
                }
                else
                {
                    ShowErrorMessage("Usuario no encontrado");
                }
            }
            catch (FormatException)
            {
                ShowErrorMessage("El ID debe ser un número válido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
                ShowErrorMessage("Error al eliminar usuario");
            }
        }

        // Método para eliminar un vehículo
        private void eliminarVehiculo()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idVEntry.Text))
                {
                    ShowErrorMessage("El campo ID no puede estar vacío");
                    return;
                }

                int id = Convert.ToInt32(idVEntry.Text);
                Vehiculos vehiculoEliminar = listaVehiculos.BuscarVehiculo(id);

                if (vehiculoEliminar != null)
                {
                    listaVehiculos.EliminarVehiculo(vehiculoEliminar.id);
                    Console.WriteLine("\n---NUEVA LISTA---");
                    listaVehiculos.Imprimir();
                    ShowErrorMessage("Vehículo eliminado correctamente");
                    
                    // Limpiar campos
                    idVEntry.Text = "";
                    idUserLabel2.Text = "";
                    marcaLabel2.Text = "";
                    modeloLabel2.Text = "";
                    placaLabel2.Text = "";
                }
                else
                {
                    ShowErrorMessage("Vehículo no encontrado");
                }
            }
            catch (FormatException)
            {
                ShowErrorMessage("El ID debe ser un número válido");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar vehículo: {ex.Message}");
                ShowErrorMessage("Error al eliminar vehículo");
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
                Console.WriteLine($"Error al mostrar mensaje: {ex.Message}");
            }
        }
    }
}