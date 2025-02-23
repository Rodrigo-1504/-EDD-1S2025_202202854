using Gtk;
using List;
using Newtonsoft.Json; //Libreria para leer Json

namespace Interfaces{
    
    public class cargaMasiva : Window
    {
        //INSTANCIAS
        ListaSimple listaUsuarios = ListaSimple.Instance;
        ListaDoble listaVehiculos = ListaDoble.Instance;
        ListaCircular listaRepuestos = ListaCircular.Instance;

        
        //COMBO-BOX-TEXT PARA HACER CASI TODO DE ESTE ARCHIVO
        ComboBoxText bulkUploadOptions = new ComboBoxText();

        //INSTANCIA DE VENTANA
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

            
            bulkUploadOptions.AppendText("Usuarios");
            bulkUploadOptions.AppendText("Vehiculos");
            bulkUploadOptions.AppendText("Repuestos");

            Button upload = new Button("Cargar");
            upload.Clicked += SeleccionarArchivo;
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

//###################################################### CARGAS MASIVAS ######################################################
        //Buscar archivo Json
        private void SeleccionarArchivo(object? sender, EventArgs e)
        {
            //Crear el explorador de archivos
            FileChooserDialog exploradorArchivos = new FileChooserDialog(
                "Seleccionar un archivo Json",
                this,
                FileChooserAction.Open,
                "Cancelar", ResponseType.Cancel,
                "Abrir", ResponseType.Accept
            );

            //Filtrar archivos json
            exploradorArchivos.Filter = new FileFilter();
            exploradorArchivos.Filter.AddPattern("*.json");

            if(exploradorArchivos.Run() == (int)ResponseType.Accept)
            {
                string rutaArchivo = exploradorArchivos.Filename;

                if(!string.IsNullOrEmpty(rutaArchivo))
                {
                    if(bulkUploadOptions.ActiveText == "Usuarios")
                    {
                        realizarCargasUsuarios(rutaArchivo);
                    }
                    else if(bulkUploadOptions.ActiveText == "Vehiculos")
                    {
                        realizarCargasVehiculos(rutaArchivo);
                    }else if(bulkUploadOptions.ActiveText == "Repuestos")
                    {
                        realizarCargasRepuestos(rutaArchivo);
                    }
                }
            }

            exploradorArchivos.Destroy();
        }

        private void realizarCargasUsuarios(string ruta)
        {
            try
            {
                //Leer archivo json
                string contenidoJson = File.ReadAllText(ruta);

                if(string.IsNullOrEmpty(contenidoJson))
                {
                    Console.WriteLine("El archivo json esta vacio");
                    return;
                }

                var usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(contenidoJson);

                if(usuarios != null && usuarios.Count > 0)
                {
                    foreach(var usuario in usuarios)
                    {
                        if(usuario != null && !string.IsNullOrEmpty((usuario.id).ToString()))
                        {
                            listaUsuarios.AgregarUsuarios(new Usuarios(usuario.id, usuario.nombres, usuario.apellidos, usuario.correo, usuario.contraseña));
                        }
                        else
                        {
                            Console.WriteLine($"Usuario con ID: {usuario?.id} tiene datos invalidos");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la carga masiva de usuario de forma correcta");
                }
                listaUsuarios.imprimirLista();
            }
            catch(JsonException jsonE)
            {
                Console.WriteLine($"Error al deserializar el archivo Json: {jsonE.Message}");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void realizarCargasVehiculos(string ruta)
        {
            try
            {
                //Leer archivo json
                string contenidoJson = File.ReadAllText(ruta);

                if(string.IsNullOrEmpty(contenidoJson))
                {
                    Console.WriteLine("El archivo json esta vacio");
                    return;
                }

                var vehiculos = JsonConvert.DeserializeObject<List<Vehiculos>>(contenidoJson);

                if(vehiculos != null && vehiculos.Count > 0)
                {
                    foreach(var vehiculo in vehiculos)
                    {
                        if(vehiculo != null && !string.IsNullOrEmpty((vehiculo.id).ToString()))
                        {
                            listaVehiculos.agregarVehiculos(new Vehiculos(vehiculo.id, vehiculo.id_user, vehiculo.marca, vehiculo.modelo, vehiculo.placa));
                        }
                        else
                        {
                            Console.WriteLine($"Usuario con ID: {vehiculo?.id} tiene datos invalidos");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la carga masiva de usuario de forma correcta");
                }
                listaVehiculos.imprimirListaDoble();
            }
            catch(JsonException jsonE)
            {
                Console.WriteLine($"Error al deserializar el archivo Json: {jsonE.Message}");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }


        private void realizarCargasRepuestos(string ruta)
        {
            try
            {
                //Leer archivo json
                string contenidoJson = File.ReadAllText(ruta);

                if(string.IsNullOrEmpty(contenidoJson))
                {
                    Console.WriteLine("El archivo json esta vacio");
                    return;
                }

                var repuestos = JsonConvert.DeserializeObject<List<Repuestos>>(contenidoJson);

                if(repuestos != null && repuestos.Count > 0)
                {
                    foreach(var repuesto in repuestos)
                    {
                        if(repuesto != null && !string.IsNullOrEmpty((repuesto.id).ToString()))
                        {
                            listaRepuestos.agregarRepuestos(new Repuestos(repuesto.id, repuesto.repuesto, repuesto.detalles, repuesto.costo));
                        }
                        else
                        {
                            Console.WriteLine($"Usuario con ID: {repuesto?.id} tiene datos invalidos");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se pudo realizar la carga masiva de usuario de forma correcta");
                }
                listaRepuestos.imprimirListaCircular();
            }
            catch(JsonException jsonE)
            {
                Console.WriteLine($"Error al deserializar el archivo Json: {jsonE.Message}");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }

}