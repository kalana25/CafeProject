namespace CafeApplication
{
    partial class Form1
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
            this.bevarageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageBeverateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageFoodToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.manageSetMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.placeOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablePlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kitchenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bevarageToolStripMenuItem,
            this.orderToolStripMenuItem,
            this.tablePlanToolStripMenuItem,
            this.kitchenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(896, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bevarageToolStripMenuItem
            // 
            this.bevarageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageBeverateToolStripMenuItem,
            this.manageFoodToolStripMenuItem1,
            this.manageSetMenuToolStripMenuItem});
            this.bevarageToolStripMenuItem.Name = "bevarageToolStripMenuItem";
            this.bevarageToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.bevarageToolStripMenuItem.Text = "Products";
            // 
            // manageBeverateToolStripMenuItem
            // 
            this.manageBeverateToolStripMenuItem.Name = "manageBeverateToolStripMenuItem";
            this.manageBeverateToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.manageBeverateToolStripMenuItem.Text = "Manage Bevarage";
            this.manageBeverateToolStripMenuItem.Click += new System.EventHandler(this.manageBeverateToolStripMenuItem_Click);
            // 
            // manageFoodToolStripMenuItem1
            // 
            this.manageFoodToolStripMenuItem1.Name = "manageFoodToolStripMenuItem1";
            this.manageFoodToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.manageFoodToolStripMenuItem1.Text = "Manage Food";
            this.manageFoodToolStripMenuItem1.Click += new System.EventHandler(this.manageFoodToolStripMenuItem1_Click);
            // 
            // manageSetMenuToolStripMenuItem
            // 
            this.manageSetMenuToolStripMenuItem.Name = "manageSetMenuToolStripMenuItem";
            this.manageSetMenuToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.manageSetMenuToolStripMenuItem.Text = "Manage Set menu";
            this.manageSetMenuToolStripMenuItem.Click += new System.EventHandler(this.manageSetMenuToolStripMenuItem_Click);
            // 
            // orderToolStripMenuItem
            // 
            this.orderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.placeOrderToolStripMenuItem,
            this.invoiceToolStripMenuItem,
            this.discountToolStripMenuItem});
            this.orderToolStripMenuItem.Name = "orderToolStripMenuItem";
            this.orderToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.orderToolStripMenuItem.Text = "Order";
            // 
            // placeOrderToolStripMenuItem
            // 
            this.placeOrderToolStripMenuItem.Name = "placeOrderToolStripMenuItem";
            this.placeOrderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.placeOrderToolStripMenuItem.Text = "Place Order";
            // 
            // invoiceToolStripMenuItem
            // 
            this.invoiceToolStripMenuItem.Name = "invoiceToolStripMenuItem";
            this.invoiceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.invoiceToolStripMenuItem.Text = "Invoice";
            // 
            // discountToolStripMenuItem
            // 
            this.discountToolStripMenuItem.Name = "discountToolStripMenuItem";
            this.discountToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.discountToolStripMenuItem.Text = "Discount";
            this.discountToolStripMenuItem.Click += new System.EventHandler(this.discountToolStripMenuItem_Click);
            // 
            // tablePlanToolStripMenuItem
            // 
            this.tablePlanToolStripMenuItem.Name = "tablePlanToolStripMenuItem";
            this.tablePlanToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.tablePlanToolStripMenuItem.Text = "Table Plan";
            // 
            // kitchenToolStripMenuItem
            // 
            this.kitchenToolStripMenuItem.Name = "kitchenToolStripMenuItem";
            this.kitchenToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.kitchenToolStripMenuItem.Text = "Kitchen";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 518);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bevarageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageBeverateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageFoodToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem manageSetMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem placeOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tablePlanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kitchenToolStripMenuItem;
    }
}

