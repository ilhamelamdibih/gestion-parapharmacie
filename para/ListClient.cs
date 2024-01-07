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
    public partial class ListClient : Form
    {
        public ListClient()
        {
            InitializeComponent();
        }
        public void MAJList(string req)
        {
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.DataSource = db.table(req);
        }

        Data db = new Data();
        private void ListClient_Load(object sender, EventArgs e)
        {
            MAJList("select * from client");

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            MAJList("select * from client where nom like '%" + textBox3.Text + "%'");

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count > 0)
            {
                int pos = guna2DataGridView1.CurrentCell.RowIndex;
                //if (pos >= 0 && pos < dataGridView1.Rows.Count - 1)
                //{
                DataGridViewRow r = guna2DataGridView1.Rows[pos];
                Commande.nomClient = r.Cells[1].Value.ToString();

                //}
            }
            if (Commande.nomClient == "")
            {
                MessageBox.Show("Veulliez selectioner un client");
            }
            else
            {
                Commande v = new Commande();
                v.Show();
                this.Hide();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Dashboard c = new Dashboard();
            c.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
