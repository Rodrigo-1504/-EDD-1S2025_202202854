using Gtk;

namespace Interfaces
{
    public class generarServicios : Window
    {
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

            joinBoxes.PackStart(boxes, true, true, 0);
            joinBoxes.PackStart(save, true, true, 0);

            Add(joinBoxes);
        }
    }
}