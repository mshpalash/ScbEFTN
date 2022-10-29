using System.Data;

namespace EFTN.BLL
{
    public class DataManager
    {
        /// <summary>
        /// It Returns the data table considering the DataStartFrom as the first row of the data set
        /// and DataStartFrom value must be greater than 1
        /// </summary>
        /// <param name="oDataSet"></param>
        /// <param name="DataStartFrom"></param>
        /// <returns></returns>
        public DataTable CopyDataFromDataSetToDataTable(DataSet oDataSet, int DataStartFrom)
        {
            int columnCounter = oDataSet.Tables[0].Columns.Count;
            int rowCounter = oDataSet.Tables[0].Rows.Count;

            DataTable myTable = new DataTable();

            for (int i = 0; i < columnCounter; i++)
            {
                myTable.Columns.Add(oDataSet.Tables[0].Rows[DataStartFrom - 2][i].ToString());
            }

            for (int i = DataStartFrom - 1; i < rowCounter; i++)
            {
                DataRow dr = myTable.NewRow();

                for (int j = 0; j < columnCounter; j++)
                {
                    dr[j] = oDataSet.Tables[0].Rows[i][j].ToString();
                }
                myTable.Rows.Add(dr);
            }

            return myTable;
        }
    }
}