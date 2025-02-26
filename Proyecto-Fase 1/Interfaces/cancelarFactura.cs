using Gtk;
using List;

namespace Interfaces
{
    public class cancelarFacturas : Window 
    {
        private Label idLabelData, idOrdenLabelData, totalLabelData;
        private Pila listaFacturas = Pila.Instance;
        private static cancelarFacturas _instance;

        public static cancelarFacturas Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new cancelarFacturas();
                }
                return _instance;
            }
        }

        public cancelarFacturas() : base("Cancel")
        {
            SetDefaultSize(400, 200);
            this.SetPosition(WindowPosition.Center);

            HBox bill = new HBox();

            VBox service = new VBox();
            service.BorderWidth = 20;
            service.Spacing = 10;

            Label idLabel = new Label("Id");
            idLabel.MarginTop = 15;
            idLabel.MarginBottom = 20;

            Label idOrdenLabel = new Label("Id Orden");
            idOrdenLabel.MarginBottom = 20;

            Label totalLabel = new Label("Total");
            totalLabel.MarginBottom = 20;

            Button mostrarFactura = new Button("Mostrar Factura Cancelada");
            mostrarFactura.MarginBottom = 5;
            mostrarFactura.Clicked += cancelacionFactura;


            service.PackStart(idLabel, false, false, 0);
            service.PackStart(idOrdenLabel, false, false, 0);
            service.PackStart(totalLabel, false, false, 0);
            service.PackStart(mostrarFactura, false, false, 0);

            VBox serviceData = new VBox();
            serviceData.BorderWidth = 20;
            serviceData.Spacing = 10;

            idLabelData = new Label("");
            idLabelData.MarginTop = 15;
            idLabelData.MarginBottom = 20;

            idOrdenLabelData = new Label("");
            idOrdenLabelData.MarginBottom = 20;

            totalLabelData = new Label("");
            totalLabelData.MarginBottom = 20;

            Button back = new Button("Regresar");
            back.MarginBottom = 5;
            back.Clicked += goBack;

            serviceData.PackStart(idLabelData, false, false, 0);
            serviceData.PackStart(idOrdenLabelData, false, false, 0);
            serviceData.PackStart(totalLabelData, false, false, 0);
            serviceData.PackStart(back, true, true, 0);

            bill.PackStart(service, true, true, 0);
            bill.PackStart(serviceData, true, true, 0);

            Add(bill);
        }

        private void goBack(Object sender, EventArgs e)
        {
            OpcionesAdmin opciones = OpcionesAdmin.Instance;
            opciones.DeleteEvent += OnWindowDelete;
            opciones.ShowAll();
            this.Hide();
        }

        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            ((Window)sender).Hide();
            args.RetVal = true;
        }

        private void cancelacionFactura(object sender, EventArgs e)
        {
            try
            {
                Facturas facturaCancelada = listaFacturas.eliminarFactura();

                idLabelData.Text = facturaCancelada.id.ToString();
                idOrdenLabelData.Text = facturaCancelada.id_Orden.ToString();
                totalLabelData.Text = facturaCancelada.total.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }
    }
}