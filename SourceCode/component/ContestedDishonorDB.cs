using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class ContestedDishonorDB
    {
        //public SqlDataReader GetContestedDishonor()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_ReceivedDishonor_Contested_ForChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public DataTable GetContestedDishonor()
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_ReceivedDishonor_Contested_ForChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateSentContestedStatus(int StatusID,
                                              Guid ContestedID,
                                              Guid DishonorID, int ApprovedBy)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must check your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_Update_SentContested_Status_ByChecker", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterStatusID = new SqlParameter("@StatusID", SqlDbType.Int);
            parameterStatusID.Value = StatusID;
            myCommand.Parameters.Add(parameterStatusID);

            SqlParameter parameterContestedID = new SqlParameter("@ContestedID", SqlDbType.UniqueIdentifier);
            parameterContestedID.Value = ContestedID;
            myCommand.Parameters.Add(parameterContestedID);

            SqlParameter parameterDishonorID = new SqlParameter("@DishonorID", SqlDbType.UniqueIdentifier);
            parameterDishonorID.Value = DishonorID;
            myCommand.Parameters.Add(parameterDishonorID);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            myConnection.Open();
            myCommand.ExecuteReader();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        //public SqlDataReader GetRejectedContestedWhichWereApprovedByMaker()
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    // Must check your procedure name 
        //    SqlCommand myCommand = new SqlCommand("EFT_GetContestedSent_ByChecker", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    myConnection.Open();
        //    SqlDataReader dr = myCommand.ExecuteReader();
        //    return dr;
        //}

        public DataTable GetRejectedContestedWhichWereApprovedByMaker(int BranchID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetContestedSent_ByChecker", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterBranchID = new SqlParameter("@BranchID", SqlDbType.Int);
            parameterBranchID.Value = BranchID;
            myAdapter.SelectCommand.Parameters.Add(parameterBranchID);


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public Guid InsertReceivedContested(string ContestedDishonoredCode,
                                            string DishonorTracenumber,
                                            DateTime SettlementJDate
                                           )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertReceivedContested", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterContestedID = new SqlParameter("@ContestedID", SqlDbType.UniqueIdentifier);
            parameterContestedID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterContestedID);

            SqlParameter parameterContestedDishonoredCode = new SqlParameter("@ContestedDishonoredCode", SqlDbType.NVarChar, 3);
            parameterContestedDishonoredCode.Value = ContestedDishonoredCode;
            myCommand.Parameters.Add(parameterContestedDishonoredCode);

            SqlParameter parameterDishonorTracenumber = new SqlParameter("@DishonorTracenumber", SqlDbType.NVarChar, 15);
            parameterDishonorTracenumber.Value = DishonorTracenumber;
            myCommand.Parameters.Add(parameterDishonorTracenumber);

            SqlParameter parameterSettlementJDate = new SqlParameter("@SettlementJDate", SqlDbType.DateTime);
            parameterSettlementJDate.Value = SettlementJDate;
            myCommand.Parameters.Add(parameterSettlementJDate);

            myConnection.Open();
            myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            myConnection.Close();

            return (Guid)parameterContestedID.Value;
        }
    }
}
