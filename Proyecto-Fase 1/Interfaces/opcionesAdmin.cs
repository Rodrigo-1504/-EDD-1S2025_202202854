using Gtk;
using Interfaces;


namespace Interfaces{
    public class OpcionesAdmin : Window
    {
        //Instanciar 1 vez la ventana, para poder reutilizarla y no destruir y luego volver a crearla
        private static OpcionesAdmin _instance;

        public static OpcionesAdmin Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new OpcionesAdmin();
                }
                return _instance;
            }
        }

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
            
            Button buttonBack = new Button("Regresar");
            buttonBack.MarginBottom = 5;
            buttonBack.Clicked += goBack;

            buttons.PackStart(buttonAll, true, true, 0);
            buttons.PackStart(buttonOne, true, true, 0);
            buttons.PackStart(buttonManagement, true, true, 0);
            buttons.PackStart(buttonService, true, true, 0);
            buttons.PackStart(buttonCancel, true, true, 0);
            buttons.PackStart(buttonBack, true, true, 0);

            Add(buttons);
        }

        private void goBulkUpload(object sender, EventArgs e)
        {
            cargaMasiva bulkUpload = cargaMasiva.Instance;
            bulkUpload.DeleteEvent += OnWindowDelete;
            bulkUpload.ShowAll();
            this.Hide();
        }

        private void goManualEntry(object sender, EventArgs e)
        {
            ingresoManual manualEntry = ingresoManual.Instance;
            manualEntry.DeleteEvent += OnWindowDelete;
            manualEntry.ShowAll();
            this.Hide();
        }

        private void goManagementUser(object sender, EventArgs e)
        {
            manejoUsuarios manageUser = manejoUsuarios.Instance;
            manageUser.DeleteEvent += OnWindowDelete;
            manageUser.ShowAll();
            this.Hide();
        }

        private void goService(object sender, EventArgs e)
        {
            generarServicios generateService = generarServicios.Instance;
            generateService.DeleteEvent += OnWindowDelete;
            generateService.ShowAll();
            this.Hide();
        }

        private void goCancelBill(object sender, EventArgs e)
        {
            cancelarFacturas cancelBill = cancelarFacturas.Instance;
            cancelBill.DeleteEvent += OnWindowDelete;
            cancelBill.ShowAll();
            this.Hide();
        }

        private void goBack(Object sender, EventArgs e)
        {
            inicioSesion inicio = inicioSesion.Instance;
            inicio.DeleteEvent += OnWindowDelete;
            inicio.ShowAll();
            this.Hide();
        }

        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            ((Window)sender).Hide();
            args.RetVal = true;
        }
    }
}