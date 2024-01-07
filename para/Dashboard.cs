using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace para
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            categorie categorie = new categorie();
            categorie.Show();
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
        Data db = new Data();
        int maxVente = 60;
        double maxProfit = 100;

        private void Dashboard_Load(object sender, EventArgs e)
        {
            label4.Text = " @ " + login.name;


            nbrClient.Text = db.table("select count(*) from client").Rows[0][0].ToString();
            nbrProduit.Text = db.table("select count(*) from produit").Rows[0][0].ToString();
            nbrFournisseur.Text = db.table("select count(*) from fournisseur").Rows[0][0].ToString();


            products.DataSource = null;
            products.DataSource = db.table("select id,nom,nomCategorie,prix from produit where nom in (select produit from detailcommande group by produit ORDER by count(produit) DESC) LIMIT 5");
            string sumResultString = db.table("select sum(quantite) from detailcommande WHERE idCommande in (SELECT id from commande where YEAR(STR_TO_DATE(dateCommande, '%d-%m-%Y')) = YEAR(DATE(NOW())))").Rows[0][0].ToString();
            if (int.TryParse(sumResultString, out int currentVente))
            {
                Console.WriteLine($"Parsed integer value: {currentVente}");
            }
            else
            {
                Console.WriteLine("Error parsing the integer value. Using a default value.");
                int defaultValue = 0; 
                currentVente = defaultValue;
            }

            // int currentVente = int.Parse(db.table("select sum(quantite) from detailcommande WHERE idCommande in (SELECT id from commande where YEAR(STR_TO_DATE(dateCommande, '%d-%m-%Y')) = YEAR(DATE(NOW())))").Rows[0][0].ToString());
            // double currentProfit = double.Parse(db.table("SELECT sum(total) from commande where YEAR(STR_TO_DATE(dateCommande, '%d-%m-%Y')) = YEAR(DATE(NOW()));").Rows[0][0].ToString());

            string profitResultString = db.table("SELECT sum(total) from commande where YEAR(STR_TO_DATE(dateCommande, '%d-%m-%Y')) = YEAR(DATE(NOW()));").Rows[0][0].ToString();
            if (double.TryParse(profitResultString, out double currentProfit))
            {
               Console.WriteLine($"Parsed double value: {currentProfit}");

            }
            else
            {
                Console.WriteLine("Error parsing the double value. Using a default value.");
                double defaultValue = 1.0; 
                currentProfit = defaultValue;
            }
            achatProgress.Value = (int)Math.Ceiling(currentProfit/maxProfit);
            VenteProgress.Value= currentVente%maxVente;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            transactions transactions = new transactions();
            transactions.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            produit produit = new produit();
            produit.Show();
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

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            this.Hide();
        }
    }
}
