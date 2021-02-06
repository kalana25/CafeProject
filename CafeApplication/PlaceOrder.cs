using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccessLayer;

namespace CafeApplication
{
    public partial class PlaceOrder : Form
    {
        private readonly Order order;
        private readonly SetMenu setMenu;
        private readonly Discount discount;
        private readonly DataTable orderItemDataTable;
        private decimal grandTotal = 0;
        public PlaceOrder()
        {
            InitializeComponent();
            order = new Order();
            setMenu = new SetMenu();
            discount = new Discount();
            orderItemDataTable = new DataTable();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }




        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void PlaceOrder_Load(object sender, EventArgs e)
        {
            gvFoodBevarage.DataSource = order.RetrieveFoodBevarage();
            gvFoodBevarage.Refresh();
            gvMenu.DataSource = setMenu.Retrieve();
            gvMenu.Refresh();
            CreateDataTable();
        }

        private void CreateDataTable ()
        {
            DataColumn colCode = new DataColumn("Code");
            orderItemDataTable.Columns.Add(colCode);
            DataColumn colName = new DataColumn("Name");
            orderItemDataTable.Columns.Add(colName);
            DataColumn colPrice = new DataColumn("Price");
            orderItemDataTable.Columns.Add(colPrice);
            DataColumn colQuantity = new DataColumn("Quantity");
            orderItemDataTable.Columns.Add(colQuantity);
            DataColumn colTotal = new DataColumn("Total");
            orderItemDataTable.Columns.Add(colTotal);
        }

        private void ClearTextBoxes()
        {
            txtItemCode.Clear();
            txtItemName.Clear();
            txtItemPrice.Clear();
            txtQuantity.Clear();
            txtDiscountRate.Clear();
            txtDiscountPrice.Clear();
        }

        private void gvFoodBevarage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtItemCode.Text = gvFoodBevarage.CurrentRow.Cells["Code"].Value.ToString();
            txtItemName.Text = gvFoodBevarage.CurrentRow.Cells["Name"].Value.ToString();
            txtItemPrice.Text = gvFoodBevarage.CurrentRow.Cells["Price"].Value.ToString();
            txtDiscountRate.Text = "0";
            txtDiscountPrice.Text = "0";
            txtQuantity.Focus();
        }

        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                DataRow dr = orderItemDataTable.NewRow();
                dr["Code"] = txtItemCode.Text;
                dr["Name"] = txtItemName.Text;
                dr["Price"] = txtItemPrice.Text;
                dr["Quantity"] = txtQuantity.Text;
                decimal total = decimal.Parse(txtItemPrice.Text) * int.Parse(txtQuantity.Text);
                dr["Total"] = total.ToString();
                orderItemDataTable.Rows.Add(dr);
                gvOrderItems.DataSource = orderItemDataTable;
                gvOrderItems.Refresh();
                grandTotal += total;
                lblGrandTotal.Text = grandTotal.ToString();
                ClearTextBoxes();
            }
        }

        private void gvMenu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtItemCode.Text = gvMenu.CurrentRow.Cells["Code"].Value.ToString();
            txtItemName.Text = gvMenu.CurrentRow.Cells["Name"].Value.ToString();
            txtItemPrice.Text = gvMenu.CurrentRow.Cells["Price"].Value.ToString();
            DataTable applicableDiscount = discount.Retrieve(int.Parse(gvMenu.CurrentRow.Cells["Id"].Value.ToString()), DateTime.Today);
            if(applicableDiscount.Rows.Count>0)
            {

            }
            txtQuantity.Focus();
        }
    }
}
