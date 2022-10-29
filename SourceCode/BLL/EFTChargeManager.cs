using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EFTN.component;
using System.Data.SqlClient;

namespace EFTN.BLL
{
    public class EFTChargeManager
    {
        public void UpdateEFTNChargeByEDRID(Guid EDRID, string connectionString, string BankCode)
        {
            if (BankCode.Equals("215"))// for SCB
            {
                ChargeApplyForSCB(EDRID, connectionString);
            }
            else if (BankCode.Equals("115"))// for HSBC
            {
                ChargeApplyForHSBC(EDRID, connectionString);
            }
            else if (BankCode.Equals("225"))// for The City Bank Limited
            {
                ChargeApplyForCityBank(EDRID, connectionString);
            }
        }

        public void UdpateEFTNChargeBYTransactionID(Guid TransactionID, string connectionString, string BankCode)
        {
            if (BankCode.Equals("215"))// for scb
            {
                EFTChargeDB eftChargeDB = new EFTChargeDB();

                DataTable dtEDRList = eftChargeDB.GetEDRIDByTransactionID(TransactionID, connectionString);
                foreach (DataRow row in dtEDRList.Rows)
                {
                    string edrID = row["EDRID"].ToString();
                    Guid EDRID = new Guid(edrID);
                    ChargeApplyForSCB(EDRID, connectionString);
                }
            }
            else if (BankCode.Equals("115"))// for HSBC
            {
                EFTChargeDB eftChargeDB = new EFTChargeDB();

                DataTable dtEDRList = eftChargeDB.GetEDRIDByTransactionID(TransactionID, connectionString);
                foreach (DataRow row in dtEDRList.Rows)
                {
                    string edrID = row["EDRID"].ToString();
                    Guid EDRID = new Guid(edrID);
                    ChargeApplyForHSBC(EDRID, connectionString);
                }
            }
            else if (BankCode.Equals("225"))// for The City Bank Limited
            {
                EFTChargeDB eftChargeDB = new EFTChargeDB();

                DataTable dtEDRList = eftChargeDB.GetEDRIDByTransactionID(TransactionID, connectionString);
                foreach (DataRow row in dtEDRList.Rows)
                {
                    string edrID = row["EDRID"].ToString();
                    Guid EDRID = new Guid(edrID);
                    ChargeApplyForCityBank(EDRID, connectionString);
                }
            }
        }

        /// <summary>
        ///  Charge Apply For The City Bank Limited
        /// </summary>
        /// <param name="EDRID"></param>
        /// <param name="connectionString"></param>
        private void ChargeApplyForCityBank(Guid EDRID, string connectionString)
        {
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.UpdateChargeForCity(EDRID, connectionString);
        }

        /// <summary>
        ///  Charge Apply For Standard Chartered Bank
        /// </summary>
        /// <param name="EDRID"></param>
        /// <param name="connectionString"></param>
        private void ChargeApplyForSCB(Guid EDRID, string connectionString)
        {
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.UpdateChargeForSCB(EDRID, connectionString);
        }

        /// <summary>
        /// ChargeApply For HSBC
        /// </summary>
        /// <param name="EDRID"></param>
        /// <param name="connectionString"></param>
        private void ChargeApplyForHSBC(Guid EDRID, string connectionString)
        {
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.UpdateChargeForHSBC(EDRID, connectionString);
        }

        public void InsertChargeDefinitionForCityBankByTransactionID(Guid TransactionID, int CityChargeDefineID)
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.InsertCityChargeDefineByTransactionID(TransactionID, CityChargeDefineID, ConnectionString);
        }

        public void InsertCityChargeCodeByTransactionID(Guid TransactionID, int CityChargeCode)
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.InsertCityChargeCodeByTransactionID(TransactionID, CityChargeCode, ConnectionString);
        }

        public void InsertCityChargeCodeByEDRID(Guid EDRID, int CityChargeCode)
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.InsertCityChargeCodeByEDRID(EDRID, CityChargeCode, ConnectionString);
        }

        public void InsertChargeDefinitionForCityBankByEDRID(Guid EDRID, int CityChargeDefineID)
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.InsertCityChargeDefineByEDRID(EDRID, CityChargeDefineID, ConnectionString);
        }

        public DataTable GetCityChargeDefineList()
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            return eftChargeDB.GetCityChargeDefineList(ConnectionString);
        }

        public DataTable GetCityChargeDefineListForBulk()
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            return eftChargeDB.GetCityChargeDefineListForBulk(ConnectionString);
        }

        public void InsertCityChargeAccount(
                                        string AccountNo,
                                        int CityChargeCode,
                                        int UserID)
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.InsertCityChargeAccount(ConnectionString, AccountNo, CityChargeCode, UserID);
        }

        public void UpdateCityChargeAccount(
                                        int CityChargeAccID,
                                        int CityChargeCode,
                                        string AccountNo)
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.UpdateCityChargeAccount(ConnectionString, CityChargeAccID, CityChargeCode, AccountNo);
        }

        public void UpdateDepartmentChargeForCity(int DepartmentID,
                                            string CityBBChargeAcc,
                                            string CityBankChargeAcc,
                                            string CityBBChargeVATAcc,
                                            string CityBankChargeVATAcc,
                                            string CityChargeWaveAcc,
                                            string CityVATWaveAcc,
                                            int CityChargeCode,
                                            int CreatedBy)
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.UpdateDepartmentChargeForCity(ConnectionString, DepartmentID, CityBBChargeAcc, CityBankChargeAcc, CityBBChargeVATAcc, CityBankChargeVATAcc, CityChargeWaveAcc, CityVATWaveAcc, CityChargeCode, CreatedBy);
        }

        public DataTable GetCityChargeAccount()
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            return eftChargeDB.GetCityChargeAccounts(ConnectionString);
        }

        public DataTable GetCityChargeCodeInfo()
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            return eftChargeDB.GetCityChargeCodeInfo(ConnectionString);
        }

        public void InsertCityChargeCodeInfo(   double BBCharge,
                                                double BankCharge,
                                                double BBChargeVAT,
                                                double BankChargeVAT,
                                                double ChargeWave,
                                                double VATWave
                                            )
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.InsertCityChargeCodeInfo(ConnectionString, BBCharge, BankCharge, BBChargeVAT, BankChargeVAT, ChargeWave, VATWave);
        }

        public void UpdateCityChargeCodeInfo(int CityChargeCode,
                                                double BBCharge,
                                                double BankCharge,
                                                double BBChargeVAT,
                                                double BankChargeVAT,
                                                double ChargeWave,
                                                double VATWave
                                            )
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.UpdateCityChargeCodeInfo(ConnectionString, CityChargeCode, BBCharge, BankCharge, BBChargeVAT, BankChargeVAT, ChargeWave, VATWave);
        }

        public void DeleteEFTCityChargeAccount(int CityChargeAccID)
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            EFTChargeDB eftChargeDB = new EFTChargeDB();
            eftChargeDB.DeleteEFTCityChargeAccount(ConnectionString, CityChargeAccID);
        }
    }
}
