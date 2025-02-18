using Gtk;

namespace Interfaces
{
    public class ingresoManual : Window
    {
        public ingresoManual() : base("Manual entry")
        {
            SetDefaultSize(500, 300);
            this.SetPosition(WindowPosition.Center);

            HBox boxes = new HBox();

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

            Entry idEntry = new Entry();
            idEntry.MarginBottom = 5;

            Entry nameEntry = new Entry();
            nameEntry.MarginBottom = 5;

            Entry lastnameEntry = new Entry();
            lastnameEntry.MarginBottom = 5;

            Entry mailEntry = new Entry();
            mailEntry.MarginBottom = 5;

            Entry pwEntry = new Entry();
            pwEntry.MarginBottom = 5;

            inputEntities.PackStart(idEntry, false, false, 0);
            inputEntities.PackStart(nameEntry, false, false, 0);
            inputEntities.PackStart(lastnameEntry, false, false, 0);
            inputEntities.PackStart(mailEntry, false, false, 0);
            inputEntities.PackStart(pwEntry, false, false, 0);

            boxes.PackStart(entities, true, true, 0);
            boxes.PackStart(inputEntities, true, true, 0);
            
            VBox joinBoxes = new VBox();
            joinBoxes.BorderWidth = 20;
            joinBoxes.Spacing = 10;

            Button save = new Button("Guardar");
            save.MarginTop = 5;
            save.MarginBottom = 5;

            joinBoxes.PackStart(boxes, true, true, 0);
            joinBoxes.PackStart(save, true, true, 0);

            Add(joinBoxes);
        }
    }
}