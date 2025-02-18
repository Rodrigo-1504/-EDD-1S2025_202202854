using Gtk;

namespace Interfaces{
    
    public class cargaMasiva : Window
    {
        public cargaMasiva() : base("Bulk Upload")
        {
            SetDefaultSize(300, 300);
            this.SetPosition(WindowPosition.Center);

            VBox entities = new VBox();
            entities.BorderWidth = 20;
            entities.Spacing = 10;

            ComboBoxText bulkUploadOptions = new ComboBoxText();
            bulkUploadOptions.AppendText("Usuarios");
            bulkUploadOptions.AppendText("Vehiculos");
            bulkUploadOptions.AppendText("Repuestos");

            bulkUploadOptions.Changed += (sender, e) =>
            {
                Console.WriteLine("Seleccionado: " + bulkUploadOptions.ActiveText);
            };

            Button upload = new Button("Cargar");
            upload.MarginTop = 20;
            upload.MarginBottom = 5;

            entities.PackStart(bulkUploadOptions, false, false, 0);
            entities.PackStart(upload, true, true, 0);

            Add(entities);
        }
    }

}