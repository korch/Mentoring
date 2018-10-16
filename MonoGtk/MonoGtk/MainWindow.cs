using System;
using Gtk;
using HelperProject;

public partial class MainWindow : Gtk.Window
{
    GreetingHelper _helper;
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
        _helper = new GreetingHelper();
        FillCombo(combobox1);
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnButton2Pressed(object sender, EventArgs e)
    {
        var dialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, _helper.Greeting(this.entry1.Text));

        dialog.Run();
        dialog.Destroy();
    }

    void FillCombo(ComboBox cb)
    {
    
        var list = _helper.HelloWords;
        combobox1.Clear();
        CellRendererText cell = new CellRendererText();
        combobox1.PackStart(cell, false);
        combobox1.AddAttribute(cell, "text", 0);
        ListStore store = new ListStore(typeof(string));
        cb.Model = store;

        foreach(var item in list) {
            store.AppendValues(item.Key);
        }
    }

    protected void OnCombobox1Changed(object sender, EventArgs e)
    {
        _helper.ChooseHelloType(combobox1.ActiveText);
    }
}
