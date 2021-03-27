using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIEjercicio7T4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Select(textBox1.SelectionStart, textBox1.SelectionLength);
            label1.Text = "Initial Point: " + textBox1.SelectionStart + "\nLenght: " + textBox1.SelectionLength;

        }
        
    }
}
