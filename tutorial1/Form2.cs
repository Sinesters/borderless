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
    public partial class Form2 : Form
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private int m_InitialStyle;
        public const int Gwl_exstyle = -20;
        public const int Ws_notTransparent = 0x80000;
        public const int Ws_transparent = 0x20;

        private const int cGrip = 16;
        private const int cCaption = 32;

        public Form2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.DarkBlue;
            this.TransparencyKey = Color.DarkRed;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if(pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if(pos.X>= this.ClientSize.Width - cGrip && pos.Y >=this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            m_InitialStyle = GetWindowLong(this.Handle, Gwl_exstyle);

            if (Properties.Settings.Default.Frm2WL != null)
            {
                this.Location = Properties.Settings.Default.Frm2WL;
            }
            else
            {
                this.Location = RestoreBounds.Location;
            }
            if(Properties.Settings.Default.Frm2SL != null)
            {
                this.Size = Properties.Settings.Default.Frm2SL;
            }
            else
            {
                this.Size=new Size(282, 248);
            }
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
                ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
                rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
                e.Graphics.FillRectangle(Brushes.Black, rc);
            }
            else
            {
                e.Graphics.Clear(Color.Black);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Properties.Settings.Default.Frm2WL = this.Location;
                Properties.Settings.Default.Frm2SL = this.Size;
                Properties.Settings.Default.chkFrm2 = checkBox1.Checked;
                Properties.Settings.Default.Save();
                checkBox1.Hide();
                SetWindowLong(this.Handle, Gwl_exstyle, m_InitialStyle | Ws_notTransparent | Ws_transparent);
                this.Invalidate();
            }
            else
            {
                Properties.Settings.Default.chkFrm2 = checkBox1.Checked;
                Properties.Settings.Default.Save();
                checkBox1.Show();
                this.Invalidate();
                SetWindowLong(this.Handle, Gwl_exstyle, m_InitialStyle | Ws_notTransparent);
            }
        }

    }
}
