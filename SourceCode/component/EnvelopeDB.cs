using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace EFTN.component
{
    public class EnvelopeDB
    {
        public int InsertEnvelopSent
        (
           int envelopType,
		   string imDestRT,
           string imOriginRT,
           string envelopXML,
           int createdBy           
        )
        {            
            SqlConnection myConnection = new SqlConnection(EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]));            
            SqlCommand myCommand = new SqlCommand("EFT_InsertEnvelopSent", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterEnvelopID = new SqlParameter("@EnvelopID", SqlDbType.Int);
            parameterEnvelopID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterEnvelopID);

            SqlParameter parameterEnvelopType = new SqlParameter("@EnvelopType", SqlDbType.TinyInt);
            parameterEnvelopType.Value = envelopType;
            myCommand.Parameters.Add(parameterEnvelopType);

            SqlParameter parameterImDestRT = new SqlParameter("@ImDestRT", SqlDbType.NVarChar, 9);
            parameterImDestRT.Value = imDestRT;
            myCommand.Parameters.Add(parameterImDestRT);

            SqlParameter parameterImOriginRT = new SqlParameter("@ImOriginRT", SqlDbType.NVarChar, 9);
            parameterImOriginRT.Value = imOriginRT;
            myCommand.Parameters.Add(parameterImOriginRT);

            SqlParameter parameterEnvelopXML = new SqlParameter("@EnvelopXML", SqlDbType.NVarChar);
            parameterEnvelopXML.Value = envelopXML;
            myCommand.Parameters.Add(parameterEnvelopXML);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
            parameterCreatedBy.Value = createdBy;
            myCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterCreationDate = new SqlParameter("@CreationDate", SqlDbType.DateTime);
            parameterCreationDate.Value = DateTime.Now;
            myCommand.Parameters.Add(parameterCreationDate);


            myConnection.Open();

            myCommand.ExecuteNonQuery();

            myConnection.Close();

            return (int)parameterEnvelopID.Value;

        }

        
    }
}
