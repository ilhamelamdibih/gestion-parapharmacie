using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace para
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        Data db = new Data();
        public static string name = "";
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("un champs est vide", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DataTable table = db.table(string.Format("select * from user where login='{0}' and password='{1}'", textBox1.Text, textBox2.Text));
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Login et mot de passe incorrecte", "Echec!!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataRow dr = table.Rows[0];
                    name = dr[3].ToString();
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                    this.Hide();
                }
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
        public Point mouseLocation;
        private void login_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
