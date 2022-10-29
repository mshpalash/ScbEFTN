
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
public class CityDebitAccountManager
{
    private string ConnectoinString;

    public DataTable GetCityAccountStatus()
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityDebitAccountDB cityDebitAccountDB = new CityDebitAccountDB();

        DataTable dtCityDebitAccountStatus = cityDebitAccountDB.GetCityDebitAccount(this.ConnectoinString);

        return dtCityDebitAccountStatus;
    }

    public int InsertCityDebitAccount(string AccountNo,
                                    string OtherBankAcNo,
                                    string RoutingNumber,
                                    string AccountName)
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityDebitAccountDB cityDebitAccountDB = new CityDebitAccountDB();

        return cityDebitAccountDB.InsertCityDebitAccount(this.ConnectoinString, AccountNo, OtherBankAcNo, RoutingNumber, AccountName);
    }

    public void UpdateCityDebitAccountStatus(int AccountID,
                                                    string AccountNo,
                                                    string OtherBankAcNo,
                                                    string RoutingNumber,
                                                    string AccountName)
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityDebitAccountDB cityDebitAccountDB = new CityDebitAccountDB();

        cityDebitAccountDB.UpdateCityDebitAccountStatus(this.ConnectoinString, AccountID, AccountNo, OtherBankAcNo, RoutingNumber, AccountName);
    }

    public void DeleteCityDebitAccountInfo(int AccountID)
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        CityDebitAccountDB cityDebitAccountDB = new CityDebitAccountDB();

        cityDebitAccountDB.DeleteCityDebitAccountInfo(AccountID, this.ConnectoinString);
    }
}