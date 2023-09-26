using FTPManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPManager.Interfaces
{
    public interface IFTPRepository
    {

        // Path içerisindeki herşeyi listeler
        void GetFolderList(string? path);

        // Path içerisindeki herşeyi (.xlsx , .csv), LocalPath içerisine indirir.
        void DownloadFolders(List<DirectoryList> directoryLists);

        // LocalPath içerisindeki herşeyi, remoteDirectory içerisine (.xlsx , .csv) yükler.
        void sendAndGetFolders(List<DirectoryList> sendDirectory, List<DirectoryList> getDirectory); 
        // Path içerisindeki herşeyi siler.
        void DeleteFoldersFromFTP(string path);  

        // Path içerisindeki herşeyi siler.
        void DeleteFoldersFromLocal(string path);
    }
}
