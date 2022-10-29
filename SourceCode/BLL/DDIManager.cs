
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
public class DDIManager
{
    private string ConnectoinString;
    private int DDIBatchID;
    private int UserID;
    private int DepartmentID;

    public void ImportDDI(int userID, int departmentID)
    {
        this.UserID = userID;
        this.DepartmentID = departmentID;

        EFTN.BLL.FileSystemDataSource ds = new EFTN.BLL.FileSystemDataSource();
        DataTable dtDDITransactionImport = ds.GetDataSourceAllType("EFTNDDI"); //ws.GetFileList(ConfigurationManager.AppSettings["PBMEFTInwardPath"], "*.XML");//
        string sourceFilePath = ConfigurationManager.AppSettings["EFTNDDI"];

        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        if (dtDDITransactionImport.Rows.Count > 0)
        {
            //EFTN.BLL.TransactionReceivedXML transactionReceivedXML = new EFTN.BLL.TransactionReceivedXML();
            for (int i = 0; i < dtDDITransactionImport.Rows.Count; i++)
            {
                DataRow dtTrImportRow = dtDDITransactionImport.Rows[i];
                string filepath = dtTrImportRow["FilePath"].ToString();
                string fileName = dtTrImportRow["FileName"].ToString();
                if (System.IO.File.Exists(filepath))
                {
                    InsertDDIData(filepath, fileName);
                    //insert then delete
                    System.IO.File.Copy(filepath, sourceFilePath + "\\bak\\" + System.DateTime.Now.ToString("yyyyMMdd-hhmm-") + fileName, true);
                    System.IO.File.Delete(filepath);

                }
            }
            //GenerateDDIReturn();

            //RunCommandPrompt();
        }
    }

    private void InsertDDIData(string filePath, string fileName)
    {
        StreamReader stmReader = File.OpenText(filePath);
        string inputText = null;
        //input = stmReader.ReadLine();
        while ((inputText = stmReader.ReadLine()) != null)
        {
            if (inputText.StartsWith("H"))
            {
                InsertHeaderRecord(inputText, fileName);
            }
            else if (inputText.StartsWith("D"))
            {
                InsertDetailedRecord(inputText);
            }
            else if (inputText.StartsWith("T"))
            {
                InsertTrailerRecord(inputText);
            }
        }

        SCBDDIDB scbDDIDB = new SCBDDIDB();
        
        //UserID
        Guid TransactionID = scbDDIDB.InsertBatchSentForDDI(this.UserID, this.DepartmentID, this.DDIBatchID, this.ConnectoinString);
        scbDDIDB.InsertTransactionSentforDDI(TransactionID, this.DDIBatchID, this.UserID, this.DepartmentID, this.ConnectoinString);

        stmReader.Close();
        stmReader.Dispose();
    }

    private void InsertHeaderRecord(string inputText, string fileName)
    {
        string DebtorBankNumber = inputText.Substring(1, 17);
        string strProcessingDate = inputText.Substring(18, 8);
        int strYYYY = ParseData.StringToInt(strProcessingDate.Substring(0, 4));
        int strMM = ParseData.StringToInt(strProcessingDate.Substring(4, 2));
        int strDD = ParseData.StringToInt(strProcessingDate.Substring(6, 2));
        string strProcessingTime = inputText.Substring(26, 6);
        int strHH = ParseData.StringToInt(strProcessingTime.Substring(0, 2));
        int strmm = ParseData.StringToInt(strProcessingTime.Substring(2, 2));
        int strSS = ParseData.StringToInt(strProcessingTime.Substring(4, 2));
        DateTime ProcessingDate = new DateTime(strYYYY, strMM, strDD, strHH, strmm, strSS);

        SCBDDIDB scbDDIDB = new SCBDDIDB();
        this.DDIBatchID = scbDDIDB.InsertSCBDDIBatch(DebtorBankNumber, ProcessingDate, fileName, this.ConnectoinString);
    }

    private void InsertDetailedRecord(string inputText)
    {
        string ReturnCode = "";
        string ReturnReason = "";
        int StatusID = 1;
        string Reason = inputText.Substring(577, 80).Trim();//debitor reference
        if (Reason.Equals(string.Empty))
        {
            Reason = "BEFTN TXN";
        }

        string SenderAccountNumber = inputText.Substring(543, 34).Substring(0, 17).Trim();
        if (SenderAccountNumber.Trim().Equals(string.Empty))
        {
            ReturnReason = "SenderAccountNumber is empty";
            StatusID = 2;
        }

        string ReceveingBankRoutingNumber = inputText.Substring(13, 17).Substring(0, 9).Trim();
        if (ReceveingBankRoutingNumber.Trim().Length<9)
        {
            ReturnReason = "Invalid Receiveing Bank Routing Number";
            StatusID = 2;
        }

        string DFIAccountNumber = inputText.Substring(36, 35).Substring(0,17).Trim();
        if (DFIAccountNumber.Equals(string.Empty))
        {
            ReturnReason = "DFIAccountNumber is empty";
            StatusID = 2;
        }

        string accountType = inputText.Substring(231, 2).Trim();
        if (accountType.Equals(string.Empty))
        {
            accountType = "C";
        }
        string TransactionCode = this.GetTransactionCode(accountType);/////*************///////

        double Amount = ParseData.StringToDouble(inputText.Substring(243, 24));
        string ReceiverID = DFIAccountNumber.Trim();//inputText.Substring(760, 22);/////*************///////
        if (ReceiverID.Equals(string.Empty))
        {
            ReturnReason = "Receiver ID is empty";
            StatusID = 2;
        }

        string ReceiverName = inputText.Substring(71, 160).Substring(0, 22).Trim();
        if (ReceiverName.Equals(string.Empty))
        {
            ReturnReason = "Receiver name is empty";
            StatusID = 2;
        }

        string ReferenceNo = inputText.Substring(1, 12).Trim();
        DateTime SettlementDate = new DateTime(ParseData.StringToInt(inputText.Substring(275, 8).Trim().Substring(4, 4)), ParseData.StringToInt(inputText.Substring(275, 8).Trim().Substring(2, 2)), ParseData.StringToInt(inputText.Substring(275, 8).Trim().Substring(0, 2)));


        //string DebitorAccountName
        SCBDDIDB scbDDIDB = new SCBDDIDB();
        scbDDIDB.InsertSCBDDITransaction(this.DDIBatchID, Reason, SenderAccountNumber, ReceveingBankRoutingNumber, DFIAccountNumber, TransactionCode, Amount, ReceiverID, ReceiverName, ReferenceNo, ReceiverName, ReturnCode, ReturnReason, SettlementDate, StatusID, ConnectoinString);
    }

    private void InsertTrailerRecord(string inputText)
    {
    }

    private string GetTransactionCode(string accountType)
    {
        return accountType.ToUpper().StartsWith("C") ? TransactionCodes.DebitCurrentAcc.ToString() : TransactionCodes.DebitSavingsAcc.ToString();
    }

    public string GenerateDDIReturn()
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);
        SCBDDIDB scbDDIdb = new SCBDDIDB();

        DataTable dtReturnData = scbDDIdb.GetDDITransactionsForReturnByDDIBatchID(this.ConnectoinString);
        int returnCount = dtReturnData.Rows.Count;
        int nCount = 0;
        int yCount = 0;

        if (returnCount > 0)
        {
            GenerateFlatFileForDDITXNReturn(dtReturnData);
            foreach (DataRow row in dtReturnData.Rows)
            {
                string TransactionStatus = row["TransactionStatus"].ToString().ToUpper();
                switch (TransactionStatus)
                {
                    case "Y": yCount++;
                        break;
                    case "N": nCount++;
                        break;
                }
            }

        }

        string msg = "Total " + nCount + " transaction Returned and " + yCount + " transaction Accepted";
        
        return msg;
    }

    private void GenerateFlatFileForDDITXNReturn(DataTable dt)
    {
        EFTN.BLL.FlatFileClient fc = new EFTN.BLL.FlatFileClient();

        string flatfileResult = CreatFlatFileForDDITXNReturn(dt);
        string fileName = "BD.BFT.BFT.BD.RCMS.RCMS.NSTXNR" + System.DateTime.Now.ToString("ddMMyyHHmm");

        string PBMPath = ConfigurationManager.AppSettings["EFTNDDIUpload"];

        File.WriteAllText(PBMPath + fileName, flatfileResult);
        File.WriteAllText(PBMPath + "\\bak\\" + fileName, flatfileResult);

        //Generate config file
        GenerateTransferConfigText(PBMPath, fileName);
    }

    //Generate config file
    private void GenerateTransferConfigText(string PBMPath, string fileName)
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine("-s BD.BFT.BFT.BD.RCMS.RCMS.NSTXNR");
        result.AppendLine("-i " + PBMPath + fileName);
        result.AppendLine("-end");
        //string result = "-s BD.BFT.BFT.BD.RCMS.RCMS.NSTXNR" + "\n"
        //                + "-i " + PBMPath + fileName + "\n"
        //                + "-end";

        string cfgPath = ConfigurationManager.AppSettings["EFTNDDITransferConfig"];

        string cfgFileName = "transfer.cfg";
        File.WriteAllText(cfgPath + cfgFileName, result.ToString());
    }

    private string CreatFlatFileForDDITXNReturn(DataTable dt)
    {
        string result = string.Empty;
        int totalNumberOfRecords = 0;
        long totalAmount = 0;

        foreach (DataRow row in dt.Rows)
        {
            if (result.Equals(string.Empty))
            {
                string DebitorBankNumber = row["DebitorBankNumber"].ToString();
                result = "H" + DebitorBankNumber + System.DateTime.Now.ToString("yyyyMMdd") + System.DateTime.Now.ToString("hhmmss");
                string headerFiller = string.Empty.PadRight(718, ' ');
                result += headerFiller;
            }
            string Identifier = row["Identifier"].ToString();
            string TransactionReferene = row["TransactionReferene"].ToString();
            string DebitorBankCode = row["DebitorBankCode"].ToString();
            string DebitorBranchCode = row["DebitorBranchCode"].ToString();
            string DebitorAccountNumber = row["DebitorAccountNumber"].ToString();
            string DebitorAccountName = row["DebitorAccountName"].ToString();
            string DebitorAccType = row["DebitorAccType"].ToString();
            string TransactionCode = row["TransactionCode"].ToString();
            string Amount = row["Amount"].ToString();
            string EntryDate = row["EntryDate"].ToString();
            string SettelementDate = row["SettelementDate"].ToString();
            string TranactionPerticulars = row["TranactionPerticulars"].ToString();
            string Remarks = row["Remarks"].ToString();
            string Unused = row["Unused"].ToString();
            string SenderAccName = row["SenderAccName"].ToString();
            string SenderAccountNo = row["SenderAccountNo"].ToString();
            string TransactionStatus = row["TransactionStatus"].ToString();
            string ReturnCode = row["ReturnCode"].ToString();
            string ReturnDescription = row["ReturnDescription"].ToString();

            //if (AccountNo.Trim().Length == 13)
            //{
            string line = "";
            line += Identifier;
            line += TransactionReferene;
            line += DebitorBankCode;
            line += DebitorBranchCode;
            line += DebitorAccountNumber;
            line += DebitorAccountName;
            line += DebitorAccType;
            line += TransactionCode;
            line += Amount;
            line += EntryDate;
            line += SettelementDate;
            line += TranactionPerticulars;
            line += Remarks;
            line += SenderAccName;
            line += SenderAccountNo;
            line += TransactionStatus;
            line += ReturnCode;
            line += ReturnDescription;
            line += Unused;

            if (result.Equals(string.Empty))
            {
                result += line;
            }

            else
            {
                result += "\n" + line;
            }
            totalNumberOfRecords++;
            totalAmount += (ParseData.StringToLong(Amount));
        }

        string footer = "T" + totalNumberOfRecords.ToString().PadLeft(20, '0') + totalAmount.ToString().PadLeft(24, '0');
        
        string footerFiller = string.Empty.PadRight(705, ' ');

        footer += footerFiller;

        result = result + "\n" + footer + "\n";

        return result;
    }

    public void RunCommandPrompt()
    {
        string DDICommandFilePath = ConfigurationManager.AppSettings["EFTNDDICommandPath"];

        string command = GetCommand(DDICommandFilePath);
        try
        {
            
            //In MS-DOS you can execute multiple commands in one line by separating the commands with an ampersand(&)
            //example: command = "/c cd c:\\informatica\\1\\clients\\bin & pmrep & connect -r rs_01";

            // create the ProcessStartInfo using "cmd" as the program to be run,
            // and "/c " as the parameters.
            // Incidentally, /c tells cmd that we want it to execute the command that follows,
            // and then exit.
            ProcessStartInfo procStartInfo =
                new ProcessStartInfo("cmd", "/c " + command);

            // The following commands are needed to redirect the standard output.
            // This means that it will be redirected to the Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            // Do not create the black window.
            procStartInfo.CreateNoWindow = true;
            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            // Get the output into a string
            //string result = proc.StandardOutput.ReadToEnd();
            // Display the command output.
            //Console.WriteLine(result);
            if (proc.HasExited)
            {
                proc.Close();
            }
        }
        catch (Exception objException)
        {
            // Log the exception
        }
    }

    private string GetCommand(string filePath)
    {
        StreamReader stmReader = File.OpenText(filePath);
        string inputText = null;
        while ((inputText = stmReader.ReadLine()) != null)
        {
            return inputText;
        }
        return inputText;
    }

    public DataTable GetReturnReceivedForDDI()
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        SCBDDIDB scbDDIDB = new SCBDDIDB();

        return scbDDIDB.GetReceivedReturnForDDI(this.ConnectoinString);
    }


    public DataTable GetDDIAccountStatus(string accountStatus)
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        SCBDDIDB scbDDIDB = new SCBDDIDB();

        DataTable dtDDIAccountStatus = scbDDIDB.GetSCBDDIAccountStatus(this.ConnectoinString, accountStatus);

        return dtDDIAccountStatus;
    }

    public int InsertDDIAccountStatus(string AccountNo,
                                            string OtherBankAcNo,
                                            string RoutingNumber,
                                            DateTime ExpiryDate,
                                            bool AccountException)
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        SCBDDIDB scbDDIDB = new SCBDDIDB();

        return scbDDIDB.InsertSCBDDIAccountStatus(AccountNo, OtherBankAcNo, RoutingNumber, ExpiryDate, this.ConnectoinString, AccountException);
    }

    public void UpdateSCBDDIAccountStatus(int AccountID, string AccountNo,
                                            string OtherBankAcNo,
                                            string RoutingNumber,
                                            DateTime ExpiryDate,
                                            bool AccountException)
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        SCBDDIDB scbDDIDB = new SCBDDIDB();

        scbDDIDB.UpdateSCBDDIAccountStatus(AccountID, AccountNo, OtherBankAcNo, RoutingNumber, ExpiryDate, this.ConnectoinString, AccountException);
    }

    public void DeleteSCBDDIAccountStatus(int AccountID)
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        SCBDDIDB scbDDIDB = new SCBDDIDB();

        scbDDIDB.DeleteSCBDDIAccountStatus(AccountID, this.ConnectoinString);
    }

    public void UpdateSCBDDIAccountStatusByChecker(int AccountID, string AccountStatus)
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        SCBDDIDB scbDDIDB = new SCBDDIDB();

        scbDDIDB.UpdateSCBDDIAccountStatusByChecker(AccountID, AccountStatus, this.ConnectoinString);
    }

    public DataTable GetDDITransactionCounter()
    {
        this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

        SCBDDIDB scbDDIDB = new SCBDDIDB();

        return scbDDIDB.GetCountsDDITransactions(this.ConnectoinString);
    }

    //public DataTable GetDDITransactionStatusReportByDateRange(string FromEntryDate,
    //                                                                    string EndEntryDate,
    //                                                                    int StatusID)
    //{
    //    this.ConnectoinString = EFTN.Utility.ConnectionStringDecryptor.Decrypt(ConfigurationManager.AppSettings["ConnectionString"]);

    //    SCBDDIDB scbDDIDB = new SCBDDIDB();

    //    return scbDDIDB.GetDDITransactionStatusReportByDateRange(this.ConnectoinString, FromEntryDate, EndEntryDate, StatusID);
    //}
}