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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



            comboBox1.Items.Add(new Map() { Name = "Bakra", ImagePath = "//Bakra.png" });
            comboBox1.Items.Add(new Map() { Name = "Yayang", ImagePath = "//Yayang.png" });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox) sender;
            Map selectedMap = (Map)comboBox.SelectedItem;
            string path = Application.StartupPath + selectedMap.ImagePath;
            pictureBox1.Image = Image.FromFile(@path);
        }
    }

    public class Map
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}
