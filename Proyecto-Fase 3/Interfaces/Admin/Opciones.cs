using Gtk;
using DS;
using System;

namespace Interfaces3
{
    public class Opciones : Window
    {
        // Singleton para la ventana de opciones de administrador
        private static Opciones? _instance;

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
            try
            {
                // Configuración de la ventana
                SetDefaultSize(300, 400);
                SetPosition(WindowPosition.Center);

                // Crear y configurar el contenedor de botones
                VBox buttonsContainer = CreateButtonsContainer();
                Add(buttonsContainer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar la ventana de opciones: {ex.Message}");
                ShowErrorMessage("Error al inicializar la ventana de opciones");
            }
        }

        // Método para crear el contenedor de botones
        private VBox CreateButtonsContainer()
        {
            VBox container = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            try
            {
                // Crear y configurar los botones
                Button cargaMasiva = CreateButton("Cargas Masivas", goCargas);
                Button insertarUsuarios = CreateButton("Insertar Usuarios", goUsers);
                Button visualizarUsuarios = CreateButton("Visualizar Usuarios", goVisualizacionUsers);
                Button visualizarRepuestos = CreateButton("Visualizacion de Repuestos", goVisualizacionR);
                Button controlLogueo = CreateButton("Control de Logueo", goControl);
                Button generarServicios = CreateButton("Generar Servicios", goServicios);
                Button generarBackUp = CreateButton("Generar Backup", goBackUp);
                Button cargarBackUp = CreateButton("Cargar BackUp", goCargarBackUp);
                Button generarReportes = CreateButton("Generar Reportes", goReportes);

                // Agregar botones al contenedor
                container.PackStart(cargaMasiva, true, true, 0);
                container.PackStart(insertarUsuarios, true, true, 0);
                container.PackStart(visualizarUsuarios, true, true, 0);
                container.PackStart(visualizarRepuestos, true, true, 0);
                container.PackStart(controlLogueo, true, true, 0);
                container.PackStart(generarServicios, true, true, 0);            
                container.PackStart(generarBackUp, true, true, 0);
                container.PackStart(cargarBackUp, true, true, 0);
                container.PackStart(generarReportes, true, true, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear botones: {ex.Message}");
                ShowErrorMessage("Error al crear la interfaz de opciones");
            }

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
            try
            {
                OpenWindow(CargaMasiva.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir Cargas Masivas: {ex.Message}");
                ShowErrorMessage("Error al abrir la ventana de Cargas Masivas");
            }
        }

        private void goUsers(object sender, EventArgs e)
        {
            try
            {
                OpenWindow(InsertarUsuarios.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir Cargas Masivas: {ex.Message}");
                ShowErrorMessage("Error al abrir la ventana de Cargas Masivas");
            }
        }

        private void goVisualizacionUsers(object sender, EventArgs e)
        {
            try
            {
                OpenWindow(visualizarUsuarios.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir Cargas Masivas: {ex.Message}");
                ShowErrorMessage("Error al abrir la ventana de Cargas Masivas");
            }
        }

        private void goVisualizacionR(object sender, EventArgs e)
        {
            try
            {
                OpenWindow(VisualizarRepuestos.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir Visualización de Repuestos: {ex.Message}");
                ShowErrorMessage("Error al abrir la ventana de Visualización");
            }
        }

        private void goServicios(object sender, EventArgs e)
        {
            try
            {
                OpenWindow(GenerarServicios.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir Generar Servicios: {ex.Message}");
                ShowErrorMessage("Error al abrir la ventana de Servicios");
            }
        }

        private void goControl(object sender, EventArgs e)
        {
            try
            {
                // Crear ventana para mostrar logs
                Window logWindow = new Window("Control de Logueo")
                {
                    DefaultWidth = 800,
                    DefaultHeight = 600,
                    WindowPosition = WindowPosition.Center
                };

                // Crear contenedor principal
                VBox mainBox = new VBox
                {
                    BorderWidth = 20,
                    Spacing = 10
                };

                // Crear tabla con scroll
                ScrolledWindow scroll = new ScrolledWindow();
                Grid grid = new Grid
                {
                    ColumnSpacing = 10,
                    RowSpacing = 10,
                    Margin = 10
                };
                scroll.Add(grid);

                // Crear encabezados
                grid.Attach(new Label("Usuario") { Xalign = 0f }, 0, 0, 1, 1);
                grid.Attach(new Label("Acción") { Xalign = 0f }, 1, 0, 1, 1);
                grid.Attach(new Label("Fecha/Hora") { Xalign = 0f }, 2, 0, 1, 1);

                // Obtener logs
                var logs = ManejoSesion.GetAccessLogs();
                int row = 1;

                // Mostrar logs en la tabla
                foreach (var log in logs)
                {
                    grid.Attach(new Label(log.Usuario) { Xalign = 0f }, 0, row, 1, 1);
                    grid.Attach(new Label(log.Accion) { Xalign = 0f }, 1, row, 1, 1);
                    grid.Attach(new Label(log.Fecha.ToString("yyyy-MM-dd HH:mm:ss.ff")) { Xalign = 0f }, 2, row, 1, 1);
                    row++;
                }

                // Botón para regresar
                Button backButton = new Button("Regresar");
                backButton.Clicked += (s, args) => 
                {
                    try
                    {
                        logWindow.Hide();
                        this.ShowAll();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al regresar: {ex.Message}");
                    }
                };

                // Agregar elementos al contenedor principal
                mainBox.PackStart(scroll, true, true, 0);
                mainBox.PackStart(backButton, false, false, 5);

                // Configurar ventana
                logWindow.Add(mainBox);
                logWindow.DeleteEvent += (s, args) => 
                {
                    try
                    {
                        logWindow.Hide();
                        this.ShowAll();
                        args.RetVal = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al cerrar ventana de logs: {ex.Message}");
                    }
                };

                // Mostrar ventana y ocultar la actual
                logWindow.ShowAll();
                this.Hide();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al mostrar logs: {ex.Message}");
                ShowErrorMessage("Error al mostrar el control de logueo");
            }
        }

        private void goBackUp(object sender, EventArgs e)
        {
            try
            {
                //OpenWindow(GenerarServicios.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir Generar Servicios: {ex.Message}");
                ShowErrorMessage("Error al abrir la ventana de Servicios");
            }
        }

        private void goCargarBackUp(object sender, EventArgs e)
        {
            try
            {
                //OpenWindow(GenerarServicios.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al abrir Generar Servicios: {ex.Message}");
                ShowErrorMessage("Error al abrir la ventana de Servicios");
            }
        }

        private void goReportes(object sender, EventArgs e)
        {
            try
            {
                OpenWindow(Login.Instance);
                ManejoSesion.Logout();
                
                BlockChain listaUsuarios = BlockChain.Instance;
                ListaDoble listaVehiculos = ListaDoble.Instance;
                ArbolBST listaServicios = ArbolBST.Instance;
                ArbolAVL listaRepuestos = ArbolAVL.Instance;
                GrafoNoDirigido relaciones = GrafoNoDirigido.Instance;
                ArbolMerkle listaFacturas = ArbolMerkle.Instance;

                string dotBlockChain = listaUsuarios.GenerarDot();
                string dotDoble = listaVehiculos.graphvizDoble();
                string dotBST = listaServicios.graphvizBST();
                string dotAVL = listaRepuestos.graphvizAVL();
                string dotGrafo = relaciones.GenerarDOT();
                string dotMerkle = listaFacturas.graphvizMerkle();

                try
                {
                    GenerateReport("Block Chain", dotBlockChain);
                    GenerateReport("Lista Doble", dotDoble);
                    GenerateReport("BST", dotBST);
                    GenerateReport("AVL", dotAVL);
                    GenerateReport("Grafo No Dirigido", dotGrafo);
                    GenerateReport("Merkle", dotMerkle);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al generar reportes: {ex.Message}");
                    ShowErrorMessage("Error al generar algunos reportes");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en proceso de reportes: {ex.Message}");
                ShowErrorMessage("Error al procesar los reportes");
            }
        }

        private void GenerateReport(string name, string dotContent)
        {
            try
            {
                Dot_Png.Convertidor.generarArchivoDot(name, dotContent);
                Dot_Png.Convertidor.ConvertirDot_a_Png($"{name}.dot");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar reporte {name}: {ex.Message}");
                throw; // Re-lanzar para manejo en el nivel superior
            }
        }

        // Método para abrir una ventana y ocultar la actual
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
                Console.WriteLine($"Error al abrir ventana: {ex.Message}");
                ShowErrorMessage("Error al navegar a la ventana solicitada");
                this.ShowAll(); // Regresar a la ventana actual si hay error
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

        // Método para mostrar mensajes de error
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
                Console.WriteLine($"Error al mostrar mensaje de error: {ex.Message}");
            }
        }
    }
}