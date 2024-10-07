using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Required for SQL operations


namespace billing_receipt
{
    public partial class Form3 : Form
    {
        public static Form3 Instance;
        public Label cus_id, prod_id, qty, ord_id, bill_id, tot_amnt, amount_pd3, chge, date_picked;

        private void printbtn_Click(object sender, EventArgs e)
        {
            PrintDialog printdialog1 = new PrintDialog();
            printdialog1.Document = printDocument1;

            DialogResult result = printdialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Billing Receipt", new Font("Arial", 20, FontStyle.Bold), Brushes.Black, new Point(10, 10));
            e.Graphics.DrawString("Office address", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 150));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            billing_info form2 = new billing_info();
            form2.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            

            string billingIdText = billing_id3.Text;
            string CustomerIdText = cust_id.Text;
            string ProductIdText = pdcts_id.Text;
            string OrderIdText = order_id3.Text;
            string amntpaidText = amt_pd.Text;
            string changeText = change3.Text;
            string dateText = date_3.Text;
            string TotalAmntText = total_amnt.Text;
            string quantityText = qnty.Text;

            string connectionString = "Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=billingreceipt;Integrated Security=True";

            string billingquery = "INSERT INTO Billing VALUES (@billing_id)";
            string customerquery = "INSERT INTO Customer VALUES (@customer_id)";
            string productquery = "INSERT INTO Product VALUES (@product_id, @quantity)";
            string orderquery = "INSERT INTO Booking VALUES (@odr_id)";
            string paymentquery = "INSERT INTO Payment VALUES (@amount_paid, @change, @date, @total_amount)";


            using (SqlConnection conn = new SqlConnection(connectionString))

            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(billingquery, conn))
                    {
                        cmd.Parameters.AddWithValue("@billing_id", billingIdText);

                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(customerquery, conn))
                    {
                        cmd.Parameters.AddWithValue("@customer_id", CustomerIdText);

                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(productquery, conn))
                    {
                        cmd.Parameters.AddWithValue("@product_id", ProductIdText);
                        cmd.Parameters.AddWithValue("@quantity", quantityText);

                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(orderquery, conn))
                    {
                        cmd.Parameters.AddWithValue("@odr_id", OrderIdText);

                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(paymentquery, conn))
                    {
                        cmd.Parameters.AddWithValue("@amount_paid", amntpaidText);
                        cmd.Parameters.AddWithValue("@change", changeText);
                        cmd.Parameters.AddWithValue("@date", dateText);
                        cmd.Parameters.AddWithValue("@total_amount", TotalAmntText);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Data Insert Successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting data: " + ex.Message);

                }
                finally
                {
                    conn.Close();

                }
            }

        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

        public Form3()
        {
            InitializeComponent();

            Instance = this;
            date_picked = date_3;
            cus_id = cust_id;
            prod_id = pdcts_id;
            qty = qnty;
            ord_id = order_id3;
            bill_id = billing_id3;
            tot_amnt = total_amnt;
            amount_pd3 = amt_pd;
            chge = change3;
            
        }
    }
}
