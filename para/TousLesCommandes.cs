using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace para
{
    public partial class TousLesCommandes : Form
    {
        public TousLesCommandes()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }
        public void MAJList(string req)
        {
            listVente.DataSource = null;
            listVente.DataSource = db.table(req);
        }
        Data db = new Data();
        private void TousLesCommandes_Load(object sender, EventArgs e)
        {
            label2.Text = " @ " + login.name;

            MAJList("select * from commande");

        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            MAJList("select * from commande where client like '%" + search.Text + "%'");

        }

        string selectCommande = "";
        string idcommande = "";
        string produit;
        string qtyD;
        int indexDeleteCommande = -1;
        int indexDeleteProduit = -1;

        private void listVente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int pos = listVente.CurrentCell.RowIndex;
            if (pos >= 0 && pos <= listVente.Rows.Count - 1)
            {
                DataGridViewRow r = listVente.Rows[pos];
                selectCommande = r.Cells[0].Value.ToString();
                indexDeleteCommande = pos;
                listProduit.DataSource = null;
                listProduit.DataSource = db.table("select * from detailcommande where idCommande='" + selectCommande + "'");

            }
        }

        private void listProduit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int pos = listProduit.CurrentCell.RowIndex;
            if (pos >= 0 && pos <= listProduit.Rows.Count - 1)
            {
                DataGridViewRow r = listProduit.Rows[pos];
                idcommande = r.Cells[0].Value.ToString();
                produit = r.Cells[1].Value.ToString();
                qtyD = r.Cells[2].Value.ToString();
                indexDeleteProduit = pos;
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (selectCommande == "")
            {
                MessageBox.Show("Veuuillez choisir une commande");
            }
            else
            {
                DialogResult m = MessageBox.Show("Voulez vraimmetn supprimer la commande : " + selectCommande, "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (m == DialogResult.Yes)
                {
                    listVente.Rows.RemoveAt(indexDeleteCommande);
                    db.insert_update_delete("delete from commande where id=" + selectCommande);
                    db.insert_update_delete("delete from detailcommande where idCommande=" + selectCommande);
                    listProduit.DataSource = null;
                    selectCommande = "";
                    indexDeleteCommande = -1;
                    MessageBox.Show("Commande Supprimer !", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (idcommande == "")
            {
                MessageBox.Show("Veuuillez choisir un produit");
            }
            else
            {
                DialogResult m = MessageBox.Show("Voulez vraimmetn supprimer le produit : " + produit, "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (m == DialogResult.Yes)
                {
                    listProduit.Rows.RemoveAt(indexDeleteProduit);
                    db.insert_update_delete("delete from detailcommande where produit='" + produit + "' and idCommande=" + idcommande);
                    db.insert_update_delete("update produit set quantite=quantite+" + qtyD + " where nom ='" + produit + "'");
                    indexDeleteProduit = -1;
                    idcommande = "";
                }
            }
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
    }
}
