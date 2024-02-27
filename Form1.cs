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
    public partial class LofiTimer : Form
    {
        // Define connection string
        private const string connectionString = "server=localhost;user=root;database=lofi_timer;Convert Zero Datetime=True";

        // Define placeholder texts
        private const string UsernamePlaceholder = "Enter your username";
        private const string PasswordPlaceholder = "Enter your password";

        public LofiTimer()
        {
            InitializeComponent();

            // Set placeholder text for textboxes when the form loads
            txtUsername.Text = UsernamePlaceholder;
            txtPassword.Text = PasswordPlaceholder;
            // Set password character to * for security
            txtPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Get username and password from textboxes
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Validate username and password against the database
            if (ValidateUser(username, password))
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Redirect to SolvingStation form
                SolvingStation solvingStationForm = new SolvingStation();
                solvingStationForm.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateUser(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // Clear placeholder text when user starts typing
            if (txtUsername.Text == UsernamePlaceholder)
            {
                txtUsername.Text = "";
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Clear placeholder text when user starts typing
            if (txtPassword.Text == PasswordPlaceholder)
            {
                txtPassword.Text = "";
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Registration registrationForm = new Registration();
            registrationForm.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}