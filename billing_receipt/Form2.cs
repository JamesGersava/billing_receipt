using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace billing_receipt
{
    public partial class billing_info : Form
    {
        public static billing_info instance;
        public billing_info()
        {
            InitializeComponent();
            instance = this;
           
        }


        private void billing_info_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-NAOMDNI0\\SQLEXPRESS;Initial Catalog=billingreceipt;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT productinfo_id, productinfo_name FROM PRODUCT_INFO", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            DataRow itemrow = dt.NewRow();
            itemrow[1] = "Selected Flavor";
            dt.Rows.InsertAt(itemrow, 0);

            comboprod.DataSource = dt;
            comboprod.DisplayMember = "productinfo_id";
            comboprod.ValueMember = "productinfo_name";

        }


        private void submit_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = anydate.Value;
            
            Form3 receipt = new Form3();
            receipt.Show();
            this.Hide();



            Form3.Instance.prodflvr.Text = flavor.Text;
            Form3.Instance.prodprice.Text = price2.Text;
            Form3.Instance.qty.Text = quantity.Text;
            Form3.Instance.ord_id.Text = order_id.Text;
            Form3.Instance.bill_id.Text = billing_id.Text;
            Form3.Instance.tot_amnt.Text = total_amount.Text;
            Form3.Instance.amount_pd3.Text = amount_paid.Text;
            Form3.Instance.chge.Text = change.Text;
            Form3.Instance.combo.Text = comboprod.Text;
            Form3.Instance.date_picked.Text = selectedDate.ToString("MM/dd/yyyy");

            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void form2exit_Click(object sender, EventArgs e)

        {
            Login form1 = new Login(); 
            form1.Show();
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            database form4 = new database();
            form4.Show();
            this.Hide();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void total_amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void amount_paid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void change_TextChanged(object sender, EventArgs e)
        {

        }

        private void change_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Random customer = new Random();
            Random order = new Random();
            Random billing = new Random();


            int randomorder = order.Next(0, 7777777);
            int randombilling = billing.Next(0, 55555);

            
            order_id.Text = randomorder.ToString();
            billing_id.Text = randombilling.ToString();
        }

        private void comboprod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboprod.SelectedItem != null)
            {
                
                comboprod.Text = "Select Product ID ";
            }

        }

        private void comboprod_SelectedValueChanged(object sender, EventArgs e)
        {
            flavor.Text = comboprod.SelectedValue.ToString();
        }
    }
}
