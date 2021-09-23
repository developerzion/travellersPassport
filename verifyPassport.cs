using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace IrishPassport
{
    public partial class verifyPassport : Form
    {
        MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=dbIris");
        MySqlCommand command;
        MySqlDataReader mdr;

        String imglocation = "";


        //===============================

        int g11, g22, r11, r22, b11, b22, x1, y1, z1, x2, y2, z2;
        Color a, b;

        Bitmap img1, img2;
        string img1_ref, img2_ref;
        int i, j;

        static string fname1, fname2;
        int count1 = 0, count2 = 0;
        bool flag = true;
        //===============================


        public verifyPassport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "png files(*.png)|.png |jpg files(*.jpg)|*.jpg |All fiels(*.*)|*.*";
            dialog.Filter = "All fiels(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imglocation = dialog.FileName.ToString();
                irishImage.ImageLocation = imglocation;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (progressBar1.Value == 100)
                {
                    progressBar1.Value = 0;
                }
                else
                {
                    progressBar1.Value = progressBar1.Value + 100;
                }

                FileInfo fi = new FileInfo(imglocation);
                String getFilename = fi.Name;

                string qline = "SELECT * FROM tbl_traveler WHERE irisName ='" + getFilename + "'";
                if (connect.State != ConnectionState.Open) connect.Open();
                command = new MySqlCommand(qline, connect);
                mdr = command.ExecuteReader();
                mdr.Read();
                if (mdr.HasRows)
                {
                    
                    if (progressBar1.Value == 100)
                    {
                        timer1.Stop();
                        byte[] img = (byte[])mdr[9];
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox2.Image = Image.FromStream(ms);


                        String passportNo = mdr[11].ToString();
                        String fname = mdr[1].ToString();
                        String sname = mdr[2].ToString();
                        String otherame = mdr[3].ToString();
                        String dob = mdr[4].ToString();
                        String sex = mdr[5].ToString();

                        textBox1.Text = passportNo;
                        firstname.Text = fname;
                        surname.Text = sname;
                        othername.Text = otherame;
                        dateofb.Text = dob;
                        gender.Text = sex;

                        label11.Text = "VERIFIED";
                        label11.ForeColor = Color.Green;
                        label17.Text = passportNo;

                       

                    }
                    
                    connect.Close();
                }
                else
                {
                    resetData();
                    connect.Close();
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void resetData()
        {
            textBox1.Text = "";
            firstname.Text = ""; 
            surname.Text = ""; 
            othername.Text = "";
            dateofb.Text = "";
            gender.Text = "";
            label17.Text = "";
            label11.Text = "NOT VERIFIED";
            label11.ForeColor = Color.Red;
            progressBar1.Value = 0;
            pictureBox2.Image = null;
        }

        public int CountRow()
        {
            string stmt = "SELECT count(*) FROM tbl_traveler";
            int CountRow = 0;
            if (connect.State != ConnectionState.Open) connect.Open();
            command = new MySqlCommand(stmt, connect);
            CountRow = (int)command.ExecuteScalar();
            return CountRow;
        }
    }
}
