using Gtk;

namespace Interfaces
{
    public class inicioSesion : Window
    {
        
        private static inicioSesion _instance;

        public static inicioSesion Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new inicioSesion();
                }
                return _instance;
            }
        }

        Entry mail, password;
        public inicioSesion() : base("Login")
        {
            //Tamaño de la ventana
            SetDefaultSize(400,250);
            
            //Centrar la ventana
            this.SetPosition(WindowPosition.Center);

            //Contenedor para organizar
            VBox container = new VBox();

            //Margenes del contenedor
            container.BorderWidth = 20;
            container.Spacing = 10;

            VBox containerMail = new VBox();
            containerMail.Spacing = 5;

            VBox containerPw = new VBox();
            containerPw.Spacing = 5;

            //Cajas de texto y labels
            Label labelMail = new Label("Correo: ");
            mail = new Entry();

            Label labelPw = new Label("Contraseña: ");
            password = new Entry();

            Button enter = new Button("Login");
            enter.Clicked += OnButtonClicked;

            //Agregar widgets al contenedor
            containerMail.PackStart(labelMail, false, false, 0);
            containerMail.PackStart(mail, true, true, 0);

            containerPw.PackStart(labelPw, false, false, 0);
            containerPw.PackStart(password, true, true, 0);
            
            container.PackStart(containerMail, true, true, 0);
            container.PackStart(containerPw, true, true, 0);
            container.PackStart(enter, true, true, 0);

            Add(container);
        }

        static void OnWindowDelete(object sender, DeleteEventArgs args)
        {
            ((Window)sender).Hide();
            args.RetVal = true;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if(mail.Text != "root@gmail.com" || password.Text != "root123")
                {
                    MessageDialog error = new MessageDialog(
                        this, 
                        DialogFlags.Modal,
                        MessageType.Info,
                        ButtonsType.Ok,
                        "Error, correo o contraseña incorrectas"
                    );

                    error.Run();
                    error.Destroy();
                }else{
                    //Arreglar para ingresar como usuario o como administrador
                    OpcionesAdmin opciones = OpcionesAdmin.Instance;
                    opciones.DeleteEvent += OnWindowDelete;
                    opciones.ShowAll();
                    this.Hide();
                }
        }
    }
}