using FTPManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPManager.Operations
{
    internal class Operation
    {

        public static void sendAndGet()
        {
            FTPRepository repository = new FTPRepository();

            string baseIncomingPath = "Gelen Dosyaların kaydedileceği Klasör yolu";          // "C:\\FtpFolder\\incoming\\";
            string baseOutGoingPath = "Gönderilecek Dosyaların bulunduğu Klasör yolu";       // "C:\\FtpFolder\\outgoing\\";

            List<DirectoryList> sendDirectory = new List<DirectoryList>(); 
            List<DirectoryList> getDirectory = new List<DirectoryList>();


            getDirectory.Add(new DirectoryList() { sourcePath = "Alınması gereken dosya yolu"/*"/outgoing/first_get_folder"*/, targetPath = baseIncomingPath + "hedef klasör" });
            getDirectory.Add(new DirectoryList() { sourcePath = "Alınması gereken dosya yolu2"/*"/outgoing/second_get_folder"*/, targetPath = baseIncomingPath + "hedef klasör" });


            sendDirectory.Add(new DirectoryList() { sourcePath = baseOutGoingPath + "Gönderilmesi gereken dosya yolu", targetPath = "hedef klasör" });
            sendDirectory.Add(new DirectoryList() { sourcePath = baseOutGoingPath + "Gönderilmesi gereken dosya yolu2", targetPath = "hedef klasör2" }); 


            repository.sendAndGetFolders(sendDirectory, getDirectory);
        }
       
    }
}
