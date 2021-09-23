
// ==========================================
// Irish verification system
// Designed by priest/zion
// Sponsored by Devparse
// ==========================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;


namespace IrishPassport
{
    public partial class registeruser : Form
    {
        MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=dbIris");
        MySqlCommand command;
        MySqlDataReader mdr;
        string imglocation = "";
        string imgiris = "";
        public registeruser()
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
                passport.ImageLocation = imglocation;
            }
        }
        public void loadData()
        {
            String qline = "SELECT passportNo,firstname,surname,othername,dob FROM tbl_traveler";
            if (ConnectionState.Open != connect.State) connect.Open();
            command = new MySqlCommand(qline, connect);
            MySqlDataAdapter mdat = new MySqlDataAdapter();
            DataTable dtt = new DataTable();
            mdat.SelectCommand = command;
            mdat.Fill(dtt);
            dataGridView1.DataSource = dtt;
            connect.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var fname = firstname.Text;
                var sname = surname.Text;
                var othern = othername.Text;
                var dateofbirth = dob.Text;
                var sex = gender.Text;
                var st = state.Text;
                var lg = lga.Text;
                var addr = address.Text;
                var passNo = "N" + randomNo().ToString();

                byte[] img = null;
                FileStream fs = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);

                if (fname == "" || sname == "" || othern == "" || dateofbirth == "" || sex == "" || st == "" || lg == "" || addr == "")
                {
                    MessageBox.Show("Error, All fields are required");
                }
                else
                {
                    File.Copy(textBox1.Text, Path.Combine(@"C:\Users\PRIEST\source\repos\IrisPassport\IrishPassport\savedIris\", Path.GetFileName(textBox1.Text)),true);
                    FileInfo fi = new FileInfo(imgiris);
                    String getfname = fi.Name;
                    string qline = "INSERT INTO `tbl_traveler`(`firstname`, `surname`, `othername`, `dob`, `gender`, `state`, `lga`, `address`, `passport`,`irisName`,`passportNo`) VALUES ('" + fname + "','" + sname + "','" + othern + "','" + dateofbirth + "','" + sex + "','" + st + "','" + lg + "','" + addr + "',@img,'"+ getfname + "', '"+ passNo + "')";
                    if (connect.State != ConnectionState.Open) connect.Open();
                    command = new MySqlCommand(qline, connect);
                    command.Parameters.Add(new MySqlParameter("@img", img));
                    mdr = command.ExecuteReader();
                    MessageBox.Show("Registration completed");
                    mdr.Close();
                    connect.Close();
                    resetdata();
                    loadData();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            


        }
        public void resetdata()
        {
            firstname.Text = "";
            surname.Text = "";
            othername.Text = "";
            dob.Text = "";
            gender.Text = "";
            state.Text = "";
            lga.Text = "";
            address.Text = "";
            passport.Image = null;
            irisBox.Image = null;
        }
        public int randomNo()
        {
            Random rn = new Random();
            int randomnumber = rn.Next(100000000, 800000000);
            return randomnumber;
        }

        private void registeruser_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resetdata();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All Images|*.jpg; *.bmp; *.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                imgiris = dialog.FileName.ToString();
                irisBox.Image = new Bitmap(dialog.FileName);
            }
        }
    }
}
