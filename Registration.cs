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

namespace LofiCubeTimer
{
    public partial class Registration : Form
    {
        // Connection string
        private string connectionString = "server=localhost;user=root;database=lofi_timer;Convert Zero Datetime=True";


        public Registration()
        {
            InitializeComponent();
            txtPassword1.UseSystemPasswordChar = true;
            txtPassword2.UseSystemPasswordChar = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password1 = txtPassword1.Text;
            string password2 = txtPassword2.Text;

            // Basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password1) || string.IsNullOrEmpty(password2))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password1 != password2)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Insert data into database
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO users (username, email, password) VALUES (@username, @email, @password)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password1); // Note: Password should be hashed in a real application
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registration successful!");
                            this.Hide();
                            // Optionally, close this form or navigate to another form
                        }
                        else
                        {
                            MessageBox.Show("Registration failed.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Other event handlers for text changed can remain empty or with validation logic if needed
        private void txtUsername_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtPassword1_TextChanged(object sender, EventArgs e) { }
        private void txtPassword2_TextChanged(object sender, EventArgs e) { }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}