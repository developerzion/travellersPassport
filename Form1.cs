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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = username.Text;
            var pass = password.Text;

            if(user == "" || pass == "")
            {
                MessageBox.Show("Error: Field Username and Passwowrd is requied");
            }
            else
            {
                if(user == "admin" && pass == "admin")
                {
                    MessageBox.Show("Authorization successfully granted");
                    this.Hide();
                    dashboard db = new dashboard();
                    db.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error: Invalid login parameters");

                }
            }
        }
    }
}
