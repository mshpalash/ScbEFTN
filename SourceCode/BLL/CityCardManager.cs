
using System.Data;
using System;
using System.IO;
using System.Text;
using FloraSoft;
using System.Configuration;
using EFTN.Utility;
using System.Web;
using System.Diagnostics;
using System.Data.SqlClient;

/// <summary> 
/// Simple class for reading delimited text files 
/// </summary> 
///
public class CityCardManager
{
    private string ConnectionString;

    public void InsertCityCards(string SenderAccNo, decimal Amount)
    {
        this.ConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityCardsAccountDB cityDebitAccountDB = new CityCardsAccountDB();

        cityDebitAccountDB.InsertTempCityCards(this.ConnectionString, SenderAccNo, Amount);
    }

    public DataTable GetCityCardsTransactionSentForXML(string SettlementDate)
    {
        this.ConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityCardsAccountDB cityDebitAccountDB = new CityCardsAccountDB();

        return cityDebitAccountDB.Get_XMLFile_ForTransactionSent_CityCards(this.ConnectionString, SettlementDate);
    }

    public DataTable GetCityCardsCustomer()
    {
        this.ConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityCardsAccountDB cityDebitAccountDB = new CityCardsAccountDB();

        return cityDebitAccountDB.GetCityCardsCustomer(this.ConnectionString);
    }

    public void InsertCityCardsCustomer(
                                            string CustomerCardNo,
                                            string BankAccNo,
                                            string OtherBankAccNo,
                                            string OtherBankAccName,
                                            string RoutingNo)
    {
        this.ConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityCardsAccountDB cityDebitAccountDB = new CityCardsAccountDB();

        cityDebitAccountDB.InsertCityCardsCustomer(this.ConnectionString,
                                                            CustomerCardNo,
                                                            BankAccNo,
                                                            OtherBankAccNo,
                                                            OtherBankAccName,
                                                            RoutingNo);
    }


    public void UpdateCityCardsCustomer(int CustomerID,
                                            string CustomerCardNo,
                                            string BankAccNo,
                                            string OtherBankAccNo,
                                            string OtherBankAccName,
                                            string RoutingNo)
    {
        this.ConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityCardsAccountDB cityDebitAccountDB = new CityCardsAccountDB();

        cityDebitAccountDB.UpdateCityCardsCustomer(this.ConnectionString,
                                                            CustomerID,
                                                            CustomerCardNo,
                                                            BankAccNo,
                                                            OtherBankAccNo,
                                                            OtherBankAccName,
                                                            RoutingNo);
    }

    public void DeleteCityCardsCustomer(int CustomerID)
    {
        this.ConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityCardsAccountDB cityDebitAccountDB = new CityCardsAccountDB();

        cityDebitAccountDB.DeleteCityCardsCustomer(this.ConnectionString,
                                                            CustomerID);
    }

    public void TransferToTransactionSent_forCityCards_FromTempTable()
    {
        this.ConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityCardsAccountDB cityDebitAccountDB = new CityCardsAccountDB();

        cityDebitAccountDB.TransferToTransactionSent_forCityCards_FromTempTable(this.ConnectionString);
    }

    public DataTable GetTransactionSentBeforeTransferForCityCards()
    {
        this.ConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityCardsAccountDB cityDebitAccountDB = new CityCardsAccountDB();

        return cityDebitAccountDB.GetTransactionSentBeforeTransfer_forCityCards(this.ConnectionString);
    }

    public DataTable GetCityCardsFailedTransaction(string EntryDate)
    {
        this.ConnectionString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityCardsAccountDB cityDebitAccountDB = new CityCardsAccountDB();

        return cityDebitAccountDB.GetFailedCardsByEntryDate(this.ConnectionString, EntryDate);
    }
    
}