using System.Runtime.InteropServices;
using Gtk;
using Structures;

namespace Interfaces2
{
    public class verServicios : Window
    {
        private ComboBoxText opciones = new ComboBoxText();
        private Grid tabla;
        private int filaActual = 1;

        // Estructuras de datos
        ArbolBST arbolServicios = ArbolBST.Instance;
        ListaDoble listaVehiculos = ListaDoble.Instance;
        
        // Singleton pattern
        private static verServicios _instance;
        public static verServicios Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new verServicios();
                }
                return _instance;
            }
        }

        public verServicios() : base("Mis Servicios")
        {
            try
            {
                SetDefaultSize(900, 400);
                SetPosition(WindowPosition.Center);

                // Verificar sesión primero
                /*if(!manejoSesion.Login())
                {
                    throw new InvalidOperationException("No hay sesión activa");
                }*/

                VBox contenedor = new VBox();
                contenedor.BorderWidth = 20;
                contenedor.Spacing = 10;

                // Configurar combo box de opciones de orden
                opciones.AppendText("PRE - ORDEN");
                opciones.AppendText("IN - ORDEN");
                opciones.AppendText("POST - ORDEN");
                opciones.Active = 0;
                opciones.Changed += CambioOpciones;

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

                contenedor.PackStart(opciones, false, false, 0);
                contenedor.PackStart(scroll, true, true, 0);
                contenedor.PackStart(back, false, false, 10);

                Add(contenedor);

                MostrarDatosOpcion();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al inicializar: " + ex.Message);
                MessageDialog md = new MessageDialog(this, 
                    DialogFlags.Modal, 
                    MessageType.Error, 
                    ButtonsType.Ok, 
                    "Error: " + ex.Message);
                md.Run();
                md.Destroy();
                this.Destroy();
            }
        }

        private void CambioOpciones(object sender, EventArgs e)
        {
            LimpiarDatos();
            MostrarDatosOpcion();
        }

        private void MostrarDatosOpcion()
        {
            try
            {
                switch(opciones.Active)
                {
                    case 0:
                        if(arbolServicios.raiz != null)
                        {
                            TablaPreOrden();
                        }
                        break;

                    case 1:
                        if(arbolServicios.raiz != null)
                        {
                            TablaInOrden();
                        }
                        break;

                    case 2:
                        if(arbolServicios.raiz != null)
                        {
                            TablaPostOrden();
                        }
                        break;
                }

                tabla.ShowAll();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al mostrar datos: " + ex.Message);
                MostrarError("Error al cargar servicios");
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
            var idHeader = new Label("ID Servicio");
            idHeader.Xalign = 0f;
            tabla.Attach(idHeader, 0, 0, 1, 1);

            var vehiculoHeader = new Label("Vehículo");
            vehiculoHeader.Xalign = 0f;
            tabla.Attach(vehiculoHeader, 1, 0, 1, 1);

            var repuestoHeader = new Label("Repuesto");
            repuestoHeader.Xalign = 0f;
            tabla.Attach(repuestoHeader, 2, 0, 1, 1);

            var detallesHeader = new Label("Detalles");
            detallesHeader.Xalign = 0f;
            tabla.Attach(detallesHeader, 3, 0, 1, 1);

            var costoHeader = new Label("Costo ($)");
            costoHeader.Xalign = 1f;
            tabla.Attach(costoHeader, 4, 0, 1, 1);

            filaActual = 1;
        }

        private void LimpiarDatos()
        {
            filaActual = 1;
            CrearEncabezados();
        }

        private void TablaPreOrden()
        {
            TablaPreOrdenRecursivo(arbolServicios.raiz);
        }

        private void TablaPreOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                // Filtrar por ID de usuario actual
                if(EsServicioDelUsuario(nodo.servicios))
                {
                    AgregarFilaTabla(nodo.servicios);
                }
                TablaPreOrdenRecursivo(nodo.izquierda);
                TablaPreOrdenRecursivo(nodo.derecha);
            }
        }

        private void TablaInOrden()
        {
            TablaInOrdenRecursivo(arbolServicios.raiz);
        }

        private void TablaInOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                TablaInOrdenRecursivo(nodo.izquierda);
                if(EsServicioDelUsuario(nodo.servicios))
                {
                    AgregarFilaTabla(nodo.servicios);
                }
                TablaInOrdenRecursivo(nodo.derecha);
            }
        }

        private void TablaPostOrden()
        {
            TablaPostOrdenRecursivo(arbolServicios.raiz);
        }

        private void TablaPostOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                TablaPostOrdenRecursivo(nodo.izquierda);
                TablaPostOrdenRecursivo(nodo.derecha);
                if(EsServicioDelUsuario(nodo.servicios))
                {
                    AgregarFilaTabla(nodo.servicios);
                }
            }
        }

        private bool EsServicioDelUsuario(Servicios servicio)
        {
            // Obtener el vehículo asociado al servicio
            var vehiculo = listaVehiculos.BuscarVehiculo(servicio.id_Vehiculo);
            
            // Verificar si el vehículo pertenece al usuario actual
            return vehiculo != null && vehiculo.ID_Usuario == manejoSesion.currentUserId;
        }

        private void AgregarFilaTabla(Servicios servicio)
        {
            try
            {
                // Obtener información del vehículo
                var vehiculo = listaVehiculos.BuscarVehiculo(servicio.id_Vehiculo);
                string infoVehiculo = vehiculo != null ? $"{vehiculo.marca} {vehiculo.modelo}" : "Desconocido";

                // ID Servicio
                var idLabel = new Label(servicio.id.ToString());
                idLabel.Xalign = 0f;
                tabla.Attach(idLabel, 0, filaActual, 1, 1);

                // Vehículo
                var vehiculoLabel = new Label(infoVehiculo);
                vehiculoLabel.Xalign = 0f;
                tabla.Attach(vehiculoLabel, 1, filaActual, 1, 1);

                // ID Repuesto (o nombre si lo tienes)
                var repuestoLabel = new Label(servicio.id_Repuesto.ToString());
                repuestoLabel.Xalign = 0f;
                tabla.Attach(repuestoLabel, 2, filaActual, 1, 1);

                // Detalles
                var detallesLabel = new Label(servicio.detalles);
                detallesLabel.Xalign = 0f;
                tabla.Attach(detallesLabel, 3, filaActual, 1, 1);

                // Costo
                var costoLabel = new Label(servicio.costo.ToString("0.00"));
                costoLabel.Xalign = 1f;
                tabla.Attach(costoLabel, 4, filaActual, 1, 1);

                // Estado (podrías añadir esta propiedad a tu clase Servicios)
                var estadoLabel = new Label("Completado"); // o "En progreso", etc.
                estadoLabel.Xalign = 0f;
                tabla.Attach(estadoLabel, 5, filaActual, 1, 1);

                filaActual++;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al agregar fila: " + ex.Message);
            }
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