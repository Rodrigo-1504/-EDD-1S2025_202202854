using Gtk;

namespace Interfaces
{
    public class cancelarFacturas : Window 
    {
        public cancelarFacturas() : base("Cancel")
        {
            SetDefaultSize(300, 200);
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

            service.PackStart(idLabel, false, false, 0);
            service.PackStart(idOrdenLabel, false, false, 0);
            service.PackStart(totalLabel, false, false, 0);

            VBox serviceData = new VBox();
            serviceData.BorderWidth = 20;
            serviceData.Spacing = 10;

            Label idLabelData = new Label("");
            idLabelData.MarginTop = 15;
            idLabelData.MarginBottom = 20;

            Label idOrdenLabelData = new Label("");
            idOrdenLabelData.MarginBottom = 20;

            Label totalLabelData = new Label("");
            totalLabelData.MarginBottom = 20;

            service.PackStart(idLabelData, false, false, 0);
            service.PackStart(idOrdenLabelData, false, false, 0);
            service.PackStart(totalLabelData, false, false, 0);


            bill.PackStart(service, true, true, 0);
            bill.PackStart(serviceData, true, true, 0);

            Add(bill);
        }
    }
}