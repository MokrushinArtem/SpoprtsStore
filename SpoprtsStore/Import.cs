using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace Security
{
    public partial class Import : Form
    {
        public Import()
        {
            InitializeComponent();
            GetTables();
        }

        string connectionString = @"host=localhost;uid=root;pwd=root;database=sportsstore";

        private void saveData_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GetTables()
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SHOW TABLES", con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void importData_Click(object sender, EventArgs e)
        {
           
        }
        // Функция импорта
        private void ImportData(string filePath, string tablename)
        {
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                int count = 0;
                connect.Open();
                bool firstString = true;
                Encoding encoding = Encoding.GetEncoding(1251);
                using (StreamReader reader = new StreamReader(filePath, encoding))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (firstString)
                        {
                            firstString = false;
                        }
                        else
                        {
                            var values = line.Split(';'); // разделитель
                            string query = string.Empty;
                            switch (tablename)
                            {
                               
                                case "contract":
                                    query = $"INSERT INTO `{tablename}` (id, surname, name, midle_name, phone_number, date_of_birth, address, login, password, idrole) VALUES ({values[0]}, '{values[1]}', '{values[2]}', '{values[3]}', '{values[4]}', '{values[5]}', '{values[6]}, {values[7]}, {values[8]}, {values[9]},')";
                                    break;
                                
                                
                            }
                            try
                            {
                                using (MySqlCommand cmd = new MySqlCommand(query, connect))
                                {
                                    cmd.ExecuteNonQuery();
                                    count++;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error: {ex.Message}");
                            }
                        }
                    }
                    MessageBox.Show($"Данные успешно импортированы! Добавлено записей: {count}");
                }
                connect.Close();
            }
        }

        private void backupData_Click(object sender, EventArgs e)
        {
           
        }
        private void RecoveryDatabase(string filePath)
        {
            string script = File.ReadAllText(filePath);
            InsertData(script);
        }
        private long InsertData(string query)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return cmd.LastInsertedId;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Выбор файла
            OpenFileDialog getFile = new OpenFileDialog();
            getFile.InitialDirectory = "c:\\";
            getFile.Filter = "CSV files (*.csv)|*.csv";
            getFile.FilterIndex = 1;
            getFile.RestoreDirectory = true;
            if (getFile.ShowDialog() == DialogResult.OK)
            {
                string filePath = getFile.FileName;
                ImportData(filePath, comboBox1.SelectedItem.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "SQL Files (*.sql)|*.sql";
                openFileDialog.Title = "Select a SQL file to restore";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    RestoreDatabase(filePath);
                }
            }
        }

        private void RestoreDatabase(string filePath)
        {
            string connectionString = "Server=localhost;Database=sportstore;User Id=root;Password=root;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = File.ReadAllText(filePath);

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Database restored successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
   
      