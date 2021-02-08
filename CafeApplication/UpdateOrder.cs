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
    public partial class UpdateOrder : Form
    {
        private readonly Order order;
        public UpdateOrder()
        {
            InitializeComponent();
            order = new Order();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ManageFood_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            LoadOrderHeader();
        }

        private void LoadOrderHeader()
        {
            DataTable dtbl = order.RetrieveOrderHeader("ToDo");
            gvOrderHeader.DataSource = dtbl;
            gvOrderHeader.Refresh();
        }

        private void LoadOrderDetail(int orderId)
        {
            if(orderId==0)
            {
                DataTable dtbl = new DataTable();
                gvOrderDetails.DataSource = dtbl;
                gvOrderDetails.Refresh();
            }
            else
            {
                DataTable dtbl = order.RetrieveOrderDetails(orderId);
                gvOrderDetails.DataSource = dtbl;
                gvOrderDetails.Refresh();
            }
        }

        private void gvOrderHeader_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null)
                return;
            if (dgv.CurrentRow.Selected)
            {
                int orderId = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                LoadOrderDetail(orderId);
                btnUpdate.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsAffected = order.UpdateOrder(int.Parse(gvOrderHeader.CurrentRow.Cells["Id"].Value.ToString()));
                btnUpdate.Enabled = false;
                LoadOrderHeader();
                LoadOrderDetail(0);
                MessageBox.Show("Sucessfully Updated", "Update", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
