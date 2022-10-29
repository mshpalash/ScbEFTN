using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace EFTN.BLL
{
    public class FileSystemDataSource
    {
        public DataTable GetDataSource(string fileType)
        {
            DataTable dt = new DataTable();
            DataColumn colFileName = new DataColumn("FileName");
            dt.Columns.Add(colFileName);

            DataColumn colFilePath = new DataColumn("FilePath");
            dt.Columns.Add(colFilePath);

            DataRow row = null;


            string path = ConfigurationManager.AppSettings[fileType];
            string[] transactionSentFiles = System.IO.Directory.GetFiles(path, "*.XML");
            foreach (string childFile in transactionSentFiles)
            {
                row = dt.NewRow();
                row["FilePath"] = childFile;
                row["FileName"] = new System.IO.FileInfo(childFile).Name;
                dt.Rows.Add(row);
            }

            return dt;
        }

        public DataTable GetDataSourceAllType(string fileType)
        {
            DataTable dt = new DataTable();
            DataColumn colFileName = new DataColumn("FileName");
            dt.Columns.Add(colFileName);

            DataColumn colFilePath = new DataColumn("FilePath");
            dt.Columns.Add(colFilePath);

            DataRow row = null;


            string path = ConfigurationManager.AppSettings[fileType];
            string[] transactionSentFiles = System.IO.Directory.GetFiles(path);
            foreach (string childFile in transactionSentFiles)
            {
                row = dt.NewRow();
                row["FilePath"] = childFile;
                row["FileName"] = new System.IO.FileInfo(childFile).Name;
                dt.Rows.Add(row);
            }

            return dt;
        }

        public DataTable GetACKDataSource(string fileType)
        {
            DataTable dt = new DataTable();
            DataColumn colFileName = new DataColumn("FileName");
            dt.Columns.Add(colFileName);

            DataColumn colFilePath = new DataColumn("FilePath");
            dt.Columns.Add(colFilePath);

            DataRow row = null;


            string path = ConfigurationManager.AppSettings[fileType];
            string[] transactionSentFiles = System.IO.Directory.GetFiles(path, "*.ACK");
            foreach (string childFile in transactionSentFiles)
            {
                row = dt.NewRow();
                row["FilePath"] = childFile;
                row["FileName"] = new System.IO.FileInfo(childFile).Name;
                dt.Rows.Add(row);
            }

            return dt;
        }
        public DataTable GetSecurityMatrix(string fileType)
        {
            DataTable dt = new DataTable();
            DataColumn colFileName = new DataColumn("FileName");
            dt.Columns.Add(colFileName);

            DataColumn colFilePath = new DataColumn("FilePath");
            dt.Columns.Add(colFilePath);

            DataRow row = null;


            string path = ConfigurationManager.AppSettings[fileType];
            string[] transactionSentFiles = System.IO.Directory.GetFiles(path, "*.csv");
            foreach (string childFile in transactionSentFiles)
            {
                row = dt.NewRow();
                row["FilePath"] = childFile;
                row["FileName"] = new System.IO.FileInfo(childFile).Name;
                dt.Rows.Add(row);
            }

            return dt;
        }

    }
}
