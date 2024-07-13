namespace POS_Jave2021.View.Admin
{
    partial class AdminDashboard
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listOfUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointOfSalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listOfInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productDeletedListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeUserListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inactiveUserListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inventoryToolStripMenuItem,
            this.productToolStripMenuItem,
            this.listOfUsersToolStripMenuItem,
            this.pointOfSalesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1203, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listOfInventoryToolStripMenuItem});
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.inventoryToolStripMenuItem.Text = "Inventory";
            // 
            // productToolStripMenuItem
            // 
            this.productToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productListToolStripMenuItem,
            this.productDeletedListToolStripMenuItem});
            this.productToolStripMenuItem.Name = "productToolStripMenuItem";
            this.productToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.productToolStripMenuItem.Text = "Product";
            // 
            // listOfUsersToolStripMenuItem
            // 
            this.listOfUsersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeUserListToolStripMenuItem,
            this.inactiveUserListToolStripMenuItem,
            this.addUserToolStripMenuItem});
            this.listOfUsersToolStripMenuItem.Name = "listOfUsersToolStripMenuItem";
            this.listOfUsersToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.listOfUsersToolStripMenuItem.Text = "List Of Users";
            // 
            // pointOfSalesToolStripMenuItem
            // 
            this.pointOfSalesToolStripMenuItem.Name = "pointOfSalesToolStripMenuItem";
            this.pointOfSalesToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.pointOfSalesToolStripMenuItem.Text = "Point Of Sales";
            this.pointOfSalesToolStripMenuItem.Click += new System.EventHandler(this.pointOfSalesToolStripMenuItem_Click);
            // 
            // listOfInventoryToolStripMenuItem
            // 
            this.listOfInventoryToolStripMenuItem.Name = "listOfInventoryToolStripMenuItem";
            this.listOfInventoryToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.listOfInventoryToolStripMenuItem.Text = "List Of Inventory";
            this.listOfInventoryToolStripMenuItem.Click += new System.EventHandler(this.listOfInventoryToolStripMenuItem_Click);
            // 
            // productListToolStripMenuItem
            // 
            this.productListToolStripMenuItem.Name = "productListToolStripMenuItem";
            this.productListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.productListToolStripMenuItem.Text = "Product List";
            // 
            // productDeletedListToolStripMenuItem
            // 
            this.productDeletedListToolStripMenuItem.Name = "productDeletedListToolStripMenuItem";
            this.productDeletedListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.productDeletedListToolStripMenuItem.Text = "Product Deleted List";
            // 
            // activeUserListToolStripMenuItem
            // 
            this.activeUserListToolStripMenuItem.Name = "activeUserListToolStripMenuItem";
            this.activeUserListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.activeUserListToolStripMenuItem.Text = "Active User List";
            // 
            // inactiveUserListToolStripMenuItem
            // 
            this.inactiveUserListToolStripMenuItem.Name = "inactiveUserListToolStripMenuItem";
            this.inactiveUserListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.inactiveUserListToolStripMenuItem.Text = "Inactive User List";
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addUserToolStripMenuItem.Text = "Add User";
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1203, 601);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdminDashboard";
            this.Text = "AdminDashboard";
            this.Load += new System.EventHandler(this.AdminDashboard_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listOfUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointOfSalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listOfInventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productDeletedListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activeUserListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inactiveUserListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
    }
}