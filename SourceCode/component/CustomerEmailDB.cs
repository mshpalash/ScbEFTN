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


namespace FloraSoft
{
    public class CustomerEmailDB
    {
        public void InsertCustEmail(string ACCNo, string ACCHolderName, string CusEmail1, string CusEmail2,string CusEmail3, string CusEmail4, string CusEmail5, string ContactNumber1, string ContactNumber2,int UptoEbbs, string SusAccNo, int CusAdvice, int CreatedBy, string MISGenerationTime)
            {

            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_InsertSCBEmailCus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterACCNo = new SqlParameter("@ACCNo", SqlDbType.NVarChar, 50);
            parameterACCNo.Value = ACCNo;
            myCommand.Parameters.Add(parameterACCNo);

            SqlParameter parameterACCHolderName = new SqlParameter("@ACCHolderName", SqlDbType.NVarChar, 50);
            parameterACCHolderName.Value = ACCHolderName;
            myCommand.Parameters.Add(parameterACCHolderName);

            SqlParameter parameterCusEmail1 = new SqlParameter("@CusEmail1", SqlDbType.NVarChar, 50);
            parameterCusEmail1.Value = CusEmail1;
            myCommand.Parameters.Add(parameterCusEmail1);

            SqlParameter parameterCusEmail2 = new SqlParameter("@CusEmail2", SqlDbType.NVarChar, 50);
            parameterCusEmail2.Value = CusEmail2;
            myCommand.Parameters.Add(parameterCusEmail2);

            SqlParameter parameterCusEmail3 = new SqlParameter("@CusEmail3", SqlDbType.NVarChar, 50);
            parameterCusEmail3.Value = CusEmail3;
            myCommand.Parameters.Add(parameterCusEmail3);

            SqlParameter parameterCusEmail4 = new SqlParameter("@CusEmail4", SqlDbType.NVarChar, 50);
            parameterCusEmail4.Value = CusEmail4;
            myCommand.Parameters.Add(parameterCusEmail4);

            SqlParameter parameterCusEmail5 = new SqlParameter("@CusEmail5", SqlDbType.NVarChar, 50);
            parameterCusEmail5.Value = CusEmail5;
            myCommand.Parameters.Add(parameterCusEmail5);

            SqlParameter parameterContactNumber1 = new SqlParameter("@ContactNumber1", SqlDbType.NVarChar, 50);
            parameterContactNumber1.Value = ContactNumber1;
            myCommand.Parameters.Add(parameterContactNumber1);

            SqlParameter parameterContactNumber2 = new SqlParameter("@ContactNumber2", SqlDbType.NVarChar, 50);
            parameterContactNumber2.Value = ContactNumber2;
            myCommand.Parameters.Add(parameterContactNumber2);

            SqlParameter parameterUptoEbbs = new SqlParameter("@UptoEbbs", SqlDbType.Int);
            parameterUptoEbbs.Value = UptoEbbs;
            myCommand.Parameters.Add(parameterUptoEbbs);

            SqlParameter parameterSusAccNo = new SqlParameter("@SusAccNo", SqlDbType.NVarChar, 50);
            parameterSusAccNo.Value = SusAccNo;
            myCommand.Parameters.Add(parameterSusAccNo);

            SqlParameter parameterCusAdvice = new SqlParameter("@CusAdvice", SqlDbType.Int);
            parameterCusAdvice.Value = CusAdvice;
            myCommand.Parameters.Add(parameterCusAdvice);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterMISGenerationTime = new SqlParameter("@MISGenerationTime", SqlDbType.VarChar, 150);
            parameterMISGenerationTime.Value = MISGenerationTime;
            myCommand.Parameters.Add(parameterMISGenerationTime);


            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public DataTable GetCustEmail()
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSCBEmailCusDetails", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public DataTable GetCustEmailByCustomerId(int customerID)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetSCBEmailCusDetailsByID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;

            SqlParameter parameterCustomerID = new SqlParameter("@CustomerID", SqlDbType.Int);
            parameterCustomerID.Value = customerID;
            myAdapter.SelectCommand.Parameters.Add(parameterCustomerID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateCustEmail(int CustomerID, string ACCNo, string ACCHolderName, string CusEmail1, string CusEmail2,string CusEmail3, string CusEmail4, string CusEmail5, string ContactNumber1, string ContactNumber2, string SusAccNo, int CreatedBy, string MISGenerationTime)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"])); 
            SqlCommand myCommand = new SqlCommand("EFT_Update_SCBEmailCus", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterCustomerID = new SqlParameter("@CustomerID", SqlDbType.Int);
            parameterCustomerID.Value = CustomerID;
            myCommand.Parameters.Add(parameterCustomerID);

            SqlParameter parameterACCNo = new SqlParameter("@ACCNo", SqlDbType.NVarChar, 50);
            parameterACCNo.Value = ACCNo;
            myCommand.Parameters.Add(parameterACCNo);

            SqlParameter parameterACCHolderName = new SqlParameter("@ACCHolderName", SqlDbType.NVarChar, 50);
            parameterACCHolderName.Value = ACCHolderName;
            myCommand.Parameters.Add(parameterACCHolderName);

            SqlParameter parameterCusEmail1 = new SqlParameter("@CusEmail1", SqlDbType.NVarChar, 50);
            parameterCusEmail1.Value = CusEmail1;
            myCommand.Parameters.Add(parameterCusEmail1);

            SqlParameter parameterCusEmail2 = new SqlParameter("@CusEmail2", SqlDbType.NVarChar, 50);
            parameterCusEmail2.Value = CusEmail2;
            myCommand.Parameters.Add(parameterCusEmail2);

            SqlParameter parameterCusEmail3 = new SqlParameter("@CusEmail3", SqlDbType.NVarChar, 50);
            parameterCusEmail3.Value = CusEmail3;
            myCommand.Parameters.Add(parameterCusEmail3);

            SqlParameter parameterCusEmail4 = new SqlParameter("@CusEmail4", SqlDbType.NVarChar, 50);
            parameterCusEmail4.Value = CusEmail4;
            myCommand.Parameters.Add(parameterCusEmail4);

            SqlParameter parameterCusEmail5 = new SqlParameter("@CusEmail5", SqlDbType.NVarChar, 50);
            parameterCusEmail5.Value = CusEmail5;
            myCommand.Parameters.Add(parameterCusEmail5);

            SqlParameter parameterContactNumber1 = new SqlParameter("@ContactNumber1", SqlDbType.NVarChar, 50);
            parameterContactNumber1.Value = ContactNumber1;
            myCommand.Parameters.Add(parameterContactNumber1);

            SqlParameter parameterContactNumber2 = new SqlParameter("@ContactNumber2", SqlDbType.NVarChar, 50);
            parameterContactNumber2.Value = ContactNumber2;
            myCommand.Parameters.Add(parameterContactNumber2);

            SqlParameter parameterSusAccNo = new SqlParameter("@SusAccNo", SqlDbType.NVarChar, 50);
            parameterSusAccNo.Value = SusAccNo;
            myCommand.Parameters.Add(parameterSusAccNo);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterMISGenerationTime = new SqlParameter("@MISGenerationTime", SqlDbType.VarChar, 150);
            parameterMISGenerationTime.Value = MISGenerationTime;
            myCommand.Parameters.Add(parameterMISGenerationTime);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        public void UpdateCustEmailChecker(int CustomerID, string ActiveStatus, int ApprovedBy)
        {
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            SqlCommand myCommand = new SqlCommand("EFT_Update_SCBEmailCus_Status", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterCustomerID = new SqlParameter("@CustomerID", SqlDbType.Int);
            parameterCustomerID.Value = CustomerID;
            myCommand.Parameters.Add(parameterCustomerID);

            SqlParameter parameterActiveStatus = new SqlParameter("@ActiveStatus", SqlDbType.NVarChar, 50);
            parameterActiveStatus.Value = ActiveStatus;
            myCommand.Parameters.Add(parameterActiveStatus);

            SqlParameter parameterApprovedBy = new SqlParameter("@ApprovedBy", SqlDbType.Int);
            parameterApprovedBy.Value = ApprovedBy;
            myCommand.Parameters.Add(parameterApprovedBy);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();
 
        }

    }
}