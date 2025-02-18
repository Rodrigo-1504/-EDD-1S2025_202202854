using Gtk;
using Interfaces;


namespace Interfaces{
    public class OpcionesAdmin : Window
    {
        public OpcionesAdmin() : base("Options")
        {
            SetDefaultSize(300, 400);
            this.SetPosition(WindowPosition.Center);

            VBox buttons = new VBox();
            buttons.BorderWidth = 20;
            buttons.Spacing = 10;

            Button buttonAll = new Button("Cargas Masivas");
            buttonAll.MarginBottom = 5;
            buttonAll.Clicked += goBulkUpload;

            Button buttonOne = new Button("Ingreso Individual");
            buttonOne.MarginBottom = 5;
            buttonOne.Clicked += goManualEntry;

            Button buttonManagement = new Button("Manejo de Usuarios");
            buttonManagement.MarginBottom = 5;
            buttonManagement.Clicked += goManagementUser;

            Button buttonService = new Button("Generar Servicios");
            buttonService.MarginBottom = 5;
            buttonService.Clicked += goService;

            Button buttonCancel = new Button("Cancelar Facturas");
            buttonCancel.MarginBottom = 5;
            buttonCancel.Clicked += goCancelBill;
            
            buttons.PackStart(buttonAll, true, true, 0);
            buttons.PackStart(buttonOne, true, true, 0);
            buttons.PackStart(buttonManagement, true, true, 0);
            buttons.PackStart(buttonService, true, true, 0);
            buttons.PackStart(buttonCancel, true, true, 0);

            Add(buttons);
        }

        private void goBulkUpload(object sender, EventArgs e)
        {
            cargaMasiva bulkUpload = new cargaMasiva();
            bulkUpload.DeleteEvent += OnWindowDelete;
            bulkUpload.ShowAll();
            this.Destroy();
        }

        private void goManualEntry(object sender, EventArgs e)
        {
            ingresoManual manualEntry = new ingresoManual();
            manualEntry.DeleteEvent += OnWindowDelete;
            manualEntry.ShowAll();
            this.Destroy();
        }

        private void goManagementUser(object sender, EventArgs e)
        {
            manejoUsuarios manageUser = new manejoUsuarios();
            manageUser.DeleteEvent += OnWindowDelete;
            manageUser.ShowAll();
            this.Destroy();
        }

        private void goService(object sender, EventArgs e)
        {
            generarServicios generateService = new generarServicios();
            generateService.DeleteEvent += OnWindowDelete;
            generateService.ShowAll();
            this.Destroy();
        }

        private void goCancelBill(object sender, EventArgs e)
        {
            cancelarFacturas cancelBill = new cancelarFacturas();
            cancelBill.DeleteEvent += OnWindowDelete;
            cancelBill.ShowAll();
            this.Destroy();
        }

        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            Application.Quit();
            args.RetVal = true;
        }
    }
}