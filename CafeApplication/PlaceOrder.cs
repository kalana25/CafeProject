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
    public partial class PlaceOrders : Form
    {
        private readonly Order order;
        private readonly SetMenu setMenu;
        private readonly Discount discount;
        //private readonly DataTable orderItemDataTable;
        private decimal grandTotal = 0;
        private string itemType;
        public PlaceOrders()
        {
            InitializeComponent();
            order = new Order();
            setMenu = new SetMenu();
            discount = new Discount();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = order.SaveOrderHeader(DateTime.Today,grandTotal);
                foreach(DataGridViewRow gvr in gvOrderItems.Rows)
                {
                    if(gvr.Cells["Type"].Value.ToString() == "Product")
                    {
                        order.SaveOrderProductDetails(orderId, int.Parse(gvr.Cells["ProductId"].Value.ToString()), int.Parse(gvr.Cells["Quantity"].Value.ToString()), decimal.Parse(gvr.Cells["Total"].Value.ToString()));
                    } 
                    else
                    {
                        order.SaveOrderMenuDetails(orderId, int.Parse(gvr.Cells["MenuId"].Value.ToString()), int.Parse(gvr.Cells["Quantity"].Value.ToString()), decimal.Parse(gvr.Cells["Total"].Value.ToString()));
                    }
                }
                MessageBox.Show("Order saved sussessfully.", "Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                PrepareForNew();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error Occured", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void PlaceOrder_Load(object sender, EventArgs e)
        {
            gvFoodBevarage.DataSource = order.RetrieveFoodBevarage();
            gvFoodBevarage.Refresh();
            gvMenu.DataSource = setMenu.Retrieve();
            gvMenu.Refresh();
            ConfigGridView();
        }

        public void ConfigGridView()
        {
            gvOrderItems.AutoGenerateColumns = false;
            gvOrderItems.Columns["MenuId"].Visible = false;
            gvOrderItems.Columns["ProductId"].Visible = false;
            gvOrderItems.Columns["Type"].Visible = false;
        }

        private void ClearTextBoxes()
        {
            txtItemCode.Clear();
            txtItemName.Clear();
            txtItemPrice.Clear();
            txtQuantity.Clear();
            txtRate.Clear();
            txtDiscountPrice.Clear();
        }

        private void PrepareForNew()
        {
            itemType = null;
            grandTotal = 0m;
            gvOrderItems.Rows.Clear();
            lblGrandTotal.Text = "0.00";
        }

        private void gvFoodBevarage_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtItemCode.Text = gvFoodBevarage.CurrentRow.Cells["Code"].Value.ToString();
            txtItemName.Text = gvFoodBevarage.CurrentRow.Cells["Name"].Value.ToString();
            txtItemPrice.Text = gvFoodBevarage.CurrentRow.Cells["Price"].Value.ToString();
            txtRate.Text = "0";
            txtDiscountPrice.Text = "0";
            itemType = "Product";
            txtItemId.Text = gvFoodBevarage.CurrentRow.Cells["Id"].Value.ToString();
            txtQuantity.Focus();
        }

        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                int index = gvOrderItems.Rows.Count;
                gvOrderItems.Rows.Add();
                gvOrderItems["Code",index].Value = txtItemCode.Text;
                gvOrderItems["ItemName",index].Value = txtItemName.Text;
                gvOrderItems["Price",index].Value = txtItemPrice.Text;
                gvOrderItems["Quantity",index].Value = txtQuantity.Text;
                decimal total = decimal.Parse(txtItemPrice.Text) * int.Parse(txtQuantity.Text);
                total = total - decimal.Parse(txtDiscountPrice.Text);
                gvOrderItems["Total",index].Value = (total).ToString();
                gvOrderItems["Type",index].Value = itemType;
                if (itemType == "Menu")
                {
                    gvOrderItems["MenuId",index].Value = int.Parse(txtItemId.Text);
                }
                else
                {
                    gvOrderItems["ProductId",index].Value = int.Parse(txtItemId.Text);

                }
                grandTotal += total;
                lblGrandTotal.Text = grandTotal.ToString();
                ClearTextBoxes();
                txtBarcode.Focus();
            }
            else
            {
                int quantity;
                if(!int.TryParse(txtQuantity.Text,out quantity))
                {
                    quantity = 0;
                }
                decimal discoutPrice = decimal.Parse(txtRate.Text) * decimal.Parse(txtItemPrice.Text) * quantity / 100;
                txtDiscountPrice.Text = discoutPrice.ToString();
            }
        }

        private void gvMenu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtItemCode.Text = gvMenu.CurrentRow.Cells["Code"].Value.ToString();
            txtItemName.Text = gvMenu.CurrentRow.Cells["Name"].Value.ToString();
            txtItemPrice.Text = gvMenu.CurrentRow.Cells["Price"].Value.ToString();
            txtItemId.Text = gvMenu.CurrentRow.Cells["Id"].Value.ToString();
            itemType = "Menu";
            DataTable applicableDiscount = discount.Retrieve(int.Parse(gvMenu.CurrentRow.Cells["Id"].Value.ToString()), DateTime.Today);
            decimal rate = 0m;
            if (applicableDiscount.Rows.Count>0)
            {
                rate= decimal.Parse(applicableDiscount.Rows[0]["Rate"].ToString());
                txtRate.Text = rate.ToString();
            }
            else
            {
                txtRate.Text = rate.ToString();
            }
            txtQuantity.Focus();
        }
    }
}
