namespace billing_receipt
{
    partial class Addcustomer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Addcustomer));
            this.namecust = new System.Windows.Forms.TextBox();
            this.addresscust = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.custidlabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.phonenum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.custadd = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.custback5 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.custadd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.custback5)).BeginInit();
            this.SuspendLayout();
            // 
            // namecust
            // 
            this.namecust.Location = new System.Drawing.Point(54, 105);
            this.namecust.Name = "namecust";
            this.namecust.Size = new System.Drawing.Size(217, 22);
            this.namecust.TabIndex = 0;
            this.namecust.TextChanged += new System.EventHandler(this.namecust_TextChanged);
            // 
            // addresscust
            // 
            this.addresscust.Location = new System.Drawing.Point(54, 171);
            this.addresscust.Name = "addresscust";
            this.addresscust.Size = new System.Drawing.Size(217, 22);
            this.addresscust.TabIndex = 1;
            this.addresscust.TextChanged += new System.EventHandler(this.addresscust_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(222, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Customer ID:";
            // 
            // custidlabel
            // 
            this.custidlabel.AutoSize = true;
            this.custidlabel.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custidlabel.Location = new System.Drawing.Point(329, 17);
            this.custidlabel.Name = "custidlabel";
            this.custidlabel.Size = new System.Drawing.Size(108, 25);
            this.custidlabel.TabIndex = 3;
            this.custidlabel.Text = "customer_id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(50, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(50, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "Address:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.phonenum);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.custadd);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.addresscust);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.namecust);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.custidlabel);
            this.panel1.Location = new System.Drawing.Point(90, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 304);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // phonenum
            // 
            this.phonenum.Location = new System.Drawing.Point(54, 241);
            this.phonenum.Name = "phonenum";
            this.phonenum.Size = new System.Drawing.Size(217, 22);
            this.phonenum.TabIndex = 9;
            this.phonenum.TextChanged += new System.EventHandler(this.phonenum_TextChanged);
            this.phonenum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.phonenum_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "Phone Number:";
            // 
            // custadd
            // 
            this.custadd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.custadd.Image = ((System.Drawing.Image)(resources.GetObject("custadd.Image")));
            this.custadd.Location = new System.Drawing.Point(344, 219);
            this.custadd.Name = "custadd";
            this.custadd.Size = new System.Drawing.Size(55, 44);
            this.custadd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.custadd.TabIndex = 7;
            this.custadd.TabStop = false;
            this.custadd.Click += new System.EventHandler(this.custadd_Click);
            this.custadd.MouseEnter += new System.EventHandler(this.custadd_MouseEnter);
            this.custadd.MouseLeave += new System.EventHandler(this.custadd_MouseLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = " Customer Details";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(82, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // custback5
            // 
            this.custback5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.custback5.Image = ((System.Drawing.Image)(resources.GetObject("custback5.Image")));
            this.custback5.Location = new System.Drawing.Point(12, 239);
            this.custback5.Name = "custback5";
            this.custback5.Size = new System.Drawing.Size(57, 42);
            this.custback5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.custback5.TabIndex = 8;
            this.custback5.TabStop = false;
            this.custback5.Click += new System.EventHandler(this.custback5_Click);
            this.custback5.MouseEnter += new System.EventHandler(this.custback5_MouseEnter);
            this.custback5.MouseLeave += new System.EventHandler(this.custback5_MouseLeave);
            // 
            // Addcustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(553, 298);
            this.Controls.Add(this.custback5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Addcustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Addcustomer";
            this.Load += new System.EventHandler(this.Addcustomer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.custadd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.custback5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox namecust;
        private System.Windows.Forms.TextBox addresscust;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label custidlabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox custadd;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox custback5;
        private System.Windows.Forms.TextBox phonenum;
        private System.Windows.Forms.Label label2;
    }
}