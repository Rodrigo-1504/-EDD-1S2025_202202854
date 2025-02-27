using Gtk;
using List;
using System;

namespace Interfaces
{
    public class ingresoManual : Window
    {
        // Entradas de texto globales
        private Entry idEntry, nameEntry, lastnameEntry, mailEntry, pwEntry;
        private Entry idVEntry, idUserEntry, marcaEntry, modeloEntry, placaEntry;
        private Entry idREntry, repuestoEntry, detallesEntry, costoEntry;
        private Entry idSEntry, idRepuestoEntry, idVehiculoEntry, detallesEntryS, costoEntryS;

        private ComboBoxText opciones = new ComboBoxText();

        // Instancias de las listas
        private ListaSimple listaUsuarios = ListaSimple.Instance;
        private ListaDoble listaVehiculos = ListaDoble.Instance;
        private ListaCircular listaRepuestos = ListaCircular.Instance;
        private Cola listaServicios = Cola.Instance;
        private Pila listaFacturas = Pila.Instance;
        private Matriz bitacora = Matriz.Instance;

        // Singleton para la ventana de ingreso manual
        private static ingresoManual _instance;

        public static ingresoManual Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ingresoManual();
                }
                return _instance;
            }
        }

        // Constructor
        public ingresoManual() : base("Manual entry")
        {
            // Configuración de la ventana
            SetDefaultSize(500, 300);
            SetPosition(WindowPosition.Center);

            // Configurar el ComboBox
            opciones.AppendText("Usuario");
            opciones.AppendText("Vehiculo");
            opciones.AppendText("Repuesto");
            opciones.AppendText("Servicio");
            opciones.Active = 0;

            // Crear y configurar los campos de entrada
            HBox campoUsuarios = CreateUsuarioFields();
            HBox campoVehiculos = CreateVehiculoFields();
            HBox camposRepuestos = CreateRepuestoFields();
            HBox campoServicios = CreateServicioFields();

            // Mostrar solo el campo de usuarios al inicio
            campoUsuarios.Show();
            campoVehiculos.Hide();
            camposRepuestos.Hide();
            campoServicios.Hide();

            // Manejar el cambio de opción en el ComboBox
            opciones.Changed += (sender, e) =>
            {
                campoUsuarios.Hide();
                campoVehiculos.Hide();
                camposRepuestos.Hide();
                campoServicios.Hide();

                switch (opciones.ActiveText)
                {
                    case "Usuario":
                        campoUsuarios.Show();
                        break;
                    case "Vehiculo":
                        campoVehiculos.Show();
                        break;
                    case "Repuesto":
                        camposRepuestos.Show();
                        break;
                    case "Servicio":
                        campoServicios.Show();
                        break;
                    default:
                        campoUsuarios.Show();
                        break;
                }
            };

            // Crear y configurar el contenedor principal
            VBox mainContainer = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            Button saveButton = CreateButton("Guardar", GuardarDatos, 5, 5);
            Button backButton = CreateButton("Regresar", goBack, 5, 5);

            mainContainer.PackStart(opciones, false, false, 0);
            mainContainer.PackStart(campoUsuarios, false, false, 0);
            mainContainer.PackStart(campoVehiculos, false, false, 0);
            mainContainer.PackStart(camposRepuestos, false, false, 0);
            mainContainer.PackStart(campoServicios, false, false, 0);
            mainContainer.PackStart(saveButton, false, false, 0);
            mainContainer.PackStart(backButton, false, false, 0);

            Add(mainContainer);
        }

        // Método para crear los campos de entrada de usuarios
        private HBox CreateUsuarioFields()
        {
            VBox labels = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            labels.PackStart(new Label("Id") { MarginTop = 15, MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Nombres") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Apellidos") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Correo") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Contraseña") { MarginBottom = 20 }, false, false, 0);

            VBox entries = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idEntry = new Entry { MarginBottom = 5 };
            nameEntry = new Entry { MarginBottom = 5 };
            lastnameEntry = new Entry { MarginBottom = 5 };
            mailEntry = new Entry { MarginBottom = 5 };
            pwEntry = new Entry { MarginBottom = 5 };

            entries.PackStart(idEntry, false, false, 0);
            entries.PackStart(nameEntry, false, false, 0);
            entries.PackStart(lastnameEntry, false, false, 0);
            entries.PackStart(mailEntry, false, false, 0);
            entries.PackStart(pwEntry, false, false, 0);

            HBox container = new HBox();
            container.PackStart(labels, false, false, 0);
            container.PackStart(entries, false, false, 0);

            return container;
        }

        // Método para crear los campos de entrada de vehículos
        private HBox CreateVehiculoFields()
        {
            VBox labels = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            labels.PackStart(new Label("Id") { MarginTop = 15, MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Id_Usuario") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Marca") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Modelo") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Placa") { MarginBottom = 20 }, false, false, 0);

            VBox entries = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idVEntry = new Entry { MarginBottom = 5 };
            idUserEntry = new Entry { MarginBottom = 5 };
            marcaEntry = new Entry { MarginBottom = 5 };
            modeloEntry = new Entry { MarginBottom = 5 };
            placaEntry = new Entry { MarginBottom = 5 };

            entries.PackStart(idVEntry, false, false, 0);
            entries.PackStart(idUserEntry, false, false, 0);
            entries.PackStart(marcaEntry, false, false, 0);
            entries.PackStart(modeloEntry, false, false, 0);
            entries.PackStart(placaEntry, false, false, 0);

            HBox container = new HBox();
            container.PackStart(labels, false, false, 0);
            container.PackStart(entries, false, false, 0);

            return container;
        }

        // Método para crear los campos de entrada de repuestos
        private HBox CreateRepuestoFields()
        {
            VBox labels = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            labels.PackStart(new Label("Id") { MarginTop = 15, MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Repuesto") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Detalles") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Costo") { MarginBottom = 20 }, false, false, 0);

            VBox entries = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idREntry = new Entry { MarginBottom = 5 };
            repuestoEntry = new Entry { MarginBottom = 5 };
            detallesEntry = new Entry { MarginBottom = 5 };
            costoEntry = new Entry { MarginBottom = 5 };

            entries.PackStart(idREntry, false, false, 0);
            entries.PackStart(repuestoEntry, false, false, 0);
            entries.PackStart(detallesEntry, false, false, 0);
            entries.PackStart(costoEntry, false, false, 0);

            HBox container = new HBox();
            container.PackStart(labels, false, false, 0);
            container.PackStart(entries, false, false, 0);

            return container;
        }

        // Método para crear los campos de entrada de servicios
        private HBox CreateServicioFields()
        {
            VBox labels = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            labels.PackStart(new Label("Id") { MarginTop = 15, MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Id_Repuesto") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Id_Vehiculo") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Detalles") { MarginBottom = 20 }, false, false, 0);
            labels.PackStart(new Label("Costo") { MarginBottom = 20 }, false, false, 0);

            VBox entries = new VBox
            {
                BorderWidth = 20,
                Spacing = 10
            };

            idSEntry = new Entry { MarginBottom = 5 };
            idRepuestoEntry = new Entry { MarginBottom = 5 };
            idVehiculoEntry = new Entry { MarginBottom = 5 };
            detallesEntryS = new Entry { MarginBottom = 5 };
            costoEntryS = new Entry { MarginBottom = 5 };

            entries.PackStart(idSEntry, false, false, 0);
            entries.PackStart(idRepuestoEntry, false, false, 0);
            entries.PackStart(idVehiculoEntry, false, false, 0);
            entries.PackStart(detallesEntryS, false, false, 0);
            entries.PackStart(costoEntryS, false, false, 0);

            HBox container = new HBox();
            container.PackStart(labels, false, false, 0);
            container.PackStart(entries, false, false, 0);

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

        // Método para guardar los datos ingresados
        private void GuardarDatos(object sender, EventArgs e)
        {
            switch (opciones.ActiveText)
            {
                case "Usuario":
                    GuardarUsuario();
                    break;
                case "Vehiculo":
                    GuardarVehiculo();
                    break;
                case "Repuesto":
                    GuardarRepuesto();
                    break;
                case "Servicio":
                    GuardarServicio();
                    break;
                default:
                    Console.WriteLine("Opción no válida");
                    break;
            }

            Console.WriteLine("Datos guardados correctamente");
            LimpiarCampos();
        }

        // Método para guardar un usuario
        private void GuardarUsuario()
        {
            Usuarios newUser = new Usuarios(
                Convert.ToInt32(idEntry.Text),
                nameEntry.Text,
                lastnameEntry.Text,
                mailEntry.Text,
                pwEntry.Text
            );
            listaUsuarios.AgregarUsuarios(newUser);
            Console.WriteLine("\n---LISTA NUEVA DE USUARIOS---");
            listaUsuarios.imprimirLista();
        }

        // Método para guardar un vehículo
        private void GuardarVehiculo()
        {
            Vehiculos newVehicle = new Vehiculos(
                Convert.ToInt32(idVEntry.Text),
                Convert.ToInt32(idUserEntry.Text),
                marcaEntry.Text,
                Convert.ToInt32(modeloEntry.Text),
                placaEntry.Text
            );
            listaVehiculos.agregarVehiculos(newVehicle);
            Console.WriteLine("\n---LISTA NUEVA DE VEHICULOS---");
            listaVehiculos.imprimirListaDoble();
        }

        // Método para guardar un repuesto
        private void GuardarRepuesto()
        {
            Repuestos newRepuesto = new Repuestos(
                Convert.ToInt32(idREntry.Text),
                repuestoEntry.Text,
                detallesEntry.Text,
                Convert.ToDouble(costoEntry.Text)
            );
            listaRepuestos.agregarRepuestos(newRepuesto);
            Console.WriteLine("\n---LISTA NUEVA DE REPUESTOS---");
            listaRepuestos.imprimirListaCircular();
        }

        // Método para guardar un servicio
        private void GuardarServicio()
        {
            try{
                Repuestos buscarRepuesto = listaRepuestos.buscarRepuesto(Convert.ToInt32(idRepuestoEntry.Text));
                Vehiculos buscarVehiculos = listaVehiculos.buscarVehiculo(Convert.ToInt32(idVehiculoEntry.Text));

                if(buscarRepuesto != null && buscarVehiculos != null)
                {
                    Servicios newService = new Servicios(
                        Convert.ToInt32(idSEntry.Text),
                        Convert.ToInt32(idRepuestoEntry.Text),
                        Convert.ToInt32(idVehiculoEntry.Text),
                        detallesEntryS.Text,
                        Convert.ToInt32(costoEntryS.Text)
                    );
                    listaServicios.agregarServicios(newService);
                    Console.WriteLine("\n---LISTA NUEVA DE SERVICIOS---");
                    listaServicios.imprimir();
                    
                    //Factura
                    double costoServicio = Convert.ToInt32(costoEntryS.Text);
                    double costoRepuesto = buscarRepuesto.costo;

                    double total = costoServicio + costoRepuesto;
                    int idFactura = Convert.ToInt32(idSEntry.Text);

                    listaFacturas.agregarFactura(new Facturas(idFactura, idFactura, total));
                    Console.WriteLine("\n---LISTA DE FACTURAS---");
                    listaFacturas.imprimir();

                    bitacora.insertar(buscarVehiculos.id, buscarRepuesto.id, detallesEntryS.Text);
                    Console.WriteLine("\n---MATRIZ DISPERSA---");
                    bitacora.mostrarMatriz();
                }
                else
                {
                    Console.WriteLine("El vehiculo o el repuesto no existe");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Método para limpiar los campos de entrada
        private void LimpiarCampos()
        {
            idEntry.Text = "";
            nameEntry.Text = "";
            lastnameEntry.Text = "";
            mailEntry.Text = "";
            pwEntry.Text = "";

            idVEntry.Text = "";
            idUserEntry.Text = "";
            marcaEntry.Text = "";
            modeloEntry.Text = "";
            placaEntry.Text = "";

            idREntry.Text = "";
            repuestoEntry.Text = "";
            detallesEntry.Text = "";
            costoEntry.Text = "";

            idSEntry.Text = "";
            idRepuestoEntry.Text = "";
            idVehiculoEntry.Text = "";
            detallesEntryS.Text = "";
            costoEntryS.Text = "";
        }
    }
}