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
    public partial class Sot : Form
    {
        public Sot()
        {
            InitializeComponent();
        }
        string conStr = data.conStr;
       
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

        private void Sot_Load(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = conStr;

                con.Open();

                MySqlCommand cmd = new MySqlCommand("select surname AS 'Фамилия', name AS 'Имя', phone_number AS 'Телефон', date_of_birth AS 'Дата Рождения', address AS 'Адресс', role AS 'Роль' from employee ", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView2.DataSource = dt;
            }
        }
    }
}
