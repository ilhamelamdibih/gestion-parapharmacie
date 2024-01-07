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

namespace para
{
    public partial class produit : Form
    {
        public produit()
        {
            InitializeComponent();
        }
        Data db = new Data();
        public void MAJList()
        {
            guna2DataGridView1.DataSource = null;
            DataTable table = db.table("select nom,nomCategorie,quantite from produit");
            guna2DataGridView1.DataSource = table;
        }
        private void produit_Load(object sender, EventArgs e)
        {
            label2.Text = " @ " + login.name;
            MAJList();
            DataTable dtF = db.table("select * from categorie");
            for (int i = 0; i < dtF.Rows.Count; i++)
            {
                categorieC.Items.Add(dtF.Rows[i][1].ToString());
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public Point mouseLocation;
        private void produit_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void produit_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void nom_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            nomp=nom.Text = categorieC.Text = "";
            categorieC.StartIndex = -1;
        }
        string nomp = "";
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataTable table = db.table(string.Format("select  nom,nomCategorie,quantite from produit WHERE nom LIKE '%{0}%'", textBox3.Text));
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = table;
            nomp = "";
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (nom.Text != ""||categorieC.Text!="")
            {
                if (nomp != "")
                {
                    int mod = db.insert_update_delete(string.Format("Update produit SET nom=lower('{0}'),nomCategorie=lower('{1}') where nom = '{2}' ", nom.Text,categorieC.Text, nomp));
                    if (mod == 0)
                    {
                        MessageBox.Show("Produit non modifier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Produit bien modifier", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    MessageBox.Show("Veillez choisir d'abord la le client dans le tableau ", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Merci de remplir les champs vide", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            MAJList();
            nomp= nom.Text = categorieC.Text = "";
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = guna2DataGridView1.CurrentCell.RowIndex;
            if (index >= 0 && index < guna2DataGridView1.Rows.Count - 1)
            {
                nom.Text = guna2DataGridView1.Rows[index].Cells[0].Value.ToString();
                nomp= guna2DataGridView1.Rows[index].Cells[0].Value.ToString();
                categorieC.Text = guna2DataGridView1.Rows[index].Cells[1].Value.ToString();

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            fournisseur c = new fournisseur();
            c.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            client c = new client();
            c.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            categorie c = new categorie();
            c.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            transactions c = new transactions();
            c.Show();
            this.Hide();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            login c = new login();
            c.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            string filePath = ".\\produits.csv";
            DataTable tableau = db.table("select * from produit");
            // Create a new CSV file
            StreamWriter sw = new StreamWriter(filePath);

            // Write the header row
            object[] header = new object[5];
            header[0] = "Id";
            header[1] = "Nom";
            header[2] = "Quantite";
            header[3] = "Categorie";
            header[4] = "Prix";
            sw.WriteLine(string.Join(";", header));

            for (int i = 0; i < tableau.Rows.Count; i++)
            {
                object[] data = new object[5];
                data[0] = tableau.Rows[i][0].ToString();
                data[1] = tableau.Rows[i][1].ToString();
                data[2] = tableau.Rows[i][2].ToString();
                data[3] = tableau.Rows[i][3].ToString();
                data[4] = tableau.Rows[i][4].ToString();
                sw.WriteLine(string.Join(";", data));
            }
            sw.Close();
            MessageBox.Show("Fichier Excel bien générer !", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string filePath = "C:\\Users\\lenovo\\Desktop\\paraDocs\\produits.csv";
            DataTable tableau = db.table("select * from produit");
            // Create a new CSV file
            StreamWriter sw = new StreamWriter(filePath);

            // Write the header row
            object[] header = new object[5];
            header[0] = "Id";
            header[1] = "Nom";
            header[2] = "Quantite";
            header[3] = "Categorie";
            header[4] = "Prix";
            sw.WriteLine(string.Join(";", header));

            for (int i = 0; i < tableau.Rows.Count; i++)
            {
                object[] data = new object[5];
                data[0] = tableau.Rows[i][0].ToString();
                data[1] = tableau.Rows[i][1].ToString();
                data[2] = tableau.Rows[i][2].ToString();
                data[3] = tableau.Rows[i][3].ToString();
                data[4] = tableau.Rows[i][4].ToString();
                sw.WriteLine(string.Join(";", data));
            }
            sw.Close();
            MessageBox.Show("Fichier Excel bien générer !", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
