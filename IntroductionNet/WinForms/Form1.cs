using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelperProject;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private GreetingHelper _helper;
        public Form1()
        {
            InitializeComponent();
            _helper = new GreetingHelper();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var words = _helper.HelloWords;

            countryCb.DataSource = new BindingSource(words, null);
            countryCb.DisplayMember = "Key";
            countryCb.ValueMember = "Key";
        }

    

        private void btnGreeting_Click(object sender, EventArgs e)
        {
            _helper.ChooseHelloType(countryCb.SelectedValue.ToString());
            MessageBox.Show(_helper.Greeting(textBox1.Text), "Greeting", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
