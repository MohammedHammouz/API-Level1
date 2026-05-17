using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace CreateAnExcelFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Excel.Application excelApp = new Excel.Application();
            try
            {
                if (excelApp == null)
                    return;
                excelApp.Visible = false;
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "MyWorkSheet";
                for(int i = 1; i <= 10; i++)
                {
                    worksheet.Cells[i, 1] = i;
                    worksheet.Cells[i, 2] = "Item" + i.ToString();
                }
                string filePath = "";
                workbook.SaveAs(filePath);
                workbook.Close(true);
            }
            catch
            {

            }
        }
    }
}
