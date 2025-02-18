using Gtk;
using System;
using Interfaces;

class Program: Window
{
    Entry mail, password;

    public Program(): base("Login")
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
                OpcionesAdmin opciones = new OpcionesAdmin();
                opciones.DeleteEvent += OnWindowDelete;
                opciones.ShowAll();
                this.Destroy();
            }
    }

    static void Main(string[] args)
    {
        
        try
        {
            //Inicializar Gtk
            Application.Init();

            //Creacion de una ventana
            Program mainWindow = new Program();

            //Terminar el programa al cerrar la aplicación
            mainWindow.DeleteEvent += OnWindowDelete;

            //Mostrar ventana
            mainWindow.ShowAll();

            //Ejecutar en bucle el programa, osea que no se cierre
            Application.Run();
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
    
    //Funcion para terminar el programa
    static void OnWindowDelete(object sender, DeleteEventArgs args)
    {
        Application.Quit();
        args.RetVal = true;
    }
}