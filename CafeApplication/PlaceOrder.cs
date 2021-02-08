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
        private string itemType;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = order.SaveOrderHeader(DateTime.Today,grandTotal);
                foreach(DataRow row in orderItemDataTable.Rows)
                {
                    if(row["ItemType"].ToString()== "Product")
                    {
                        order.SaveOrderProductDetails(orderId, int.Parse(row["ProductId"].ToString()), int.Parse(row["Quantity"].ToString()), decimal.Parse(row["Total"].ToString()));
                    } 
                    else
                    {
                        order.SaveOrderMenuDetails(orderId, int.Parse(row["MenuId"].ToString()), int.Parse(row["Quantity"].ToString()), decimal.Parse(row["Total"].ToString()));
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
            DataColumn colProdId = new DataColumn("ProductId");
            orderItemDataTable.Columns.Add(colProdId);
            DataColumn colMenuId = new DataColumn("MenuId");
            orderItemDataTable.Columns.Add(colMenuId);
            DataColumn colItemType = new DataColumn("ItemType");
            orderItemDataTable.Columns.Add(colItemType);
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
            orderItemDataTable.Rows.Clear();
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
                DataRow dr = orderItemDataTable.NewRow();
                dr["Code"] = txtItemCode.Text;
                dr["Name"] = txtItemName.Text;
                dr["Price"] = txtItemPrice.Text;
                dr["Quantity"] = txtQuantity.Text;
                decimal total = decimal.Parse(txtItemPrice.Text) * int.Parse(txtQuantity.Text);
                total = total - decimal.Parse(txtDiscountPrice.Text);
                dr["Total"] = (total).ToString();
                dr["ItemType"] = itemType;
                if (itemType == "Menu")
                {
                    dr["MenuId"] = int.Parse(txtItemId.Text);
                }
                else
                {
                    dr["ProductId"] = int.Parse(txtItemId.Text);

                }

                orderItemDataTable.Rows.Add(dr);
                gvOrderItems.DataSource = orderItemDataTable;
                gvOrderItems.Refresh();
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
