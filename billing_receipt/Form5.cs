using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billing_receipt
{
    public partial class Addcustomer : Form
    {
        //private int customerId;
        private billing_info form2;

        public Addcustomer(int nextCustomerId, billing_info parentForm)
        {
            InitializeComponent();
            //customerId = newCustomerId;
            form2 = parentForm;
        }

        private void Addcustomer_Load(object sender, EventArgs e)
        {
            //custidlabel.Text = customerId.ToString();
            int nextAvailableCustomerId = GetNextAvailableCustomerId();
            custidlabel.Text = nextAvailableCustomerId.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void custback5_Click(object sender, EventArgs e)
        {
            form2.Show();

            this.Close();
        }

        private void custadd_Click(object sender, EventArgs e)
        {
            string name = namecust.Text.Trim();
            string address = addresscust.Text.Trim();
            string phoneNumber = phonenum.Text.Trim();
            int CustomerId = int.Parse(custidlabel.Text);

            if (string.IsNullOrWhiteSpace(name) ||  string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("Please enter Name, Address, and Phone Number.");
                return;
            }

            InsertCustomerData(CustomerId, name, address, phoneNumber);

            MessageBox.Show("Customer added Successfully!");

            form2.Show();
            this.Close();
        }


        private void InsertCustomerData(int customerId, string name, string address, string phoneNumber)
        {
            string connectionString = "Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {    
                string procedureName = "InsertCustomerData"; // Name of the stored procedure

                using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Specify that this is a stored procedure
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private int GetNextAvailableCustomerId()
        {
            string connectionString = "Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True";
            int nextAvailableId = 1361; // Start from 1361

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Get existing customer IDs
                string existingCustomerIdsQuery = "SELECT customer_id FROM Customer";
                HashSet<int> existingCustomerIds = new HashSet<int>();

                using (SqlCommand cmd = new SqlCommand(existingCustomerIdsQuery, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            existingCustomerIds.Add(reader.GetInt32(0));
                        }
                    }
                }

                // Find the next available customer ID
                for (int i = nextAvailableId; i < nextAvailableId + 10000; i++)
                {
                    if (!existingCustomerIds.Contains(i))
                    {
                        return i; // Return the first missing customer ID
                    }
                }

                // If no gaps are found, get the max customer_id and increment it
                string maxQuery = "SELECT ISNULL(MAX(customer_id), 1360) + 1 FROM Customer";
                using (SqlCommand maxCmd = new SqlCommand(maxQuery, conn))
                {
                    nextAvailableId = Convert.ToInt32(maxCmd.ExecuteScalar());
                }
            }

            return nextAvailableId; // Return the next available ID or incremented max ID
        }



        private void custback5_MouseEnter(object sender, EventArgs e)
        {
            custback5.BackColor = Color.BurlyWood;
        }

        private void custback5_MouseLeave(object sender, EventArgs e)
        {
            custback5.BackColor = Color.Transparent;
        }

        private void custadd_MouseEnter(object sender, EventArgs e)
        {
            custadd.BackColor = Color.LightGray;
        }

        private void custadd_MouseLeave(object sender, EventArgs e)
        {
            custadd.BackColor = Color.Transparent;
        }

        private void phonenum_TextChanged(object sender, EventArgs e)
        {
            if (phonenum.Text.Length > 11)
            {
                phonenum.Text = phonenum.Text.Substring(0, 11);  // Trim to 11 characters
                phonenum.SelectionStart = phonenum.Text.Length;  // Set cursor to the end
            }
        }

        private void phonenum_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numeric digits and control keys (like Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)  // 8 is the Backspace key
            {
                e.Handled = true;  // Prevent the character from being entered
            }
        }

        private void namecust_TextChanged(object sender, EventArgs e)
        {
            string inputText = namecust.Text;
            // Capitalize the first letter of each word and make the rest lowercase
            namecust.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputText.ToLower());
            // Set the cursor to the end of the text after modifying it
            namecust.SelectionStart = namecust.Text.Length;
        }

        private void addresscust_TextChanged(object sender, EventArgs e)
        {
            string inputText = addresscust.Text;
            // Capitalize the first letter of each word and make the rest lowercase
            addresscust.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputText.ToLower());
            // Set the cursor to the end of the text after modifying it
            addresscust.SelectionStart = addresscust.Text.Length;
        }
    }
}
