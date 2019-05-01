using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTwo.Application.Data
{
    public class ErrorLogger
    {
        static string errorLog = "";
        public static void logError(string errorMessage,Exception errorInnerException,string stackTrace)
        {
            errorLog += errorMessage + " Error Occurred: "+ DateTime.Now + " \n";
            if (errorInnerException != null)
            {
                errorLog += errorInnerException.Message + "\n";
               
            }
            errorLog += "No Inner Exception";
           
           // WriteToFile(errorLog);

        }

        public static void WriteToFile(string errorInfo)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\Raluchukwu\source\repos\BankTwo\BankTwo.Application.Data\ErrorLog.txt", true))
            {
                file.WriteLine(errorInfo);
            }
        }

    }
}
