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
    public class DepartmentsDB
    {
        public void InsertDepartment(  
                                        string DepartmentName,
                                        string ParkingAccountIn,
                                        string ParkingAccountOut,
                                        int CreatedBy
                                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_InsertDepartment", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDepartmentName = new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 50);
            parameterDepartmentName.Value = DepartmentName;
            myCommand.Parameters.Add(parameterDepartmentName);

            SqlParameter parameterParkingAccountIn = new SqlParameter("@ParkingAccountIn", SqlDbType.NVarChar, 17);
            parameterParkingAccountIn.Value = ParkingAccountIn;
            myCommand.Parameters.Add(parameterParkingAccountIn);

            SqlParameter parameterParkingAccountOut = new SqlParameter("@ParkingAccountOut", SqlDbType.NVarChar, 17);
            parameterParkingAccountOut.Value = ParkingAccountOut;
            myCommand.Parameters.Add(parameterParkingAccountOut);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = CreatedBy;
            myCommand.Parameters.Add(parameterCreatedBy);


            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();
        }
        
        public DataTable GetDepartments()
        {

            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetDepartments", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 600;


            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }

        public void UpdateDepartment(
                                        string DepartmentName,
                                        string ParkingAccountIn,
                                        string ParkingAccountOut,
                                        int DepartmentID
                                    )
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlCommand myCommand = new SqlCommand("EFT_UpdateDepartment", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDepartmentName = new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 50);
            parameterDepartmentName.Value = DepartmentName;
            myCommand.Parameters.Add(parameterDepartmentName);

            SqlParameter parameterParkingAccountIn = new SqlParameter("@ParkingAccountIn", SqlDbType.NVarChar, 17);
            parameterParkingAccountIn.Value = ParkingAccountIn;
            myCommand.Parameters.Add(parameterParkingAccountIn);

            SqlParameter parameterParkingAccountOut = new SqlParameter("@ParkingAccountOut", SqlDbType.NVarChar, 17);
            parameterParkingAccountOut.Value = ParkingAccountOut;
            myCommand.Parameters.Add(parameterParkingAccountOut);

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int);
            parameterDepartmentID.Value = DepartmentID;
            myCommand.Parameters.Add(parameterDepartmentID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
            myCommand.Dispose();
            myConnection.Dispose();
        }

        //public void DeleteDepartments(int DepartmentID)
        //{
        //    SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
        //    SqlCommand myCommand = new SqlCommand("EFT_DeleteDepartments", myConnection);
        //    myCommand.CommandType = CommandType.StoredProcedure;

        //    SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.Int, 4);
        //    parameterDepartmentID.Value = DepartmentID;
        //    myCommand.Parameters.Add(parameterDepartmentID);

            

        //    myConnection.Open();
        //    myCommand.ExecuteNonQuery();
        //    myConnection.Close();
        //    myCommand.Dispose();
        //    myConnection.Dispose();

        //}

        public DataTable EFT_GetDepartmentDetailsByDepartmentID(int DepartmentID)
        {
            // Must enter your connection string
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));
            // Must write your procedure name 
            SqlDataAdapter myAdapter = new SqlDataAdapter("EFT_GetDepartmentDetailsByDepartmentID", myConnection);
            myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            myAdapter.SelectCommand.CommandTimeout = 3600;

            SqlParameter parameterDepartmentID = new SqlParameter("@DepartmentID", SqlDbType.NChar);
            parameterDepartmentID.Value = DepartmentID;
            myAdapter.SelectCommand.Parameters.Add(parameterDepartmentID);

            DataTable myDT = new DataTable();
            myConnection.Open();
            myAdapter.Fill(myDT);
            myConnection.Close();
            return myDT;
        }


    }
}
