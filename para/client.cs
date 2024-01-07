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
    public partial class client : Form
    {
        public client()
        {
            InitializeComponent();
        }
        string id="";
        Data db = new Data();
        public void MAJList()
        {
            guna2DataGridView1.DataSource = null;
            DataTable table = db.table("select id,nom,tele from client");
            guna2DataGridView1.DataSource = table;
        }
        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (nom.Text != "")
            {
                if (id != "")
                {
                        DialogResult m = MessageBox.Show("Voulez-vous vraiment supprimer ce client " + nom.Text, "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DialogResult.Yes == m)
                        {
                            int mod = db.insert_update_delete(string.Format("DELETE from client where id = {0} ", id));
                            if (mod == 0)
                            {
                                MessageBox.Show("Client non supprimer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show("Client bien supprimer", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                                MAJList();
                                nom.Text = tele.Text = "";
                        }
                    }

                   
                }
                else
                {
                    MessageBox.Show("Veillez choisir d'abord le Client dans le tableu ", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Merci de remplir les champs vide", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            id = "";
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (nom.Text != "" || tele.Text != "")
            {
                if (id != "")
                {
                    int mod = db.insert_update_delete(string.Format("Update client SET nom=lower('{0}'),tele=lower('{1}') where id = {2} ", nom.Text, tele.Text, id));
                    if (mod == 0)
                    {
                        MessageBox.Show("Client non modifier", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Client bien modifier", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    MessageBox.Show("Veillez choisir d'abord le Client dans le tableu ", "Information!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Merci de remplir les champs vide", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            MAJList();
            nom.Text = tele.Text = "";
            id = "";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable table = db.table(string.Format("select id,nom,tele from client WHERE nom LIKE '%{0}%'", textBox1.Text));
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = table;
            id = "";
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = guna2DataGridView1.CurrentCell.RowIndex;
            if (index >= 0 && index < guna2DataGridView1.Rows.Count - 1)
            {
                id = guna2DataGridView1.Rows[index].Cells[0].Value.ToString();
                nom.Text = guna2DataGridView1.Rows[index].Cells[1].Value.ToString();
                tele.Text = guna2DataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }
        public Point mouseLocation;
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void client_Load(object sender, EventArgs e)
        {
            label2.Text = " @ " + login.name;
            MAJList();
            id = "";
        }

        private void client_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void client_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            transactions transactions = new transactions();
            transactions.Show();
            this.Hide();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            Dashboard c = new Dashboard();
            c.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            produit c = new produit();
            c.Show();
            this.Hide();
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            login c = new login();
            c.Show();
            this.Hide();
        }
    }
}
