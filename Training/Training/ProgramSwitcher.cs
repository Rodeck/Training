using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Training
{
    public partial class ProgramSwitcher : Form
    {

        public List<ProgramForm> Forms { get; set; }

        public ProgramSwitcher(List<ProgramForm> forms)
        {
            Forms = forms;

            InitializeComponent();

            Forms.ForEach(s => comboBox1.Items.Add(s));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Type t = ((ProgramForm)comboBox1.SelectedItem).FormType;
            dynamic instance = Activator.CreateInstance(t);

            instance.Show();
        }
    }
}
