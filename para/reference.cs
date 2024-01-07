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
    public partial class reference : Form
    {
        public static int nbr = 1;
        public reference()
        {
            InitializeComponent();
        }
        Data db = new Data();

        DataTable dt;
        private void reference_Load(object sender, EventArgs e)
        {
            dt = db.table("select id,quantite from achat where id=(select max(id) from achat)");
            label1.Text = "Ajouter le produit numero " + nbr;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (nbr <= int.Parse(dt.Rows[0][1].ToString()))
            {
                if (guna2TextBox1.Text == "")
                {
                    MessageBox.Show("Le champs reference est obligatoire", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    db.insert_update_delete("insert into reference (idAchat,idProduit) values ('" + dt.Rows[0][0].ToString() + "','" + guna2TextBox1.Text + "')");
                    nbr++;
                    label1.Text = "Ajouter le produit numero " + nbr;
                    guna2TextBox1.Text = "";
                    if(nbr == int.Parse(dt.Rows[0][1].ToString()))
                    {
                        achats a = new achats();
                        a.Show();
                        this.Hide();
                    }
                }
            }
            else
            {
                achats a = new achats();
                a.Show();
                this.Hide();
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
