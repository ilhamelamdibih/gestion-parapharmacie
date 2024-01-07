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
    public partial class fournisseur : Form
    {
        public fournisseur()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        Data db = new Data();
        public void MAJList()
        {
            guna2DataGridView1.DataSource = null;
            DataTable table = db.table("select nom,tele,adresse from fournisseur");
            guna2DataGridView1.DataSource = table;
        }
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (nom.Text != "" || tele.Text!=""|| adresse.Text !="")
            {
                DataTable table = db.table(string.Format("select * from fournisseur WHERE lower(nom) LIKE lower('{0}') ", nom.Text));
                if (table.Rows.Count == 0)
                {
                    int c = db.insert_update_delete(string.Format("insert into fournisseur (nom,tele,adresse) values( lower('{0}'), lower('{1}'), lower('{2}'))", nom.Text,tele.Text,adresse.Text));
                    if (c == 0)
                    {
                        MessageBox.Show("Echec d'ajout", "Echec!!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MAJList();
                        nom.Text=tele.Text=adresse.Text = "";
                    }

                }
                else
                {
                    MessageBox.Show("Fournisseur déja exister", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Merci de remplir les champs vide", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            nomF = "";
        }
        string nomF = "";
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void fournisseur_Load(object sender, EventArgs e)
        {
            label2.Text = " @ " + login.name;
            MAJList();
            nomF = "";
        }
        public Point mouseLocation;

        private void fournisseur_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void fournisseur_MouseDown(object sender, MouseEventArgs e)
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
            if(nom.Text=="")
            {
                MessageBox.Show("Merci de remplir le champs nom", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataTable table = db.table(string.Format("select * from fournisseur where LOWER(nom) LIKE LOWER('{0}');", nom.Text));
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("Fournisseur n'existe pas!!", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    DialogResult m = MessageBox.Show("Voulez-vous vraiment supprimer le fournisseur " + nom.Text, "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == m)
                    {
                        int c = db.insert_update_delete(string.Format("DELETE from fournisseur where LOWER(nom) LIKE LOWER('{0}')", nom.Text));
                        if (c == 0)
                        {
                            MessageBox.Show("Fournisseur non supprimer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Fournisseur bien supprimer", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                            MAJList();
                            nom.Text = tele.Text = adresse.Text = "";
                        }
                    }
                }
            }

            nomF = "";
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (nom.Text != "" || tele.Text != "" || adresse.Text != "")
            {
                if (nomF != "")
                {
                    int mod = db.insert_update_delete(string.Format("Update fournisseur SET nom=lower('{0}'),tele='{1}',adresse=lower('{2}') where nom = '{3}' ", nom.Text,tele.Text,adresse.Text, nomF));
                    if (mod == 0)
                    {
                        MessageBox.Show("Fournisseur non modifier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Fournisseur bien modifier", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    MessageBox.Show("Veillez choisir d'abord le Fournisseur dans le tableu ", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Merci de remplir les champs vide", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            MAJList();
            nom.Text = tele.Text = adresse.Text = "";
            nomF = "";
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = guna2DataGridView1.CurrentCell.RowIndex;
            if (index >= 0 && index < guna2DataGridView1.Rows.Count - 1)
            {
                nom.Text = guna2DataGridView1.Rows[index].Cells[0].Value.ToString();
                nomF= guna2DataGridView1.Rows[index].Cells[0].Value.ToString();
                tele.Text = guna2DataGridView1.Rows[index].Cells[1].Value.ToString();
                adresse.Text = guna2DataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataTable table = db.table(string.Format("select nom,tele,adresse from fournisseur WHERE nom LIKE '%{0}%'", textBox3.Text));
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = table;
            nomF = "";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            fournisseur c = new fournisseur();
            c.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            categorie categorie = new categorie();
            categorie.Show();
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

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            login c = new login();
            c.Show();
            this.Hide();
        }
    }
}
