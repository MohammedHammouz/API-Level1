using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace SendEmailViaOutlook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Outlook.Application outlookapplication = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookapplication.CreateItem(Outlook.OlItemType.olMobileItemSMS);
                mailItem.Subject = "Update Application";
                mailItem.To = "mohammedhammouz@yahoo.com";
                mailItem.Body = "Hello";
                mailItem.Importance = Outlook.OlImportance.olImportanceHigh;
                mailItem.Display(false);
                mailItem.Send();
                Console.WriteLine("Email sent successfully");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}
