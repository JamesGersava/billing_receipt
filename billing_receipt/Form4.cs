using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace billing_receipt
{
    public partial class database : Form
    {
        public database()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.database_Load);
            this.dtp4.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
        }

        string connectionString = "Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True";

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            billing_info form2 = new billing_info();
            form2.Show();
            this.Hide();
        }
        private void LoadNewData()
        {
            string query = @"
            SELECT DISTINCT 
                C.customer_id, 
                C.customer_name, 
                C.customer_address, 
                C.phone_number, 
                O.odr_id, 
                O.odr_date, 
                O.Total AS OrderTotal,  
                OD.package_name, 
                OD.quantity, 
                OD.price AS OrderPrice,  
                B.amount_paid, 
                B.change, 
                B.payment_method, 
                P.package_detail
            FROM 
                Customer C
            INNER JOIN 
                Odr O ON C.customer_id = O.customer_id
            INNER JOIN 
                Order_detail OD ON O.odr_id = OD.odr_id
            INNER JOIN 
                Billing B ON O.odr_id = B.odr_id
            INNER JOIN 
                Package P ON OD.package_name = P.package_name";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }

                    dgv_4.DataSource = null; // Clear any prior data source
                    dgv_4.DataSource = dt;   // Set new data source

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
            }
        }

        private void LoadFilterData()
        {
            string selectedDate = dtp4.Value.ToString("MM/dd/yyyy");

            string query = @"
            SELECT DISTINCT 
                C.customer_id, 
                C.customer_name, 
                C.customer_address, 
                C.phone_number, 
                O.odr_id, 
                O.odr_date, 
                O.Total AS OrderTotal,  
                OD.package_name, 
                OD.quantity, 
                OD.price AS OrderPrice,  
                B.amount_paid, 
                B.change, 
                B.payment_method, 
                P.package_detail
            FROM 
                Customer C
            INNER JOIN 
                Odr O ON C.customer_id = O.customer_id
            INNER JOIN 
                Order_detail OD ON O.odr_id = OD.odr_id
            INNER JOIN 
                Billing B ON O.odr_id = B.odr_id
            INNER JOIN 
                Package P ON OD.package_name = P.package_name
            WHERE
                CONVERT(VARCHAR(10), O.odr_date, 101) = @SelectedDate";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }

                    dgv_4.DataSource = null; // Clear any prior data source
                    dgv_4.DataSource = dt;   // Set filtered data

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
            }
        }


        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.LightGray;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
        }

        private void database_Load(object sender, EventArgs e)
        {
            LoadNewData();
            delcust.Visible = false;
        }

        private void dgv_4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadFilterData();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (dgv_4.SelectedRows.Count > 0)
            {
                // Confirmation prompt for multi-row deletion
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected records?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string connectionString = "Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        try
                        {
                            // Start a transaction to ensure all deletes happen together
                            using (SqlTransaction transaction = conn.BeginTransaction())
                            {
                                // Loop through selected rows and delete each one
                                foreach (DataGridViewRow row in dgv_4.SelectedRows)
                                {
                                    // Skip the new row (if any)
                                    if (row.IsNewRow) continue;

                                    int customerId = Convert.ToInt32(row.Cells["customer_id"].Value);
                                    int orderId = Convert.ToInt32(row.Cells["odr_id"].Value);

                                    // Delete from Billing table
                                    string deleteBillingQuery = "DELETE FROM Billing WHERE odr_id = @OrderId";
                                    using (SqlCommand billingCmd = new SqlCommand(deleteBillingQuery, conn, transaction))
                                    {
                                        billingCmd.Parameters.AddWithValue("@OrderId", orderId);
                                        billingCmd.ExecuteNonQuery();
                                    }

                                    // Delete from Order_detail table
                                    string deleteOrderDetailQuery = "DELETE FROM Order_detail WHERE odr_id = @OrderId";
                                    using (SqlCommand orderDetailCmd = new SqlCommand(deleteOrderDetailQuery, conn, transaction))
                                    {
                                        orderDetailCmd.Parameters.AddWithValue("@OrderId", orderId);
                                        orderDetailCmd.ExecuteNonQuery();
                                    }

                                    // Delete from Package table
                                    string deletePackageQuery = "DELETE FROM Package WHERE package_name = @PackageName";
                                    using (SqlCommand packageCmd = new SqlCommand(deletePackageQuery, conn, transaction))
                                    {
                                        string packageName = row.Cells["package_name"].Value.ToString();
                                        packageCmd.Parameters.AddWithValue("@PackageName", packageName);
                                        packageCmd.ExecuteNonQuery();
                                    }

                                    // Delete from Odr table
                                    string deleteOrderQuery = "DELETE FROM Odr WHERE odr_id = @OrderId";
                                    using (SqlCommand orderCmd = new SqlCommand(deleteOrderQuery, conn, transaction))
                                    {
                                        orderCmd.Parameters.AddWithValue("@OrderId", orderId);
                                        orderCmd.ExecuteNonQuery();
                                    }

                                    // Delete from Customer table
                                    string deleteCustomerQuery = "DELETE FROM Customer WHERE customer_id = @CustomerId";
                                    using (SqlCommand customerCmd = new SqlCommand(deleteCustomerQuery, conn, transaction))
                                    {
                                        customerCmd.Parameters.AddWithValue("@CustomerId", customerId);
                                        customerCmd.ExecuteNonQuery();
                                    }
                                }

                                // Commit the transaction to apply all deletions
                                transaction.Commit();
                            }

                            // Refresh the DataGridView to reflect deletions
                            LoadNewData();
                            MessageBox.Show("Selected rows deleted successfully.");
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction in case of error
                            MessageBox.Show("Error occurred during deletion: " + ex.Message);
                            // Rollback transaction (if necessary)
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select at least one row to delete.");
            }
        }
        private void reset4_Click(object sender, EventArgs e)
        {
            LoadNewData();
            delt4.Visible = true;
            delcust.Visible = false;
        }

        private void reset4_MouseEnter(object sender, EventArgs e)
        {
            reset4.BackColor = Color.LightGray;
        }

        private void reset4_MouseLeave(object sender, EventArgs e)
        {
            reset4.BackColor = Color.Transparent;
        }

        private void delt4_MouseEnter(object sender, EventArgs e)
        {
            delt4.BackColor = Color.LightGray;
        }

        private void delt4_MouseLeave(object sender, EventArgs e)
        {
            delt4.BackColor = Color.Transparent;
        }

        private void showcust_Click(object sender, EventArgs e)
        {
            LoadCustomerData();
            delt4.Visible = false;
            delcust.Visible = true;
        }

        private void showcust_MouseEnter(object sender, EventArgs e)
        {
            showcust.BackColor = Color.LightGray;
        }

        private void showcust_MouseLeave(object sender, EventArgs e)
        {
            showcust.BackColor = Color.Transparent;
        }

        private void LoadCustomerData()
        {
            // Define the connection string to the database
            string connectionString = "Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True;";

            // Create a DataTable to hold the result
            DataTable dt = new DataTable();

            // Establish connection and fetch the data using the stored procedure
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    conn.Open();

                    // Create the SqlCommand object for the stored procedure
                    using (SqlCommand cmd = new SqlCommand("GetAllCustomers", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Use SqlDataAdapter to fill the DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }

                    // Bind the DataTable to the DataGridView
                    dgv_4.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching customer data: " + ex.Message);
                }
            }
        }

        //private void delcust_Click(object sender, EventArgs e)
        //{
        //    if (dgv_4.SelectedRows.Count > 0)
        //    {
        //        int customerId = Convert.ToInt32(dgv_4.SelectedRows[0].Cells["customer_id"].Value);
        //        string connectionString = "Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True";

        //        // Confirmation prompt
        //        var confirmationResult = MessageBox.Show(
        //            "Are you sure you want to delete this customer?",
        //            "Confirm Deletion",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Question
        //        );

        //        // If the user selects "No", cancel the deletion
        //        if (confirmationResult == DialogResult.No)
        //        {
        //            return;
        //        }

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            try
        //            {
        //                // Check if there are related orders
        //                string checkProcedure = "CheckRelatedOrders";
        //                using (SqlCommand checkCmd = new SqlCommand(checkProcedure, conn))
        //                {
        //                    checkCmd.CommandType = CommandType.StoredProcedure;
        //                    checkCmd.Parameters.AddWithValue("@CustomerId", customerId);

        //                    int relatedRecordsCount = (int)checkCmd.ExecuteScalar();

        //                    if (relatedRecordsCount > 0)
        //                    {
        //                        MessageBox.Show(
        //                            "This customer cannot be deleted because there are related orders. Please delete related orders first.",
        //                            "Deletion Not Allowed",
        //                            MessageBoxButtons.OK,
        //                            MessageBoxIcon.Warning
        //                        );
        //                        return;
        //                    }
        //                }

        //                // If no related orders, proceed to delete the customer
        //                string deleteProcedure = "DeleteCustomer";
        //                using (SqlCommand deleteCmd = new SqlCommand(deleteProcedure, conn))
        //                {
        //                    deleteCmd.CommandType = CommandType.StoredProcedure;
        //                    deleteCmd.Parameters.AddWithValue("@CustomerId", customerId);
        //                    deleteCmd.ExecuteNonQuery();
        //                }

        //                MessageBox.Show("Customer deleted successfully.");
        //                LoadCustomerData(); // Refresh the DataGridView
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error: " + ex.Message);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select a customer record to delete.");
        //    }
        //}

        private void delcust_Click(object sender, EventArgs e)
        {
            // Ensure there are selected rows
            if (dgv_4.SelectedRows.Count > 0)
            {
                // Confirmation prompt before deletion
                var confirmationResult = MessageBox.Show(
                    "Are you sure you want to delete the selected customers?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                // If the user selects "No", cancel the deletion
                if (confirmationResult == DialogResult.No)
                {
                    return;
                }

                // Open the connection once
                string connectionString = "Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    try
                    {
                        foreach (DataGridViewRow row in dgv_4.SelectedRows)
                        {
                            // Ensure that the row is not the new row (empty row added for user to input new data)
                            if (row.IsNewRow) continue;

                            // Get the customer ID for the selected row
                            int customerId = Convert.ToInt32(row.Cells["customer_id"].Value);

                            // Check if there are related orders for the customer
                            string checkProcedure = "CheckRelatedOrders";
                            using (SqlCommand checkCmd = new SqlCommand(checkProcedure, conn))
                            {
                                checkCmd.CommandType = CommandType.StoredProcedure;
                                checkCmd.Parameters.AddWithValue("@CustomerId", customerId);

                                int relatedRecordsCount = (int)checkCmd.ExecuteScalar();

                                if (relatedRecordsCount > 0)
                                {
                                    MessageBox.Show(
                                        "This customer cannot be deleted because there are related orders. Please delete related orders first.",
                                        "Deletion Not Allowed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    );
                                    return;
                                }
                            }

                            // If no related orders, delete the customer
                            string deleteProcedure = "DeleteCustomer";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteProcedure, conn))
                            {
                                deleteCmd.CommandType = CommandType.StoredProcedure;
                                deleteCmd.Parameters.AddWithValue("@CustomerId", customerId);
                                deleteCmd.ExecuteNonQuery();
                            }

                            // Optionally, remove the deleted row from the DataGridView (if you want immediate UI update)
                            dgv_4.Rows.Remove(row);
                        }

                        MessageBox.Show("Selected customers deleted successfully.");
                        LoadCustomerData(); // Refresh the DataGridView after deletion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select at least one customer record to delete.");
            }
        }


        private void searchbar_Click(object sender, EventArgs e)
        {
            string customerName = searchname.Text.Trim(); // Get the entered name

            if (string.IsNullOrWhiteSpace(customerName))
            {
                MessageBox.Show("Please enter a name to filter.");
                return;
            }

            LoadDataByName(customerName); // Call method to load data by name
        }

        private void LoadDataByName(string customerName)
        {
            string query = @"
    SELECT DISTINCT 
        C.customer_id, 
        C.customer_name, 
        C.customer_address, 
        C.phone_number, 
        O.odr_id, 
        O.odr_date, 
        O.Total AS OrderTotal,  
        OD.package_name, 
        OD.quantity, 
        OD.price AS OrderPrice,  
        B.amount_paid, 
        B.change, 
        B.payment_method, 
        P.package_detail
    FROM 
        Customer C
    INNER JOIN 
        Odr O ON C.customer_id = O.customer_id
    INNER JOIN 
        Order_detail OD ON O.odr_id = OD.odr_id
    INNER JOIN 
        Billing B ON O.odr_id = B.odr_id
    INNER JOIN 
        Package P ON OD.package_name = P.package_name
    WHERE
        C.customer_name = @CustomerName";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerName", customerName); // Add the name parameter
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }

                    dgv_4.DataSource = null; // Clear any prior data source
                    dgv_4.DataSource = dt;   // Set filtered data

                    if (dt.Rows.Count == 0) // Check if no data was found
                    {
                        MessageBox.Show("No records found for the entered name.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching data: " + ex.Message);
                }
            }
        }

        private void searchbar_MouseEnter(object sender, EventArgs e)
        {
            searchbar.BackColor = Color.LightGray;
        }

        private void searchbar_MouseLeave(object sender, EventArgs e)
        {
            searchbar.BackColor = Color.Transparent;
        }

        private void searchname_TextChanged(object sender, EventArgs e)
        {
            string inputText = searchname.Text;
            // Capitalize the first letter of each word and make the rest lowercase
            searchname.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputText.ToLower());
            // Set the cursor to the end of the text after modifying it
           searchname.SelectionStart = searchname.Text.Length;
        }
    }
}
