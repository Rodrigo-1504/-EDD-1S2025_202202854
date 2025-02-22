using Gtk;

namespace Interfaces
{
    public class generarServicios : Window
    {

        private static generarServicios _instance;

        public static generarServicios Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new generarServicios();
                }
                return _instance;
            }
        }

        public generarServicios() : base("Services")
        {
            SetDefaultSize(500, 300);
            this.SetPosition(WindowPosition.Center);

            HBox boxes = new HBox();

            VBox services = new VBox();
            services.BorderWidth = 20;
            services.Spacing = 10;

            Label idLabel = new Label("Id");
            idLabel.MarginTop = 15;
            idLabel.MarginBottom = 20;

            Label idReplacement = new Label("Id Repuesto");
            idReplacement.MarginBottom = 20;

            Label idVehiculo = new Label("Id Vehiculo");
            idVehiculo.MarginBottom = 20;
            
            Label details = new Label("Detalles");
            details.MarginBottom = 20;

            Label cost = new Label("Costo");
            cost.MarginBottom = 20;

            services.PackStart(idLabel, false, false, 0);
            services.PackStart(idReplacement, false, false, 0);
            services.PackStart(idVehiculo, false, false, 0);
            services.PackStart(details, false, false, 0);
            services.PackStart(cost, false, false, 0);

            VBox inputServices = new VBox();
            inputServices.BorderWidth = 20;
            inputServices.Spacing = 10;

            Entry idEntry = new Entry();
            idEntry.MarginBottom = 5;

            Entry replacementEntry = new Entry();
            replacementEntry.MarginBottom = 5;

            Entry idCarEntry = new Entry();
            idCarEntry.MarginBottom = 5;

            Entry detailsEntry = new Entry();
            detailsEntry.MarginBottom = 5;

            Entry costEntry = new Entry();
            costEntry.MarginBottom = 5;

            inputServices.PackStart(idEntry, false, false, 0);
            inputServices.PackStart(replacementEntry, false, false, 0);
            inputServices.PackStart(idCarEntry, false, false, 0);
            inputServices.PackStart(detailsEntry, false, false, 0);
            inputServices.PackStart(costEntry, false, false, 0);

            boxes.PackStart(services, true, true, 0);
            boxes.PackStart(inputServices, true, true, 0);
            
            VBox joinBoxes = new VBox();
            joinBoxes.BorderWidth = 20;
            joinBoxes.Spacing = 10;

            Button save = new Button("Guardar");
            save.MarginTop = 5;
            save.MarginBottom = 5;

            Button back = new Button("Regresar");
            back.MarginBottom = 5;
            back.Clicked += goBack;

            joinBoxes.PackStart(boxes, true, true, 0);
            joinBoxes.PackStart(save, true, true, 0);
            joinBoxes.PackStart(back, true, true, 0);

            Add(joinBoxes);
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
    }
}