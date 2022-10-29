using System;
using System.Data;
using System.Configuration;
using System.Threading;
using EFTGateway;
using EFTN.component;
using EFTN.Utility;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace EFTN.BLL
{
    public class FinacleManager
    {
        private Socket client;
        private byte[] data = new byte[1024];
        private int size = 1024;

        private string logPath;
        private string logFile;

        private int serverPort;
        private string serverIP;
        private TcpGateway tcpGateWay;

        public bool IsConnected;

        public FinacleManager()
        {
            serverIP = ConfigurationManager.AppSettings["FiNacleIP"];
            serverPort = System.Convert.ToInt32(
                ConfigurationManager.AppSettings["Port"]);
            logPath = ConfigurationManager.AppSettings["LogPath"];

            logFile  = DateTime.Today.ToString("yyyyMMdd") + ".txt";
        }

        public void InsertTransactionIntoFinacle(DataTable dt)
        {
            InsertFinacleTransaction(dt);
            Thread.Sleep(25000);
            InsertNonResponsedMsgs();
        }

        public void Connect()
        {
            log("Trying to Connect...");
            try
            {
                Socket newsock = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);

                IsConnected = true;
            }
            catch (Exception ex)
            {
                log("Error: " + ex.Message);
            }
        }

        public void Disconnect()
        {
            if (client != null)
            {
                client.Close();
                log("Disconnected");
            }
            IsConnected = false;
        }

        private void InsertFinacleTransaction(DataTable dt)
        {
            //tcpGateWay = new TcpGateway(serverIP, serverPort);
            byte[] readBytes;

            //System.Net.Sockets.TcpClient tcpClient;
            ISOMessageDB isoMessageDB = new ISOMessageDB();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ISOMessage isoMessage;
                if (dt.Rows[i]["TranType"].ToString().Equals("Dr"))
                {

                    isoMessage = new ISOMessage(
                        dt.Rows[i]["AccountNo"].ToString(),
                        "400000",
                        dt.Rows[i]["Amount"].ToString(),
                        dt.Rows[i]["TraceNumber"].ToString(),
                        DVar.TransDateTime,
                        DVar.CaptureDate,
                        DVar.FunctionCode,
                        DVar.AcqIdentCode,
                        string.Empty,
                        DVar.DeviceID,
                        DVar.CardAcceptorID,
                        DVar.CardAcceptorName,
                        DVar.AmountFees,
                        DVar.TransCurrency,
                        dt.Rows[i]["AccountNo"].ToString(),
                        DVar.EftAccountNo,
                        DVar.DelvChannelID,
                        DVar.TerminalType,
                        DVar.ReservedCode);
                }
                else
                {
                    isoMessage = new ISOMessage(
                        dt.Rows[i]["AccountNo"].ToString(),
                        "400000",
                        dt.Rows[i]["Amount"].ToString(),
                        dt.Rows[i]["TraceNumber"].ToString(),
                        DVar.TransDateTime,
                        DVar.CaptureDate,
                        DVar.FunctionCode,
                        DVar.AcqIdentCode,
                        string.Empty,       // retrieval ref. no.
                        DVar.DeviceID,
                        DVar.CardAcceptorID,
                        DVar.CardAcceptorName,
                        DVar.AmountFees,
                        DVar.TransCurrency,
                        DVar.EftAccountNo,
                        dt.Rows[i]["AccountNo"].ToString(),
                        DVar.DelvChannelID,
                        DVar.TerminalType,
                        DVar.ReservedCode);
                }

                bool sentISOMessage = false;

                try
                {
                    //sentISOMessage = tcpGateWay.SendMessage(isoMessage.ToByteArray());
                    Guid EDRID = (Guid)(dt.Rows[i]["EDRID"]);
                    isoMessageDB.InsertISOMessege(
                            EDRID, ISOMessageType.RegularMessage, isoMessage.mti, isoMessage.bitMap
                            , isoMessage.actNumber, isoMessage.procCode, isoMessage.tranAmount
                            , isoMessage.sysTraceAuditNo, isoMessage.transDateTime, isoMessage.captureDate
                            , isoMessage.functionCode, isoMessage.acqIdentCode, isoMessage.retrievalRefNo
                            , isoMessage.approvalCode, isoMessage.actionCode, isoMessage.deviceID
                            , isoMessage.cardAcceptorId, isoMessage.cardAcceptorName, isoMessage.amountFees
                            , isoMessage.additionalData, isoMessage.transCurrency, isoMessage.debitActNumber
                            , isoMessage.creditActNumber, isoMessage.delvChannelID, isoMessage.terminal
                            , isoMessage.resv);
                    
                    sentISOMessage = sendMessage(isoMessage);

                    if (sentISOMessage)
                    {
                        log("Sent Rev -- MTI: " + isoMessage.mti +
                           "Adt: " + isoMessage.sysTraceAuditNo +
                           " CaptDate: " + isoMessage.captureDate +
                           " actCode: " + isoMessage.actionCode +
                           " msgType: " + 1 +
                           " msgStatus: " + 1);
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    log("Error: " + ex.Message);
                }
            }

            //while (tcpGateWay.ReadMessage(out readBytes) > 0)
            //{
            //    ISOMessage response = new ISOMessage(readBytes);
            //    isoMessageDB.ISOMessageUpdate(
            //        ISOMessageType.RegularMessage,
            //        response.sysTraceAuditNo,
            //        response.captureDate,
            //        response.actionCode,
            //        ISOMessageStatus.MessageSentAcknowledgement);
            //}

            //tcpGateWay.Close();
        }

        public void InsertNonResponsedMsgs()
        {
            ISOMessageDB isoMessageDB = new ISOMessageDB();
            DataTable dt = isoMessageDB.ISOMessageGet(ISOMessageStatus.MessageSent);
            byte[] readBytes;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ISOReversalMessage isoReversalMessage;
                if (!string.IsNullOrEmpty(dt.Rows[i]["ActNumber"].ToString()))
                {
                    isoReversalMessage = new ISOReversalMessage(
                       dt.Rows[i]["ActNumber"].ToString(),
                       "400000",
                       dt.Rows[i]["TransAmount"].ToString(),
                       dt.Rows[i]["SystemTraceAuditNo"].ToString(),
                       DVar.TransDateTime,
                       DVar.CaptureDate,
                       DVar.FunctionCode,
                       dt.Rows[i]["TransAmount"].ToString(),
                       DVar.AcqIdentCode,
                       string.Empty,
                       DVar.DeviceID,
                       DVar.CardAcceptorID,
                       DVar.CardAcceptorName,
                       DVar.AmountFees,
                       DVar.TransCurrency,
                       string.Empty,
                       DVar.AmountFees,
                       dt.Rows[i]["ActNumber"].ToString(),
                       DVar.EftAccountNo,
                       DVar.DelvChannelID,
                       DVar.TerminalType,
                       DVar.ReservedCode);
                }
                else
                {
                    isoReversalMessage = new ISOReversalMessage(
                       dt.Rows[i]["ActNumber"].ToString(),
                       "400000",
                       dt.Rows[i]["TransAmount"].ToString(),
                       dt.Rows[i]["SystemTraceAuditNo"].ToString(),
                       DVar.TransDateTime,
                       DVar.CaptureDate,
                       DVar.FunctionCode,
                       dt.Rows[i]["TransAmount"].ToString(),
                       DVar.AcqIdentCode,
                       string.Empty,
                       DVar.DeviceID,
                       DVar.CardAcceptorID,
                       DVar.CardAcceptorName,
                       DVar.AmountFees,
                       DVar.TransCurrency,
                       string.Empty,
                       DVar.AmountFees,
                       DVar.EftAccountNo,
                       dt.Rows[i]["ActNumber"].ToString(),//dt.Rows[i]["CreditAccountNumber"].ToString(),
                       DVar.DelvChannelID,
                       DVar.TerminalType,
                       DVar.ReservedCode);
                }
                bool sentISOMessage = false;
                try
                {
                    Guid EDRID = (Guid)(dt.Rows[i]["EDRID"]);
                        isoMessageDB.InsertISOMessege(EDRID, ISOMessageType.ReversalMessage, isoReversalMessage.mti, isoReversalMessage.bitMap
                            , isoReversalMessage.actNumber, isoReversalMessage.procCode, isoReversalMessage.tranAmount
                            , isoReversalMessage.sysTraceAuditNo, isoReversalMessage.transDateTime, isoReversalMessage.captureDate
                            , isoReversalMessage.functionCode, isoReversalMessage.acqIdentCode, isoReversalMessage.retrievalRefNo
                            , isoReversalMessage.approvalCode, isoReversalMessage.actionCode, isoReversalMessage.deviceID
                            , isoReversalMessage.cardAcceptorId, isoReversalMessage.cardAcceptorName, isoReversalMessage.amountFees
                            , isoReversalMessage.additionalData, isoReversalMessage.transCurrency, isoReversalMessage.debitActNumber
                            , isoReversalMessage.creditActNumber, isoReversalMessage.delvChannelID, isoReversalMessage.terminal
                            , isoReversalMessage.resv);

                    sentISOMessage = sendMessage(isoReversalMessage);
                    if (sentISOMessage)
                    {

                        log("Sent Rev -- MTI: " + isoReversalMessage.mti +
                          "Adt: " + isoReversalMessage.sysTraceAuditNo +
                          " CaptDate: " + isoReversalMessage.captureDate +
                          " actCode: " + isoReversalMessage.actionCode +
                          " msgType: " + 3 +
                          " msgStatus: " + 3);

                        isoMessageDB.ISOMessageUpdate(
                            ISOMessageType.RegularMessage,
                            isoReversalMessage.sysTraceAuditNo,
                            isoReversalMessage.captureDate,
                            isoReversalMessage.actionCode,
                            ISOMessageStatus.ReverseMessageSent);
                        Thread.Sleep(100);

                    }
                }
                catch (Exception ex)
                {
                    log("Error: " + ex.Message);
                }
            }

        }


        void log(string text)
        {
            FileInfo fInfo = new FileInfo(logPath+"\\"+logFile);
            while (IsFileLocked(fInfo))
            {
                System.Threading.Thread.Sleep(50);
            }
            try
            {
                TextWriter tw = new StreamWriter(fInfo.FullName, true);
                tw.WriteLine(DateTime.Now.ToString("yyyyMMdd:hhmmss - ") + text);
                tw.Close();
            }
            catch
            {
            }
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        private bool sendMessage(ISOMessage msg)
        {
            try
            {
                byte[] message = msg.ToByteArray();

                client.BeginSend(message, 0, message.Length, SocketFlags.None,
                    new AsyncCallback(SendData), client);
                //log("Sent: " + msg.sysTraceAuditNo);
                return true;
            }
            catch (Exception ex)
            {
                log("Error: " + ex.Message);
            }
            return false;
        }

        void Connected(IAsyncResult iar)
        {
            client = (Socket)iar.AsyncState;
            try
            {
                client.EndConnect(iar);
                log("Connected to: " + client.RemoteEndPoint.ToString());
                client.BeginReceive(data, 0, size, SocketFlags.None,
                              new AsyncCallback(ReceiveData), client);
            }
            catch (SocketException ex)
            {
                log("Error connecting: " + ex.Message);
            }
        }

        void ReceiveData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int recv = remote.EndReceive(iar);

            ISOMessageDB isoMessageDB = new ISOMessageDB();
            try
            {
                ISOMessage response = new ISOMessage(data);

                int msgType;
                int msgStatus;
                if (response.mti.StartsWith("12"))
                {
                    msgType = ISOMessageType.RegularMessage;
                    msgStatus = ISOMessageStatus.MessageSentAcknowledgement;
                }
                else
                {
                    msgType = ISOMessageType.ReversalMessage;
                    msgStatus = ISOMessageStatus.ReverseMessageSentAcknowledgement;
                }

                isoMessageDB.ISOMessageUpdate(
                    msgType,
                    response.sysTraceAuditNo,
                    response.captureDate,
                    response.actionCode,
                    msgStatus);

                //results.Items.Add(stringData);
                log("Recieved Adt: " + response.sysTraceAuditNo + 
                    " CaptDate: " + response.captureDate + 
                    " actCode: " + response.actionCode + 
                    " msgType: " + msgType + 
                    " msgStatus: " + msgStatus);
            }
            catch (Exception ex)
            {
                log("Error: Bad Response. " + ex.Message);
            }
        }

        void SendData(IAsyncResult iar)
        {
            log("SendData callback");
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
            remote.BeginReceive(data, 0, size, SocketFlags.None,
                          new AsyncCallback(ReceiveData), remote);
        }
    }
}
