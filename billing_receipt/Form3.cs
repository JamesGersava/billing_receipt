using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing; // Required for SQL operations


namespace billing_receipt
{
    public partial class Receipt : Form
    {
        private PrintDocument printDocument = new PrintDocument();
        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        
        public static Receipt Instance;
        public Label qty, ord_id, tot_amnt, amount_pd3, chge, p_price, date_picked,
            combo, cusid3, cusname, cusaddr, meth, p_number, pack_detls;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pdcts_id_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            return3.BackColor = Color.LightGray;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            return3.BackColor = Color.Transparent;
        }

        string connectionString = "Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True";

        private void Receipt_Load(object sender, EventArgs e)
        {

        }

        private void print_pb_MouseEnter(object sender, EventArgs e)
        {
            print_pb.BackColor = Color.LightGray;
        }

        private void print_pb_MouseLeave(object sender, EventArgs e)
        {
            print_pb.BackColor = Color.Transparent;
        }

        private void insert3_MouseEnter(object sender, EventArgs e)
        {
            insert3.BackColor = Color.LightGray;
        }

        private void date_3_Click(object sender, EventArgs e)
        {

        }

        private void insert3_MouseLeave(object sender, EventArgs e)
        {
            insert3.BackColor= Color.Transparent;
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height));

            float scaleX = e.MarginBounds.Width / (float)bmp.Width;
            float scaleY = e.MarginBounds.Height / (float)bmp.Height;
            float scale = Math.Min(scaleX, scaleY);

            int width = (int)(bmp.Width * scale);
            int height = (int)(bmp.Height * scale);

            int left = e.MarginBounds.Left + (e.MarginBounds.Width - width) / 2;
            int top = e.MarginBounds.Top + (e.MarginBounds.Height - height) / 2;

            e.Graphics.DrawImage(bmp, left, top, width, height);
        }

        private void printPreviewDialog1_Load_1(object sender, EventArgs e)
        {

        }
        private void preview_MouseEnter(object sender, EventArgs e)
        {
            preview.BackColor = Color.LightGray;
        }

        private void preview_MouseLeave(object sender, EventArgs e)
        {
            preview.BackColor = Color.Transparent;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            print_pb.Visible = false;
            insert3.Visible = false;
            return3.Visible = false;
            preview.Visible = false;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            printDialog.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }

            print_pb.Visible = true;
            insert3.Visible = true;
            return3.Visible = true;
            preview.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
            this.Close();

            billing_info form2 = Application.OpenForms["billing_info"] as billing_info;

            if (form2 != null)
            {
                form2.Show();
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string customerIdText = cust_rec.Text;
            string OrderIdText = order_id3.Text;
            string amntpaidText = amt_pd.Text;
            string changeText = change3.Text;
            string dateText = date_3.Text;
            string TotalAmntText = total_amnt.Text;
            string quantityText = qnty.Text;
            string priceText = prod_price3.Text;
            string packagenameText = prod_flvor3.Text;
            string paymethodText = paymeth.Text;
            string packagedetailsText = packdet3.Text;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string odrQuery = "INSERT INTO Odr (odr_id, customer_id, odr_date, Total) VALUES (@odr_id, @customer_id, @odr_date, @Total)";
                    using (SqlCommand cmd1 = new SqlCommand(odrQuery, conn))
                    {
                        cmd1.Parameters.AddWithValue("@odr_id", OrderIdText);
                        cmd1.Parameters.AddWithValue("@customer_id", customerIdText);
                        cmd1.Parameters.AddWithValue("@odr_date", dateText);
                        cmd1.Parameters.AddWithValue("@Total", TotalAmntText);
                        cmd1.ExecuteNonQuery();
                    }

                    string orderDetailQuery = "INSERT INTO Order_detail (odr_id, package_name, quantity, price) VALUES (@odr_id, @package_name, @quantity, @price)";
                    using (SqlCommand cmd2 = new SqlCommand(orderDetailQuery, conn))
                    {
                        cmd2.Parameters.AddWithValue("@odr_id", OrderIdText);
                        cmd2.Parameters.AddWithValue("@package_name", packagenameText);
                        cmd2.Parameters.AddWithValue("@quantity", quantityText);
                        cmd2.Parameters.AddWithValue("@price", priceText);
                        cmd2.ExecuteNonQuery();
                    }

                    string billingQuery = "INSERT INTO Billing (odr_id, Total, amount_paid, change, payment_method) VALUES (@odr_id, @Total, @amount_paid, @change, @payment_method)";
                    using (SqlCommand cmd3 = new SqlCommand(billingQuery, conn))
                    {
                        cmd3.Parameters.AddWithValue("@odr_id", OrderIdText);
                        cmd3.Parameters.AddWithValue("@Total", TotalAmntText);
                        cmd3.Parameters.AddWithValue("@amount_paid", amntpaidText);
                        cmd3.Parameters.AddWithValue("@change", changeText);
                        cmd3.Parameters.AddWithValue("@payment_method", paymethodText);
                        cmd3.ExecuteNonQuery();
                    }

                    string packageQuery = "INSERT INTO Package (package_name, package_detail, price) VALUES (@package_name, @package_detail, @price)";
                    using (SqlCommand cmd4 = new SqlCommand(packageQuery, conn))
                    {
                        cmd4.Parameters.AddWithValue("@package_name", packagenameText);
                        cmd4.Parameters.AddWithValue("@package_detail", packagedetailsText);
                        cmd4.Parameters.AddWithValue("@price", priceText);
                        cmd4.ExecuteNonQuery();
                    }

                    MessageBox.Show("Data Inserted Successfully!");
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

        public Receipt(string paymentMethod)
        {
            InitializeComponent();
            DisplayPaymentMethod(paymentMethod);

            printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage_1);
            printDocument.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);

            Instance = this;
            date_picked = date_3;
            qty = qnty;
            ord_id = order_id3;
            tot_amnt = total_amnt;
            amount_pd3 = amt_pd;
            chge = change3;
            p_price = prod_price3;
            combo = prod_flvor3;
            cusid3 = cust_rec;
            cusname = name_rec;
            cusaddr = address_rec;
            meth = paymeth;
            p_number = phnumber3;
            //pack_detls = packdet3;

        }
        public void SetValues(string allPackage, string   allPrices, string allQuantities, string allPackageDetails)
        {
            prod_flvor3.Text = allPackage;
            prod_price3.Text = allPrices;
            qnty.Text = allQuantities;
            packdet3.Text = allPackageDetails;
        }  

        private void DisplayPaymentMethod(string paymentMethod)
        {
            paymeth.Text = paymentMethod;
        }

    }
}
