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
    public partial class loadingPage : Form
    {
        public loadingPage()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = progressBar1.Value + 20;
            if(progressBar1.Value == 100)
            {
                timer1.Stop();
                this.Hide();
                Form1 fm = new Form1();
                fm.ShowDialog();
            }
           
        }

        private void loadingPage_Load(object sender, EventArgs e)
        {

        }
    }
}
