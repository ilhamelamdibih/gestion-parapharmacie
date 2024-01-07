using Guna.UI2.WinForms;
using MySqlX.XDevAPI.Relational;
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
    public partial class categorie : Form
    {
        public categorie()
        {
            InitializeComponent();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                DataTable table = db.table(string.Format("select * from categorie WHERE lower(nom) LIKE lower('{0}');", textBox2.Text));
                if (table.Rows.Count == 0)
                {
                    int c = db.insert_update_delete(string.Format("insert into categorie (nom) values(lower('{0}'))", textBox2.Text));
                    if (c == 0)
                    {
                        MessageBox.Show("Echec d'ajout", "Echec!!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MAJList();
                        textBox2.Text = "";
                    }

                }
                else
                {
                    MessageBox.Show("Categorie déja exister", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Merci de remplir les champs vide", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            id = "";
        }
        Data db = new Data();
        public void MAJList()
        {
            guna2DataGridView1.DataSource = null;
            DataTable table = db.table("select * from categorie");
            guna2DataGridView1.DataSource = table;
        }
        private void categorie_Load(object sender, EventArgs e)
        {
           label2.Text = " @ "+ login.name;
            MAJList();
           id = "";
        }
        public Point mouseLocation;

        private void categorie_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void categorie_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="")
            {
                MessageBox.Show("Merci de remplir le champ nom", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DataTable table = db.table(string.Format("select * from categorie where LOWER(nom) LIKE LOWER('{0}');", textBox2.Text));
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Categorie n'existe pas!!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    DialogResult m = MessageBox.Show("Voulez-vous vraiment supprimer la categorie " + textBox2.Text, "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == m)
                    {
                        int c = db.insert_update_delete(string.Format("DELETE from categorie where LOWER(nom) LIKE LOWER('{0}')", textBox2.Text));
                        if (c == 0)
                        {
                            MessageBox.Show("Categorie non supprimer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Categorie bien supprimer", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                            MAJList();
                            textBox2.Text = "";
                        }
                    }
                }
            }
            
            id = "";
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (id!="")
                {
                    int mod = db.insert_update_delete(string.Format("Update categorie SET nom=lower('{0}') where id = {1} ", textBox2.Text,id));
                    if (mod == 0)
                    {
                        MessageBox.Show("Categorie non modifier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Categorie bien modifier", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    MessageBox.Show("Veillez choisir d'abord la catégorie dans le tableu ", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Merci de remplir les champs vide", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            MAJList();
            textBox2.Text = "";
            id="";
        }
        string id;
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = guna2DataGridView1.CurrentCell.RowIndex;
            if (index >= 0 && index < guna2DataGridView1.Rows.Count - 1)
            {
                id = guna2DataGridView1.Rows[index].Cells[0].Value.ToString();
                textBox2.Text = guna2DataGridView1.Rows[index].Cells[1].Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            DataTable table = db.table(string.Format("select * from categorie WHERE nom LIKE '%{0}%'", textBox1.Text));
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = table;
            id = "";
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            fournisseur fournisseur = new fournisseur();
            fournisseur.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            client client = new client();
            client.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            transactions transactions = new transactions();
            transactions.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            produit c = new produit();
            c.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            categorie c = new categorie();
            c.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            login c = new login();
            c.Show();
            this.Hide();
        }
    }
}
