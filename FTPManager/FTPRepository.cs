using FTPManager.Entities;
using FTPManager.Interfaces;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPManager
{
    public class FTPRepository : IFTPRepository
    {

        string host = "ip or dns";// target ip or ftp dns;
        int port = 22; // default port is 22
        string username = "user_name";
        string password = "password"; 

        public void GetFolderList(string? path)
        {
            using (var client = new SftpClient(host, port, username, password))
            {
                client.Connect(); 
                if (client.IsConnected)
                {
                    Console.WriteLine("SFTP sunucusuna bağlantı başarılı.");

                    var files = client.ListDirectory("/" + path);
                    foreach (var file in files)
                    {
                        Console.WriteLine(file.Name);
                    }

                    client.Disconnect();
                }
                else
                {
                    Console.WriteLine("SFTP sunucusuna bağlantı başarısız.");
                }
            }
        }

        public void DownloadFolders(List<DirectoryList> directoryLists)
        {
            using (var client = new SftpClient(host, port, username, password))
            {
                client.Connect();

                Console.WriteLine("SFTP sunucusuna bağlantı başarılı.");
                if (client.IsConnected)
                { 
                    client.Disconnect(); 
                    Console.WriteLine("SFTP sunucu bağlantısı kesildi.");
                }
                else
                {
                    Console.WriteLine("SFTP sunucusuna bağlantı başarısız.");
                }
            }
        }

        public void sendAndGetFolders(List<DirectoryList> sendDirectory, List<DirectoryList> getDirectory)
        {
            using (var client = new SftpClient(host, port, username, password))
            {
                client.Connect();
                Console.WriteLine("SFTP sunucusuna bağlantı başarılı.");

                if (client.IsConnected)
                {
                    foreach (var item in sendDirectory)
                    {
                        // string[] extensions = { ".xlsx", ".csv", ".xlsm" };
                        string[] localFiles = Directory.GetFiles(item.sourcePath);
                            //, "*.*", SearchOption.TopDirectoryOnly).Where(file => extensions.Contains(Path.GetExtension(file))).ToArray();

                        foreach (var localFile in localFiles)
                        {
                            string fileName = Path.GetFileName(localFile);
                            string remoteFilePath = item.targetPath + "/" + fileName; 
                            using (var fileStream = File.OpenRead(localFile))
                            {
                                client.UploadFile(fileStream, remoteFilePath);
                            }
                            Console.WriteLine("Dosya yüklendi: " + fileName);
                            File.Delete(localFile);
                            Console.WriteLine(" Dosya Lokal'den silindi: " + fileName);
                        }
                    }
                    foreach (var item in getDirectory)
                    {

                        var files = client.ListDirectory(item.sourcePath);
                        foreach (var file in files)
                        {
                            if (!file.IsDirectory)
                            {
                                string remoteFilePath = item.sourcePath + "/" + file.Name;
                                string localFilePath = Path.Combine(item.targetPath, file.Name);
                                using (var fileStream = File.OpenWrite(localFilePath))
                                {
                                    client.DownloadFile(remoteFilePath, fileStream);
                                }
                                Console.WriteLine("Dosya indirildi: " + file.Name);
                                client.DeleteFile(remoteFilePath);
                                Console.WriteLine("Dosya SFTP'den silindi: " + file.Name);
                            }
                        }
                    }

                    client.Disconnect(); Console.WriteLine("SFTP sunucu bağlantısı kesildi.");
                }
                else
                {
                    Console.WriteLine("SFTP sunucusuna bağlantı başarısız.");
                }
            }
        }



        public void DeleteFoldersFromFTP(string path)
        {

            using (var client = new SftpClient(host, port, username, password))
            {
                client.Connect();

                if (client.IsConnected)
                {
                    Console.WriteLine("SFTP sunucusuna bağlantı başarılı.");

                    var files = client.ListDirectory(path);
                    foreach (var file in files)
                    {
                        if (!file.IsDirectory && (file.Name.EndsWith(".csv") || file.Name.EndsWith(".xlsx")))
                        {
                            string remoteFilePath = path + "/" + file.Name;
                            client.DeleteFile(remoteFilePath);
                            Console.WriteLine("Dosya silindi: " + file.Name);
                        }
                    }

                    client.Disconnect();
                }
                else
                {
                    Console.WriteLine("SFTP sunucusuna bağlantı başarısız.");
                }
            }

        }

        public void DeleteFoldersFromLocal(string path)
        { 
            string[] files = Directory.GetFiles(path);   
            foreach (string file in files)
            {
                File.Delete(file); 
                Console.WriteLine("Dosya silindi: " + file);
            }

        }
    }
}
