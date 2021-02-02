using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CafeApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void manageBeverateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageBevarage window = new ManageBevarage();
            window.MdiParent = this;
            window.Show();
        }

        private void manageFoodToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageFood foodChild = new ManageFood();
            foodChild.MdiParent = this;
            foodChild.Show();
        }

        private void manageSetMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageSetMenu menu = new ManageSetMenu();
            menu.MdiParent = this;
            menu.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void discountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDiscount discount = new ManageDiscount();
            discount.MdiParent = this;
            discount.Show();
        }

        private void placeOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaceOrder order = new PlaceOrder();
            order.MdiParent = this;
            order.Show();
        }
    }
}
