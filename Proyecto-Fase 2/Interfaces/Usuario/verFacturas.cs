using System;
using Gtk;
using Structures;

namespace Interfaces2
{
    public class verFacturas : Window
    {
        private Grid tabla;
        private int filaActual = 1;
        
        // Estructuras de datos
        private ArbolB listaFacturas = ArbolB.Instance;
        private ListaDoble listaVehiculos = ListaDoble.Instance;
        private ArbolBST listaServicios = ArbolBST.Instance;
        
        // Singleton pattern
        private static verFacturas _instance;
        public static verFacturas Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new verFacturas();
                }
                return _instance;
            }
        }

        public verFacturas() : base("Mis Facturas Pendientes")
        {
            try
            {
                SetDefaultSize(900, 400);
                SetPosition(WindowPosition.Center);

                VBox contenedor = new VBox();
                contenedor.BorderWidth = 20;
                contenedor.Spacing = 10;

                // Configurar tabla con scroll
                var scroll = new ScrolledWindow();
                tabla = new Grid
                {
                    ColumnSpacing = 10,
                    RowSpacing = 10,
                    Margin = 20,
                    ColumnHomogeneous = false
                };
                scroll.Add(tabla);

                Button back = new Button("Regresar");
                back.Clicked += Regresar;

                CrearEncabezados();
                MostrarFacturasUsuario();

                contenedor.PackStart(scroll, true, true, 0);
                contenedor.PackStart(back, false, false, 5);

                Add(contenedor);
                DeleteEvent += OnWindowDelete;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al inicializar: " + ex.Message);
                MostrarError("Error al cargar facturas");
                this.Destroy();
            }
        }

        private void CrearEncabezados()
        {
            var parent = tabla.Parent;
            if (parent != null)
            {
                ((Container)parent).Remove(tabla);
            }
            
            tabla = new Grid
            {
                ColumnSpacing = 10,
                RowSpacing = 10,
                Margin = 20,
                ColumnHomogeneous = false
            };

            if (parent != null)
            {
                ((Container)parent).Add(tabla);
            }

            // Encabezados
            var idHeader = new Label("ID Factura");
            idHeader.Xalign = 0f;
            tabla.Attach(idHeader, 0, 0, 1, 1);

            var ordenHeader = new Label("ID Orden");
            ordenHeader.Xalign = 0f;
            tabla.Attach(ordenHeader, 1, 0, 1, 1);

            var totalHeader = new Label("Total ($)");
            totalHeader.Xalign = 1f;
            tabla.Attach(totalHeader, 2, 0, 1, 1);

            filaActual = 1;
        }

        private void MostrarFacturasUsuario()
        {
            try
            {
                LimpiarDatos();
                
                // Obtener todos los vehículos del usuario actual
                var vehiculosUsuario = listaVehiculos.BuscarVehiculo(manejoSesion.currentUserId);
                if(vehiculosUsuario == null)
                {
                    Console.WriteLine("No hay vehiculos registrados");
                    return;
                }
                Console.WriteLine("Vehiculo Id: " + vehiculosUsuario.id);

                var servicioVehiculo = listaServicios.Buscar2(vehiculosUsuario.id);
                if(servicioVehiculo == null)
                {
                    Console.WriteLine("No hay servicios al vehiculo");
                    return;
                }
                Console.WriteLine("Servicio id:" + servicioVehiculo.servicios.id);

                var facturasVehiculo = listaFacturas.Buscar2(servicioVehiculo.servicios.id);
                if(facturasVehiculo == null)
                {
                    Console.WriteLine("No hay facturas pendientes");
                    return;
                }

                AgregarFilaTabla(facturasVehiculo);
                tabla.ShowAll();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al mostrar facturas: " + ex.Message);
                MostrarError("Error al cargar facturas");
            }
        }

        private void AgregarFilaTabla(Facturas factura)
        {
            try
            {
                // ID Factura
                var idLabel = new Label(factura.id.ToString());
                idLabel.Xalign = 0f;
                tabla.Attach(idLabel, 0, filaActual, 1, 1);

                // ID Orden
                var ordenLabel = new Label(factura.id_Servicio.ToString());
                ordenLabel.Xalign = 0f;
                tabla.Attach(ordenLabel, 1, filaActual, 1, 1);

                // Total
                var totalLabel = new Label(factura.total.ToString("0.00"));
                totalLabel.Xalign = 1f;
                tabla.Attach(totalLabel, 2, filaActual, 1, 1);

                filaActual++;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al agregar fila: " + ex.Message);
            }
        }

        private void LimpiarDatos()
        {
            filaActual = 1;
            CrearEncabezados();
        }

        private void Regresar(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                var opciones = OpcionesUsuario.Instance;
                opciones.ShowAll();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al regresar: " + ex.Message);
                MostrarError("Error al regresar al menú");
            }
        }

        private void MostrarError(string mensaje)
        {
            MessageDialog md = new MessageDialog(this, 
                DialogFlags.Modal, 
                MessageType.Error, 
                ButtonsType.Ok, 
                mensaje);
            md.Run();
            md.Destroy();
        }

        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            ((Window)sender).Hide();
            args.RetVal = true;
        }
    }
}