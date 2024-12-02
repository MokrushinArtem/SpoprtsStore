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
using System.Text.RegularExpressions;
using System.Drawing;

namespace SpoprtsStore
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
            LoadSuppliers();
        }
        string conStr = data.conStr;
        private void LoadSuppliers() 
        {
           
            using (MySqlConnection conn = new MySqlConnection(data.conStr))
            {
                conn.ConnectionString = conStr;
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id FROM supplier", conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add($"{reader["id"]}");
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product viewadd = new Product();
            this.Visible = false;
            viewadd.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox4.Text;
            int quantity;
            decimal price;

            if (!int.TryParse(textBox5.Text, out quantity))
            {
                MessageBox.Show("Введите корректное количество товара.");
                return;
            }

            if (!decimal.TryParse(textBox6.Text, out price))
            {
                MessageBox.Show("Введите корректную цену товара.");
                return;
            }

            string supplier = comboBox2.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(supplier))
            {
                MessageBox.Show("Пожалуйста, выберите поставщика.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(data.conStr))
            {
                conn.Open();
                string query = "INSERT INTO tovars (tovarcol, quantity, price, idsupplier) VALUES (@tovarcol, @quantity, @price, @supplier)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tovarcol", name);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@supplier", supplier);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Товар успешно добавлен.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при добавлении товара: " + ex.Message);
                    }
                }
            }

            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox2.SelectedIndex = -1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar !='.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = Regex.Replace(textBox4.Text, @"[^а-яА-Яa-zA-Z\s]", "");
            textBox4.SelectionStart = textBox4.Text.Length;
        }
    }
}
