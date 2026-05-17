using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
namespace CreatePowerPoint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PowerPoint.Application application = new PowerPoint.Application();
            if (application == null)
                return;
            
        }
    }
}
