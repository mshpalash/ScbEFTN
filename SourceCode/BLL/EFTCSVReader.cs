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
    public class EFTCSVReader
    {
        public DataTable ReadCSV(string file)
        {
            DataTable dtCSV = new DataTable();
            using (CsvReader reader = new CsvReader(file))
            {
                int i = 0;
                foreach (string[] values in reader.RowEnumerator)
                {
                    if (i == 0)
                    {
                        foreach (string colName in values)
                            dtCSV.Columns.Add(colName);
                        i++;
                    }
                    else
                    {
                        dtCSV.Rows.Add(values);
                    }
                }
            }
            return dtCSV;
        }

        public DataTable ReadCSVNewFormat(string file)
        {
            DataTable dtCSV = new DataTable();
            using (CsvReader reader = new CsvReader(file))
            {
                int i = 6;
                foreach (string[] values in reader.RowEnumerator)
                {
                    string oldFormatValue = values[0];
                    if (reader.RowIndex == 1 && oldFormatValue == "Customer ID")
                    {
                        dtCSV.Columns.Add("STS");    
                        return dtCSV; 
                    }
                    else
                    {
                        if (reader.RowIndex >= 6)
                        {
                            if (i == 6)
                            {
                                foreach (string colName in values)
                                    dtCSV.Columns.Add(colName);
                                i++;
                            }
                            else
                            {
                                dtCSV.Rows.Add(values);
                            }
                        }
                    }
                }
            }
            return dtCSV;
        }
    }
}
