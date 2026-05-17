using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
namespace CreateAWordDocument
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Word.Application wordApp = new Word.Application();
            try
            {
                wordApp.Visible = false;
                Word.Document doc = wordApp.Documents.Add();
                Word.Paragraph para = doc.Paragraphs.Add();
                para.Range.Text = "hi hi";
                string filePath = "";
                doc.SaveAs2(filePath);
                doc.Close();
            }
            catch
            {

            }
        }
    }
}
