using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void billing_info_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void submit_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = anydate.Value;
            
            Form3 receipt = new Form3();
            receipt.Show();
            this.Hide();

            Form3.Instance.cus_id.Text = customer_id.Text;
            Form3.Instance.prod_id.Text = product_id.Text;
            Form3.Instance.qty.Text = quantity.Text;
            Form3.Instance.ord_id.Text = order_id.Text;
            Form3.Instance.bill_id.Text = billing_id.Text;
            Form3.Instance.tot_amnt.Text = total_amount.Text;
            Form3.Instance.amount_pd3.Text = amount_paid.Text;
            Form3.Instance.chge.Text = change.Text;
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

        private void random_num_Click(object sender, EventArgs e)
        {
            Random customer = new Random();
            Random order = new Random();
            Random billing = new Random();

            int randomcustomer = customer.Next(0, 88888888);
            int randomorder = order.Next(0, 7777777);
            int randombilling = billing.Next(0, 55555);


            customer_id.Text = randomcustomer.ToString();
            order_id.Text = randomorder.ToString();
            billing_id.Text = randombilling.ToString();
        }
    }
}
