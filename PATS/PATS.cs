using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
namespace com.ddsc.Pats
{

    public struct SettlementPriceItem
    {
        public string ExchangeName;
        public string ContractName;
        public string ContractDate;
        public int SettlementType;
        public string Price;
        public string Time;
        public string Date;
    }


    public class Patsystem
    {
        LogonStruct _LogonDetails;
        public delegate void LogHandler(string val);
        public delegate void ErrorHandler(string val);
        public delegate void PriceConnectedStatusHandler(bool val);
        public delegate void DoGetContractHandler(ExtendedContractStruct val);
        public delegate void DoGetExchangeHandler(ExchangeStruct val);
        public delegate void DoGetCommodityHandler(CommodityStruct val);


        public delegate void PriceTiggerHandler(string ExchangeName, string ContractName, string ContractDate, PriceStruct val);
        public delegate void TickerTiggerHandler(TickerUpdStruct val);
        public delegate void SettlementTiggerHandler(SettlementPriceStruct val);

        public event LogHandler Log;
        public event ErrorHandler Error;
        public event PriceConnectedStatusHandler PriceConnectedStatus;
        public event PriceTiggerHandler PriceTigger;
        public event TickerTiggerHandler TickerTigger;
        public event SettlementTiggerHandler SettlementTigger;

        public event DoGetContractHandler DoGetPatsContract;
        public event DoGetExchangeHandler DoGetPatsExchange;
        public event DoGetCommodityHandler DoGetPatsCommodity;

        private void OnLog(string val)
        {
            if (Log != null)
                Log(val);
        }
        private void OnError(string val)
        {
            if (Error != null)
                Error(val);
        }
        private void OnPriceConnectedStatus(bool val)
        {
            if (PriceConnectedStatus != null)
                PriceConnectedStatus(val);
        }
        private void OnPriceTigger(string ExchangeName, string ContractName, string ContractDate, PriceStruct val)
        {
            if (PriceTigger != null)
                PriceTigger(ExchangeName, ContractName, ContractDate, val);
        }
        private void OnTickerTigger(TickerUpdStruct val)
        {
            if (TickerTigger != null)
                TickerTigger(val);
        }
        private void OnSettlementTigger(SettlementPriceStruct val)
        {
            if (SettlementTigger != null)
                SettlementTigger(val);
        }

        QueuePoolByLock<bool> PriceLink;
        public Patsystem(string Username, string Password
                     , string ApplicationID, string LicenseID
                     , string HostIP, string HostPort, string PriceFeedIP, string PriceFeedPort,string path)
        {

            OnLog("初始 patsystem start");
            int ret;
            ret = ptSetClientPath(path);
            ret = ptInitialise(ptClient, "v2.8.3", ApplicationID, "", LicenseID, false);
           
         
            ret = ptSetInternetUser('0');
            ret = ptSetHostAddress(HostIP, HostPort);
            ret = ptSetPriceAddress(PriceFeedIP, PriceFeedPort);
            ret = ptEnable(0);
          
            LogonStatusHandler = new ProcAddr(LogonStatus);
            ForcedLogoutHandler = new ProcAddr(ForcedLogout);
            DataDLCompleteHandler = new ProcAddr(DataDLComplete);
            HostLinkProcAddrHandler = new LinkProcAddr(HostLinkStateChange);
            PriceLinkProcAddrHandler = new LinkProcAddr(PriceLinkStateChange);
            PriceProcAddrHandler = new PriceProcAddr(PriceUpdate);
            TickerUpdateProcAddrHandler = new TickerUpdateProcAddr(Ticker);
            SettlementProcAddrHandler = new SettlementProcAddr(SettlementPrice);
            ret = ptRegisterLinkStateCallback(ptHostLinkStateChange, HostLinkProcAddrHandler);
            ret = ptRegisterLinkStateCallback(ptPriceLinkStateChange, PriceLinkProcAddrHandler);

            ret = ptRegisterCallback(ptLogonStatus, LogonStatusHandler);

            ret = ptRegisterCallback(ptForcedLogout, ForcedLogoutHandler);
            ret = ptRegisterCallback(ptDataDLComplete, DataDLCompleteHandler);
            ret = ptRegisterSettlementCallback(ptSettlementCallback, SettlementProcAddrHandler);
            ret = ptRegisterTickerCallback(ptTickerUpdate, TickerUpdateProcAddrHandler);
            ret = ptRegisterPriceCallback(ptPriceUpdate, PriceProcAddrHandler);

            _LogonDetails = new LogonStruct();
            /* strcpy( LogonDetails.UserID,"APPID_007");
             strcpy( LogonDetails.Password,"NC7Ruwk8Ty");*/
            _LogonDetails.UserID = Username.PadRight(256, '\0');
            _LogonDetails.Password = Password.PadRight(256, '\0');
            _LogonDetails.NewPassword = "".PadRight(256, '\0');
            _LogonDetails.Reports = 'N';
            _LogonDetails.Reset = 'N';
            _LogonDetails.OTPassword = "".PadRight(21, '\0');
            PriceLink = new QueuePoolByLock<bool>(1);
            PriceLink.ParameterExcute += new QueuePoolByLock<bool>.ParameterHandler(PriceLink_ParameterExcute);
            PriceLink.Go();
            OnLog("初始 patsystem end");
        }

        void PriceLink_ParameterExcute(bool ff)
        {
            if (ff)
            {
                OnLog("PriceLink 連線成功");
                OnPriceConnectedStatus(true);
            }
            else
            {
                OnLog("PriceLink 連線失敗");
                OnPriceConnectedStatus(false);
            }
        }
        ~Patsystem()
        {
            Dispose();
        }
        public void Dispose()
        {

            if (PriceLink != null)
            {
                PriceLink.ParameterExcute -= new QueuePoolByLock<bool>.ParameterHandler(PriceLink_ParameterExcute);
                PriceLink.Dispose();
            }
        }

        public void Go()
        {
            OnLog("Ready start");
            int ret = 0;
            ret = ptReady();
            if (ret != 0)
            {
                OnError("ptReady ret =" + ret);
            }
            OnLog("Ready end");
        }
  
        public void DoContract()
        {
            int ContractCount = 0;
            ptCountContracts(ref ContractCount);
            int ret = 0;

            ExtendedContractStruct contract = new ExtendedContractStruct();
            //ExtendedContractStruct contract;

            for (int i = 0; i < ContractCount; i++)
            {


                if (0 == ptGetExtendedContract(i, ref contract))
                {

                    if (DoGetPatsContract != null)
                    {
                        DoGetPatsContract(contract);
                    }

                }

            }
        }
        public void DoExchange()
        {
            int ExchangeCount = 0;
            ptCountExchanges(ref ExchangeCount);
            int ret = 0;

            for (int i = 0; i < ExchangeCount; i++)
            {
                ExchangeStruct exchange = new ExchangeStruct();

                if (0 == ptGetExchange(i, ref exchange))
                {

                    if (DoGetPatsExchange != null)
                    {
                        DoGetPatsExchange(exchange);
                    }

                }
            }
        }
        public void DoCommdity()
        {
            int CommodityCount = 0;
            ptCountCommodities(ref CommodityCount);
            int ret = 0;

            for (int i = 0; i < CommodityCount; i++)
            {
                CommodityStruct Commodity = new CommodityStruct();

                if (0 == ptGetCommodity(i, ref Commodity))
                {

                    if (DoGetPatsCommodity != null)
                    {
                        DoGetPatsCommodity(Commodity);
                    }

                }
            }
        }
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void ProcAddr();
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void LinkProcAddr(ref  LinkStateStruct value);
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void PriceProcAddr(ref PriceUpdStruct value);
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void TickerUpdateProcAddr(ref TickerUpdStruct value);
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        public delegate void SettlementProcAddr(ref  SettlementPriceStruct value);
        static ProcAddr LogonStatusHandler;
        static ProcAddr ForcedLogoutHandler;
        static ProcAddr DataDLCompleteHandler;
        static LinkProcAddr HostLinkProcAddrHandler;
        static LinkProcAddr PriceLinkProcAddrHandler;
        static PriceProcAddr PriceProcAddrHandler;
        static TickerUpdateProcAddr TickerUpdateProcAddrHandler;
        static SettlementProcAddr SettlementProcAddrHandler;
        void LogonStatus()
        {

            LogonStatusStruct Status = new LogonStatusStruct();
            int ret = ptGetLogonStatus(ref Status);

            if (ret == 0)
            {
                OnLog("LogonStatus 登入成功");
            }
            else
            {
                OnError("LogonStatus ret =" + ret + " " + Status.Reason);
            }


        }
        void ForcedLogout()
        {
            OnError("ForcedLogout");
        }
        void DataDLComplete()
        {

            OnLog("DataDLComplete");
        }

        public void SubscribeAll()
        {
            int ContractCount = 0;
            ptCountContracts(ref ContractCount);
            int ret = 0;

            for (int i = 0; i < ContractCount; i++)
            {
                ExtendedContractStruct contract = new ExtendedContractStruct();
                //ExtendedContractStruct contract;
                if (0 == ptGetExtendedContract(i, ref contract))
                {

                    /*printf( "  price  Exchange=%s Commodity=%s Contract=%s\n"
                        ,contract.ExchangeName,contract.ContractName,contract.ContractDate); */
                    //ret = ptSubscribePrice("SIM", "MINI GOLD"
                    //    , "DEC17");
                    if (contract.NumberOfLegs == 1)
                        ret = ptSubscribePrice(contract.ExchangeName, contract.ContractName, contract.ContractDate);

                }
            }
        }
        void PriceUpdate(ref PriceUpdStruct value)
        {
            PriceStruct ps = new PriceStruct();
            ptGetPriceForContract(value.ExchangeName
                , value.ContractName
                , value.ContractDate, ref ps);

            OnPriceTigger(value.ExchangeName, value.ContractName, value.ContractDate, ps);
            if ((ps.ChangeMask & ptChangeBid) == ptChangeBid)
            {
            }
            if ((ps.ChangeMask & ptChangeOffer) == ptChangeOffer)
            {
            }
            if ((ps.ChangeMask & ptChangeImpliedBid) == ptChangeImpliedBid)
            {
            }
            if ((ps.ChangeMask & ptChangeImpliedOffer) == ptChangeImpliedOffer)
            {
            }
            if ((ps.ChangeMask & ptChangeRFQ) == ptChangeRFQ)
            {
            }
            if ((ps.ChangeMask & ptChangeLast) == ptChangeLast)
            {
            }
            if ((ps.ChangeMask & ptChangeTotal) == ptChangeTotal)
            {
            }
            if ((ps.ChangeMask & ptChangeHigh) == ptChangeHigh)
            {
            }
            if ((ps.ChangeMask & ptChangeLow) == ptChangeLow)
            {
            }


            if ((ps.ChangeMask & ptChangeOpening) == ptChangeOpening)
            {
            }
            if ((ps.ChangeMask & ptChangeClosing) == ptChangeClosing)
            {
            }



            if ((ps.ChangeMask & ptChangeBidDOM) == ptChangeBidDOM)
            {
            }
            if ((ps.ChangeMask & ptChangeOfferDOM) == ptChangeOfferDOM)
            {
            }



            if ((ps.ChangeMask & ptChangeTGE) == ptChangeTGE)
            {
            }
            if ((ps.ChangeMask & ptChangeOfferDOM) == ptChangeOfferDOM)
            {
            }


            if ((ps.ChangeMask & ptChangeSettlement) == ptChangeSettlement)
            {
            }
            if ((ps.ChangeMask & ptChangeIndic) == ptChangeIndic)
            {
            }
        }
        void HostLinkStateChange(ref LinkStateStruct value)
        {
            if (value.NewState == ptLinkConnected)
            {
                OnLog("HostLink 連線成功");
                System.Threading.Thread.Sleep(5000);
                int ret = ptLogOn(ref _LogonDetails);
                if (ret == 0)
                {
                }
                else
                {
                    OnError("ptLogOn ret =" + ret);
                }
            }
            else if (value.NewState == ptLinkClosed)
            {
                OnLog("HostLink 連線失敗");
            }

        }
        void PriceLinkStateChange(ref LinkStateStruct value)
        {
            if (value.NewState == ptLinkConnected)
            {

                PriceLink.PutData2Queue(true);



            }
            else if (value.NewState == ptLinkClosed)
            {
                PriceLink.PutData2Queue(false);

            }
        }
        void SettlementPrice(ref SettlementPriceStruct SettlementPriceStruct)
        {

            OnSettlementTigger(SettlementPriceStruct);
        }
        void Ticker(ref TickerUpdStruct data)
        {
            OnTickerTigger(data);
        }
        #region patsystem's function

        // Constants for callback types.
        public const int ptHostLinkStateChange = 1;
        public const int ptPriceLinkStateChange = 2;
        public const int ptLogonStatus = 3;
        public const int ptMessage = 4;
        public const int ptForcedLogout = 6;
        public const int ptDataDLComplete = 7;
        public const int ptPriceUpdate = 8;
        public const int ptTickerUpdate = 17;
        public const int ptSettlementCallback = 22;
        // application environment types
        public const char ptGateway = 'G';
        public const char ptClient = 'C';
        public const char ptTestClient = 'T';
        public const char ptTestGateway = 'g';
        public const char ptDemoClient = 'D';
        public const char ptBroker = 'B';
        public const char ptTestBroker = 'b';

        // Constants to describe socket link states
        public const int ptLinkOpened = 1;
        public const int ptLinkConnecting = 2;
        public const int ptLinkConnected = 3;
        public const int ptLinkClosed = 4;
        public const int ptLinkInvalid = 5;
        //Logon States
        public const int ptLogonFailed = 0;
        public const int ptLogonSucceeded = 1;
        public const int ptForcedOut = 2;
        public const int ptObsoleteVers = 3;
        public const int ptWrongEnv = 4;
        public const int ptDatabaseErr = 5;
        public const int ptInvalidUser = 6;
        public const int ptLogonRejected = 7;
        public const int ptInvalidAppl = 8;
        public const int ptLoggedOn = 9;
        public const int ptInvalidLogonState = 99;

        // Price Changes
        public const int ptChangeBid = 0x000001;
        public const int ptChangeOffer = 0x000002;
        public const int ptChangeImpliedBid = 0x000004;
        public const int ptChangeImpliedOffer = 0x000008;
        public const int ptChangeRFQ = 0x000010;
        public const int ptChangeLast = 0x000020;
        public const int ptChangeTotal = 0x000040;
        public const int ptChangeHigh = 0x000080;
        public const int ptChangeLow = 0x000100;
        public const int ptChangeOpening = 0x000200;
        public const int ptChangeClosing = 0x000400;
        public const int ptChangeBidDOM = 0x000800;
        public const int ptChangeOfferDOM = 0x001000;
        public const int ptChangeTGE = 0x002000;
        public const int ptChangeSettlement = 0x004000;
        public const int ptChangeIndic = 0x008000;
        [DllImport("PATSAPI.DLL", EntryPoint = "ptInitialise")]
        public static extern int ptInitialise(char Env, string APIversion, string ApplicID, string ApplicVersion, string License, bool InitReset);
        [DllImport("PATSAPI.DLL", EntryPoint = "ptLogOn")]
        public static extern int ptLogOn(ref LogonStruct LogonDetails);
        [DllImport("PATSAPI.DLL", EntryPoint = "ptLogOff")]
        public static extern int ptLogOn();
        [DllImport("PATSAPI.DLL", EntryPoint = "ptEnable")]
        public static extern int ptEnable(int code);
        [DllImport("PATSAPI.DLL", EntryPoint = "ptSetHostAddress")]
        public static extern int ptSetHostAddress(string IPaddress, string IPSocket);


        [DllImport("PATSAPI.DLL", EntryPoint = "ptSetPriceAddress")]
        public static extern int ptSetPriceAddress(string IPaddress, string IPSocket);


        [DllImport("PATSAPI.DLL", EntryPoint = "ptReady")]
        public static extern int ptReady();
        [DllImport("PATSAPI.DLL", EntryPoint = "ptDisconnect")]
        public static extern int ptDisconnect();

        [DllImport("PATSAPI.DLL", EntryPoint = "ptGetErrorMessage")]
        public static extern string ptGetErrorMessage(int Error);
        [DllImport("PATSAPI.DLL", EntryPoint = "ptGetLogonStatus")]
        public static extern int ptGetLogonStatus(ref LogonStatusStruct LogonStatus);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptSetInternetUser")]
        public static extern int ptSetInternetUser(char Enabled);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptSetSuperTAS")]
        public static extern int ptSetSuperTAS(char Enabled);
        [DllImport("PATSAPI.DLL", EntryPoint = "ptCountContracts")]
        public static extern int ptCountContracts(ref int Count);
        [DllImport("PATSAPI.DLL", EntryPoint = "ptGetContract")]
        public static extern int ptGetContract(int Index, ref ContractStruct Contract);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptGetExtendedContract")]
        public static extern int ptGetExtendedContract(int Index, ref ExtendedContractStruct Contract);


        [DllImport("PATSAPI.DLL", EntryPoint = "ptSubscribePrice")]
        public static extern int ptSubscribePrice(string ExchangeName, string ContractName, string ContractDate);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptUnsubscribePrice")]
        public static extern int ptUnsubscribePrice(string ExchangeName, string ContractName, string ContractDate);
        [DllImport("PATSAPI.DLL", EntryPoint = "ptCountCommodities")]
        public static extern int ptCountCommodities(ref int Count);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptNotifyAllMessages")]
        public static extern int ptNotifyAllMessages(char Enabled);


        [DllImport("PATSAPI.DLL", EntryPoint = "ptGetPriceForContract")]
        public static extern int ptGetPriceForContract(string ExchangeName, string ContractName, string ContractDate, ref PriceStruct CurrentPrice);



        [DllImport("PATSAPI.DLL", EntryPoint = "ptGetCommodity")]
        public static extern int ptGetCommodity(int Index, ref CommodityStruct Commodity);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptCountExchanges")]
        public static extern int ptCountExchanges(ref int Count);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptGetExchange")]
        public static extern int ptGetExchange(int Index, ref ExchangeStruct ExchangeDetails);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptGetAPIBuildVersion")]
        public static extern int ptGetAPIBuildVersion(ref string APIVersion);


        [DllImport("PATSAPI.DLL", EntryPoint = "ptRegisterCallback", CallingConvention = CallingConvention.Winapi)]
        public static extern int ptRegisterCallback(int callbackID, ProcAddr CBackProc);
        [DllImport("PATSAPI.DLL", EntryPoint = "ptRegisterPriceCallback", CallingConvention = CallingConvention.Winapi)]
        public static extern int ptRegisterPriceCallback(int callbackID, PriceProcAddr CBackProc);




        [DllImport("PATSAPI.DLL", EntryPoint = "ptRegisterTickerCallback", CallingConvention = CallingConvention.Winapi)]
        public static extern int ptRegisterTickerCallback(int callbackID, TickerUpdateProcAddr CBackProc);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptRegisterLinkStateCallback", CallingConvention = CallingConvention.Winapi)]
        public static extern int ptRegisterLinkStateCallback(int callbackID, LinkProcAddr CBackProc);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptRegisterSettlementCallback", CallingConvention = CallingConvention.Winapi)]
        public static extern int ptRegisterSettlementCallback(int callbackID, SettlementProcAddr CBackProc);

        [DllImport("PATSAPI.DLL", EntryPoint = "ptSetClientPath", CallingConvention = CallingConvention.Winapi)]
        public static extern int ptSetClientPath(string Path);


        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct SettlementPriceStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ExchangeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ContractName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
            public string ContractDate;

            public int SettlementType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string Price;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
            public string Time;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string Date;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct LinkStateStruct
        {
            public byte OldState;
            public byte NewState;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct TickerUpdStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ExchangeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ContractName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
            public string ContractDate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string BidPrice;
            public int BidVolume;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string OfferPrice;
            public int OfferVolume;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string LastPrice;
            public int LastVolume;
            public char Bid;
            public char Offer;
            public char Last;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct PriceUpdStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ExchangeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ContractName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
            public string ContractDate;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct LogonStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string UserID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string Password;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string NewPassword;
            public char Reset;
            public char Reports;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string OTPassword;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct LogonStatusStruct
        {
            public byte Status;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 61)]
            public string Reason;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string DefaultTraderAccount;
            public char ShowReason;
            public char DOMEnabled;
            public char PostTradeAmend;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string UserName;
            public char GTEnabled;
        }
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct LegStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string oo1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string oo2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string oo3;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string oo4;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string oo5;
        }
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct ContractStruct
        {

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ContractName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
            public string ContractDate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ExchangeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string ExpiryDate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string LastTradeDate;
            public int NumberOfLegs;
            public int TicksPerPoint;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string TickSize;
            public char Tradable;
            public int GTStatus;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string Margin;
            public char ESATemplate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
            public string MarketRef;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string lnExchangeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string lnContractName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
            public string lnContractDate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public LegStruct[] ExternalID;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct ExtendedContractStruct
        {

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ContractName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
            public string ContractDate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ExchangeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string ExpiryDate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
            public string LastTradeDate;
            public int NumberOfLegs;
            public int TicksPerPoint;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string TickSize;
            public char Tradable;
            public int GTStatus;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string Margin;
            public char ESATemplate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
            public string MarketRef;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string lnExchangeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string lnContractName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 51)]
            public string lnContractDate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public LegStruct[] ExternalID;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct PriceDetailStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
            public string Price;
            public int Volume;
            public byte AgeCounter;
            public byte Direction;
            public byte Hour;
            public byte Minute;
            public byte Second;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct PriceStruct
        {
            public PriceDetailStruct Bid;
            public PriceDetailStruct Offer;
            public PriceDetailStruct ImpliedBid;
            public PriceDetailStruct ImpliedOffer;
            public PriceDetailStruct RFQ;
            public PriceDetailStruct Last0;
            public PriceDetailStruct Last1;
            public PriceDetailStruct Last2;
            public PriceDetailStruct Last3;
            public PriceDetailStruct Last4;
            public PriceDetailStruct Last5;
            public PriceDetailStruct Last6;
            public PriceDetailStruct Last7;
            public PriceDetailStruct Last8;
            public PriceDetailStruct Last9;
            public PriceDetailStruct Last10;
            public PriceDetailStruct Last11;
            public PriceDetailStruct Last12;
            public PriceDetailStruct Last13;
            public PriceDetailStruct Last14;
            public PriceDetailStruct Last15;
            public PriceDetailStruct Last16;
            public PriceDetailStruct Last17;
            public PriceDetailStruct Last18;
            public PriceDetailStruct Last19;
            public PriceDetailStruct Total;
            public PriceDetailStruct High;
            public PriceDetailStruct Low;
            public PriceDetailStruct Opening;
            public PriceDetailStruct Closing;
            public PriceDetailStruct BidDOM0;
            public PriceDetailStruct BidDOM1;
            public PriceDetailStruct BidDOM2;
            public PriceDetailStruct BidDOM3;
            public PriceDetailStruct BidDOM4;
            public PriceDetailStruct BidDOM5;
            public PriceDetailStruct BidDOM6;
            public PriceDetailStruct BidDOM7;
            public PriceDetailStruct BidDOM8;
            public PriceDetailStruct BidDOM9;
            public PriceDetailStruct BidDOM10;
            public PriceDetailStruct BidDOM11;
            public PriceDetailStruct BidDOM12;
            public PriceDetailStruct BidDOM13;
            public PriceDetailStruct BidDOM14;
            public PriceDetailStruct BidDOM15;
            public PriceDetailStruct BidDOM16;
            public PriceDetailStruct BidDOM17;
            public PriceDetailStruct BidDOM18;
            public PriceDetailStruct BidDOM19;
            public PriceDetailStruct OfferDOM0;
            public PriceDetailStruct OfferDOM1;
            public PriceDetailStruct OfferDOM2;
            public PriceDetailStruct OfferDOM3;
            public PriceDetailStruct OfferDOM4;
            public PriceDetailStruct OfferDOM5;
            public PriceDetailStruct OfferDOM6;
            public PriceDetailStruct OfferDOM7;
            public PriceDetailStruct OfferDOM8;
            public PriceDetailStruct OfferDOM9;
            public PriceDetailStruct OfferDOM10;
            public PriceDetailStruct OfferDOM11;
            public PriceDetailStruct OfferDOM12;
            public PriceDetailStruct OfferDOM13;
            public PriceDetailStruct OfferDOM14;
            public PriceDetailStruct OfferDOM15;
            public PriceDetailStruct OfferDOM16;
            public PriceDetailStruct OfferDOM17;
            public PriceDetailStruct OfferDOM18;
            public PriceDetailStruct OfferDOM19;
            public PriceDetailStruct LimitUp;
            public PriceDetailStruct LimitDown;
            public PriceDetailStruct ExecutionUp;
            public PriceDetailStruct ExecutionDown;
            public PriceDetailStruct ReferencePrice;
            public PriceDetailStruct pvCurrStl;
            public PriceDetailStruct pvSODStl;
            public PriceDetailStruct pvNewStl;
            public PriceDetailStruct pvIndBid;
            public PriceDetailStruct pvIndOffer;
            public int Status;
            public int ChangeMask;
            public int PriceStatus;
        }


        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct CommodityStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ExchangeName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string ContractName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string Currency;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string Group;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string OnePoint;
            public int TicksPerPoint;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string TickSize;
            public int GTStatus;
        }



        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct ExchangeStruct
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
            public string Name;
            public char QueryEnabled;
            public char AmendEnabled;
            public int Strategy;
            public char CustomDecs;
            public int Decimals;
            public char TicketType;
            public char RFQA;
            public char RFQT;
            public char EnableBlock;
            public char EnableBasis;
            public char EnableAA;
            public char EnableCross;
            public int GTStatus;
        }

        #endregion
        class QueuePoolByLock<T>
        {
            readonly object _locker = new object();
            Thread[] _workers;
            int _DefaultThreadCount = 25;
            Queue<T> _itemQ = new Queue<T>();
            public delegate void ParameterHandler(T ff);
            private ParameterHandler m_ParameteExcute;
            public event ParameterHandler ParameterExcute
            {
                add
                {
                    m_ParameteExcute += value;
                }
                remove
                {
                    m_ParameteExcute -= value;
                }
            }

            public QueuePoolByLock()
            {
                _workers = new Thread[_DefaultThreadCount];

                for (int i = 0; i < _DefaultThreadCount; i++)
                {
                    _workers[i] = new Thread(execQueueData);
                    _workers[i].IsBackground = true;

                }
            }

            public QueuePoolByLock(int workerCount)
            {
                _DefaultThreadCount = workerCount;
                _workers = new Thread[_DefaultThreadCount];

                for (int i = 0; i < _DefaultThreadCount; i++)
                {
                    _workers[i] = new Thread(execQueueData);
                    _workers[i].IsBackground = true;

                }
            }

            public void Go()
            {
                for (int i = 0; i < _workers.Length; i++)
                {
                    _workers[i].Start();
                }

            }
            public void Dispose()
            {
                foreach (Thread worker in _workers)
                    worker.Abort();
                GC.SuppressFinalize(this);
            }
            public void Shutdown(bool waitForWorkers)
            {
                T f = default(T);
                foreach (Thread worker in _workers)
                    PutData2Queue(f);

                if (waitForWorkers)
                {
                    foreach (Thread worker in _workers)
                        worker.Join();
                }
                else
                {
                    foreach (Thread worker in _workers)
                        worker.Abort();
                }
            }

            public void PutData2Queue(T item)
            {
                lock (_locker)
                {
                    _itemQ.Enqueue(item);
                    Monitor.Pulse(_locker);
                }
            }

            internal void execQueueData()
            {


                while (true)
                {
                    T item;
                    lock (_locker)
                    {
                        while (_itemQ.Count == 0) Monitor.Wait(_locker);
                        item = _itemQ.Dequeue();
                    }
                    if (item == null)
                    {
                        return;
                    }
                    if (m_ParameteExcute != null)
                        m_ParameteExcute(item);

                }
            }
        }


    }
}
