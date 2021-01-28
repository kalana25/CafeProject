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
    public partial class ManageDiscount : Form
    {
        private readonly SetMenu setMenu;
        private readonly Discount discount;
        private string mode;
        public ManageDiscount()
        {
            InitializeComponent();
            setMenu = new SetMenu();
            discount = new Discount();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ManageFood_Load(object sender, EventArgs e)
        {
            DataTable result = discount.Retrieve();
            gvDiscounts.DataSource = result;
            gvDiscounts.Refresh();
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            txtid.Enabled = false;
            EnableDisableTextBoxes(false);
            cmbMenu.DataSource = setMenu.Retrieve();
            cmbMenu.DisplayMember = "Name";
            cmbMenu.ValueMember = "Id";
            cmbMenu.SelectedIndex = -1;
        }

        private void ClearTextbox()
        {
            txtid.Clear();
            cmbMenu.SelectedIndex = -1;
            txtDiscountRate.Clear();
            dtpkStartDate.Value = DateTime.Today;
            dtpkEndDate.Value = DateTime.Today;
        }

        private void EnableDisableTextBoxes(bool val)
        {
            cmbMenu.Enabled = val;
            txtDiscountRate.Enabled = val;
            dtpkEndDate.Enabled = val;
            dtpkStartDate.Enabled = val;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearTextbox();
            mode = "New";
            btnSave.Enabled = true;
            EnableDisableTextBoxes(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                MessageBox.Show("Please select the row you want to delete.", "Select the row", MessageBoxButtons.OK);
            }
            else
            {
                int rowsAffected = discount.Delete(int.Parse(txtid.Text));
                if (rowsAffected > 0)
                {
                    gvDiscounts.DataSource = discount.Retrieve();
                    gvDiscounts.Refresh();
                    ClearTextbox();
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                    EnableDisableTextBoxes(false);
                    MessageBox.Show("Sucessfully deleted", "Discount", MessageBoxButtons.OK);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mode == "New")
            {
                int result = discount.insert(int.Parse(cmbMenu.SelectedValue.ToString()), decimal.Parse(txtDiscountRate.Text), dtpkStartDate.Value, dtpkEndDate.Value); ;
                if (result > 0)
                {
                    gvDiscounts.DataSource = discount.Retrieve();
                    gvDiscounts.Refresh();
                    ClearTextbox();
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    EnableDisableTextBoxes(false);
                    MessageBox.Show("Bevarage saved sussessfully.", "Success", MessageBoxButtons.OK);
                }
            }
            else if (mode == "Edit")
            {
                //int result = food.Edit(int.Parse(txtid.Text), txtcode.Text, txtname.Text, decimal.Parse(txtprice.Text), txtdesc.Text);
                //if (result > 0)
                //{
                //    gvFood.DataSource = food.Retrieve();
                //    gvFood.Refresh();
                //    ClearTextbox();
                //    btnSave.Enabled = false;
                //    btnDelete.Enabled = false;
                //    EnableDisableTextBoxes(false);
                //    MessageBox.Show("Bevarage updated sussessfully.", "Update", MessageBoxButtons.OK);
                //}
            }
        }

        private void gvDiscounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null)
                return;
            if (dgv.CurrentRow.Selected)
            {
                txtid.Text = dgv.CurrentRow.Cells["Id"].Value.ToString();
                txtDiscountRate.Text = dgv.CurrentRow.Cells["Rate"].Value.ToString();
                dtpkStartDate.Value = DateTime.Parse(dgv.CurrentRow.Cells["StartDate"].Value.ToString());
                dtpkEndDate.Value = DateTime.Parse(dgv.CurrentRow.Cells["EndDate"].Value.ToString());
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                mode = "Edit";
                EnableDisableTextBoxes(true);
            }
        }
    }
}
