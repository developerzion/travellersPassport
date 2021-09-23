using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IrishPassport
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to logout ?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                Form1 frm = new Form1();
                this.Hide();
                frm.Show();
            }
        }

        private void registerUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            registeruser ru = new registeruser();
            ru.ShowDialog();
        }

        private void verifyUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verifyPassport vp = new verifyPassport();
            vp.ShowDialog();
        }
    }
}
