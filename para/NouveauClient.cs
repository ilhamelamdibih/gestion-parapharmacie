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
    public partial class NouveauClient : Form
    {
        public NouveauClient()
        {
            InitializeComponent();
        }
        Data db = new Data();
        private void btnAddClient_Click(object sender, EventArgs e)
        {
            if (nom.Text == "" || phone.Text == "")
            {
                MessageBox.Show("Veuilliez remplir tous les champs");
            }
            else
            {
                DataTable dt = db.table("select * from client where nom='" + nom.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    DialogResult m = MessageBox.Show("This client already exist ,Voullez vous continuer l'operation avec le client " + nom.Text, "attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (m == DialogResult.OK)
                    {
                        Commande.nomClient = nom.Text;
                        Commande v = new Commande();
                        v.Show();
                        this.Hide();
                    }
                }
                else
                {
                    int c = db.insert_update_delete("insert into client (nom,tele) values ('" + nom.Text + "','" + phone.Text + "')");
                    if (c != 0)
                    {
                        MessageBox.Show("Client Ajouter");
                        Commande.nomClient = nom.Text;
                        Commande v = new Commande();
                        v.Show();
                        this.Hide();
                    }

                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Dashboard c = new Dashboard();
            c.Show();
            this.Hide();
        }
    }
}
