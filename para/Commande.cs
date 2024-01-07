using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
//using static System.Net.Mime.MediaTypeNames;

namespace para
{
    public partial class Commande : Form
    {
        public Commande()
        {
            InitializeComponent();
        }
        public static string nomClient="";
        Data db = new Data();
        public void LoadCombdo(string req)
        {
            DataTable dt = db.table(req);
            products.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
                products.Items.Add(dt.Rows[i][0].ToString());
        }
        private void Commande_Load(object sender, EventArgs e)
        {
            label2.Text = " @ " + login.name;

            LoadCombdo("select nom from produit");

            listProduct.Columns.Add("Produit", "Produit");
            listProduct.Columns.Add("Qunatity", "Qunatity");
            listProduct.Columns.Add("Prix", "Prix");
            listProduct.Rows.Clear();
            labelTotal.Text = "Total à payer : 0";
            client.Text = "COMMANDE DE "+nomClient.ToUpper();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            LoadCombdo("select nom from produit where nom like '%" + search.Text + "%'");

        }

        private void listProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        double total;

        public void calculTotal()
        {
            total = 0;
            for (int i = 0; i < listProduct.Rows.Count; i++)
            {
                int qty = int.Parse(listProduct.Rows[i].Cells[1].Value.ToString());
                double prix = double.Parse(listProduct.Rows[i].Cells[2].Value.ToString());
                total += (qty * prix);
            }
            labelTotal.Text = "Total à payer : " + total;
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            int q = 0;
            if (qty.Text == "" || products.Text == "")
            {
                MessageBox.Show("Veuillz choisir un produit et remplir le champs quantite");
            }
            else
            {
                bool testQ = true;
                try
                {
                    q = int.Parse(qty.Text);
                }
                catch
                {
                    MessageBox.Show("La quantite doit être une nombre", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    testQ = false;
                }
                if (q < 0 || testQ == false)
                {
                    MessageBox.Show("La quantite doit être supérieur de 0", "Champ obligatoire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bool t = false;
                    DataTable dt = db.table("select quantite,prix from produit where nom='" + products.Text + "'");
                    //if(int.Parse(dt.Rows[0][0].ToString())>=int.Parse(qty.Text))
                    for (int i = 0; i < listProduct.Rows.Count; i++)
                    {
                        if (listProduct.Rows[i].Cells[0].Value.ToString() == products.Text)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) >= int.Parse(qty.Text) + int.Parse(listProduct.Rows[i].Cells[1].Value.ToString()))
                            {
                                listProduct.Rows[i].Cells[1].Value = (int.Parse(listProduct.Rows[i].Cells[1].Value.ToString()) + int.Parse(qty.Text)).ToString();
                                calculTotal();
                            }
                            else
                            {
                                MessageBox.Show("Stock inssufisant");
                            }
                            t = true;
                            break;
                        }
                    }
                    if (!t)
                    {
                        if (int.Parse(dt.Rows[0][0].ToString()) >= int.Parse(qty.Text))
                        {
                            listProduct.Rows.Add(products.Text, qty.Text, dt.Rows[0][1].ToString());
                            calculTotal();
                        }
                        else
                        {
                            MessageBox.Show("Stock inssufisant");
                        }

                    }
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.UtcNow;
            if (listProduct.Rows.Count > 0)
            {
                db.insert_update_delete("insert into commande (client,dateCommande,total) values('" + nomClient + "','" + now.ToShortDateString() + "','" + total + "')");
                DataTable dt = db.table("select max(id) from commande");
                string idCommande = dt.Rows[0][0].ToString();

                for (int i = 0; i < listProduct.Rows.Count; i++)
                {
                    string qty = listProduct.Rows[i].Cells[1].Value.ToString();
                    string produit = listProduct.Rows[i].Cells[0].Value.ToString();
                    db.insert_update_delete("update produit set quantite=quantite-" + qty + " where nom='" + produit + "'");
                    db.insert_update_delete("insert into detailcommande (idCommande,produit,quantite) values ('" + idCommande + "','" + produit + "','" + qty + "')");

                }
                MessageBox.Show("Commnde bien ajouter","Informations",MessageBoxButtons.OK,MessageBoxIcon.Information) ;

                try
                {

                    DataTable tableau = db.table("select * from detailcommande where idCommande=" + idCommande);
                    //create a pdf document
                    iTextSharp.text.Document doc = new iTextSharp.text.Document();
                    PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\lenovo\\Desktop\\paraDocs\\" + nomClient + "_" + now.ToString("yyyy-mm-dd") + "_recus.pdf", FileMode.Create));
                    doc.Open();
                    //add header image
                    iTextSharp.text.Image headerImg = iTextSharp.text.Image.GetInstance("C:\\Users\\lenovo\\Desktop\\paraDocs\\logo.png");
                    headerImg.ScaleToFit(doc.PageSize.Width, 60);
                    headerImg.Alignment = Element.ALIGN_CENTER;
                    doc.Add(headerImg);
                    //add title
                    Paragraph title = new Paragraph("Client : " + nomClient);
                    title.Alignment = Element.ALIGN_LEFT;
                    title.SpacingAfter = 15;
                    Paragraph title1 = new Paragraph("Date achat : " + now.ToUniversalTime().ToShortDateString());
                    title1.Alignment = Element.ALIGN_LEFT;
                    title1.SpacingAfter = 15;
                    Paragraph title2 = new Paragraph("Total  : " + total);
                    title2.Alignment = Element.ALIGN_LEFT;
                    title2.SpacingAfter = 15;
                    doc.Add(title);
                    doc.Add(title1);
                    doc.Add(title2);
                    //create table and add header row
                    PdfPTable table = new PdfPTable(2);
                    table.AddCell("Produit");
                    table.AddCell("Quantity");
                    //iterate through data and add to table
                    for (int i = 0; i < tableau.Rows.Count; i++)
                    {
                        table.AddCell(tableau.Rows[i][1].ToString());
                        table.AddCell(tableau.Rows[i][2].ToString());
                    }
                    //add table to pdf
                    doc.Add(table);

                    //add footer
                    //Paragraph footer = new Paragraph("");
                    //footer.Alignment = Element.ALIGN_CENTER;
                    //footer.SpacingBefore = 10;
                    //doc.Add(footer);
                    doc.Close();
                    MessageBox.Show("Recus du client "+nomClient+" est généré !", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Veuilliez remplir la liste des produits !", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
             Application.Exit();
            //Application.Exit();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
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

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if(prod==-1)
            {
                MessageBox.Show("Veuillez selectionnez un proudit dans la liste !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                listProduct.Rows.RemoveAt(prod);
                prod = -1;
                calculTotal();
            }
        }
        int prod=-1;
        private void listProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int pos = listProduct.CurrentCell.RowIndex;
            if (pos >= 0 && pos <= listProduct.Rows.Count - 1)
            {
                prod = pos;
            }
        }
    }
}
