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
    public partial class ManageSetMenu : Form
    {
        private readonly Bevarage bevarage;
        private readonly Food food;
        private readonly SetMenu setmenu;
        private string mode;
        private DataTable foodList;
        private DataTable bevarageList;
        public ManageSetMenu()
        {
            InitializeComponent();
            bevarage = new Bevarage();
            food = new Food();
            setmenu = new SetMenu();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ManageSetMenu_Load(object sender, EventArgs e)
        {
            foodList = food.Retrieve();
            cmbFood.DataSource = foodList;
            cmbFood.DisplayMember = "Name";
            cmbFood.ValueMember = "Id";

            bevarageList = bevarage.Retrieve();
            cmbBevarage.DataSource = bevarageList;
            cmbBevarage.DisplayMember = "Name";
            cmbBevarage.ValueMember = "Id";

            DataTable result = setmenu.Retrieve();
            gvSetMenu.DataSource = result;
            gvSetMenu.Refresh();
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            txtid.Enabled = false;
            EnableDisableTextBoxes(false);
            EnableDisableItemControlSection(false);
            btnMenuItemDelete.Enabled = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearTextbox();
            txtcode.Enabled = true;
            txtname.Enabled = true;
            txtprice.Enabled = true;
            mode = "New";
            btnSave.Enabled = true;
            EnableDisableTextBoxes(true);
        }

        private void ClearTextbox()
        {
            txtid.Clear();
            txtcode.Clear();
            txtname.Clear();
            txtprice.Clear();
        }

        private void EnableDisableTextBoxes(bool val)
        {
            txtcode.Enabled = val;
            txtprice.Enabled = val;
            txtname.Enabled = val;
        }

        private void EnableDisableItemControlSection(bool val)
        {
            cmbBevarage.Enabled = val;
            cmbFood.Enabled = val;
            txtBevarageQty.Enabled = val;
            txtFoodQty.Enabled = val;
            btnBevarageAdd.Enabled = val;
            btnFoodAdd.Enabled = val;
            gvSetMenuItems.Enabled = val;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mode == "New")
            {
                int result = setmenu.insert(txtcode.Text.Trim(), txtname.Text.Trim(), decimal.Parse(txtprice.Text.Trim()));
                if (result > 0)
                {
                    gvSetMenu.DataSource = setmenu.Retrieve();
                    gvSetMenu.Refresh();

                    ClearTextbox();
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    EnableDisableTextBoxes(false);
                    MessageBox.Show("Menu saved sussessfully.", "Success", MessageBoxButtons.OK);
                }
            }
            else if (mode == "Edit")
            {
                int result = setmenu.Edit(int.Parse(txtid.Text), txtcode.Text, txtname.Text, decimal.Parse(txtprice.Text));
                if (result > 0)
                {
                    gvSetMenu.DataSource = setmenu.Retrieve();
                    gvSetMenu.Refresh();
                    ClearTextbox();
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    EnableDisableTextBoxes(false);
                    MessageBox.Show("Bevarage updated sussessfully.", "Update", MessageBoxButtons.OK);
                }
            }
        }

        private void gvSetMenu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
                mode = "Edit";
                EnableDisableTextBoxes(true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                MessageBox.Show("Please select the row you want to delete.", "Select the row", MessageBoxButtons.OK);
            }
            else
            {
                int rowsAffected = setmenu.Delete(int.Parse(txtid.Text));
                if (rowsAffected > 0)
                {
                    gvSetMenu.DataSource = setmenu.Retrieve();
                    gvSetMenu.Refresh();
                    ClearTextbox();
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                    EnableDisableTextBoxes(false);
                    MessageBox.Show("Sucessfully deleted", "Delete", MessageBoxButtons.OK);
                }
            }
        }

        private void btnFoodAdd_Click(object sender, EventArgs e)
        {
            int menuId = int.Parse(gvSetMenu.SelectedRows[0].Cells[0].Value.ToString());
            DataRow[] row= foodList.Select($"Id={cmbFood.SelectedValue}");
           int result = setmenu.insertMenuItem(int.Parse(cmbFood.SelectedValue.ToString()), int.Parse(txtFoodQty.Text), menuId,decimal.Parse(row[0]["Price"].ToString()));
            if (result > 0)
            {
                gvSetMenuItems.DataSource = setmenu.RetrieveMenuItems(menuId);
                gvSetMenuItems.Refresh();
                gvSetMenu.DataSource = setmenu.Retrieve();
                gvSetMenu.Refresh();
            }
        }

        private void btnBevarageAdd_Click(object sender, EventArgs e)
        {
            int menuId = int.Parse(gvSetMenu.SelectedRows[0].Cells[0].Value.ToString());
            DataRow[] row = bevarageList.Select($"Id={cmbBevarage.SelectedValue}");
            int result = setmenu.insertMenuItem(int.Parse(cmbBevarage.SelectedValue.ToString()), int.Parse(txtBevarageQty.Text), menuId, decimal.Parse(row[0]["Price"].ToString()));
            if (result > 0)
            {
                gvSetMenuItems.DataSource = setmenu.RetrieveMenuItems(menuId);
                gvSetMenuItems.Refresh();
                gvSetMenu.DataSource = setmenu.Retrieve();
                gvSetMenu.Refresh();
            }
        }

        private void btnMenuItemDelete_Click(object sender, EventArgs e)
        {
            int menuItemId = int.Parse(gvSetMenuItems.SelectedRows[0].Cells[0].Value.ToString());
            int menuId = int.Parse(gvSetMenu.SelectedRows[0].Cells[0].Value.ToString());
            int rowsAffected = setmenu.DeleteMenuItem(menuItemId,int.Parse(gvSetMenuItems.SelectedRows[0].Cells[4].Value.ToString()),decimal.Parse(gvSetMenuItems.SelectedRows[0].Cells[3].Value.ToString()),menuId);
            if (rowsAffected > 0)
            {
                gvSetMenuItems.DataSource = setmenu.RetrieveMenuItems(menuId);
                gvSetMenuItems.Refresh();
                gvSetMenu.DataSource = setmenu.Retrieve();
                gvSetMenu.Refresh();
                btnMenuItemDelete.Enabled = false;
                //ClearTextbox();
                //btnDelete.Enabled = false;
                //btnSave.Enabled = false;
                //EnableDisableTextBoxes(false);
                MessageBox.Show("Sucessfully deleted", "Set Menu", MessageBoxButtons.OK);
            }

        }

        private void gvSetMenu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EnableDisableItemControlSection(true);
            gvSetMenuItems.DataSource = setmenu.RetrieveMenuItems(int.Parse(gvSetMenu.SelectedRows[0].Cells[0].Value.ToString()));
            gvSetMenuItems.Refresh();
        }

        private void gvSetMenuItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnMenuItemDelete.Enabled = true;
        }
    }
}
