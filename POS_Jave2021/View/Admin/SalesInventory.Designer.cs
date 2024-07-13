namespace POS_Jave2021.View.Admin
{
    partial class SalesInventory
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
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.isVoidYes = new System.Windows.Forms.RadioButton();
            this.isVoidNo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.creditToNo = new System.Windows.Forms.RadioButton();
            this.creditToYes = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.totalSalesAmount = new System.Windows.Forms.TextBox();
            this.totalVoidAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.totalCreditAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PriceAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cashAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.changeAmount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.NumOfItems = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.remark = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSales
            // 
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AllowUserToDeleteRows = false;
            this.dgvSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSales.Location = new System.Drawing.Point(12, 113);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.Size = new System.Drawing.Size(773, 318);
            this.dgvSales.TabIndex = 0;
            this.dgvSales.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSales_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "FROM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(369, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "TO";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(75, 26);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(273, 26);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Location = new System.Drawing.Point(407, 25);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(273, 26);
            this.dateTimePicker2.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(686, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 36);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Silver;
            this.groupBox1.Controls.Add(this.isVoidNo);
            this.groupBox1.Controls.Add(this.isVoidYes);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 49);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Is Void?";
            // 
            // isVoidYes
            // 
            this.isVoidYes.AutoSize = true;
            this.isVoidYes.Location = new System.Drawing.Point(84, 19);
            this.isVoidYes.Name = "isVoidYes";
            this.isVoidYes.Size = new System.Drawing.Size(58, 24);
            this.isVoidYes.TabIndex = 0;
            this.isVoidYes.TabStop = true;
            this.isVoidYes.Text = "Yes";
            this.isVoidYes.UseVisualStyleBackColor = true;
            // 
            // isVoidNo
            // 
            this.isVoidNo.AutoSize = true;
            this.isVoidNo.Location = new System.Drawing.Point(162, 19);
            this.isVoidNo.Name = "isVoidNo";
            this.isVoidNo.Size = new System.Drawing.Size(49, 24);
            this.isVoidNo.TabIndex = 1;
            this.isVoidNo.TabStop = true;
            this.isVoidNo.Text = "No";
            this.isVoidNo.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Silver;
            this.groupBox2.Controls.Add(this.creditToNo);
            this.groupBox2.Controls.Add(this.creditToYes);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(373, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 49);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Credit To";
            // 
            // creditToNo
            // 
            this.creditToNo.AutoSize = true;
            this.creditToNo.Location = new System.Drawing.Point(174, 19);
            this.creditToNo.Name = "creditToNo";
            this.creditToNo.Size = new System.Drawing.Size(49, 24);
            this.creditToNo.TabIndex = 1;
            this.creditToNo.TabStop = true;
            this.creditToNo.Text = "No";
            this.creditToNo.UseVisualStyleBackColor = true;
            // 
            // creditToYes
            // 
            this.creditToYes.AutoSize = true;
            this.creditToYes.Location = new System.Drawing.Point(96, 19);
            this.creditToYes.Name = "creditToYes";
            this.creditToYes.Size = new System.Drawing.Size(58, 24);
            this.creditToYes.TabIndex = 0;
            this.creditToYes.TabStop = true;
            this.creditToYes.Text = "Yes";
            this.creditToYes.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.totalCreditAmount);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.totalVoidAmount);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.totalSalesAmount);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(791, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 205);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sub Total";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total Sales Amount";
            // 
            // totalSalesAmount
            // 
            this.totalSalesAmount.Enabled = false;
            this.totalSalesAmount.Location = new System.Drawing.Point(9, 56);
            this.totalSalesAmount.Name = "totalSalesAmount";
            this.totalSalesAmount.Size = new System.Drawing.Size(214, 26);
            this.totalSalesAmount.TabIndex = 3;
            // 
            // totalVoidAmount
            // 
            this.totalVoidAmount.Enabled = false;
            this.totalVoidAmount.Location = new System.Drawing.Point(9, 110);
            this.totalVoidAmount.Name = "totalVoidAmount";
            this.totalVoidAmount.Size = new System.Drawing.Size(214, 26);
            this.totalVoidAmount.TabIndex = 5;
            this.totalVoidAmount.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Total Void Amount";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // totalCreditAmount
            // 
            this.totalCreditAmount.Enabled = false;
            this.totalCreditAmount.Location = new System.Drawing.Point(9, 164);
            this.totalCreditAmount.Name = "totalCreditAmount";
            this.totalCreditAmount.Size = new System.Drawing.Size(214, 26);
            this.totalCreditAmount.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Total Debt Credit Amount";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.remark);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.NumOfItems);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.changeAmount);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.cashAmount);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.PriceAmount);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(791, 226);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(230, 364);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Details";
            // 
            // PriceAmount
            // 
            this.PriceAmount.Enabled = false;
            this.PriceAmount.Location = new System.Drawing.Point(10, 49);
            this.PriceAmount.Name = "PriceAmount";
            this.PriceAmount.Size = new System.Drawing.Size(211, 26);
            this.PriceAmount.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Total Price Amount";
            // 
            // cashAmount
            // 
            this.cashAmount.Enabled = false;
            this.cashAmount.Location = new System.Drawing.Point(9, 101);
            this.cashAmount.Name = "cashAmount";
            this.cashAmount.Size = new System.Drawing.Size(212, 26);
            this.cashAmount.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Cash Amount";
            // 
            // changeAmount
            // 
            this.changeAmount.Enabled = false;
            this.changeAmount.Location = new System.Drawing.Point(9, 156);
            this.changeAmount.Name = "changeAmount";
            this.changeAmount.Size = new System.Drawing.Size(213, 26);
            this.changeAmount.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Change Amount";
            // 
            // NumOfItems
            // 
            this.NumOfItems.Enabled = false;
            this.NumOfItems.Location = new System.Drawing.Point(9, 208);
            this.NumOfItems.Name = "NumOfItems";
            this.NumOfItems.Size = new System.Drawing.Size(212, 26);
            this.NumOfItems.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 20);
            this.label9.TabIndex = 10;
            this.label9.Text = "Number Of Items";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(9, 237);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Remarks";
            // 
            // remark
            // 
            this.remark.Enabled = false;
            this.remark.Location = new System.Drawing.Point(11, 269);
            this.remark.Name = "remark";
            this.remark.Size = new System.Drawing.Size(212, 96);
            this.remark.TabIndex = 13;
            this.remark.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 450);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(773, 141);
            this.dataGridView1.TabIndex = 10;
            // 
            // SalesInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 602);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSales);
            this.Name = "SalesInventory";
            this.Text = "SalesInventory";
            this.Load += new System.EventHandler(this.SalesInventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton isVoidYes;
        private System.Windows.Forms.RadioButton isVoidNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton creditToNo;
        private System.Windows.Forms.RadioButton creditToYes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox totalVoidAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox totalSalesAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox totalCreditAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox remark;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox NumOfItems;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox changeAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox cashAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox PriceAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}