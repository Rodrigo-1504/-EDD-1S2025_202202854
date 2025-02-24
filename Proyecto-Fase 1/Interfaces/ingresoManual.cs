using Gtk;
using List;

namespace Interfaces
{
    public class ingresoManual : Window
    {

        //Entradas de texto globales
        private Entry idEntry, nameEntry, lastnameEntry, mailEntry, pwEntry;
        private Entry idVEntry, idUserEntry, marcaEntry, modeloEntry, placaEntry;
        private Entry idREntry, repuestoEntry, detallesEntry, costoEntry;
        private Entry idSEntry, idRepuestoEntry, idVehiculoEntry, detallesEntryS, costoEntryS;

        ComboBoxText opciones = new ComboBoxText();

        //Creando-instanciando las listas para el ingreso manual
        ListaSimple listaUsuarios = ListaSimple.Instance;
        ListaDoble listaVehiculos = ListaDoble.Instance;
        ListaCircular listaRepuestos = ListaCircular.Instance;
        Cola listaServicios = Cola.Instance;

        private static ingresoManual _instance;

        public static ingresoManual Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ingresoManual();
                }
                return _instance;
            }
        }

        public ingresoManual() : base("Manual entry")
        {
            SetDefaultSize(500, 300);
            this.SetPosition(WindowPosition.Center);
            
            opciones.AppendText("Usuario");
            opciones.AppendText("Vehiculo");
            opciones.AppendText("Repuesto");
            opciones.AppendText("Servicio");
            opciones.Active = 0;

            // Contenedor para cada campo
            HBox campoUsuarios = new HBox();
            HBox campoVehiculos = new HBox();
            HBox camposRepuestos = new HBox();
            HBox campoServicios = new HBox();

            // USUARIOS
            VBox entities = new VBox();
            entities.BorderWidth = 20;
            entities.Spacing = 10;

            Label idLabel = new Label("Id");
            idLabel.MarginTop = 15;
            idLabel.MarginBottom = 20;

            Label nameLabel = new Label("Nombres");
            nameLabel.MarginBottom = 20;

            Label lastnameLabel = new Label("Apellidos");
            lastnameLabel.MarginBottom = 20;

            Label mailLabel = new Label("Correo");
            mailLabel.MarginBottom = 20;

            Label pwLabel = new Label("Contraseña");
            pwLabel.MarginBottom = 20;

            entities.PackStart(idLabel, false, false, 0);
            entities.PackStart(nameLabel, false, false, 0);
            entities.PackStart(lastnameLabel, false, false, 0);
            entities.PackStart(mailLabel, false, false, 0);
            entities.PackStart(pwLabel, false, false, 0);

            VBox inputEntities = new VBox();
            inputEntities.BorderWidth = 20;
            inputEntities.Spacing = 10;

            idEntry = new Entry();
            idEntry.MarginBottom = 5;

            nameEntry = new Entry();
            nameEntry.MarginBottom = 5;

            lastnameEntry = new Entry();
            lastnameEntry.MarginBottom = 5;

            mailEntry = new Entry();
            mailEntry.MarginBottom = 5;

            pwEntry = new Entry();
            pwEntry.MarginBottom = 5;

            inputEntities.PackStart(idEntry, false, false, 0);
            inputEntities.PackStart(nameEntry, false, false, 0);
            inputEntities.PackStart(lastnameEntry, false, false, 0);
            inputEntities.PackStart(mailEntry, false, false, 0);
            inputEntities.PackStart(pwEntry, false, false, 0);

            campoUsuarios.PackStart(entities, false, false, 0);
            campoUsuarios.PackStart(inputEntities, false, false, 0);

            // VEHICULOS
            VBox entitiesV = new VBox();
            entitiesV.BorderWidth = 20;
            entitiesV.Spacing = 10;

            Label idVLabel = new Label("Id");
            idVLabel.MarginTop = 15;
            idVLabel.MarginBottom = 20;

            Label idUserLabel = new Label("Id_Usuario");
            idUserLabel.MarginBottom = 20;

            Label marcaLabel = new Label("Marca");
            marcaLabel.MarginBottom = 20;

            Label modeloLabel = new Label("Modelo");
            modeloLabel.MarginBottom = 20;

            Label placaLabel = new Label("Placa");
            placaLabel.MarginBottom = 20;

            entitiesV.PackStart(idVLabel, false, false, 0);
            entitiesV.PackStart(idUserLabel, false, false, 0);
            entitiesV.PackStart(marcaLabel, false, false, 0);
            entitiesV.PackStart(modeloLabel, false, false, 0);
            entitiesV.PackStart(placaLabel, false, false, 0);

            VBox inputEntitiesV = new VBox();
            inputEntitiesV.BorderWidth = 20;
            inputEntitiesV.Spacing = 10;

            idVEntry = new Entry();
            idVEntry.MarginBottom = 5;

            idUserEntry = new Entry();
            idUserEntry.MarginBottom = 5;

            marcaEntry = new Entry();
            marcaEntry.MarginBottom = 5;

            modeloEntry = new Entry();
            modeloEntry.MarginBottom = 5;

            placaEntry = new Entry();
            placaEntry.MarginBottom = 5;

            inputEntitiesV.PackStart(idVEntry, false, false, 0);
            inputEntitiesV.PackStart(idUserEntry, false, false, 0);
            inputEntitiesV.PackStart(marcaEntry, false, false, 0);
            inputEntitiesV.PackStart(modeloEntry, false, false, 0);
            inputEntitiesV.PackStart(placaEntry, false, false, 0);

            campoVehiculos.PackStart(entitiesV, false, false, 0);
            campoVehiculos.PackStart(inputEntitiesV, false, false, 0);

            // REPUESTOS
            VBox entitiesR = new VBox();
            entitiesR.BorderWidth = 20;
            entitiesR.Spacing = 10;

            Label idRLabel = new Label("Id");
            idRLabel.MarginTop = 15;
            idRLabel.MarginBottom = 20;

            Label repuestoLabel = new Label("Repuesto");
            repuestoLabel.MarginBottom = 20;

            Label detallesLabel = new Label("Detalles");
            detallesLabel.MarginBottom = 20;

            Label costoLabel = new Label("Costo");
            costoLabel.MarginBottom = 20;

            entitiesR.PackStart(idRLabel, false, false, 0);
            entitiesR.PackStart(repuestoLabel, false, false, 0);
            entitiesR.PackStart(detallesLabel, false, false, 0);
            entitiesR.PackStart(costoLabel, false, false, 0);

            VBox inputEntitiesR = new VBox();
            inputEntitiesR.BorderWidth = 20;
            inputEntitiesR.Spacing = 10;

            idREntry = new Entry();
            idREntry.MarginBottom = 5;

            repuestoEntry = new Entry();
            repuestoEntry.MarginBottom = 5;

            detallesEntry = new Entry();
            detallesEntry.MarginBottom = 5;

            costoEntry = new Entry();
            costoEntry.MarginBottom = 5;

            inputEntitiesR.PackStart(idREntry, false, false, 0);
            inputEntitiesR.PackStart(repuestoEntry, false, false, 0);
            inputEntitiesR.PackStart(detallesEntry, false, false, 0);
            inputEntitiesR.PackStart(costoEntry, false, false, 0);

            camposRepuestos.PackStart(entitiesR, false, false, 0);
            camposRepuestos.PackStart(inputEntitiesR, false, false, 0);

            // SERVICIOS
            VBox entitiesS = new VBox();
            entitiesS.BorderWidth = 20;
            entitiesS.Spacing = 10;

            Label idSLabel = new Label("Id");
            idSLabel.MarginTop = 15;
            idSLabel.MarginBottom = 20;

            Label idRepuestoLabel = new Label("Id_Repuesto");
            idRepuestoLabel.MarginBottom = 20;

            Label idVehiculoLabel = new Label("Id_Vehiculo");
            idVehiculoLabel.MarginBottom = 20;

            Label detallesLabelS = new Label("Detalles");
            detallesLabelS.MarginBottom = 20;

            Label costoLabelS = new Label("Costo");
            costoLabelS.MarginBottom = 20;

            entitiesS.PackStart(idSLabel, false, false, 0);
            entitiesS.PackStart(idRepuestoLabel, false, false, 0);
            entitiesS.PackStart(idVehiculoLabel, false, false, 0);
            entitiesS.PackStart(detallesLabelS, false, false, 0);
            entitiesS.PackStart(costoLabelS, false, false, 0);

            VBox inputEntitiesS = new VBox();
            inputEntitiesS.BorderWidth = 20;
            inputEntitiesS.Spacing = 10;

            idSEntry = new Entry();
            idSEntry.MarginBottom = 5;

            idRepuestoEntry = new Entry();
            idRepuestoEntry.MarginBottom = 5;

            idVehiculoEntry = new Entry();
            idVehiculoEntry.MarginBottom = 5;

            detallesEntryS = new Entry();
            detallesEntryS.MarginBottom = 5;

            costoEntryS = new Entry();
            costoEntryS.MarginBottom = 5;

            inputEntitiesS.PackStart(idSEntry, false, false, 0);
            inputEntitiesS.PackStart(idRepuestoEntry, false, false, 0);
            inputEntitiesS.PackStart(idVehiculoEntry, false, false, 0);
            inputEntitiesS.PackStart(detallesEntryS, false, false, 0);
            inputEntitiesS.PackStart(costoEntryS, false, false, 0);

            campoServicios.PackStart(entitiesS, false, false, 0);
            campoServicios.PackStart(inputEntitiesS, false, false, 0);

            // Mostrar solo el campo de usuarios al inicio
            campoUsuarios.Show();
            campoVehiculos.Hide();
            camposRepuestos.Hide();
            campoServicios.Hide();

            opciones.Changed += (sender, e) =>
            {
                campoUsuarios.Hide();
                campoVehiculos.Hide();
                camposRepuestos.Hide();
                campoServicios.Hide();

                switch (opciones.ActiveText)
                {
                    case "Usuario":
                        campoUsuarios.Show();
                        break;

                    case "Vehiculo":
                        campoVehiculos.Show();
                        break;

                    case "Repuesto":
                        camposRepuestos.Show();
                        break;

                    case "Servicio":
                        campoServicios.Show();
                        break;

                    default:
                        campoUsuarios.Show();
                        break;
                }
            };

            VBox joinBoxes = new VBox();
            joinBoxes.BorderWidth = 20;
            joinBoxes.Spacing = 10;

            Button save = new Button("Guardar");
            save.MarginTop = 5;
            save.MarginBottom = 5;
            save.Clicked += GuardarDatos;

            Button back = new Button("Regresar");
            back.MarginBottom = 5;
            back.Clicked += goBack;

            joinBoxes.PackStart(opciones, false, false, 0);
            joinBoxes.PackStart(campoUsuarios, false, false, 0);
            joinBoxes.PackStart(campoVehiculos, false, false, 0);
            joinBoxes.PackStart(camposRepuestos, false, false, 0);
            joinBoxes.PackStart(campoServicios, false, false, 0);
            joinBoxes.PackStart(save, false, false, 0);
            joinBoxes.PackStart(back, false, false, 0);

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

        private void GuardarDatos(object sender, EventArgs e)
        {
            switch(opciones.ActiveText)
            {
                case "Usuario":
                    //private Entry idEntry, nameEntry, lastnameEntry, mailEntry, pwEntry;
                    string idUser = idEntry.Text;
                    string nameUser = nameEntry.Text;
                    string lastnameUser = lastnameEntry.Text;
                    string mailUser = mailEntry.Text;
                    string pwUser = pwEntry.Text;

                    Usuarios newUser = new Usuarios(Convert.ToInt32(idUser), nameUser, lastnameUser, mailUser, pwUser);
                    listaUsuarios.AgregarUsuarios(newUser);
                    listaUsuarios.imprimirLista();
                    break;

                case "Vehiculo":
                    //private Entry idVEntry, idUserEntry, marcaEntry, modeloEntry, placaEntry;
                    string idVehiculo = idVEntry.Text;
                    string idUserVehiculo = idUserEntry.Text;
                    string marcaVehiculo = marcaEntry.Text;
                    string modelVehiculo = modeloEntry.Text;
                    string placaVehiculo = placaEntry.Text;

                    Vehiculos newVehicle = new Vehiculos(Convert.ToInt32(idVehiculo), Convert.ToInt32(idUserVehiculo), marcaVehiculo, Convert.ToInt32(modelVehiculo), placaVehiculo);
                    listaVehiculos.agregarVehiculos(newVehicle);
                    listaVehiculos.imprimirListaDoble();
                    break;

                case "Repuesto":
                    //private Entry idREntry, repuestoEntry, detallesEntry, costoEntry;
                    string idRepuesto = idREntry.Text;
                    string repairRepuesto = repuestoEntry.Text;
                    string detalleRepuesto = detallesEntry.Text;
                    string costoRepuesto = costoEntry.Text;

                    Repuestos newReplace = new Repuestos(Convert.ToInt32(idRepuesto), repairRepuesto, detalleRepuesto, Convert.ToDouble(costoRepuesto));
                    listaRepuestos.agregarRepuestos(newReplace);
                    listaRepuestos.imprimirListaCircular();
                    break;

                case "Servicio":
                    //private Entry idSEntry, idRepuestoEntry, idVehiculoEntry, detallesEntryS, costoEntryS;
                    string idServicio = idSEntry.Text;
                    string idRepairServicio = idRepuestoEntry.Text;
                    string idVehiculoServicio = idVehiculoEntry.Text;
                    string detalleServicio = detallesEntryS.Text;
                    string costoServicio = costoEntryS.Text;

                    Servicios newService = new Servicios(Convert.ToInt32(idServicio), Convert.ToInt32(idRepairServicio), Convert.ToInt32(idVehiculoServicio), detalleServicio, Convert.ToInt32(costoServicio));
                    listaServicios.agregarServicios(newService);
                    listaServicios.imprimir();
                    break;

                default:
                    Console.WriteLine("Opcion no valida");
                    break;
            }

            Console.WriteLine("Datos guardados correctamente");
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            idEntry.Text = "";
            nameEntry.Text = "";
            lastnameEntry.Text = "";
            mailEntry.Text = "";
            pwEntry.Text = "";

            idVEntry.Text = "";
            idUserEntry.Text = "";
            marcaEntry.Text = "";
            modeloEntry.Text = "";
            placaEntry.Text = "";

            idREntry.Text = "";
            repuestoEntry.Text = "";
            detallesEntry.Text = "";
            costoEntry.Text = "";

            idSEntry.Text = "";
            idRepuestoEntry.Text = "";
            idVehiculoEntry.Text = "";
            detallesEntryS.Text = "";
            costoEntryS.Text = "";
        }
    }
}