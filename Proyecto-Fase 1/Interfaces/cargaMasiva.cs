using Gtk;

namespace Interfaces{
    
    public class cargaMasiva : Window
    {

        private static cargaMasiva _instance;

        public static cargaMasiva Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new cargaMasiva();
                }
                return _instance;
            }
        }

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

            Button back = new Button("Regresar");
            back.MarginBottom = 5;
            back.Clicked += goBack;

            entities.PackStart(bulkUploadOptions, false, false, 0);
            entities.PackStart(upload, true, true, 0);
            entities.PackStart(back, true, true, 0);
            

            Add(entities);
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