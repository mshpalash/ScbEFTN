using System;
using System.Data;
using System.Configuration;
using System.Xml;
using EFTN.Utility;
using System.Globalization;
using EFTN.component;

namespace EFTN.BLL
{
    public class TransactionAcknowledgement
    {
        public void SaveDataToTransactionSent(string fileName)
        {
            AcknowledgementDB acknowledgementDB = new AcknowledgementDB();
            acknowledgementDB.ImportAckTransactionSent(fileName);
        }

        public void SaveDataToTransactionSentAcknowledgement(string fileName, int session, DateTime settlementDate, string connectionString)
        {
            TransactionXMLFileNameDB txnXMLNameDB = new TransactionXMLFileNameDB();
            txnXMLNameDB.UpdateTransactionSentXMLFilesAcknowledgement(fileName, session, settlementDate, connectionString);
        }

        public void SaveDataToReturnSent(string fileName, int session, DateTime settlementDate)
        {
            AcknowledgementDB acknowledgementDB = new AcknowledgementDB();
            acknowledgementDB.ImportAckReturnSent(fileName, session, settlementDate);
        }
        //Added New on BACH II upgrade on April 2018
        public void SaveDataToDishonorSent(string fileName, int session)
        {
            AcknowledgementDB acknowledgementDB = new AcknowledgementDB();
            //acknowledgementDB.ImportAckDishonorSent(fileName, session, settlementJDate);
            acknowledgementDB.ImportAckDishonorSent(fileName, session);
        }
        public void SaveDataToNOCSent(string fileName, int session)
        {
            AcknowledgementDB acknowledgementDB = new AcknowledgementDB();
            acknowledgementDB.ImportAckNOCSent(fileName, session);
        }
    }
}
