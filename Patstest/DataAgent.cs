using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Data;
//using MySql.Data.MySqlClient;
using pfcf.tool.server;

using pfcf.tool;
using pfcf.trade;
using pfcf.pats;
using System.Xml;
using System.Data.SqlClient;

namespace FTrade
{


    public class DataAgent
    {
        public static string TradeDate = "";
        public static string BASEPATH = "";
        public static string CURRENTPATH = "";



        static SettingProvider _SettingProvider;

        internal static SettingProvider SettingProvider
        {
            get { return _SettingProvider; }
            set { _SettingProvider = value; }
        }
        private static Dictionary _DEFAULTProvider;

        public static Dictionary DEFAULTProvider
        {
            get { return _DEFAULTProvider; }
            set { _DEFAULTProvider = value; }
        }
        private SAEAServer _SAEAServer;
        public SAEAServer SAEAServer { get => _SAEAServer; set => _SAEAServer = value; }
        private AccountCore _AccountCore;
        public AccountCore AccountCore { get => _AccountCore; set => _AccountCore = value; }
        public Patsystem Pats { get => _Pats; set => _Pats = value; }

        public static LogProvider _LM;


        private TradeProvider _t;

        Patsystem _Pats;

        UIProvider _UIProvider;

        DataProvider _ExchangeData;
        DataProvider _ContractData;
        DataProvider _CommodityData;

        Dictionary<string, ExtendedContract> _Contract2ID;

        Dictionary<string, ExtendedContract> _ID2Contract;

        bool _BufferLog = false;

        public DataAgent(UIProvider UIProvider)
        {
            _UIProvider = UIProvider;

        }
        public void Dispose()
        {


        }
        ~DataAgent()
        {
            Dispose();
        }

        public void init()
        {
            DataAgent.BASEPATH = AppDomain.CurrentDomain.BaseDirectory.ToString().TrimEnd() + "log\\";
            _LM = new LogProvider();

            _SettingProvider = new SettingProvider(AppDomain.CurrentDomain.BaseDirectory.ToString().TrimEnd() + "Patstest.cfg");
            _DEFAULTProvider = _SettingProvider.Get("DEFAULT").First.Value;


            int begin = int.Parse(_DEFAULTProvider.GetString("ClearDataTime"));
            int now = int.Parse(DateTime.Now.ToString("HHmm"));




            if (begin > now)
            {
                CURRENTPATH = DataAgent.TradeDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            }
            else
            {
                CURRENTPATH = DataAgent.TradeDate = DateTime.Now.ToString("yyyyMMdd");
            }


        }




        public static void MsgAlert(string msg)
        {
            try
            {



            }
            catch (Exception ex)
            {
            }
        }


        public DateTime GetDate(string time)
        {


            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day
                , int.Parse(time.PadRight(8, '0').Substring(0, 2))
                , int.Parse(time.PadRight(8, '0').Substring(2, 2))
                , int.Parse(time.PadRight(8, '0').Substring(4, 2)));
            return date;
        }


        public void Start()
        {
            try
            {

                init();
                _LM.AddWriter("DataAgentLog", new FileProvider(DataAgent.BASEPATH + CURRENTPATH, "DataAgentLog"));


                _LM.AddWriter("PatsystemLog", new FileProvider(DataAgent.BASEPATH + CURRENTPATH, "PatsystemLog"));

                _LM.AddWriter("PriceTigger", new FileProvider(DataAgent.BASEPATH + CURRENTPATH, "PriceTigger"));

                _LM.AddWriter("TickerTigger", new FileProvider(DataAgent.BASEPATH + CURRENTPATH, "TickerTigger"));

                _LM.AddWriter("SettlementTigger", new FileProvider(DataAgent.BASEPATH + CURRENTPATH, "SettlementTigger"));



                _ExchangeData = new DataProvider("Exchange", DataAgent.BASEPATH + CURRENTPATH);
                _ExchangeData.RealKeepFile = false;
                _ContractData = new DataProvider("Contract", DataAgent.BASEPATH + CURRENTPATH);
                _ContractData.RealKeepFile = false;
                _CommodityData = new DataProvider("Commodity", DataAgent.BASEPATH + CURRENTPATH);
                _CommodityData.RealKeepFile = false;

                _BufferLog = (_DEFAULTProvider.GetString("BufferLog") == "Y" ? true : false);

                try
                {
                    _UIProvider.Add("SAEAServer"
                                         , _DEFAULTProvider.GetString("SAEAServerIP")
                                         , _DEFAULTProvider.GetString("SAEAServerPort"));
                    _SAEAServer = new SAEAServer(8196, 8196, 2000);

                    _SAEAServer.backlog = 25;
                    _SAEAServer.Actived += new ActivedEventHandler(_SAEAServer_Actived);
                    _SAEAServer.Disconnected += new DisconnectedEventHandler(_SAEAServer_Disconnected);
                    _SAEAServer.Connected += new ConnectedEventHandler(_SAEAServer_Connected);
                    _SAEAServer.Received += new ReceiveEventHandler(_SAEAServer_Received);
                    _SAEAServer.Error += new SAEAServer.ErrorHandler(_SAEAServer_Error);

                    _SAEAServer.Listen(_DEFAULTProvider.GetString("SAEAServerIP")
                                     , _DEFAULTProvider.GetInt("SAEAServerPort"));


                }
                catch (Exception ex)
                {
                    _LM.Log("DataAgentLog", "SAEAServer:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());

                }


                _t = new TradeProvider(this);





                Dictionary PATSYSTEMSet = _SettingProvider.Get("PATSYSTEM").First.Value;

                Pats = new Patsystem(PATSYSTEMSet.GetString("Username")
                                               , PATSYSTEMSet.GetString("Password")
                                                , PATSYSTEMSet.GetString("ApplicationID")
                                                , PATSYSTEMSet.GetString("LicenseID")
                                               , PATSYSTEMSet.GetString("HostIP")
                                               , PATSYSTEMSet.GetString("HostPort")
                                               , PATSYSTEMSet.GetString("PriceFeedIP")
                                              , PATSYSTEMSet.GetString("PriceFeedPort")
                                              , DataAgent.BASEPATH + DataAgent.CURRENTPATH + "\\", PATSYSTEMSet.GetString("TestMode") == "Y" ? true : false
                                              );
                _UIProvider.Add("Patsystem_Trade", PATSYSTEMSet.GetString("HostIP")
                               , PATSYSTEMSet.GetString("HostPort"));
                _UIProvider.Add("Patsystem_Price", PATSYSTEMSet.GetString("PriceFeedIP")
                               , PATSYSTEMSet.GetString("PriceFeedPort"));
                Pats.Log += _Pats_Log;

                Pats.Error += _Pats_Error;
                Pats.PriceConnectedStatus += _Pats_PriceConnectedStatus;
                Pats.PriceTigger += _Pats_PriceTigger;
                Pats.SettlementTigger += _Pats_SettlementTigger;
                Pats.TickerTigger += _Pats_TickerTigger;
                Pats.DoGetPatsCommodity += _Pats_DoGetPatsCommodity;
                Pats.DoGetPatsContract += _Pats_DoGetPatsContract;
                Pats.DoGetPatsExchange += _Pats_DoGetPatsExchange;
                Pats.OrderReply += Pats_OrderReply;
                Pats.FillReply += Pats_FillReply;

                Pats.Go();
                _UIProvider.Add("連線數", "", "");
                _UIProvider.Change("連線數", _SAEAServer.NumberOfConnectedSockets.ToString(), "");

                _Contract2ID = new Dictionary<string, ExtendedContract>();

                _ID2Contract = new Dictionary<string, ExtendedContract>();


            }
            catch (Exception ex)
            {
                _LM.Log("DataAgentLog", "DataAgent:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
            }

        }


        private void _Pats_DoGetPatsExchange(PatsNative.ExchangeStruct val)
        {
            string key = val.Name;
            string data = val.Name + '\x0001' + val.QueryEnabled.ToString()
                    + '\x0001' + val.QueryEnabled.ToString()
   + '\x0001' + val.AmendEnabled.ToString()
   + '\x0001' + val.Strategy.ToString()
   + '\x0001' + val.CustomDecs.ToString()
   + '\x0001' + val.Decimals.ToString()
   + '\x0001' + val.TicketType.ToString()
   + '\x0001' + val.RFQA.ToString()
 + '\x0001' + val.RFQT.ToString()
 + '\x0001' + val.EnableBlock.ToString()
 + '\x0001' + val.EnableBasis.ToString()
 + '\x0001' + val.EnableAA.ToString()
 + '\x0001' + val.EnableCross.ToString()
 + '\x0001' + val.GTStatus.ToString()
;
            _ExchangeData.Add(key, data);
        }

        private void _Pats_DoGetPatsContract(PatsNative.ExtendedContractStruct val)
        {
            if (val.NumberOfLegs == 1)
            {
                string key = val.ExchangeName + val.ContractName + val.ContractDate;
                string data = val.ExchangeName
                + '\x0001' + val.ContractName
                + '\x0001' + val.ContractDate
                + '\x0001' + val.ExpiryDate.ToString()
                + '\x0001' + val.LastTradeDate.ToString()
                + '\x0001' + val.NumberOfLegs.ToString()
                + '\x0001' + val.TicksPerPoint.ToString()
                + '\x0001' + val.TickSize.ToString()
                + '\x0001' + val.Tradable.ToString()
                + '\x0001' + val.GTStatus.ToString()
                + '\x0001' + val.Margin.ToString()
                + '\x0001' + val.ESATemplate.ToString()
                + '\x0001' + val.MarketRef.ToString()
                + '\x0001' + val.lnExchangeName.ToString()
                + '\x0001' + val.lnContractName.ToString()
                + '\x0001' + val.lnContractDate.ToString()


       + '\x0001' + val.ExternalID[0].oo1.Trim()

       + '\x0001' + val.ExternalID[0].oo2

       + '\x0001' + val.ExternalID[0].oo3.Trim()

       + '\x0001' + val.ExternalID[0].oo4.Trim()
  + '\x0001' + val.ExternalID[0].oo5.Trim()

; ;


                ;
                _ContractData.Add(key, data);

                _Contract2ID.Add(key, new ExtendedContract(val));




            }
        }

        private void _Pats_DoGetPatsCommodity(PatsNative.CommodityStruct val)
        {
            string key = val.ExchangeName + val.ContractName;
            string data = val.ExchangeName
+ '\x0001' + val.ContractName
+ '\x0001' + val.Currency.ToString()
+ '\x0001' + val.Group.ToString()
+ '\x0001' + val.OnePoint.ToString()
+ '\x0001' + val.TicksPerPoint.ToString()
+ '\x0001' + val.TickSize.ToString()
+ '\x0001' + val.GTStatus.ToString()

                    ;
            _CommodityData.Add(key, data);
        }

        private void _Pats_SettlementTigger(PatsNative.SettlementPriceStruct val)
        {
            try
            {
                string data = "";
                string key = val.ExchangeName + val.ContractName + val.ContractDate;
                ExtendedContract Contract = null;
                if (_Contract2ID.TryGetValue(key, out Contract))
                {
                    data = "ST" + '\x0001' + Contract.Symbol + '\x0001' + Contract.Maturity + '\x0001' + Contract.Strike + '\x0001' + Contract.type;
                }
                data += '\x0001' + val.SettlementType.ToString() + '\x0001' + val.Date + val.Time + '\x0001' + val.Price;
                _LM.Log("SettlementTigger", data);
            }
            catch (Exception ex)
            {
                _LM.Log("DataAgentLog", "_Pats_SettlementTigger:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
            }
        }

        private void _Pats_PriceTigger(string ExchangeName, string ContractName, string ContractDate, PatsNative.PriceStruct ps)
        {
            try
            {
                string value = "";
                string key = ExchangeName + ContractName + ContractDate;
                ExtendedContract Contract = null;
                if (_Contract2ID.TryGetValue(key, out Contract))
                {
                    value = "PT" + '\x0001' + Contract.Symbol + '\x0001' + Contract.Maturity + '\x0001' + Contract.Strike + '\x0001' + Contract.type;
                }
                value += ps.Total.Volume + "\x0001";
                value += ps.Last0.Price + "\x0001"
                + ps.Last0.Volume + "\x0001"
                 + ps.Last0.Hour.ToString() + ps.Last0.Minute.ToString() + ps.Last0.Second.ToString() + "\x0001"
                + ps.Last1.Price + "\x0001"
                + ps.Last1.Volume + "\x0001"
                + ps.Last1.Hour.ToString() + ps.Last1.Minute.ToString() + ps.Last1.Second.ToString() + "\x0001"
                + ps.BidDOM0.Price + "\x0001" + ps.BidDOM0.Volume + "\x0001"
                + ps.BidDOM1.Price + "\x0001" + ps.BidDOM1.Volume + "\x0001"
                + ps.BidDOM2.Price + "\x0001" + ps.BidDOM2.Volume + "\x0001"
                + ps.BidDOM3.Price + "\x0001" + ps.BidDOM3.Volume + "\x0001"
                + ps.BidDOM4.Price + "\x0001" + ps.BidDOM4.Volume + "\x0001"
                + ps.BidDOM5.Price + "\x0001" + ps.BidDOM5.Volume + "\x0001"
                + ps.BidDOM6.Price + "\x0001" + ps.BidDOM6.Volume + "\x0001"
                + ps.BidDOM7.Price + "\x0001" + ps.BidDOM7.Volume + "\x0001"
                + ps.BidDOM8.Price + "\x0001" + ps.BidDOM8.Volume + "\x0001"
                + ps.BidDOM9.Price + "\x0001" + ps.BidDOM9.Volume + "\x0001"

                + ps.OfferDOM0.Price + "\x0001" + ps.OfferDOM0.Volume + "\x0001"
                + ps.OfferDOM1.Price + "\x0001" + ps.OfferDOM1.Volume + "\x0001"
                + ps.OfferDOM2.Price + "\x0001" + ps.OfferDOM2.Volume + "\x0001"
                + ps.OfferDOM3.Price + "\x0001" + ps.OfferDOM3.Volume + "\x0001"
                + ps.OfferDOM4.Price + "\x0001" + ps.OfferDOM4.Volume + "\x0001"
                + ps.OfferDOM5.Price + "\x0001" + ps.OfferDOM5.Volume + "\x0001"
                + ps.OfferDOM6.Price + "\x0001" + ps.OfferDOM6.Volume + "\x0001"
                + ps.OfferDOM7.Price + "\x0001" + ps.OfferDOM7.Volume + "\x0001"
                + ps.OfferDOM8.Price + "\x0001" + ps.OfferDOM8.Volume + "\x0001"
                + ps.OfferDOM9.Price + "\x0001" + ps.OfferDOM9.Volume + "\x0001"

                + ps.ImpliedBid.Price + "\x0001"
                + ps.ImpliedBid.Volume + "\x0001"
              + ps.ImpliedOffer.Price + "\x0001"
                + ps.ImpliedOffer.Volume + "\x0001"
                + ps.High.Price + "\x0001"
                 + ps.Low.Price + "\x0001"
                 + ps.Opening.Price + "\x0001"
                 + ps.Closing.Price + "\x0001"

           + ps.pvCurrStl.Price + "\x0001"
           + ps.pvNewStl.Price + "\x0001"
;

                _LM.Log("PriceTigger", value);

                MainForm.form.Invoke(new DisplayMarketDatahandler(DisplayMarketData), ExchangeName, Contract.Symbol, Contract.Maturity, ps);
            }
            catch (Exception ex)
            {
                _LM.Log("DataAgentLog", "_Pats_PriceTigger:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
            }

        }
        private void _Pats_TickerTigger(PatsNative.TickerUpdStruct val)
        {
            try
            {
                string data = "";
                string key = val.ExchangeName + val.ContractName + val.ContractDate;
                ExtendedContract Contract = null;
                if (_Contract2ID.TryGetValue(key, out Contract))
                {
                    data = "TT" + '\x0001' + Contract.Symbol + '\x0001' + Contract.Maturity + '\x0001' + Contract.Strike + '\x0001' + Contract.type;
                }
                data += '\x0001' + val.BidPrice.ToString() + '\x0001' + val.BidVolume
+ val.OfferPrice + '\x0001' + val.OfferVolume
     + '\x0001' + val.LastPrice + '\x0001' + val.LastVolume
+ '\x0001' + val.Bid.ToString() + val.Offer.ToString() + val.Last.ToString()

;
                _LM.Log("TickerTigger", data);
            }
            catch (Exception ex)
            {
                _LM.Log("DataAgentLog", "TickerTigger:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
            }



        }
        private void _Pats_PriceConnectedStatus(bool val)
        {
            if (val)
            {
                _UIProvider.Change("Patsystem_Price", "Connected", "");

                _LM.Log("DataAgentLog", "DoExchange begin");
                Pats.DoExchange();
                _LM.Log("DataAgentLog", "DoExchange end");
                _LM.Log("DataAgentLog", "DoCommdity begin");
                Pats.DoCommdity();
                _LM.Log("DataAgentLog", "DoCommdity end");
                _LM.Log("DataAgentLog", "DoContract begin");
                Pats.DoContract();
                _LM.Log("DataAgentLog", "DoContract end");
                _LM.Log("DataAgentLog", "SubscribeAll begin");
              //  Pats.SubscribeAll();
                _LM.Log("DataAgentLog", "SubscribeAll end");
            }
            else
            {
                _UIProvider.Change("Patsystem_Price", "Disconnected", "");
            }
        }

        private void _Pats_Error(string val)
        {
            _LM.Log("PatsystemLog", val);

        }

        private void _Pats_Log(string val)
        {
            _LM.Log("PatsystemLog", val);
        }






        //===================SAEA===================
        void _SAEAServer_Actived()
        {
            if (_SAEAServer.Active)
            {
                _UIProvider.Change("SAEAServer", "listen", "");

            }
            else
            {
                _UIProvider.Change("SAEAServer", "Error", "");
            }
        }
        void _SAEAServer_Error(string msg)
        {

            _LM.Log("DataAgentLog", "SAEAServer_Error_" + msg);

        }
        void _SAEAServer_Disconnected(DisconnectedEventArgs Saea)
        {
            try
            {
                //UserInfo u = _t.AccountCore.FindUserInfo(Saea.SocketAsyncEventArgs);
                //if (u != null)
                //{
                //    string[] arrPutIPLogoutCmd = new string[1];
                //    arrPutIPLogoutCmd[0] = "UPDATE  [dbo].[TRADEIPLOG]"
                //      + "SET [LOGOUTTIME] ='" + DateTime.Now.ToString("HH:mm:ss") + "',[STATUS] = ''  "
                //    + "WHERE USERID='" + u.UserId + "' and IP='" + Saea.SocketAsyncEventArgs.AcceptSocket.RemoteEndPoint.ToString().Split(':')[0] + "' and PORT='" + Saea.SocketAsyncEventArgs.AcceptSocket.RemoteEndPoint.ToString().Split(':')[1] + "' and  LOGINTIME ='" + u.LoginTime + "'";
                //    _t.SaveDB.PutData2Queue(arrPutIPLogoutCmd);
                //}
                // this._AccountCore.RemoveSAEA(Saea.SocketAsyncEventArgs);
                DataToken Token = Saea.SocketAsyncEventArgs.UserToken as DataToken;

                // string RemoteEndPoint = Token.RemoteEndPoint;//(((System.Net.IPEndPoint)Saea.Socket.RemoteEndPoint).Address.ToString());
                //  string RemoteIP = RemoteEndPoint.Split(':')[0];
                // this.T._ActiveReplyCore.RemoveSAEA(Saea.SocketAsyncEventArgs);

                _UIProvider.displayStatusMsg(Token.Ip + ":" + Token.Port + " 離開!");
                _UIProvider.displayRemoveConnections(Token.Ip + ":" + Token.Port);

                _UIProvider.Change("連線數", _SAEAServer.NumberOfConnectedSockets.ToString(), "");

                _LM.Log("SAEAServerLogRCV" + Token.Ip, "[" + Token.Port + "] disconnected");
                _LM.Log("SAEAServerLogSend" + Token.Ip, "[" + Token.Port + "] disconnected");

            }

            catch (Exception ex)
            {
                _LM.Log("DataAgentLog", "_SAEAServer_Disconnected" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
            }
        }
        void _SAEAServer_Connected(ConnectedEventArgs Saea)
        {
            try
            {
                DataToken Token = Saea.SocketAsyncEventArgs.UserToken as DataToken;
                _UIProvider.displayAddConnections(Token.Ip + ":" + Token.Port);
                _UIProvider.Change("連線數", _SAEAServer.NumberOfConnectedSockets.ToString(), "");

                if (!_LM.checkFileWriter("SAEAServerLogRCV" + Token.Ip))
                {
                    _LM.AddWriter("SAEAServerLogRCV" + Token.Ip, new FileProvider(DataAgent.BASEPATH + CURRENTPATH + @"\connections", "SAEAServerLogRCV" + Token.Ip));
                    _LM.AddWriter("SAEAServerLogSend" + Token.Ip, new FileProvider(DataAgent.BASEPATH + CURRENTPATH + @"\connections", "SAEAServerLogSend" + Token.Ip));
                }
                _UIProvider.displayStatusMsg(Token.Ip + ":" + Token.Port + " 登入!");
                _LM.Log("SAEAServerLogRCV" + Token.Ip, "[" + Token.Port + "] connected");
                _LM.Log("SAEAServerLogSend" + Token.Ip, "[" + Token.Port + "] connected");


            }
            catch (Exception ex)
            {
                _LM.Log("DataAgentLog", "_SAEAServer_Connected_" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
            }
        }
        void _SAEAServer_Received(ReceiveEventArgs Saea)
        {

            try
            {
                _t.ParseDDSCData(Saea.SocketAsyncEventArgs, Saea.ReceiveBytes);
            }
            catch (Exception ex)
            {
                _LM.Log("DataAgentLog", "_SAEAServer_Received:" + ex.Message + ex.StackTrace.ToString() + ex.Source.ToString());
            }
            // _SAEAServerLog.WriteLine(Saea.ReceiveBytes);
        }



        public void Stop()
        {


            if (_ExchangeData != null)
                _ExchangeData.close();
            if (_ContractData != null)
                _ContractData.close();
            if (_CommodityData != null)
                _CommodityData.close();
            if (Pats != null)
            {
                Pats.Log -= _Pats_Log;

                Pats.Error -= _Pats_Error;
                Pats.PriceConnectedStatus -= _Pats_PriceConnectedStatus;
                Pats.PriceTigger -= _Pats_PriceTigger;
                Pats.SettlementTigger -= _Pats_SettlementTigger;
                Pats.DoGetPatsCommodity -= _Pats_DoGetPatsCommodity;
                Pats.DoGetPatsContract -= _Pats_DoGetPatsContract;
                Pats.DoGetPatsExchange -= _Pats_DoGetPatsExchange;
                Pats.OrderReply -= Pats_OrderReply;
                Pats.FillReply -= Pats_FillReply;
                Pats.Dispose();
            }


            if (_SAEAServer != null)
            {
                _SAEAServer.Actived -= new ActivedEventHandler(_SAEAServer_Actived);
                _SAEAServer.Disconnected -= new DisconnectedEventHandler(_SAEAServer_Disconnected);
                _SAEAServer.Connected -= new ConnectedEventHandler(_SAEAServer_Connected);
                _SAEAServer.Received -= new ReceiveEventHandler(_SAEAServer_Received);
                _SAEAServer.Error -= new SAEAServer.ErrorHandler(_SAEAServer_Error);
                _SAEAServer.close();
            }


            if (_t != null)
                _t.Dispose();

            //if (_AccountCore != null)
            //    _AccountCore.Close();
            if (_LM != null)
                _LM.Dispose();

        }








        delegate void DisplayMarketDatahandler(string ExchangeName, string Symbol, string Maturity, PatsNative.PriceStruct obj);
        void DisplayMarketData(string ExchangeName, string Symbol, string Maturity, PatsNative.PriceStruct obj)
        {
            bool addflag = false;
            string[] key = { ExchangeName, Symbol, Maturity };
            DataRow dr = MainForm.form._dtMarket.Rows.Find(key);
            if (dr == null) addflag = true;
            if (addflag)
            {
                dr = MainForm.form._dtMarket.NewRow();
            }

            dr.BeginEdit();
            dr["SecurityExchange"] = ExchangeName;
            dr["SecurityType"] = "";
            dr["Symbol"] = Symbol;
            dr["MaturityMonthYear"] = Maturity;
            dr["PutOrCall"] = "";
            dr["StrikePrice"] = "";

            dr["LastPx"] = obj.Last0.Price;
            dr["LastSize"] = obj.Last0.Volume;
            dr["TotalVolume"] = obj.Total.Volume;

            dr["Openingprice"] = obj.Opening.Price;

            dr["Closingprice"] = obj.Closing.Price;

            dr["Settlprice"] = obj.pvCurrStl.Price;


            dr["Referenceprice"] = obj.ReferencePrice.Price;

            dr["Dailyhigh"] = obj.High.Price;

            dr["Dailylow"] = obj.Low.Price;

            dr["BidPx"] = obj.Bid.Price;
            dr["BidSize"] = obj.Bid.Volume;

            dr["Bid0Px"] = obj.BidDOM0.Price;
            dr["Bid0Size"] = obj.BidDOM0.Volume;

            dr["Bid1Px"] = obj.BidDOM1.Price;
            dr["Bid1Size"] = obj.BidDOM1.Volume;

            dr["Bid2Px"] = obj.BidDOM2.Price;
            dr["Bid2Size"] = obj.BidDOM2.Volume;


            dr["Bid3Px"] = obj.BidDOM3.Price;
            dr["Bid3Size"] = obj.BidDOM3.Volume;

            dr["Bid4Px"] = obj.BidDOM4.Price;
            dr["Bid4Size"] = obj.BidDOM4.Volume;

            dr["Bid5Px"] = obj.BidDOM5.Price;
            dr["Bid5Size"] = obj.BidDOM5.Volume;

            dr["Bid5Px"] = obj.BidDOM5.Price;
            dr["Bid5Size"] = obj.BidDOM5.Volume;

            dr["Bid6Px"] = obj.BidDOM6.Price;
            dr["Bid6Size"] = obj.BidDOM6.Volume;

            dr["Bid7Px"] = obj.BidDOM7.Price;
            dr["Bid7Size"] = obj.BidDOM7.Volume;

            dr["Bid8Px"] = obj.BidDOM8.Price;
            dr["Bid8Size"] = obj.BidDOM8.Volume;
            dr["Bid9Px"] = obj.BidDOM9.Price;
            dr["Bid9Size"] = obj.BidDOM9.Volume;

            dr["OfferPx"] = obj.Offer.Price;
            dr["OfferSize"] = obj.Offer.Volume;

            dr["Offer1Px"] = obj.OfferDOM1.Price;
            dr["Offer1Size"] = obj.OfferDOM1.Volume;

            dr["Offer2Px"] = obj.OfferDOM2.Price;
            dr["Offer2Size"] = obj.OfferDOM2.Volume;


            dr["Offer3Px"] = obj.OfferDOM3.Price;
            dr["Offer3Size"] = obj.OfferDOM3.Volume;

            dr["Offer4Px"] = obj.OfferDOM4.Price;
            dr["Offer4Size"] = obj.OfferDOM4.Volume;

            dr["Offer5Px"] = obj.OfferDOM5.Price;
            dr["Offer5Size"] = obj.OfferDOM5.Volume;

            dr["Offer6Px"] = obj.OfferDOM6.Price;
            dr["Offer6Size"] = obj.OfferDOM6.Volume;
            dr["Offer7Px"] = obj.OfferDOM7.Price;
            dr["Offer7Size"] = obj.OfferDOM7.Volume;
            dr["Offer8Px"] = obj.OfferDOM8.Price;
            dr["Offer8Size"] = obj.OfferDOM8.Volume;
            dr["Offer9Px"] = obj.OfferDOM9.Price;
            dr["Offer9Size"] = obj.OfferDOM9.Volume;


            dr.EndEdit();
            if (addflag)
            {
                MainForm.form._dtMarket.Rows.Add(dr);

            }
        }




        private void Pats_FillReply(PatsNative.FillStruct obj)
        {
            DataRow drM = MainForm.form._dtMatch. NewRow();
            drM["ExecType"] = "";
            drM["SecurityExchange"] = obj.ExchangeName;
            drM["SecurityType"] = "";
            drM["Symbol"] = obj.ContractName;
            drM["MaturityMonthYear"] = obj.ContractDate;
            drM["PutOrCall"] = "";
            drM["StrikePrice"] = "";
            drM["Side"] = obj.BuyOrSell;
            drM["OrderID"] = obj.OrderID;
            drM["LastShares"] = obj.Lots;
            drM["LastPx"] = obj.Price;

            drM["ClOrdID"] = "";
            drM["ExecID"] = obj.ExchOrderId;
            drM["ExecRefID"] = "";

            drM["ExecTransType"] = "";

            drM["OrdStatus"] = "";
            drM["SecurityType2"] = "";
            drM["Symbol2"] = "";
            drM["MaturityMonthYear2"] = "";
            drM["PutOrCall2"] = "";
            drM["StrikePrice2"] = "";
            drM["Side1"] = "";
            drM["Side2"] = "";
            drM["TransactTime"] = "";
            drM["LocalTransactTime"] = "";
            drM["LegNum"] = obj.Leg;
            MainForm.form._dtMatch.Rows.Add(drM);
        }
        string[] patstatus = { "", "ptQueued", "ptSent", "ptWorking", "ptRejected"
, "ptCancelled", "ptBalCancelled" , "ptPartFilled" , "ptFilled" , "ptCancelPending" , "ptAmendPending" , "ptUnconfirmedFilled" 
, "ptUnconfirmedPartFilled", "ptHeldOrder", "ptCancelHeldOrder" , "ptTransferred"  , "ptExternalCancelled"};
        private void Pats_OrderReply(PatsNative.OrderDetailStruct obj)
        {
            DataRow dr = MainForm.form._dtReply.Rows.Find(obj.OrderID); 

            bool addflag = false;

            if (dr == null)
            {
                addflag = true;
                dr = MainForm.form._dtReply.NewRow();
            }
            dr.BeginEdit();
            dr["ExecType"] ="";
            dr["SecurityExchange"] = obj.ExchangeName;
            dr["SecurityType"] = "";
            dr["Symbol"] = obj.ContractName;
            dr["MaturityMonthYear"] = obj.ContractDate;
            dr["PutOrCall"] = "";
            dr["StrikePrice"] = "";

            dr["OrderID"] = obj.OrderID;
            dr["OrdType"] = obj.OrderType;
            dr["Side"] = obj.BuyOrSell;
            dr["Price"] = obj.Price;
            dr["OrderQty"] = obj.Lots;
            dr["LeavesQty"] = obj.NoOfFills;
            dr["CumQty"] = obj.AmountFilled;
            dr["Openclose"] = obj.OpenOrClose;
            dr["TimeInForce"] = "";
            dr["ClOrdID"] = "";
            dr["OrigClOrdID"] = "";
            dr["StopPx"] = obj.Price2;
            dr["ExecTransType"] = "";

            dr["OrdStatus"] = patstatus[obj.Status];
            dr["SecurityType2"] = "";
            dr["Symbol2"] = "";
            dr["MaturityMonthYear2"] = "";
            dr["PutOrCall2"] = "";
            dr["StrikePrice2"] = "";
            dr["Side1"] = "";
            dr["Side2"] = "";
            dr["TransactTime"] = obj.EntryTime;
            dr["LocalTransactTime"] = "";
            dr["ExpireDate"] = "";
            dr["Text"] = obj.NonExecReason;


            dr.EndEdit();
            if (addflag)
            {
                MainForm.form._dtReply.Rows.Add(dr);

            }
        }


    }



    public class ExtendedContract
    {
        public ExtendedContract(PatsNative.ExtendedContractStruct val)
        {
            ContractName = val.ContractName;
            ContractDate = val.ContractDate;
            ExchangeName = val.ExchangeName;
            ExpiryDate = val.ExpiryDate;
            NumberOfLegs = val.NumberOfLegs;
            TicksPerPoint = val.TicksPerPoint;
            TickSize = val.TickSize;
            Tradable = val.Tradable.ToString();
            GTStatus = val.GTStatus;
            Margin = val.Margin;
            ESATemplate = val.ESATemplate.ToString();
            MarketRef = val.MarketRef;
            lnExchangeName = val.lnExchangeName;
            lnContractName = val.lnContractName;
            lnContractDate = val.lnContractDate;
            type = val.ExternalID[0].oo1.Trim();
            Symbol = val.ExternalID[0].oo2;
            Maturity = val.LastTradeDate.Substring(0, 6);
            Strike = val.ExternalID[0].oo4.Trim();



        }

        public string ContractName = "";

        public string ContractDate = "";

        public string ExchangeName = "";

        public string ExpiryDate = "";
        public int NumberOfLegs = 0;
        public int TicksPerPoint = 0;

        public string TickSize = "";
        public string Tradable = "";
        public int GTStatus = 0;

        public string Margin = "";
        public string ESATemplate = "";

        public string MarketRef = "";

        public string lnExchangeName = "";

        public string lnContractName = "";

        public string lnContractDate = "";

        public string type = "";
        public string Symbol = "";
        public string Maturity = "";
        public string Strike = "";
    }
}
