using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace billing_receipt
{
    public partial class Sales : Form
    {
        public Sales()
        {
            InitializeComponent();
        }

        private void back6_Click(object sender, EventArgs e)
        {
            billing_info Form2 = new billing_info();
            Form2.Show();
            this.Close();
        }

        private void Sales_Load(object sender, EventArgs e)
        {

        }

        //private void UpdateSalesTotals(DateTime selectedDate)
        //{
        //    daily.Text = GetTotalForRange(selectedDate, "daily").ToString("C");
        //    weekly.Text = GetTotalForRange(selectedDate, "weekly").ToString("C");
        //    monthly.Text = GetTotalForRange(selectedDate, "monthly").ToString("C");
        //    yearly.Text = GetTotalForRange(selectedDate, "yearly").ToString("C");
        //}
        private void UpdateSalesTotals(DateTime selectedDate)
        {
            // Call the UDF for daily, weekly, monthly, and yearly totals
            daily.Text = GetSalesTotalFromUDF(selectedDate, "daily").ToString("C");
            weekly.Text = GetSalesTotalFromUDF(selectedDate, "weekly").ToString("C");
            monthly.Text = GetSalesTotalFromUDF(selectedDate, "monthly").ToString("C");
            yearly.Text = GetSalesTotalFromUDF(selectedDate, "yearly").ToString("C");
        }
        //private decimal GetTotalForRange(DateTime selectedDate, string range)
        //{
        //    decimal total = 0;
        //    string query = "";

        //    // Calculate the start (Monday) and end (Sunday) of the week for weekly range
        //    DateTime startOfWeek = selectedDate.AddDays(-(int)selectedDate.DayOfWeek + (int)DayOfWeek.Monday);
        //    DateTime endOfWeek = startOfWeek.AddDays(6);

        //    // Define the query based on the range
        //    switch (range)
        //    {
        //        case "daily":
        //            // Match the date portion only for daily sales
        //            query = "SELECT SUM(CAST(Total AS DECIMAL(18, 2))) FROM Odr WHERE CAST(odr_date AS DATE) = CAST(@selectedDate AS DATE)";
        //            break;
        //        case "weekly":
        //            // Use the start and end of the week for Monday to Sunday, ensuring only the date part is used in comparison
        //            query = "SELECT SUM(CAST(Total AS DECIMAL(18, 2))) FROM Odr WHERE CAST(odr_date AS DATE) >= CAST(@startOfWeek AS DATE) AND CAST(odr_date AS DATE) <= CAST(@endOfWeek AS DATE)";
        //            break;
        //        case "monthly":
        //            query = "SELECT SUM(CAST(Total AS DECIMAL(18, 2))) FROM Odr WHERE MONTH(odr_date) = MONTH(@selectedDate) AND YEAR(odr_date) = YEAR(@selectedDate)";
        //            break;
        //        case "yearly":
        //            query = "SELECT SUM(CAST(Total AS DECIMAL(18, 2))) FROM Odr WHERE YEAR(odr_date) = YEAR(@selectedDate)";
        //            break;
        //        default:
        //            throw new ArgumentException("Invalid range specified.");
        //    }

        //    // Database interaction with error handling
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True"))
        //        {
        //            conn.Open();
        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                // Set parameters for the date range
        //                if (range == "daily" || range == "monthly" || range == "yearly")
        //                {
        //                    cmd.Parameters.AddWithValue("@selectedDate", selectedDate);
        //                }
        //                else if (range == "weekly")
        //                {
        //                    cmd.Parameters.AddWithValue("@startOfWeek", startOfWeek);
        //                    cmd.Parameters.AddWithValue("@endOfWeek", endOfWeek);
        //                }

        //                object result = cmd.ExecuteScalar();

        //                // Check if result is null or DBNull
        //                if (result != null && result != DBNull.Value)
        //                {
        //                    total = Convert.ToDecimal(result);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    return total;
        //}
        private decimal GetSalesTotalFromUDF(DateTime selectedDate, string range)
        {
            decimal total = 0;
            string query = "SELECT dbo.GetSalesTotalForRange(@selectedDate, @range)";

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Set parameters for the UDF
                        cmd.Parameters.AddWithValue("@selectedDate", selectedDate);
                        cmd.Parameters.AddWithValue("@range", range);

                        object result = cmd.ExecuteScalar();

                        // Check if result is null or DBNull
                        if (result != null && result != DBNull.Value)
                        {
                            total = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return total;
        }


        private void timepicker6_ValueChanged(object sender, EventArgs e)
        {
            UpdateSalesTotals(timepicker6.Value);
        }

        private void weekly_Click(object sender, EventArgs e)
        {

        }

        private void back6_MouseEnter(object sender, EventArgs e)
        {
            back6.BackColor = Color.LightGray;
        }

        private void back6_MouseLeave(object sender, EventArgs e)
        {
            back6.BackColor = Color.Transparent;
        }
    }
}
