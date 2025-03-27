using System.Runtime.InteropServices;
using Gtk;
using Structures;

namespace Interfaces2
{
    public class verServicios : Window
    {

        private ComboBoxText opciones = new ComboBoxText();
        private Grid table;
        private int filaActual = 1;

        //LISTAS
        ArbolBST listaServicios = ArbolBST.Instance;

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

        public verServicios() : base("Visualizacion de Servicios")
        {
            try
            {
                SetDefaultSize(900, 400);
                SetPosition(WindowPosition.Center);

                VBox contenedor = new VBox();
                contenedor.BorderWidth = 20;
                contenedor.Spacing = 10;

                opciones.AppendText("PRE - ORDEN");
                opciones.AppendText("IN - ORDEN");
                opciones.AppendText("POST - ORDEN");
                opciones.Active = 0;
                opciones.Changed += cambioOpciones;

                var scroll = new ScrolledWindow();
                table = new Grid
                {
                    ColumnSpacing = 10,
                    RowSpacing = 10,
                    Margin = 20,
                    ColumnHomogeneous = false
                };
                scroll.Add(table);

                Button back = new Button("Regresar");
                back.Clicked += goBack;

                CrearEncabezados();

                contenedor.PackStart(opciones, false, false, 0);
                contenedor.PackStart(scroll, true, true, 0);
                contenedor.PackStart(back, false, false, 10);

                Add(contenedor);

                MostrarDatosOpcion();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            
        }

        private void cambioOpciones(object sender, EventArgs e)
        {
            LimipiarDatos();
            MostrarDatosOpcion();
        }

        private void MostrarDatosOpcion()
        {
            switch(opciones.Active)
            {
                case 0:
                    if(listaServicios.raiz != null)
                    {
                        tablePreOrden();
                    }
                    break;

                case 1:
                    if(listaServicios.raiz != null)
                    {
                        tableInOrden();
                    }
                    break;

                case 2:
                    if(listaServicios.raiz != null)
                    {
                        tablePostOrden();
                    }
                    break;
            }

            table.ShowAll();
        }

        private void CrearEncabezados()
        {
            // Limpiar el grid completamente
            // En GTK, para limpiar un Grid, podemos simplemente crear uno nuevo
            var parent = table.Parent;
            if (parent != null)
            {
                ((Container)parent).Remove(table);
            }
            
            table = new Grid
            {
                ColumnSpacing = 10,
                RowSpacing = 10,
                Margin = 20,
                ColumnHomogeneous = false
            };

            if (parent != null)
            {
                ((Container)parent).Add(table);
            }

            // Crear encabezados manualmente sin usar arrays
            var idHeader = new Label("ID");
            idHeader.Xalign = 0f;
            table.Attach(idHeader, 0, 0, 1, 1);

            var repuestoHeader = new Label("Repuesto");
            repuestoHeader.Xalign = 0f;
            table.Attach(repuestoHeader, 1, 0, 1, 1);

            var VehiculoHeader = new Label("Vehiculo");
            VehiculoHeader.Xalign = 0f;
            table.Attach(VehiculoHeader, 2, 0, 1, 1);

            var detallesHeader = new Label("Detalles");
            detallesHeader.Xalign = 0f;
            table.Attach(detallesHeader, 3, 0, 1, 1);

            var costoHeader = new Label("Costo ($)");
            costoHeader.Xalign = 1f;
            table.Attach(costoHeader, 4, 0, 1, 1);
        }

        private void LimipiarDatos()
        {
            filaActual = 1;
            CrearEncabezados();
        }

        private void tablePreOrden()
        {
            tablePreOrdenRecursivo(listaServicios.raiz);
        }

        private void tablePreOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                AgregarFilatable(nodo.servicios);
                tablePreOrdenRecursivo(nodo.izquierda);
                tablePreOrdenRecursivo(nodo.derecha);
            }
        }

        private void tableInOrden()
        {
            tableInOrdenRecursivo(listaServicios.raiz);
        }

        private void tableInOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                tableInOrdenRecursivo(nodo.izquierda);
                AgregarFilatable(nodo.servicios);
                tableInOrdenRecursivo(nodo.derecha);
            }
        }

        private void tablePostOrden()
        {
            tablePostOrdenRecursivo(listaServicios.raiz);
        }

        private void tablePostOrdenRecursivo(NodoBST nodo)
        {
            if(nodo != null)
            {
                tablePostOrdenRecursivo(nodo.izquierda);
                tablePostOrdenRecursivo(nodo.derecha);
                AgregarFilatable(nodo.servicios);
            }
        }

        private void AgregarFilatable(Servicios servicio)
        {
            // ID
            var idLabel = new Label(servicio.id.ToString());
            idLabel.Xalign = 0f;
            table.Attach(idLabel, 0, filaActual, 1, 1);

            // Repuesto
            var repuestoLabel = new Label(servicio.id_Repuesto);
            repuestoLabel.Xalign = 0f;
            table.Attach(repuestoLabel, 1, filaActual, 1, 1);

            // Vehiculos
            var vehiculosLabel = new Label(servicio.id_Vehiculo);
            vehiculosLabel.Xalign = 0f;
            table.Attach(vehiculosLabel, 2, filaActual, 1, 1);

            // Detalles
            var detallesLabel = new Label(servicio.detalles);
            detallesLabel.Xalign = 0f;
            table.Attach(detallesLabel, 3, filaActual, 1, 1);

            // Costo
            var costoLabel = new Label(servicio.costo.ToString("0.00"));
            costoLabel.Xalign = 1f; // Alinear a la derecha
            table.Attach(costoLabel, 4, filaActual, 1, 1);

            filaActual++;
        }

        // Método para manejar el evento de clic en el botón "Regresar"
        private void goBack(object sender, EventArgs e)
        {
            OpcionesUsuario opciones = OpcionesUsuario.Instance;
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
    }
}