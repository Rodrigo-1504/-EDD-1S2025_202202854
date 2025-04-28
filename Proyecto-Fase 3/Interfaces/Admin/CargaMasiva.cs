using Gtk;
using Newtonsoft.Json;
using DS;
using System;
using System.IO;
using System.Collections.Generic;

namespace Interfaces3
{
    public class CargaMasiva : Window
    {
        // Instancias de las listas
        private readonly BlockChain listaUsuarios = BlockChain.Instance;
        private readonly ListaDoble listaVehiculos = ListaDoble.Instance;
        private readonly ArbolAVL listaRepuestos = ArbolAVL.Instance;
        private readonly ArbolBST listaServicios = ArbolBST.Instance;
        private readonly ArbolMerkle listaFacturas = ArbolMerkle.Instance;
        private readonly GrafoNoDirigido relaciones = GrafoNoDirigido.Instance;

        // ComboBox para seleccionar el tipo de carga masiva
        private readonly ComboBoxText bulkUploadOptions = new ComboBoxText();

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

            try
            {
                // Crear y configurar el contenedor principal
                VBox mainContainer = CreateMainContainer();
                Add(mainContainer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar la ventana: {ex.Message}");
                // Considerar mostrar un mensaje al usuario
            }
        }

        // Método para crear el contenedor principal
        private VBox CreateMainContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            try
            {
                // Configurar el ComboBox
                bulkUploadOptions.AppendText("Usuarios");
                bulkUploadOptions.AppendText("Vehiculos");
                bulkUploadOptions.AppendText("Repuestos");
                bulkUploadOptions.AppendText("Servicios");

                // Crear botones
                Button uploadButton = CreateButton("Cargar", SeleccionarArchivo, 20, 5);
                Button backButton = CreateButton("Regresar", goBack, 5, 5);

                // Agregar widgets al contenedor
                container.PackStart(bulkUploadOptions, false, false, 0);
                container.PackStart(uploadButton, true, true, 0);
                container.PackStart(backButton, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la interfaz: {ex.Message}");
                // Podrías añadir un Label de error en la interfaz
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
                // Mostrar mensaje al usuario
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

        // Método para seleccionar un archivo JSON
        private void SeleccionarArchivo(object sender, EventArgs e)
        {
            try
            {
                // Crear el explorador de archivos
                using (FileChooserDialog fileChooser = new FileChooserDialog(
                    "Seleccionar un archivo Json",
                    this,
                    FileChooserAction.Open,
                    "Cancelar", ResponseType.Cancel,
                    "Abrir", ResponseType.Accept))
                {
                    // Filtrar archivos JSON
                    fileChooser.Filter = new FileFilter();
                    fileChooser.Filter.AddPattern("*.json");

                    if (fileChooser.Run() == (int)ResponseType.Accept)
                    {
                        string filePath = fileChooser.Filename;

                        if (!string.IsNullOrEmpty(filePath))
                        {
                            ProcesarArchivoSeleccionado(filePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al seleccionar archivo: {ex.Message}");
                // Mostrar mensaje al usuario
            }
        }

        private void ProcesarArchivoSeleccionado(string filePath)
        {
            try
            {
                string selectedOption = bulkUploadOptions.ActiveText;

                switch (selectedOption)
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
                    case "Servicios":
                        realizarCargarServicios(filePath);
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar archivo: {ex.Message}");
            }
        }

        // Método para realizar la carga masiva de usuarios
        private void realizarCargasUsuarios(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    Console.WriteLine("El archivo JSON está vacío.");
                    return;
                }

                var usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(jsonContent);

                if (usuarios == null || usuarios.Count == 0)
                {
                    Console.WriteLine("No hay usuarios válidos en el archivo.");
                    return;
                }

                int usuariosCargados = 0;
                foreach (var usuario in usuarios)
                {
                    try
                    {
                        if (usuario != null && !string.IsNullOrWhiteSpace(usuario.id.ToString()))
                        {
                            listaUsuarios.addUser(new Usuarios(
                                usuario.id, 
                                usuario.nombres, 
                                usuario.apellidos, 
                                usuario.correo, 
                                usuario.edades,
                                usuario.contrasenia));
                            usuariosCargados++;
                        }
                        else
                        {
                            Console.WriteLine("Usuario con datos inválidos encontrado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al agregar usuario: {ex.Message}");
                    }
                }

                Console.WriteLine($"Se cargaron {usuariosCargados} de {usuarios.Count} usuarios.");
                Console.WriteLine("---LISTA USUARIOS---");
                listaUsuarios.Imprimir();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error en el formato JSON: {jsonEx.Message}");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error de lectura de archivo: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }

        // Método para realizar la carga masiva de vehículos
        private void realizarCargasVehiculos(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    Console.WriteLine("El archivo JSON está vacío.");
                    return;
                }

                var vehiculos = JsonConvert.DeserializeObject<List<Vehiculos>>(jsonContent);

                if (vehiculos == null || vehiculos.Count == 0)
                {
                    Console.WriteLine("No hay vehículos válidos en el archivo.");
                    return;
                }

                int vehiculosCargados = 0;
                foreach (var vehiculo in vehiculos)
                {
                    try
                    {
                        if (vehiculo != null && !string.IsNullOrWhiteSpace(vehiculo.id.ToString()))
                        {
                            listaVehiculos.AgregarVehiculos(new Vehiculos(
                                vehiculo.id, 
                                vehiculo.ID_Usuario, 
                                vehiculo.marca, 
                                vehiculo.modelo, 
                                vehiculo.placa));
                            vehiculosCargados++;
                        }
                        else
                        {
                            Console.WriteLine("Vehículo con datos inválidos encontrado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al agregar vehículo: {ex.Message}");
                    }
                }

                Console.WriteLine($"Se cargaron {vehiculosCargados} de {vehiculos.Count} vehículos.");
                Console.WriteLine("---LISTA VEHICULOS---");
                listaVehiculos.Imprimir();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error en el formato JSON: {jsonEx.Message}");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error de lectura de archivo: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }

        // Método para realizar la carga masiva de repuestos
        private void realizarCargasRepuestos(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    Console.WriteLine("El archivo JSON está vacío.");
                    return;
                }

                var repuestos = JsonConvert.DeserializeObject<List<Repuestos>>(jsonContent);

                if (repuestos == null || repuestos.Count == 0)
                {
                    Console.WriteLine("No hay repuestos válidos en el archivo.");
                    return;
                }

                int repuestosCargados = 0;
                foreach (var repuesto in repuestos)
                {
                    try
                    {
                        if (repuesto != null && !string.IsNullOrWhiteSpace(repuesto.id.ToString()))
                        {
                            listaRepuestos.agregarRepuestos(new Repuestos(
                                repuesto.id, 
                                repuesto.repuesto, 
                                repuesto.detalles, 
                                repuesto.costo));
                            repuestosCargados++;
                        }
                        else
                        {
                            Console.WriteLine("Repuesto con datos inválidos encontrado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al agregar repuesto: {ex.Message}");
                    }
                }

                Console.WriteLine($"Se cargaron {repuestosCargados} de {repuestos.Count} repuestos.");
                Console.WriteLine("---LISTA REPUESTOS---");
                listaRepuestos.RecorridoEnOrden();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error en el formato JSON: {jsonEx.Message}");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error de lectura de archivo: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }

        private void realizarCargarServicios(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    Console.WriteLine("El archivo JSON está vacío.");
                    return;
                }

                var servicios = JsonConvert.DeserializeObject<List<Servicios>>(jsonContent);

                if (servicios == null || servicios.Count == 0)
                {
                    Console.WriteLine("No hay Servicio válidos en el archivo.");
                    return;
                }

                int serviciosCargados = 0;
                foreach (var servicio in servicios)
                {
                    try
                    {
                        if (servicio != null && !string.IsNullOrWhiteSpace(servicio.id.ToString()))
                        {
                            listaServicios.agregarServicios(new Servicios(
                                servicio.id,
                                servicio.id_Repuesto,
                                servicio.id_Vehiculo,
                                servicio.detalles,
                                servicio.costo));
                            serviciosCargados++;
                        }
                        else
                        {
                            Console.WriteLine("Servicio con datos inválidos encontrado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al agregar Servicio: {ex.Message}");
                    }
                }

                Console.WriteLine($"Se cargaron {serviciosCargados} de {servicios.Count} servicios.");
                Console.WriteLine("---LISTA SERVICIOS---");
                listaServicios.RecorridoEnOrden();

                Console.WriteLine("---GRAFO NO DIRIGIDO");
                relaciones.ImprimirGrafoNoDirigido();

                Console.WriteLine("---LISTA FACTURAS---");
                listaFacturas.ImprimirFacturas();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error en el formato JSON: {jsonEx.Message}");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error de lectura de archivo: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }
    }
}