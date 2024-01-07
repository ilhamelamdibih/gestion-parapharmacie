using Guna.UI2.WinForms;
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
    public partial class achats : Form
    {
        public achats()
        {
            InitializeComponent();
        }

        private void nom_TextChanged(object sender, EventArgs e)
        {

        }
        Data db = new Data();
        public void MAJList()
        {
            guna2DataGridView1.DataSource = null;
            DataTable table = db.table("select * from achat");
            guna2DataGridView1.DataSource = table;
        }
        private void achats_Load(object sender, EventArgs e)
        {
            label2.Text = " @ " + login.name;
            MAJList();
            DataTable dtF = db.table("select * from fournisseur");
            for (int i = 0; i < dtF.Rows.Count; i++)
            {
                fournisseurC.Items.Add(dtF.Rows[i][1].ToString());
            }
            DataTable dtC = db.table("select * from categorie");
            for (int i = 0; i < dtC.Rows.Count; i++)
            {
                categorieC.Items.Add(dtC.Rows[i][1].ToString());
            }
        }
        public Point mouseLocation;

        private void achats_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void achats_MouseDown(object sender, MouseEventArgs e)
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
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            int q = 0;
            double price = 0;
            if (fournisseurC.Text == "" || categorieC.Text == "" || produit.Text == "" || prix.Text =="")
            {
                MessageBox.Show("Merci de remplir les champs vide", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else 
            {
                bool testQ = true;
                bool testP = true;
                try
                {
                    try
                    {
                        q = int.Parse(quantite.Text);
                    }
                    catch
                    {
                        MessageBox.Show("La quantite doit être une nombre", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        testQ = false;
                    }
                    //Prix invalide
                    try
                    {
                        price = double.Parse(prix.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Le prix doit être une nombre", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        testP = false;
                    }
                    if (q < 0||testQ==false)
                    {
                        MessageBox.Show("La quantite doit être supérieur de 0", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (price < 0 || testP == false)
                    {
                        MessageBox.Show("Le prix doit être supérieur de 0", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (guna2DateTimePicker1.Value < DateTime.Now)
                    {
                        MessageBox.Show("La date de garantie doît étre supérieur à la date d'aujourd'hui", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int c = db.insert_update_delete(string.Format("insert into achat (nomFournisseur,nomProduit,quantite,dateGarantie,prix) values( lower('{0}'), lower('{1}'),{2},'{3}','{4}')", fournisseurC.Text, produit.Text, quantite.Text, DateTime.Parse(guna2DateTimePicker1.Value.ToShortDateString()).ToString("yyyy-MM-dd"), prix.Text));
                        if (c == 0)
                        {
                            MessageBox.Show("Echec d'ajout", "Echec!!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {

                            DataTable table = db.table(string.Format("select * from produit WHERE lower(nom) LIKE lower('{0}') ", produit.Text));
                            if (table.Rows.Count != 0)
                            {
                                int mod = db.insert_update_delete(string.Format("Update produit SET quantite=quantite+{0} where nom ='{1}' ", q, produit.Text));
                            }
                            else
                            {
                                int p = db.insert_update_delete(string.Format("insert into produit (nom,quantite,nomCategorie,prix) values(lower('{0}'),{1},'{2}','{3}')", produit.Text, quantite.Text, categorieC.Text,prix.Text));

                            }
                            if(categorieC.Text=="materiel")
                            {
                                reference f = new reference();
                                f.Show();
                                this.Hide();
                            }
                            MAJList();
                            fournisseurC.Text = produit.Text = categorieC.Text = quantite.Text = "";
                            guna2DateTimePicker1.Value = DateTime.Now;      
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                 
            }
           
            //nomF = "";
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if(idCommande==-1 || nomProd=="")
            {
                MessageBox.Show("Veuillez selectioner une commande pour l'annuler!", "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult m = MessageBox.Show("Voulez-vous vraiment supprimer la commande " + idCommande, "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == m)
                {
                    DateTime now= DateTime.Now;
                    
                    if(now.Year == DateTime.Parse(dateCommande).Year && now.Month== DateTime.Parse(dateCommande).Month && now.Day == DateTime.Parse(dateCommande).Day)
                    {
                        DataTable tb = db.table("select quantite from produit where nom='"+nomProd+"'");
                        if (qty <= int.Parse(tb.Rows[0][0].ToString()))
                        {
                            db.insert_update_delete(string.Format("update produit set quantite=quantite-{1} where nom='{0}'", nomProd,qty));
                            int c = db.insert_update_delete(string.Format("update achat set etat='annule' where id='{0}'", idCommande));
                            if (c == 0)
                            {
                                MessageBox.Show("Commande non annuler", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("Commande bien annuler", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                                MAJList();
                                nomProd = "";
                                idCommande = -1;
                                qty = -1;

                            }

                        }
                        else
                        {
                            MessageBox.Show("QTYImpossible d'annuler cette commande", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Impossible d'annuler cette commande", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataTable table = db.table(string.Format("select * from achat WHERE nomFournisseur LIKE '%{0}%' or  nomProduit  LIKE '%{0}%'", textBox3.Text));
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = table;
            
        }
        int idCommande=-1;
        int qty = -1;
        string nomProd="";
        string dateCommande = "";
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = guna2DataGridView1.CurrentCell.RowIndex;
            if (index >= 0 && index < guna2DataGridView1.Rows.Count - 1)
            {
                idCommande = int.Parse(guna2DataGridView1.Rows[index].Cells[0].Value.ToString());
                qty = int.Parse(guna2DataGridView1.Rows[index].Cells[3].Value.ToString());
                nomProd = guna2DataGridView1.Rows[index].Cells[2].Value.ToString();
                dateCommande = guna2DataGridView1.Rows[index].Cells[4].Value.ToString();
            }
            else
            {
                nomProd = "";
                idCommande = -1;
                qty = -1;
                dateCommande = "";
            }
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

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            transactions c = new transactions();
            c.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            categorie c = new categorie();
            c.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            client c = new client();
            c.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            fournisseur c = new fournisseur();
            c.Show();
            this.Hide();
        }
    }
}
