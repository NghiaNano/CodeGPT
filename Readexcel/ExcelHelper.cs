using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ReadExcel
{
	public class ExcelHelper
	{
		private Application excelApp;
		public int StartColumn { get;set; }
		public int StartRow { get;set; }
		public ExcelHelper() 
		{
			excelApp = new Application();
		}
		public void CopyRange (string sourcepath, string despath, string sourceSheetName, string desSheetName, string sourceRangeAdress, string desRangeAdress)
		{
			//Source
			Workbook sourceWorkbook = excelApp.Workbooks.Open(sourcepath);
			Worksheet sourceworksheet = (Worksheet)sourceWorkbook.Sheets[sourceSheetName];
			Excel.Range sourceRange = sourceworksheet.Range[sourceRangeAdress];

			//Dest
			Workbook desWorkbook = excelApp.Workbooks.Open(despath);
			Worksheet desworksheet = (Worksheet)desWorkbook.Sheets[desSheetName];
			Excel.Range desRange = desworksheet.Range[desRangeAdress];

			//Coppy paste value

			sourceRange.Copy();
			desRange.PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);

			// Sao chép giá trị và định dạng màu sắc
			sourceRange.Copy();
			desRange.PasteSpecial(XlPasteType.xlPasteValues, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
			desRange.PasteSpecial(XlPasteType.xlPasteFormats, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);

			// Sao chép định dạng merge và split
			sourceRange.Copy();
			desRange.PasteSpecial(XlPasteType.xlPasteColumnWidths, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
			desRange.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);

			//Save
			desWorkbook.Save();

			//Close
			desWorkbook.Close();
			sourceWorkbook.Close();

		}
		public void CloseExcelApp()
		{
			excelApp.Quit();
		}

	}
}
