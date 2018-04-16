using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace pfcf.pats
{
   public class PatsNative
    {

        public const string ptAPIversion = "v2.8.3";
        #region patsystem's function

        // Constants for callback types.
        public const int ptHostLinkStateChange = 1;
        public const int ptPriceLinkStateChange = 2;
        public const int ptLogonStatus = 3;
        public const int ptMessage = 4;
        public const int ptOrder = 5;
        public const int ptForcedLogout = 6;
        public const int ptDataDLComplete = 7;
        public const int ptPriceUpdate = 8;
        public const int ptFill = 9;
        public const int ptStatusChange = 10;
        public const int ptContractAdded = 11;
        public const int ptContractDeleted = 12;
        public const int ptExchangeRate = 13;
        public const int ptConnectivityStatus = 14;
        public const int ptOrderCancelFailure = 15;
        public const int ptAtBestUpdate = 16;
        public const int ptTickerUpdate = 17;
        public const int ptMemoryWarning = 18;
        public const int ptSubscriberDepthUpdate = 19;
        public const int ptVTSCallback = 20;
        public const int ptDOMUpdate = 21;
        public const int ptSettlementCallback = 22;
        public const int ptStrategyCreateSuccess = 23;
        public const int ptStrategyCreateFailure = 24;
        public const int ptAmendFailureCallback = 25;
        public const int ptGenericPriceUpdate = 26;
        public const int ptBlankPrice = 27;
        public const int ptOrderSentFailure = 28;
        public const int ptOrderQueuedFailure = 29;
        public const int ptOrderBookReset = 30;
        public const int ptExchangeUpdate = 31;
        public const int ptCommodityUpdate = 32;
        public const int ptContractDateUpdate = 33;
        public const int ptPurgeCompleted = 36;
        public const int ptTraderAdded = 37;
        public const int ptOrderTypeUpdate = 38;
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
        // User message types
        public const int ptAlert = 1;
        public const int ptNormal = 2;

        //Group types inside messages
        public const int ptFillGroup = 0;
        public const int ptLegsGroup = 1;
        public const int ptOrderGroup = 2;


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


        //Fill Types
        public const int ptNormalFill = 1;
        public const int ptExternalFill = 2;
        public const int ptNettedFill = 3;
        public const int ptRetainedFill = 5;
        public const int ptBlockLegFill = 52;

        // Pats Order Type Ids
        public const int ptOrderTypeMarket = 1;
        public const int ptOrderTypeLimit = 2;
        public const int ptOrderTypeLimitFAK = 3;
        public const int ptOrderTypeLimitFOK = 4;
        public const int ptOrderTypeStop = 5;
        public const int ptOrderTypeSynthStop = 6;
        public const int ptOrderTypeSynthStopLimit = 7;
        public const int ptOrderTypeMIT = 8;
        public const int ptOrderTypeSynthMIT = 9;
        public const int ptOrderTypeMarketFOK = 10;
        public const int ptOrderTypeMOO = 11;
        public const int ptOrderTypeIOC = 12;
        public const int ptOrderTypeStopRise = 13;
        public const int ptOrderTypeStopFall = 14;
        public const int ptOrderTypeRFQ = 15;
        public const int ptOrderTypeStopLoss = 16;
        public const int ptLimitAtOpen = 17;
        public const int ptMLM = 18;
        public const int ptAggregateOrder = 25;
        public const int ptCustomerRequest = 26;
        public const int ptRFQi = 27;
        public const int ptRFQt = 28;
        public const int ptCrossingBatchType = 42;
        public const int ptBasisBatchType = 43;
        public const int ptBlockBatchType = 44;
        public const int ptAABatchType = 45;
        public const int ptCrossFaKBatchType = 46;
        public const int ptGTCMarket = 50;
        public const int ptGTCLimit = 51;
        public const int ptGTCStop = 52;
        public const int ptGTDMarket = 53;
        public const int ptGTDLimit = 54;
        public const int ptGTDStop = 55;
        public const int ptSETSRepenter = 90;
        public const int ptSETSRepcancel = 91;
        public const int ptSETSRepprerel = 92;
        public const int ptSETSSectDel = 93;
        public const int ptSETSInstDel = 94;
        public const int ptSETSCurDel = 95;
        public const int ptIceberg = 130;
        public const int ptGhost = 131;
        public const int ptProtected = 132;
        public const int ptStop = 133;

        //Order States
        public const int ptQueued = 1;
        public const int ptSent = 2;
        public const int ptWorking = 3;
        public const int ptRejected = 4;
        public const int ptCancelled = 5;
        public const int ptBalCancelled = 6;
        public const int ptPartFilled = 7;
        public const int ptFilled = 8;
        public const int ptCancelPending = 9;
        public const int ptAmendPending = 10;
        public const int ptUnconfirmedFilled = 11;
        public const int ptUnconfirmedPartFilled = 12;
        public const int ptHeldOrder = 13;
        public const int ptCancelHeldOrder = 14;
        public const int ptTransferred = 20;
        public const int ptExternalCancelled = 24;
        //Order Sub States
        public const int ptSubStatePending = 1;
        public const int ptSubStateTriggered = 2;

        // Price Movement
        public const int ptPriceSame = 0;
        public const int ptPriceRise = 1;
        public const int ptPriceFall = 2;


        //GENERIC PRICES
        public const int ptBuySide = 1; //	The RFQ is a buy order
        public const int ptSellSide = 2;//	The RFQ is a sell order
        public const int ptBothSide = 3;//	The RFQ is a for both sides
        public const int PtCrossSide = 4;//	This is for crossing RFQs

        //Fill Sub Types
        public const int ptFillSubTypeSettlement = 1;
        public const int ptFillSubTypeMinute = 2;
        public const int ptFillSubTypeUnderlying = 3;
        public const int ptFillSubTypeReverse = 4;

        //Settlement Price Types
        public const int ptStlLegacyPrice = 0;
        public const int ptStlCurPrice = 7;
        public const int ptStlLimitUp = 21;
        public const int ptStlLimitDown = 22;
        public const int ptStlExecDiff = 23;
        public const int ptStlYDSPPrice = 24;
        public const int ptStlNewPrice = 25;
        public const int ptStlRFQiPrice = 26;
        public const int ptStlRFQtPrice = 27;
        public const int ptStlIndicative = 28;
        public const int ptEFPVolume = 33;
        public const int ptEFSVolume = 34;
        public const int ptBlockVolume = 35;
        public const int ptEFPCummVolume = 36;
        public const int ptEFSCummVolume = 37;
        public const int ptBlockCummVolume = 38;


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
    }



}
