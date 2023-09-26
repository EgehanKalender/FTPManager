using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPManager.Operations
{
    internal class FTPWindowsService
    {
        private bool isStopRequestReceived;

        public void OnStart()
        {
            string message = $"Servis başladı.";
            FileLogger.Instance.WriteLog(message);

            Task.Run(() => InitService());
        }

        public void OnStop()
        {
            isStopRequestReceived = true;
            string message = $"Servis durdu.";
            FileLogger.Instance.WriteLog(message);
        }

        public void OnPause()
        {
            isStopRequestReceived = true;
            string message = $"Servis duraklatıldı.";
            FileLogger.Instance.WriteLog(message);
        }

        public void OnContinue()
        {
            isStopRequestReceived = false;
            string message = $"Servis devam ettirildi.";
            FileLogger.Instance.WriteLog(message);
        }

        private void InitService()
        {
            while (!isStopRequestReceived)
            {
                string message = "Servis çalışıyor.";
                Operation.sendAndGet();
                FileLogger.Instance.WriteLog(message);
                Thread.Sleep(30000);
            }
        }
    }
}
