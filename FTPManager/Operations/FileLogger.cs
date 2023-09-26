using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPManager.Operations
{
    public class FileLogger
    {
        private static FileLogger _instance;
        public static FileLogger Instance => _instance ?? (_instance = new FileLogger());

        public void WriteLog(string message)
        {

            string logFilePath = "C:\\log\\" + $@"\{DateTime.Now:yyyyMMdd}-Log.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

            string formattedMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n";

            File.AppendAllText(logFilePath, formattedMessage);
        }
    }
}
