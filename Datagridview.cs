using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}




		private void dataGridView2_CellValidated(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void dataGridView2_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{

		}

		private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			if (dataGridView2.CurrentCell.ColumnIndex == dataGridView2.Columns["Combobox"].Index && e.Control is ComboBox)
			{
				// Đăng ký sự kiện SelectedIndexChanged cho ComboBox
				ComboBox comboBox = e.Control as ComboBox;
				comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
				comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
			}
		}
		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Lấy giá trị của ComboBox khi người dùng chọn
			ComboBox comboBox = sender as ComboBox;
			string selectedValue = comboBox.SelectedItem.ToString();

			// Xử lý giá trị được chọn
			MessageBox.Show("Giá trị mới: " + selectedValue);
		}
	}
}
