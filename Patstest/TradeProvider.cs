using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Collections;
using pfcf.tool.server;
using pfcf.tool;
using pfcf.trade;
 

namespace FTrade
{


    public class TradeProvider
    {
       
        string SPLIT_STRING = "\x0001";

        SeqProvider _Seq = null;
        SeqProvider _Networkid = null;
 


     

        private string _SrcID = "";
        private DataAgent _DataAgent;




        SAEAServer _SAEA;

        AccountCore _AccountCore;
 



        bool _DataEncode = false;
        public bool DataEncode
        {
            set { _DataEncode = value; }
            get { return _DataEncode; }
        }


        //   SaveDB[] _SaveDB;


        delegate int SignVerifyHandler(string networkid, string data, string Signature, string CertSN, string idno, string IP);
        SignVerifyHandler SignVerifyExcute;




        public TradeProvider(DataAgent DataAgent)
        {
            try
            {



                _DataEncode = true;

                _DataAgent = DataAgent;


                _SAEA = _DataAgent.SAEAServer;
                _AccountCore = _DataAgent.AccountCore;

 
      


                SignVerifyExcute = new SignVerifyHandler(SignVerify);

 


            }
            catch (Exception ex)
            {

                WriteLog("DataAgentLog", "TradeStringHandler:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
            }

        }


        public void load()
        {

        }

        ~TradeProvider()
        {
            Dispose();
        }

        public void Dispose()
        {

            if (_Seq != null)
                _Seq.Dispose();

 



            GC.SuppressFinalize(this);
        }


        public void ParseDDSCData(SocketAsyncEventArgs S, byte[] buffer)
        {

            DataToken Token = S.UserToken as DataToken;
            string RemoteIP = Token.Ip;
            string port = Token.Port;
            try
            {

                switch (buffer[0])
                {

                    case 0xFF:
                        if (_DataEncode)
                            buffer = SocketRaw.DecodeData(buffer);
                        // WriteLog("SAEAServerLogRCV" + S.AcceptSocket.RemoteEndPoint.ToString().Replace(":", ";"), buffer);
                        WriteLog("SAEAServerLogRCV" + RemoteIP, buffer);
                        ParseMA(S, buffer);
                        break;

                    case SocketRaw.Alive:
                        if (_DataEncode)
                            buffer = SocketRaw.DecodeData(buffer);
                        // WriteLog("SAEAServerLogRCV" + S.AcceptSocket.RemoteEndPoint.ToString().Replace(":", ";"), buffer);
                        WriteLog("SAEAServerLogRCV" + RemoteIP, buffer);
                        ParseAlive(S, buffer);
                        break;
                    case SocketRaw.RegTradeAct:
                        if (_DataEncode)
                            buffer = SocketRaw.DecodeData(buffer);
                        //WriteLog("SAEAServerLogRCV" + S.AcceptSocket.RemoteEndPoint.ToString().Replace(":", ";"), buffer);
                        WriteLog("SAEAServerLogRCV" + RemoteIP, buffer);
                        ParseRegTradeAct(S, buffer);
                        break;
                    case SocketRaw.Connecting:
                        if (_DataEncode)
                            buffer = SocketRaw.DecodeData(buffer);
                        //WriteLog("SAEAServerLogRCV" + S.AcceptSocket.RemoteEndPoint.ToString().Replace(":", ";"), buffer);
                        WriteLog("SAEAServerLogRCV" + RemoteIP, buffer);
                        ParseConnecting(S, buffer);
                        break;

                    case SocketRaw.GeneralTrade:

                        if (_DataEncode)
                            buffer = SocketRaw.DecodeData(buffer);
                        //WriteLog("SAEAServerLogRCV" + S.AcceptSocket.RemoteEndPoint.ToString().Replace(":", ";"), buffer);
                        WriteLog("SAEAServerLogRCV" + RemoteIP, buffer);

                        //if (buffer[0] == SocketRaw.GeneralTrade)
                        //    ParseGeneralTrade(Token, S, buffer);

                        break;




                    default:



                        WriteLog("DataAgentLog", "ParseDDSCData:" + "length=" + buffer.Length + RemoteIP + ":" + port.ToString() + "format error!");
                        WriteLog("DataAgentLog", "ParseDDSCData:" + ASCIIEncoding.ASCII.GetString(buffer));
                        WriteLog("DataAgentLog", "ParseDDSCData:" + ASCIIEncoding.ASCII.GetString(_DataEncode ? SocketRaw.DecodeData(buffer) : buffer));
                        _SAEA.CloseClientSocket(S);
                        break;
                }


            }
            catch (Exception ex)
            {

                WriteLog("DataAgentLog", "ParseDDSCData:" + "length=" + buffer.Length + "-" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString() + RemoteIP + ":" + port.ToString());
                WriteLog("DataAgentLog", "ParseDDSCData:" + ASCIIEncoding.ASCII.GetString(buffer));
                WriteLog("DataAgentLog", "ParseDDSCData:" + ASCIIEncoding.ASCII.GetString(_DataEncode ? SocketRaw.DecodeData(buffer) : buffer));
                _SAEA.CloseClientSocket(S);
            }
        }




        private void ParseMA(SocketAsyncEventArgs S, byte[] buffer)
        {
            try
            {

                SocketRaw.Header raw = new SocketRaw.Header();

                SocketRaw.ParserStruct<SocketRaw.Header>.ByteArrayToStructure(buffer, ref raw);


                raw.HEAD = new byte[] { 0xff };

                string body = "";
                //0 saeaserver 
                //1 connection 
                //2 speedy

                if (_DataAgent.SAEAServer.Active)
                    body += "Y" + SPLIT_STRING;
                else
                    body += "N" + SPLIT_STRING;

                body += _SAEA.NumberOfConnectedSockets.ToString() + SPLIT_STRING;
 


                byte[] bytebody = ASCIIEncoding.ASCII.GetBytes(body);

                raw.LENGTH = BitConverter.GetBytes(Convert.ToUInt16(bytebody.Length));
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(raw.LENGTH);
                }
                buffer = SocketRaw.StructureToByteArray(raw);
                byte[] ret = new byte[buffer.Length + bytebody.Length + 1];
                Array.Copy(buffer, 0, ret, 0, buffer.Length);
                Array.Copy(bytebody, 0, ret, buffer.Length, bytebody.Length);
                ret[ret.Length - 1] = SocketRaw.End;


                SendDataToAP(S, ret);

            }
            catch (Exception ex)
            {
                WriteLog("DataAgentLog", "ParseConnecting:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
                throw ex;
            }
        }

        private void ParseAlive(SocketAsyncEventArgs S, byte[] buffer)
        {
            try
            {
                // string len = getDDSCRawDataLength(buffer);
                SendDataToAP(S, buffer);
                //this._SAEA.SendData(S, buffer);

            }
            catch (Exception ex)
            {
                WriteLog("DataAgentLog", "ParseAlive:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
                throw ex;
            }
        }

        private void ParseRegTradeAct(SocketAsyncEventArgs S, byte[] buffer)
        {
            try
            {
                SocketRaw.Header raw = new SocketRaw.Header();

                SocketRaw.ParserStruct<SocketRaw.Header>.ByteArrayToStructure(buffer, ref raw);

                string Userid = ASCIIEncoding.ASCII.GetString(raw.USERID).Trim();
                string body = ASCIIEncoding.ASCII.GetString(buffer, Marshal.SizeOf(typeof(SocketRaw.Header))
                    , buffer.Length - Marshal.SizeOf(typeof(SocketRaw.Header)) - 1);
                ArrayList comact = new ArrayList();

                string brokerid = body.Substring(0, 7).Trim();
                string act = body.Substring(7, 7).Trim();
                string subactno = body.Substring(14, 7).Trim();
                comact.Add(brokerid + act + subactno);
                if (subactno.Length > 0)
                    _AccountCore.AddACTNOFlag(act);
                comact.Add(brokerid + act + subactno);
                Array o = comact.ToArray(typeof(string));

                _AccountCore.AddUser(Userid, (string[])o);
                if (_AccountCore.AddSAEA(Userid, S))
                {

                }
                else
                {
                    WriteLog("DataAgentLog", "ParseConnecting:" + "無此帳號:" + Userid);
                    _SAEA.CloseClientSocket(S);
                }

            }
            catch (Exception ex)
            {
                WriteLog("DataAgentLog", "ParseConnecting:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
                throw ex;
            }
        }

        private void ParseConnecting(SocketAsyncEventArgs S, byte[] buffer)
        {
            try
            {
                SocketRaw.Header raw = new SocketRaw.Header();

                SocketRaw.ParserStruct<SocketRaw.Header>.ByteArrayToStructure(buffer, ref raw);

                string Userid = ASCIIEncoding.ASCII.GetString(raw.USERID).Trim();
                string body = ASCIIEncoding.ASCII.GetString(buffer, Marshal.SizeOf(typeof(SocketRaw.Header))
                    , buffer.Length - Marshal.SizeOf(typeof(SocketRaw.Header)) - 1);
                //ArrayList comact = new ArrayList();
                //for (int i = 0; i * (7 + 7 + 7) < body.Length; i++)//brokerid(7)+act(7)+subactno(7)
                //{
                //    string brokerid = body.Substring(i * (7 + 7 + 7), 21).Substring(0, 7).Trim();
                //    string act = body.Substring(i * (7 + 7 + 7), 21).Substring(7, 7).Trim();
                //    string subactno = body.Substring(i * (7 + 7 + 7), 21).Substring(14, 7).Trim();
                //    comact.Add(brokerid + act + subactno);
                //    if (subactno.Length > 0)
                //        _AccountCore.AddACTNOFlag(act);
                //}
                //Array o = comact.ToArray(typeof(string));
                //_AccountCore.AddUser(Userid, (string[])o);
                _AccountCore.AddUser(Userid);
                if (_AccountCore.AddSAEA(Userid, S))
                {
                    raw.HEAD = new byte[] { SocketRaw.Connected };

                    UserInfo u = _AccountCore.FindUserInfo(S);
                    byte[] seq = getSeq();

                    Array.Copy(seq, 0, raw.NEWSEQ, 0, seq.Length);
                    Array.Copy(seq, 0, raw.OLDSEQ, 0, seq.Length);
                    raw.LENGTH = BitConverter.GetBytes(UInt16.Parse("0"));
                    buffer = SocketRaw.StructureToByteArray(raw);
                    byte[] ret = new byte[buffer.Length + 1];
                    Array.Copy(buffer, 0, ret, 0, buffer.Length);
                    ret[ret.Length - 1] = SocketRaw.End;

                    //string[] arrPutIPLoginCmd = new string[1];
                    //arrPutIPLoginCmd[0] = " INSERT INTO [dbo].[TRADEIPLOG] "
                    //+ "  ([USERID],[IP],[PORT],[LOGINTIME],[STATUS],[LOGDATE])VALUES "
                    //+ "('" + Userid + "','" + S.AcceptSocket.RemoteEndPoint.ToString().Split(':')[0] + "','" + S.AcceptSocket.RemoteEndPoint.ToString().Split(':')[1] + "','" + u.LoginTime + "','',convert(char(8),getdate(),112))";
                    //_SaveDB.PutData2Queue(arrPutIPLoginCmd);

                    SendDataToAP(S, ret);
                }
                else
                {
                    WriteLog("DataAgentLog", "ParseConnecting:" + "無此帳號:" + Userid);
                    _SAEA.CloseClientSocket(S);
                }
            }
            catch (Exception ex)
            {
                WriteLog("DataAgentLog", "ParseConnecting:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
                throw ex;
            }
        }

        //private void CombineErrorGeneralTrade(SockClientParserFunction.Order Order, string ErrorCode, string ORDERSTATUS)
        //{
        //    try
        //    {

        //        string BROKERID;
        //        string INVESTORACNO;
        //        string NETWORKID;

        //        BROKERID = ASCIIEncoding.ASCII.GetString(Order.BROKERID).Trim();
        //        INVESTORACNO = ASCIIEncoding.ASCII.GetString(Order.INVESTORACNO).Trim();
        //        NETWORKID = ASCIIEncoding.ASCII.GetString(Order.CLORDID).Trim(); ;

        //        string SUBACT = "";//ASCIIEncoding.ASCII.GetString(Order.SUBACT).Trim();

        //        byte[] buffer = DbServerProvider.ResponseOrderToError(ref Order, ErrorCode, ORDERSTATUS);
        //        buffer = SendOrderDataToAP(BROKERID + INVESTORACNO + SUBACT, SockClientParserFunction.DDSCSocketHead.Reply, NETWORKID, buffer);



        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLog("DataAgentLog", "CombineErrorGeneralTrade:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
        //    }
        //}

 

 
        //public void parseTTFixReport(string system, string SenderCompID, string TargetCompID, TT.ExecutionReport obj)
        //{
        //    ExecutionReport TT = FIXProvider.TT2EXCHForExecutionReport(obj, ref _SymbolProvider);
        //    parseFixReport(system, SenderCompID, TargetCompID, TT);
        //}
 

        private DateTime StampToDateTime(string timeStamp)
        {

            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 8, 0, 0));// +8 TW
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }
 

        /// <summary>
        /// 新增單回AP
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="buffer"></param>
        private byte[] SendOrderDataToAP(string Account, byte head, string NetWorkID, byte[] buffer)
        {
            byte[] retBuffer = null;
            try
            {
                Dictionary<int, SocketAsyncEventArgs> Sockets = this._AccountCore.FindSAEA(Account);
                SocketRaw.Header Head = new SocketRaw.Header();
                Head.HEAD = new byte[1] { head };
                Head.MESSAGETIME = ASCIIEncoding.ASCII.GetBytes(DateTime.Now.ToString("HHmmssfff"));
                byte[] headbyte;
                byte[] netoworkid = ASCIIEncoding.ASCII.GetBytes(NetWorkID);
                //取seq
                NetWorkIdItem item = _AccountCore.FindNetWorkIdItem(netoworkid);
                if (item != null)
                {
                    Head.OLDSEQ = ASCIIEncoding.ASCII.GetBytes(item.OldSeq.PadRight(9, ' '));
                    Head.NEWSEQ = ASCIIEncoding.ASCII.GetBytes(item.NewSeq.PadRight(9, ' '));
                    Head.USERID = ASCIIEncoding.ASCII.GetBytes(item.UserId.PadRight(20, ' '));


                    byte[] Data = Encoding.Default.GetBytes(item.Data);
                    byte[] byteReply = buffer;
                    var list = new List<byte>();
                    list.AddRange(byteReply);
                    list.AddRange(Data);
                    buffer = list.ToArray();

                }

                else
                {
                    Head.OLDSEQ = ASCIIEncoding.ASCII.GetBytes("".PadRight(9, ' '));
                    Head.NEWSEQ = ASCIIEncoding.ASCII.GetBytes("".PadRight(9, ' '));
                    Head.USERID = ASCIIEncoding.ASCII.GetBytes("".PadRight(20, ' '));

                }
                Head.LENGTH = BitConverter.GetBytes(UInt16.Parse(buffer.Length.ToString()));
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(Head.LENGTH);

                retBuffer = new byte[50 + buffer.Length + 1];
                headbyte = SocketRaw.StructureToByteArray(Head);
                Array.Copy(headbyte, 0, retBuffer, 0, headbyte.Length);
                Array.Copy(buffer, 0, retBuffer, headbyte.Length, buffer.Length);
                retBuffer[retBuffer.Length - 1] = SocketRaw.End;
                if (Sockets != null)
                {
                    int[] arr = Sockets.Keys.ToArray();
                    try
                    {//有可能在取得socket 物件後 斷線
                        //foreach (int key in Sockets.Keys.ToArray())
                        for (int i = 0; i < arr.Length; i++)
                        {

                            try
                            {

                                this._SAEA.SendData(Sockets[arr[i]], _DataEncode ? SocketRaw.EncodeData(retBuffer) : retBuffer);
                                // WriteLog("SAEAServerLogSend" + Sockets[arr[i]].AcceptSocket.RemoteEndPoint.ToString().Replace(":", ";"), retBuffer);

                                string RemoteIP = ((System.Net.IPEndPoint)Sockets[arr[i]].AcceptSocket.RemoteEndPoint).Address.ToString();
                                WriteLog("SAEAServerLogSend" + RemoteIP, retBuffer);
                            }
                            catch (Exception ex)
                            {
                                WriteLog("DataAgentLog", "Socket is null:" + Account + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        WriteLog("DataAgentLog", "Sockets is null:" + Account + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
                    }
                }




             
            }
            catch (Exception ex)
            {
                WriteLog("DataAgentLog", "SendOrderDataToAP:" + Account + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
            }
            finally
            {

            }
            return retBuffer;
        }


        private string getNetworkId()
        {

            try
            {
                int networkid = _Networkid.getSeq();

                return _SrcID + (Int2String(int.Parse(DateTime.Now.ToString("MMdd"))) + networkid.ToString()).PadLeft(9, '0');
            }
            catch (Exception ex)
            {
            }
            return "";

        }



        private byte[] getSeq()
        {
            byte[] Seq;

            int seq = _Seq.getSeq();
            seq = 1000000 * (int)DateTime.Now.DayOfWeek + seq;

            Seq = ASCIIEncoding.ASCII.GetBytes((_SrcID + Int2String(seq).PadLeft(4, '0')).PadRight(9, ' '));


            return Seq;
        }



        private void SendDataToAP(SocketAsyncEventArgs S, byte[] buffer)
        {
            Array.Copy(ASCIIEncoding.ASCII.GetBytes(DateTimeProvider.GetNowTime().ToString("HHmmssfff")), 0, buffer, 1, 9);
            string RemoteIP = ((System.Net.IPEndPoint)S.AcceptSocket.RemoteEndPoint).Address.ToString();
            this._SAEA.SendData(S, _DataEncode ? SocketRaw.EncodeData(buffer) : buffer);

            WriteLog("SAEAServerLogSend" + RemoteIP, buffer);


        }





        private void WriteLog(string key, byte[] data)
        {

            DataAgent._LM.Log(key, data);
        }
        private void WriteLog(string key, string data)
        {
            DataAgent._LM.Log(key, data);
        }



        private int SignVerify(string networkid, string data, string Signature, string CertSN, string idno, string IP)
        {

            int intResult = 9999;
            try
            {
                string signature = "";

                bool verify = true;
                if (verify) intResult = 0;
                //intResult = _VA.VA_STOCK_P1VerifySignEx(Signature, data, "", 0, 0, idno, CertSN
                //  , Settings.Default.SrcCode
                //  , Settings.Default.BizId
                //  , "default", IP);


                return intResult;

            }
            catch (Exception ex)
            {
                return 9999;
            }
        }




        public static String Int2String(Int64 intValue)
        {
            String strValue = String.Empty;
            String strArr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            while (intValue != 0)
            {
                int intRem = 0;
                intRem = (int)(intValue % strArr.Length);
                strValue = strArr.Substring(intRem, 1) + strValue;
                intValue /= strArr.Length;
            }
            return strValue;
        }
        private string GetString(byte[] b)
        {
            try
            {
                return ASCIIEncoding.Default.GetString(b).Trim();
            }
            catch (Exception ex)
            {

            }
            return "";
        }
    }
}
