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
		int index;
		public Form1()
		{
			InitializeComponent();
		}


		private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			DataGridViewComboBoxColumn combobox = dataGridView2.Columns["TestCombobox"] as DataGridViewComboBoxColumn;
			index = dataGridView2.CurrentCell.RowIndex;
			if (dataGridView2.CurrentCell.ColumnIndex == dataGridView2.Columns["TestCombobox"].Index && e.Control is ComboBox)
			{
				// Đăng ký sự kiện SelectedIndexChanged cho ComboBox
				ComboBox comboBox = e.Control as ComboBox;
				comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
				comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
			}
		}
		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Lấy giá trị của ComboBox khi người dùng chọnn 
			ComboBox comboBox = sender as ComboBox;
			string selectedValue = comboBox.SelectedItem.ToString();

			label1.Text = "" + index;
			
			Task.Run(async () =>
			{
				await Task.Delay(1000);
				if (index == 2)
					dataGridView2.BeginInvoke(new Action(() =>
					{
						dataGridView2.Rows.RemoveAt(index);
					}));

			});
			// Xử lý giá trị được chọn
			//MessageBox.Show("Giá trị mới: " + selectedValue);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DataGridViewComboBoxColumn combobox = dataGridView2.Columns["TestCombobox"] as DataGridViewComboBoxColumn;

			DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dataGridView2.Rows[0].Cells[0];

			if (cell != null)
			{
				DataGridViewComboBoxCell.ObjectCollection collection = cell.Items as DataGridViewComboBoxCell.ObjectCollection;
			}

			DataGridViewRowCollection row = dataGridView2.Rows;
			List<string> values = new List<string>();
			foreach(DataGridViewRow rowItem in dataGridView2.Rows)
			{
				DataGridViewComboBoxCell cells = rowItem.Cells[combobox.Index] as DataGridViewComboBoxCell;
				if(cells != null)
				{
					values.Add(cells.Value?.ToString());
				}
			}
			values.ForEach(value => { Console.WriteLine(value); });
			dataGridView2.Rows.RemoveAt(index);
			//Hi


		}
	}
}
