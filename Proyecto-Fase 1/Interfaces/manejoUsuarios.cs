using Gtk;
using List;

namespace Interfaces
{
    public class manejoUsuarios : Window
    {

        ListaSimple listaUsuarios = ListaSimple.Instance;

        private Label idLabel, nameLabel, lastnameLabel, mailLabel;
        private Label nameLabel2, lastnameLabel2, mailLabel2;
        private Entry nameEntry, lastnameEntry, mailEntry;
        private Entry idEntry;

        private static manejoUsuarios _instance;

        public static manejoUsuarios Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new manejoUsuarios();
                }
                return _instance;
            }
        }

        public manejoUsuarios() : base("Users management")
        {
            SetDefaultSize(500, 400);
            this.SetPosition(WindowPosition.Center);

            HBox dataUsers = new HBox();
            

            VBox labels = new VBox();
            labels.BorderWidth = 20;
            labels.Spacing = 10;

            idLabel = new Label("Id");
            idLabel.MarginTop = 15;
            idLabel.MarginBottom = 20;

            nameLabel = new Label("Nombres");
            nameLabel.MarginBottom = 20;

            lastnameLabel = new Label("Apellidos");
            lastnameLabel.MarginBottom = 20;
            
            mailLabel = new Label("Correo");
            mailLabel.MarginBottom = 20;

            labels.PackStart(idLabel, false, false, 0);
            labels.PackStart(nameLabel, false, false, 0);
            labels.PackStart(lastnameLabel, false, false, 0);
            labels.PackStart(mailLabel, false, false, 0);

            VBox labels2 = new VBox();
            labels2.BorderWidth = 20;
            labels2.Spacing = 10;

            idEntry = new Entry();
            idEntry.MarginBottom = 5;

            nameLabel2 = new Label("");
            nameLabel2.MarginBottom = 20;

            lastnameLabel2 = new Label("");
            lastnameLabel2.MarginBottom = 20;
            
            mailLabel2 = new Label("");
            mailLabel2.MarginBottom = 20;

            labels2.PackStart(idEntry, false, false, 0);
            labels2.PackStart(nameLabel2, false, false, 0);
            labels2.PackStart(lastnameLabel2, false, false, 0);
            labels2.PackStart(mailLabel2, false, false, 0);
            
            VBox Entry = new VBox();
            Entry.BorderWidth = 20;
            Entry.Spacing = 10;

            Button idButton = new Button("Buscar");
            idButton.MarginBottom = 5;
            idButton.Clicked += buscarUsuario;

            nameEntry = new Entry();
            nameEntry.MarginBottom = 5;

            lastnameEntry = new Entry();
            lastnameEntry.MarginBottom = 5;

            mailEntry = new Entry();
            mailEntry.MarginBottom = 5;

            Entry.PackStart(idButton, false, false, 0);
            Entry.PackStart(nameEntry, false, false, 0);
            Entry.PackStart(lastnameEntry, false, false, 0);
            Entry.PackStart(mailEntry, false, false, 0);

            dataUsers.PackStart(labels, true, true, 0);
            dataUsers.PackStart(labels2, true, true, 0);
            dataUsers.PackStart(Entry, true, true, 0);

            VBox joinAll = new VBox();
            joinAll.BorderWidth = 20;
            joinAll.Spacing = 10;

            Button updateData = new Button("Actualizar");
            updateData.MarginBottom = 5;
            updateData.Clicked += editarUsuario;

            Button deleteData = new Button("Eliminar");
            deleteData.MarginBottom = 5;
            deleteData.Clicked += eliminarUsuario;

            Button back = new Button("Regresar");
            back.MarginBottom = 5;
            back.Clicked += goBack;

            joinAll.PackStart(dataUsers, true, true, 0);
            joinAll.PackStart(updateData, true, true, 0);
            joinAll.PackStart(deleteData, true, true, 0);
            joinAll.PackStart(back, true, true, 0);

            Add(joinAll);

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

        private void buscarUsuario(object sender, EventArgs e)
        {
            Usuarios usuarioBuscado = listaUsuarios.BuscarUsuario(Convert.ToInt32(idEntry.Text));



            if(usuarioBuscado != null)
            {
                //private Label nameLabel2, lastnameLabel2, mailLabel2;
                
                string name = usuarioBuscado.nombres;
                string lastname = usuarioBuscado.apellidos;
                string mail = usuarioBuscado.correo;

                nameLabel2.Text = name;
                lastnameLabel2.Text = lastname;
                mailLabel2.Text = mail;
            }
        }

        private void editarUsuario(object sender, EventArgs e)
        {
            Usuarios usuarioEditar = listaUsuarios.BuscarUsuario(Convert.ToInt32(idEntry.Text));

            if(usuarioEditar != null)
            {
                //private Entry nameEntry, lastnameEntry, mailEntry;
                string newName = nameEntry.Text;
                string newLastname = lastnameEntry.Text;
                string newMail = mailEntry.Text;

                listaUsuarios.actualizarUsuario(usuarioEditar.id, newName, newLastname, newMail);
            }
            Console.WriteLine("\n--NUEVA LISTA---");
            listaUsuarios.imprimirLista();
        }

        private void eliminarUsuario(object sender, EventArgs e)
        {
            Usuarios usuarioEliminar = listaUsuarios.BuscarUsuario(Convert.ToInt32(idEntry.Text));
            
            if(usuarioEliminar != null)
            {
                listaUsuarios.EliminarUsuario(usuarioEliminar.id);
            }

            Console.WriteLine("\n---NUEVA LISTA---");
            listaUsuarios.imprimirLista();
        }
    }
}