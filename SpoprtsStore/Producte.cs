using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SpoprtsStore
{
    public partial class Producte : Form
    {
        public Producte()
        {
            InitializeComponent();
        }
        string conStr = data.conStr;
        private void Producte_Load(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = conStr;

                con.Open();

                MySqlCommand cmd = new MySqlCommand("select tovarcol AS Артикул, quantity AS Количество, price AS Цена, idsupplier AS Поставщик from tovars;", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (data.role == "3")
            {
                Admin admin = new Admin();
                this.Visible = false;
                admin.ShowDialog();
                this.Close();
            }
            else if (data.role == "2")
            {
                Manager meneger = new Manager();
                this.Visible = false;
                meneger.ShowDialog();
                this.Close();
            }
            else if (data.role == "4")
            {
                NonAvtoraiz nonAvtoraiz = new NonAvtoraiz();
                this.Visible = false;
                nonAvtoraiz.ShowDialog();
                this.Close();
            }
            else
            {
                User user = new User();
                this.Visible = false;
                user.ShowDialog();
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Producte_Load_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
