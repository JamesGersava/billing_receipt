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

namespace billing_receipt
{
    public partial class Login : Form
    {

        public Login ()
        {
            InitializeComponent();

            button_login.FlatStyle = FlatStyle.Standard;
            button_login.FlatAppearance.BorderSize = 1; 

            button_login.MouseEnter += new EventHandler(button_login_MouseEnter);
            button_login.MouseLeave += new EventHandler(button_login_MouseLeave);

            button_clear.FlatStyle = FlatStyle.Standard;
            button_clear.FlatAppearance.BorderSize = 1;

            button_clear.MouseEnter += new EventHandler(button_clear_MouseEnter);
            button_clear.MouseLeave += new EventHandler(button_clear_MouseLeave);

            txt_username.BorderStyle = BorderStyle.FixedSingle;
            txt_username.Paint += new PaintEventHandler(Txt_Username_Paint);

            txt_password.BorderStyle = BorderStyle.FixedSingle;
            txt_password.Paint += new PaintEventHandler(Txt_Password_Paint);
        }

        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-NAOMDNI0\SQLEXPRESS;Initial Catalog=Billingsystem;Integrated Security=True");


        private void Login_Load(object sender, EventArgs e)
        {
        
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }



        private void button_clear_Click_1(object sender, EventArgs e)
        {
            txt_username.Clear();
            txt_password.Clear();

            // TO FOCUS USERNAME
            txt_username.Focus();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                txt_password.UseSystemPasswordChar = false;
            }
            else
            {

                txt_password.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_login_Click_1(object sender, EventArgs e)
        {

            String username, user_password;

            username = txt_username.Text;
            user_password = txt_password.Text;

            try
            {
                String querry = "SELECT * FROM loginn WHERE username = '" + txt_username.Text + "' AND password = '" + txt_password.Text + "' ";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    username = txt_username.Text;
                    user_password = txt_password.Text;

                    billing_info form2 = new billing_info();
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_username.Clear();
                    txt_password.Clear();

                    txt_username.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.BurlyWood;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_login_MouseEnter(object sender, EventArgs e)
        {
            button_login.FlatStyle = FlatStyle.Flat;    
            button_login.FlatAppearance.BorderSize = 0;
        }

        private void button_clear_MouseEnter(object sender, EventArgs e)
        {
            button_clear.FlatStyle = FlatStyle.Flat; 
            button_clear.FlatAppearance.BorderSize = 0;
        }

        private void button_login_MouseLeave(object sender, EventArgs e)
        {
            button_login.FlatStyle = FlatStyle.Standard;
            button_login.FlatAppearance.BorderSize = 1;
        }

        private void button_clear_MouseLeave(object sender, EventArgs e)
        {
            button_clear.FlatStyle = FlatStyle.Standard;
            button_clear.FlatAppearance.BorderSize = 1;
        }

        private void txt_username_MouseEnter(object sender, EventArgs e)
        {

        }
        private void Txt_Username_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 0, txt_username.Height - 1, txt_username.Width, txt_username.Height - 1);
        }
        private void Txt_Password_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 0, txt_password.Height - 1, txt_password.Width, txt_password.Height - 1);
        }
    }
}
    
