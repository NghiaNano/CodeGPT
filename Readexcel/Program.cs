using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;


namespace ReadExcel
{
	class Program
	{
		static void Main(string[] args)
		{
			string sourceFilePath = "C:\\Users\\Nghia Nano\\Desktop\\sourceTest.xlsx";
			//string destinationFilePath = "C:\\Users\\Nghia Nano\\Desktop\\desTest.xlsx";

			List<string> dest = new List<string>() { "desTest1.xlsx", "desTest2.xlsx" };

			List<string[]> copyRange = new List<string[]>()
			{
				new string[2]{"ád","ads"}
			};

			foreach(string destItem in dest)
			{
				ExcelHelper excelHelper = new ExcelHelper();
				string destinationFilePath = $"C:\\Users\\Nghia Nano\\Desktop\\{destItem}";

				//CheckTable
				excelHelper.CopyRange(sourceFilePath, destinationFilePath, "Sheet1", "Sheet1", "B5", "B5");

				//Parameter
				excelHelper.CopyRange(sourceFilePath, destinationFilePath, "Sheet1", "Sheet1", "B12:B14", "B12");

				excelHelper.CloseExcelApp();
			}


            Console.WriteLine("Done");
        }
	}
}

