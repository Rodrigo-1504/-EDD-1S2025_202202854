using Gtk;

namespace Interfaces
{
    public class manejoUsuarios : Window
    {
        public manejoUsuarios() : base("Users management")
        {
            SetDefaultSize(500, 400);
            this.SetPosition(WindowPosition.Center);

            HBox dataUsers = new HBox();
            

            VBox labels = new VBox();
            labels.BorderWidth = 20;
            labels.Spacing = 10;

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

            labels.PackStart(idLabel, false, false, 0);
            labels.PackStart(nameLabel, false, false, 0);
            labels.PackStart(lastnameLabel, false, false, 0);
            labels.PackStart(mailLabel, false, false, 0);
            labels.PackStart(pwLabel, false, false, 0);


            VBox labels2 = new VBox();
            labels2.BorderWidth = 20;
            labels2.Spacing = 10;

            Entry idEntry = new Entry();
            idEntry.MarginBottom = 5;

            Label nameLabel2 = new Label("");
            nameLabel2.MarginBottom = 20;

            Label lastnameLabel2 = new Label("");
            lastnameLabel2.MarginBottom = 20;
            
            Label mailLabel2 = new Label("");
            mailLabel2.MarginBottom = 20;

            Label pwLabel2 = new Label("");
            pwLabel2.MarginBottom = 20; 

            labels2.PackStart(idEntry, false, false, 0);
            labels2.PackStart(nameLabel2, false, false, 0);
            labels2.PackStart(lastnameLabel2, false, false, 0);
            labels2.PackStart(mailLabel2, false, false, 0);
            labels2.PackStart(pwLabel2, false, false, 0);


            VBox Entry = new VBox();
            Entry.BorderWidth = 20;
            Entry.Spacing = 10;

            Button idButton = new Button("Buscar");
            idButton.MarginBottom = 5;

            Entry nameEntry = new Entry();
            nameEntry.MarginBottom = 5;

            Entry lastnameEntry = new Entry();
            lastnameEntry.MarginBottom = 5;

            Entry mailEntry = new Entry();
            mailEntry.MarginBottom = 5;

            Entry pwEntry = new Entry();
            pwEntry.MarginBottom = 5;

            Entry.PackStart(idButton, false, false, 0);
            Entry.PackStart(nameEntry, false, false, 0);
            Entry.PackStart(lastnameEntry, false, false, 0);
            Entry.PackStart(mailEntry, false, false, 0);
            Entry.PackStart(pwEntry, false, false, 0);

            dataUsers.PackStart(labels, true, true, 0);
            dataUsers.PackStart(labels2, true, true, 0);
            dataUsers.PackStart(Entry, true, true, 0);

            VBox joinAll = new VBox();
            joinAll.BorderWidth = 20;
            joinAll.Spacing = 10;

            Button updateData = new Button("Actualizar");
            updateData.MarginBottom = 5;

            Button deleteData = new Button("Eliminar");
            deleteData.MarginBottom = 5;

            joinAll.PackStart(dataUsers, true, true, 0);
            joinAll.PackStart(updateData, true, true, 0);
            joinAll.PackStart(deleteData, true, true, 0);

            Add(joinAll);

        }
    }
}