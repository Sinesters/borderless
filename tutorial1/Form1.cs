using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tutorial1
{
    public partial class Form1 : Form
    {
        Form2 fr2 = new Form2();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fr2.checkBox1.Checked == true)
            {
                fr2.checkBox1.Checked = false;
            }
            else
            {
                fr2.checkBox1.Checked = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fr2.Visible == true)
            {
                fr2.Visible = false;
            }
            else
            {
                fr2.Visible = true;
            }
        }
    }
}
