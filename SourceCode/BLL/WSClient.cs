using System;
using System.Data;
using System.IO;
using EFTN.PBMServer;

namespace FloraSoft
{
    /// <summary>
    /// Web Service Client
    /// </summary>
    /// <remarks>
    /// <author>
    /// Sayeed Safayet Alam
    /// </author>
    /// <date>
    /// December 2010
    /// </date>
    /// </remarks>
    public class WSClient
    {
       

        #region Connection Related
        public bool IsConnected()
        {
            try
            {
                
                EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
                return pbm.AreYouUp();
            }
            catch 
            {
                return false;
            }
        }

        #endregion

        #region File Transfer
        public DataSet GetFileList(string sourcePath, string searchString)
        {
            if (IsConnected())
            {
                EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
                return pbm.GetFileList(sourcePath, searchString);
            }
            else
            {
                return null;
            }
        }

        public void GetSingleFile(string sourcePath, string destinationPath)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            Byte[] mybytearray = pbm.GetSingleFileData(sourcePath,true);
            if (mybytearray != null)
            {
                MemoryStream objstreaminput = new MemoryStream();
                FileStream objfilestream = new FileStream(destinationPath, FileMode.Create, FileAccess.ReadWrite);
                objfilestream.Write(mybytearray, 0, mybytearray.Length);
                objfilestream.Close();
            }
        }

        public void GetFiles(string sourceFolderPath,string destinationFolderPath, string searchString)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            string[] allFiles = pbm.GetFilePaths(sourceFolderPath, searchString, false);
            string destinationFolder = destinationFolderPath;
            FileInfo fi = new FileInfo(destinationFolder);
            //////////////////Whether given destination folder path is given as a path to a file
            if (fi.Exists)
            {
                destinationFolder = fi.DirectoryName;
            }

            foreach (string file in allFiles)
            {
                fi = new FileInfo(file);
                Byte[] mybytearray = pbm.GetSingleFileData(fi.FullName, false);
                if (mybytearray != null)
                {
                    MemoryStream objstreaminput = new MemoryStream();
                    FileStream objfilestream = new FileStream(destinationFolder + "\\" + fi.Name, FileMode.Create, FileAccess.ReadWrite);

                    objfilestream.Write(mybytearray, 0, mybytearray.Length);
                    objfilestream.Close();
                }
                
            }
        }

        public void GetFilesAndBackup(string sourceFolderPath, string destinationFolderPath, string searchString)
        {
            if (IsConnected())
            {
                EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
                string[] allFiles = pbm.GetFilePaths(sourceFolderPath, searchString, false);
                string destinationFolder = destinationFolderPath;
                string backupDestinationFolder = sourceFolderPath + "\\bak";
                FileInfo fi = new FileInfo(destinationFolder);
                //////////////////Whether given destination folder path is given as a path to a file
                if (fi.Exists)
                {
                    destinationFolder = fi.DirectoryName;
                }

                foreach (string file in allFiles)
                {
                    fi = new FileInfo(file);
                    Byte[] mybytearray = pbm.GetSingleFileData(fi.FullName, false);
                    if (mybytearray != null)
                    {
                        MemoryStream objstreaminput = new MemoryStream();
                        FileStream objfilestream = new FileStream(destinationFolder + "\\" + fi.Name, FileMode.Create, FileAccess.ReadWrite);

                        objfilestream.Write(mybytearray, 0, mybytearray.Length);
                        objfilestream.Close();
                        pbm.CopyFile(file, backupDestinationFolder + "\\" + fi.Name);
                        pbm.DeleteFile(file);
                    }

                }
            }
        }
        public void SendSingleFile(string sourcePath, string destinationPath)
        {
            if (File.Exists(sourcePath))
            {
                EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();

                FileStream objfilestream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
                int len = (int)objfilestream.Length;
                Byte[] mybytearray = new Byte[len];
                objfilestream.Read(mybytearray, 0, len);

                pbm.SaveDocument(destinationPath, mybytearray);
                objfilestream.Close();
            }
        }

        public void SendSingleFileAndBackUp(string sourcePath, string destinationPath)
        {
            if (IsConnected())
            {
                FileInfo fi = new FileInfo(sourcePath);
                string dir = fi.DirectoryName;

                if (!Directory.Exists(dir + "\\bak"))
                {
                    Directory.CreateDirectory(dir + "\\bak");
                }
                SendSingleFile(sourcePath, destinationPath);
                File.Copy(sourcePath, dir + "\\bak\\" + fi.Name);
                File.Delete(sourcePath);
            }

        }
        public void SendFiles(string sourceFolderPath, string destinationFolderPath, string searchString)
        {

            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();

            string sourceFolder = sourceFolderPath;
            FileInfo fi = new FileInfo(sourceFolder);
            //////////////////Whether given source folder path is given as a path to a file
            if (fi.Exists)
            {
                sourceFolder = fi.DirectoryName;
            }
            string[] allFiles = Directory.GetFiles(sourceFolder, searchString);

            foreach (string file in allFiles)
            {
                fi = new FileInfo(file);
                SendSingleFile(file, destinationFolderPath + "\\" + fi.Name);
            }

        }
        #endregion

        #region PBM File System 
        public void CreatePBMFolder(string path)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            pbm.CreateFolder(path);
        }

        public void CopyPBMFile(string sourceFilePath, string destinationFilePath)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            pbm.CopyFile(sourceFilePath, destinationFilePath);
        }

        public void DeletePBMFile(string filePath)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            pbm.DeleteFile(filePath);
        }

        public void MovePBMFile(string sourceFilePath, string destinationFilePath)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            
            pbm.CopyFile(sourceFilePath, destinationFilePath);

            pbm.DeleteFile(sourceFilePath);
        }

        public void CopyPBMFolderFiles(string sourceFolderPath, string destinationFolderPath, string searchString)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            pbm.CopyFolderFiles(sourceFolderPath, destinationFolderPath, searchString);            
        }

        public void DeleteFolderFiles(string filepath, string searchstring)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            pbm.DeleteFolderFiles(filepath, searchstring);
        }

        public void MovePBMFolderFiles(string sourceFolderPath, string destinationFolderPath, string searchString)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            pbm.CopyFolderFiles(sourceFolderPath, destinationFolderPath, searchString);

            pbm.DeleteFolderFiles(sourceFolderPath, searchString);
        }
        #endregion

        #region Others 
        public DataSet GetPBMDetailData(string SettlementDate)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            return pbm.GetPBMDetailData(SettlementDate);
        }

        public DataSet GetPBMErrorFileData()
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();
            return pbm.GetPBMErrorFileData();
        }
        #endregion

        #region Database Related

        public DataTable GetPBMDBData(string command)
        {
            EFTN.PBMServer.Service1 pbm = new EFTN.PBMServer.Service1();

            return pbm.GetDBData(command).Tables[0];

        }
        #endregion
    }
}
