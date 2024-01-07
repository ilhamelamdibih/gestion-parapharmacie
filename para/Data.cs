using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace para
{
    internal class Data
    {
        MySqlConnection cn = new MySqlConnection("SERVER=localhost;DATABASE=parapharmacie;UID=root;PASSWORD=;Convert Zero Datetime=True");
        public DataTable table(string requete)
        {
            cn.Open();
            MySqlCommand cmd = new MySqlCommand(requete, cn);
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cn.Close();
            return dt;
        }
        public int insert_update_delete(string requete)
        {
            cn.Open();
            MySqlCommand cmd = new MySqlCommand(requete, cn);
            int a = cmd.ExecuteNonQuery();
            cn.Close();
            return a;
        }
    }
}
