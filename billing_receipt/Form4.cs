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
    public partial class database : Form
    {
        public database()
        {
            InitializeComponent();
        }

        private void back4_Click(object sender, EventArgs e)
        {
            billing_info form2 = new billing_info();
            form2.Show();
            this.Hide();
        }
    }
}
