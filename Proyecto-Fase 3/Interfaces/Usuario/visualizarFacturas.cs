using System;
using Gtk;
using DS;

namespace Interfaces3
{
    public class visualizarFacturas : Window
    {
        private Grid tabla;
        private int filaActual = 1;

        // Estructuras de datos
        private ArbolMerkle listasFacturas = ArbolMerkle.Instance;
        private ListaDoble listaVehiculos = ListaDoble.Instance;
        private ArbolBST listasServicios = ArbolBST.Instance;
        
        // Singleton pattern
        private static visualizarFacturas _instance;
        public static visualizarFacturas Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new visualizarFacturas();
                }
                return _instance;
            }
        }

        public visualizarFacturas() : base("Mis Facturas Pendientes")
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
                MostrarFacturasInOrden();

                contenedor.PackStart(scroll, true, true, 0);
                contenedor.PackStart(back, false, false, 10);

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

            var ordenHeader = new Label("Orden");
            ordenHeader.Xalign = 0f;
            tabla.Attach(ordenHeader, 1, 0, 1, 1);

            var totalHeader = new Label("Total ($)");
            totalHeader.Xalign = 1f;
            tabla.Attach(totalHeader, 2, 0, 1, 1);

            filaActual = 1;
        }

        private void MostrarFacturasInOrden()
        {
            try
            {
                LimpiarDatos();
                // Cambiar el recorrido del árbol B por el de Merkle
                if(listasFacturas.Hojas != null)
                {
                    foreach(var hoja in listasFacturas.Hojas)
                    {
                        if(hoja.facturas != null && EsFacturaDelUsuario(hoja.facturas))
                        {
                            AgregarFilaTabla(hoja.facturas);
                        }
                    }
                }
                tabla.ShowAll();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al mostrar facturas: " + ex.Message);
                MostrarError("Error al cargar facturas");
            }
        }

        private bool EsFacturaDelUsuario(Facturas factura)
        {
            // El árbol de Merkle ya contiene las facturas directamente
            // Solo necesitas verificar el usuario asociado
            var servicio = listasServicios.Buscar(factura.id_Servicio);
            if(servicio == null) return false;

            var vehiculo = listaVehiculos.BuscarVehiculo(servicio.servicios.id_Vehiculo);
            return vehiculo != null && vehiculo.ID_Usuario == ManejoSesion.CurrentUserId;
        }

        private void AgregarFilaTabla(Facturas factura)
        {
            try
            {
                // Buscar información relacionada
                var servicio = listasServicios.Buscar(factura.id_Servicio);
                var vehiculo = servicio != null ? listaVehiculos.BuscarVehiculo(servicio.servicios.id_Vehiculo) : null;
                string infoVehiculo = vehiculo != null ? $"{vehiculo.marca} {vehiculo.modelo}" : "Desconocido";

                // ID Factura
                var idLabel = new Label(factura.id.ToString());
                idLabel.Xalign = 0f;
                tabla.Attach(idLabel, 0, filaActual, 1, 1);

                // ID Servicio
                var ordenLabel = new Label(factura.metodoPago.ToString());
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