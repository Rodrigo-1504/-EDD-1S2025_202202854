using Gtk;
using Newtonsoft.Json; // Librería para leer Json
using Structures;

namespace Interfaces2
{
    public class CargaMasiva : Window
    {
        // Instancias de las listas
        ListaSimple listaUsuarios = ListaSimple.Instance;
        ListaDoble listaVehiculos = ListaDoble.Instance;
        ArbolAVL listaRepuestos = ArbolAVL.Instance;

        // ComboBox para seleccionar el tipo de carga masiva
        private ComboBoxText bulkUploadOptions = new ComboBoxText();

        // Instancia de la ventana (Singleton)
        private static CargaMasiva _instance;

        public static CargaMasiva Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CargaMasiva();
                }
                return _instance;
            }
        }

        // Constructor
        public CargaMasiva() : base("Bulk Upload")
        {
            // Configuración de la ventana
            SetDefaultSize(300, 300);
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

            // Configurar el ComboBox
            bulkUploadOptions.AppendText("Usuarios");
            bulkUploadOptions.AppendText("Vehiculos");
            bulkUploadOptions.AppendText("Repuestos");

            // Crear botones
            Button uploadButton = CreateButton("Cargar", SeleccionarArchivo, 20, 5);
            Button backButton = CreateButton("Regresar", goBack, 5, 5);

            // Agregar widgets al contenedor
            container.PackStart(bulkUploadOptions, false, false, 0);
            container.PackStart(uploadButton, true, true, 0);
            container.PackStart(backButton, true, true, 0);

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

        // Método para seleccionar un archivo JSON
        private void SeleccionarArchivo(object sender, EventArgs e)
        {
            // Crear el explorador de archivos
            FileChooserDialog fileChooser = new FileChooserDialog(
                "Seleccionar un archivo Json",
                this,
                FileChooserAction.Open,
                "Cancelar", ResponseType.Cancel,
                "Abrir", ResponseType.Accept
            );

            // Filtrar archivos JSON
            fileChooser.Filter = new FileFilter();
            fileChooser.Filter.AddPattern("*.json");

            if (fileChooser.Run() == (int)ResponseType.Accept)
            {
                string filePath = fileChooser.Filename;

                if (!string.IsNullOrEmpty(filePath))
                {
                    switch (bulkUploadOptions.ActiveText)
                    {
                        case "Usuarios":
                            realizarCargasUsuarios(filePath);
                            break;
                        case "Vehiculos":
                            realizarCargasVehiculos(filePath);
                            break;
                        case "Repuestos":
                            realizarCargasRepuestos(filePath);
                            break;
                    }
                }
            }

            fileChooser.Destroy();
        }

        // Método para realizar la carga masiva de usuarios
        private void realizarCargasUsuarios(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(jsonContent))
                {
                    Console.WriteLine("El archivo JSON está vacío.");
                    return;
                }

                var usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(jsonContent);

                if (usuarios != null && usuarios.Count > 0)
                {
                    foreach (var usuario in usuarios)
                    {
                        if (usuario != null && !string.IsNullOrEmpty(usuario.id.ToString()))
                        {
                            listaUsuarios.AgregarUsuarios(new Usuarios(usuario.id, usuario.nombres, usuario.apellidos, usuario.correo, usuario.contrasenia, usuario.edades));
                        }
                        else
                        {
                            Console.WriteLine($"Usuario con ID: {usuario?.id} tiene datos inválidos.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la carga masiva de usuarios de forma correcta.");
                }
                Console.WriteLine("---LISTA USUARIOS---");
                listaUsuarios.Imprimir();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error al deserializar el archivo JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Método para realizar la carga masiva de vehículos
        private void realizarCargasVehiculos(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(jsonContent))
                {
                    Console.WriteLine("El archivo JSON está vacío.");
                    return;
                }

                var vehiculos = JsonConvert.DeserializeObject<List<Vehiculos>>(jsonContent);

                if (vehiculos != null && vehiculos.Count > 0)
                {
                    foreach (var vehiculo in vehiculos)
                    {
                        if (vehiculo != null && !string.IsNullOrEmpty(vehiculo.id.ToString()))
                        {
                            listaVehiculos.AgregarVehiculos(new Vehiculos(vehiculo.id, vehiculo.ID_Usuario, vehiculo.marca, vehiculo.modelo, vehiculo.placa));
                        }
                        else
                        {
                            Console.WriteLine($"Vehículo con ID: {vehiculo?.id} tiene datos inválidos.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la carga masiva de vehículos de forma correcta.");
                }
                Console.WriteLine("---LISTA VEHICULOS---");
                listaVehiculos.Imprimir();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error al deserializar el archivo JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Método para realizar la carga masiva de repuestos
        private void realizarCargasRepuestos(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(jsonContent))
                {
                    Console.WriteLine("El archivo JSON está vacío.");
                    return;
                }

                var repuestos = JsonConvert.DeserializeObject<List<Repuestos>>(jsonContent);

                if (repuestos != null && repuestos.Count > 0)
                {
                    foreach (var repuesto in repuestos)
                    {
                        if (repuesto != null && !string.IsNullOrEmpty(repuesto.id.ToString()))
                        {
                            listaRepuestos.agregarRepuestos(new Repuestos(repuesto.id, repuesto.repuesto, repuesto.detalles, repuesto.costo));
                        }
                        else
                        {
                            Console.WriteLine($"Repuesto con ID: {repuesto?.id} tiene datos inválidos.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la carga masiva de repuestos de forma correcta.");
                }
                Console.WriteLine("---LISTA REPUESTOS---");
                listaRepuestos.RecorridoEnOrden();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error al deserializar el archivo JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}