// See https://aka.ms/new-console-template for more information


using FTPManager;
using FTPManager.Entities;
using FTPManager.Operations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Renci.SshNet;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Topshelf;

internal class Program
{
    private static void Main(string[] args)
    {
        //Operation.sendAndGet();

        var hostRun = HostFactory.Run(
               x =>
               {
                   x.Service<FTPWindowsService>(
                        service =>
                        {
                            service.ConstructUsing(() => new FTPWindowsService());
                            service.WhenStarted(s => s.OnStart());
                            service.WhenStopped(s => s.OnStop());
                            service.WhenPaused(s => s.OnPause());
                            service.WhenContinued(s => s.OnContinue());
                        });

                   x.RunAsLocalSystem();
                   x.StartAutomatically();
                   x.SetServiceName("Servis Adı");
                   x.SetDisplayName("Servis Adı");
                   x.SetDescription(@"Servis Açıklaması");
               });

        var exitCode = (int)Convert.ChangeType(hostRun, hostRun.GetTypeCode());
        Environment.ExitCode = exitCode;

    }





}


