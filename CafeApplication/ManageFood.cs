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
    public partial class ManageFood : Form
    {
        private readonly Food food;
        private string mode;
        public ManageFood()
        {
            InitializeComponent();
            food = new Food();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ManageFood_Load(object sender, EventArgs e)
        {
            DataTable result = food.Retrieve();
            gvFood.DataSource = result;
            gvFood.Refresh();
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            txtid.Enabled = false;
            EnableDisableTextBoxes(false);
        }

        private void ClearTextbox()
        {
            txtid.Clear();
            txtcode.Clear();
            txtname.Clear();
            txtprice.Clear();
            txtdesc.Clear();
        }

        private void EnableDisableTextBoxes(bool val)
        {
            txtcode.Enabled = val;
            txtdesc.Enabled = val;
            txtprice.Enabled = val;
            txtname.Enabled = val;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearTextbox();
            txtcode.Enabled = true;
            txtname.Enabled = true;
            txtprice.Enabled = true;
            txtdesc.Enabled = true;
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
                int rowsAffected = food.Delete(int.Parse(txtid.Text));
                if (rowsAffected > 0)
                {
                    gvFood.DataSource = food.Retrieve();
                    gvFood.Refresh();
                    ClearTextbox();
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                    EnableDisableTextBoxes(false);
                    MessageBox.Show("Sucessfully deleted", "Delete", MessageBoxButtons.OK);
                }
            }
        }

        private void gvFood_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null)
                return;
            if (dgv.CurrentRow.Selected)
            {
                txtid.Text = dgv.CurrentRow.Cells["Id"].Value.ToString();
                txtcode.Text = dgv.CurrentRow.Cells["Code"].Value.ToString();
                txtname.Text = dgv.CurrentRow.Cells["Name"].Value.ToString();
                txtprice.Text = dgv.CurrentRow.Cells["Price"].Value.ToString();
                txtdesc.Text = dgv.CurrentRow.Cells["Description"].Value.ToString();
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                mode = "Edit";
                EnableDisableTextBoxes(true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mode == "New")
            {
                int result = food.insert(txtcode.Text.Trim(), txtname.Text.Trim(), decimal.Parse(txtprice.Text.Trim()), txtdesc.Text.Trim());
                if (result > 0)
                {
                    gvFood.DataSource = food.Retrieve();
                    gvFood.Refresh();
                    ClearTextbox();
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    EnableDisableTextBoxes(false);
                    MessageBox.Show("Bevarage saved sussessfully.", "Success", MessageBoxButtons.OK);
                }
            }
            else if (mode == "Edit")
            {
                int result = food.Edit(int.Parse(txtid.Text), txtcode.Text, txtname.Text, decimal.Parse(txtprice.Text), txtdesc.Text);
                if (result > 0)
                {
                    gvFood.DataSource = food.Retrieve();
                    gvFood.Refresh();
                    ClearTextbox();
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    EnableDisableTextBoxes(false);
                    MessageBox.Show("Bevarage updated sussessfully.", "Update", MessageBoxButtons.OK);
                }
            }
        }
    }
}
