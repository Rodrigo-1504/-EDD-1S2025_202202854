using System.Runtime.InteropServices;
using Gtk;
using Structures;

namespace Interfaces2
{
    public class VisualizarRepuestos : Window
    {

        private ComboBoxText opciones = new ComboBoxText();
        private Grid tabla;
        private int filaActual = 1;

        //LISTAS
        ArbolAVL listaRepuestos = ArbolAVL.Instance;

        private static VisualizarRepuestos _instance;
        public static VisualizarRepuestos Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new VisualizarRepuestos();
                }
                return _instance;
            }
        }

        public VisualizarRepuestos() : base("Visualizacion de Repuestos")
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
            tabla = new Grid
            {
                ColumnSpacing = 10,
                RowSpacing = 10,
                Margin = 20,
                ColumnHomogeneous = false
            };
            scroll.Add(tabla);

            Button back = new Button("Regresar");
            back.Clicked += goBack;

            CrearEncabezados();

            contenedor.PackStart(opciones, false, false, 0);
            contenedor.PackStart(scroll, true, true, 0);
            contenedor.PackStart(back, false, false, 10);

            Add(contenedor);

            MostrarDatosOpcion();
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
                    if(listaRepuestos.raiz != null)
                    {
                        tablaPreOrden();
                    }
                    break;

                case 1:
                    if(listaRepuestos.raiz != null)
                    {
                        tablaInOrden();
                    }
                    break;

                case 2:
                    if(listaRepuestos.raiz != null)
                    {
                        tablaPostOrden();
                    }
                    break;
            }

            tabla.ShowAll();
        }

        private void CrearEncabezados()
        {
            // Limpiar el grid completamente
            // En GTK, para limpiar un Grid, podemos simplemente crear uno nuevo
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

            // Crear encabezados manualmente sin usar arrays
            var idHeader = new Label("ID");
            idHeader.Xalign = 0f;
            tabla.Attach(idHeader, 0, 0, 1, 1);

            var repuestoHeader = new Label("Repuesto");
            repuestoHeader.Xalign = 0f;
            tabla.Attach(repuestoHeader, 1, 0, 1, 1);

            var detallesHeader = new Label("Detalles");
            detallesHeader.Xalign = 0f;
            tabla.Attach(detallesHeader, 2, 0, 1, 1);

            var costoHeader = new Label("Costo ($)");
            costoHeader.Xalign = 1f;
            tabla.Attach(costoHeader, 3, 0, 1, 1);
        }

        private void LimipiarDatos()
        {
            filaActual = 1;
            CrearEncabezados();
        }

        private void tablaPreOrden()
        {
            tablaPreOrdenRecursivo(listaRepuestos.raiz);
        }

        private void tablaPreOrdenRecursivo(NodoAVL nodo)
        {
            if(nodo != null)
            {
                AgregarFilaTabla(nodo.repuestos);
                tablaPreOrdenRecursivo(nodo.izquierda);
                tablaPreOrdenRecursivo(nodo.derecha);
            }
        }

        private void tablaInOrden()
        {
            tablaInOrdenRecursivo(listaRepuestos.raiz);
        }

        private void tablaInOrdenRecursivo(NodoAVL nodo)
        {
            if(nodo != null)
            {
                tablaInOrdenRecursivo(nodo.izquierda);
                AgregarFilaTabla(nodo.repuestos);
                tablaInOrdenRecursivo(nodo.derecha);
            }
        }

        private void tablaPostOrden()
        {
            tablaPostOrdenRecursivo(listaRepuestos.raiz);
        }

        private void tablaPostOrdenRecursivo(NodoAVL nodo)
        {
            if(nodo != null)
            {
                tablaPostOrdenRecursivo(nodo.izquierda);
                tablaPostOrdenRecursivo(nodo.derecha);
                AgregarFilaTabla(nodo.repuestos);
            }
        }

        private void AgregarFilaTabla(Repuestos repuesto)
        {
            // ID
            var idLabel = new Label(repuesto.id.ToString());
            idLabel.Xalign = 0f;
            tabla.Attach(idLabel, 0, filaActual, 1, 1);

            // Repuesto
            var repuestoLabel = new Label(repuesto.repuesto);
            repuestoLabel.Xalign = 0f;
            tabla.Attach(repuestoLabel, 1, filaActual, 1, 1);

            // Detalles
            var detallesLabel = new Label(repuesto.detalles);
            detallesLabel.Xalign = 0f;
            tabla.Attach(detallesLabel, 2, filaActual, 1, 1);

            // Costo
            var costoLabel = new Label(repuesto.costo.ToString("0.00"));
            costoLabel.Xalign = 1f; // Alinear a la derecha
            tabla.Attach(costoLabel, 3, filaActual, 1, 1);

            filaActual++;
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
    }
}