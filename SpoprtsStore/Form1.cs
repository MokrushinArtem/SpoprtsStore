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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string conString = data.conStr;

        string login = "admin";
        string password = "admin";

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            textBox2.PasswordChar = '*';
        }

        private void label4_Click(object sender, EventArgs e)
        {
            string role = "4";
            data.role = role;
            NonAvtoraiz nonAvtoraiz = new NonAvtoraiz();
            this.Visible = false;
            nonAvtoraiz.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(login =="admin" && password == "admin")
            {
                this.Hide();
                Import import = new Import();
                import.Show();
            }
            try
            {
                string login = textBox1.Text.ToString();
                string userName = string.Empty;
                string hashPassword = string.Empty;
                string role = string.Empty;
                MySqlConnection con = new MySqlConnection(conString);
                con.Open();

                MySqlCommand cmd = new MySqlCommand($"Select * From clients Where Login = '{login}'", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                hashPassword = textBox2.Text.ToString();
                userName = dt.Rows[0].ItemArray.GetValue(1).ToString();

                data.usrName = userName;

                if (hashPassword == dt.Rows[0].ItemArray.GetValue(8).ToString())
                {
                    role = dt.Rows[0].ItemArray.GetValue(9).ToString();
                    data.role = role;
                    MessageBox.Show("Вы успешно авторизовались");
                    if (role == "3")
                    {
                        Admin admin = new Admin();
                        this.Visible = false;
                        admin.ShowDialog();
                        this.Close();
                    }
                    else if (role == "2")
                    {
                        Manager meneger = new Manager();
                        this.Visible = false;
                        meneger.ShowDialog();
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
                else
                {
                    MessageBox.Show("Неверный пароль");
                    textBox2.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = default;
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
