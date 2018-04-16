using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using pfcf.tool;
using pfcf.pats;
namespace FTrade
{
    public partial class MainForm : Form
    {
        public static MainForm form;
        public DataTable _dtMarket;
        public DataTable _dtReply;
        public DataTable _dtMatch;
        private DataAgent _DataAgent;
        private Timer _Timer;
        private bool _ServerRunning;
        public MainForm()
        {
            InitializeComponent();

            this.Text += "(V" + Application.ProductVersion + ")";
            UIProvider DI = new UIProvider();

            DI.frmMainForm = this;
            DI.lstSysMsg = this.lstSysMsg;
            DI.lstConnections = this.lstConnections;
            DI.DgvStatus = this.dgvStatus;

            this._DataAgent = new DataAgent(DI);

            DataTable dt = new DataTable();
            dt.Columns.Add("ConnectionName");
            dt.Columns.Add("ConnectionIp");

            dt.Columns.Add("ConnectionStatus");
            dt.Columns.Add("ConnectionRemark");

            dt.PrimaryKey = new DataColumn[] { dt.Columns["ConnectionName"] };
            this.dgvStatus.DataSource = dt;
            this.dgvStatus.Columns["ConnectionName"].Width = 200;
            this.dgvStatus.Columns["ConnectionName"].HeaderText = "名稱";
            this.dgvStatus.Columns["ConnectionIp"].Width = 100;
            this.dgvStatus.Columns["ConnectionIp"].HeaderText = "位置";

            this.dgvStatus.Columns["ConnectionStatus"].Width = 150;
            this.dgvStatus.Columns["ConnectionStatus"].HeaderText = "狀態";
            this.dgvStatus.Columns["ConnectionRemark"].Width = 500;
            this.dgvStatus.Columns["ConnectionRemark"].HeaderText = "說明";
            this.dgvStatus.ReadOnly = true;
            this.dgvStatus.AllowUserToAddRows = false;
            initdata();
            form = this;
        }
        private void initdata()
        {
            _dtMarket = new DataTable();
            _dtMarket.Columns.Add("SecurityExchange");
            _dtMarket.Columns.Add("SecurityType");
            _dtMarket.Columns.Add("Symbol");
            _dtMarket.Columns.Add("MaturityMonthYear");
            _dtMarket.Columns.Add("PutOrCall");
            _dtMarket.Columns.Add("StrikePrice");
            _dtMarket.Columns.Add("LastPx");
            _dtMarket.Columns.Add("LastSize");
            _dtMarket.Columns.Add("TotalVolume");
            _dtMarket.Columns.Add("Openingprice");
            _dtMarket.Columns.Add("Closingprice");
            _dtMarket.Columns.Add("Settlprice");
            _dtMarket.Columns.Add("Referenceprice");
            _dtMarket.Columns.Add("Dailyhigh");
            _dtMarket.Columns.Add("Dailylow");
            _dtMarket.Columns.Add("BidPx");
            _dtMarket.Columns.Add("BidSize");
            _dtMarket.Columns.Add("Bid0Px");
            _dtMarket.Columns.Add("Bid0Size");
            _dtMarket.Columns.Add("Bid1Px");
            _dtMarket.Columns.Add("Bid1Size");
            _dtMarket.Columns.Add("Bid2Px");
            _dtMarket.Columns.Add("Bid2Size");
            _dtMarket.Columns.Add("Bid3Px");
            _dtMarket.Columns.Add("Bid3Size");
            _dtMarket.Columns.Add("Bid4Px");
            _dtMarket.Columns.Add("Bid4Size");
            _dtMarket.Columns.Add("Bid5Px");
            _dtMarket.Columns.Add("Bid5Size");


            _dtMarket.Columns.Add("Bid6Px");
            _dtMarket.Columns.Add("Bid6Size");
            _dtMarket.Columns.Add("Bid7Px");
            _dtMarket.Columns.Add("Bid7Size");
            _dtMarket.Columns.Add("Bid8Px");
            _dtMarket.Columns.Add("Bid8Size");
            _dtMarket.Columns.Add("Bid9Px");
            _dtMarket.Columns.Add("Bid9Size");
            _dtMarket.Columns.Add("Bid10Px");
            _dtMarket.Columns.Add("Bid10Size");
            _dtMarket.Columns.Add("OfferPx");
            _dtMarket.Columns.Add("OfferSize");
            _dtMarket.Columns.Add("Offer0Px");
            _dtMarket.Columns.Add("Offer0Size");
            _dtMarket.Columns.Add("Offer1Px");
            _dtMarket.Columns.Add("Offer1Size");
            _dtMarket.Columns.Add("Offer2Px");
            _dtMarket.Columns.Add("Offer2Size");
            _dtMarket.Columns.Add("Offer3Px");
            _dtMarket.Columns.Add("Offer3Size");
            _dtMarket.Columns.Add("Offer4Px");
            _dtMarket.Columns.Add("Offer4Size");
            _dtMarket.Columns.Add("Offer5Px");
            _dtMarket.Columns.Add("Offer5Size");


            _dtMarket.Columns.Add("Offer6Px");
            _dtMarket.Columns.Add("Offer6Size");
            _dtMarket.Columns.Add("Offer7Px");
            _dtMarket.Columns.Add("Offer7Size");
            _dtMarket.Columns.Add("Offer8Px");
            _dtMarket.Columns.Add("Offer8Size");
            _dtMarket.Columns.Add("Offer9Px");
            _dtMarket.Columns.Add("Offer9Size");
            _dtMarket.Columns.Add("Offer10Px");
            _dtMarket.Columns.Add("Offer10Size");

            _dtMarket.Columns.Add("OpenCloseSettleFlag");
            _dtMarket.PrimaryKey = new DataColumn[] { _dtMarket.Columns["SecurityExchange"]

                , _dtMarket.Columns["Symbol"]
                , _dtMarket.Columns["MaturityMonthYear"] };

            dgvMarket.DataSource = _dtMarket;

            _dtReply = new DataTable();
            _dtReply.Columns.Add("ExecType");
            _dtReply.Columns.Add("SecurityExchange");
            _dtReply.Columns.Add("SecurityType");
            _dtReply.Columns.Add("Symbol");
            _dtReply.Columns.Add("MaturityMonthYear");
            _dtReply.Columns.Add("PutOrCall");
            _dtReply.Columns.Add("StrikePrice");

            _dtReply.Columns.Add("OrderID");
            _dtReply.Columns.Add("OrdType");
            _dtReply.Columns.Add("Side");
            _dtReply.Columns.Add("Price");
            _dtReply.Columns.Add("OrderQty");
            _dtReply.Columns.Add("LeavesQty");
            _dtReply.Columns.Add("CumQty");
            _dtReply.Columns.Add("Openclose");
            _dtReply.Columns.Add("TimeInForce");
            _dtReply.Columns.Add("ClOrdID");
            _dtReply.Columns.Add("OrigClOrdID");
            _dtReply.Columns.Add("StopPx");
            _dtReply.Columns.Add("ExecTransType");

            _dtReply.Columns.Add("OrdStatus");
            _dtReply.Columns.Add("SecurityType2");
            _dtReply.Columns.Add("Symbol2");
            _dtReply.Columns.Add("MaturityMonthYear2");
            _dtReply.Columns.Add("PutOrCall2");
            _dtReply.Columns.Add("StrikePrice2");
            _dtReply.Columns.Add("Side1");
            _dtReply.Columns.Add("Side2");
            _dtReply.Columns.Add("TransactTime");
            _dtReply.Columns.Add("LocalTransactTime");
            _dtReply.Columns.Add("ExpireDate");
            _dtReply.Columns.Add("Text");


            _dtReply.PrimaryKey = new DataColumn[] { _dtReply.Columns["OrderID"] };
            dgvReply.DataSource = _dtReply;
            _dtMatch = new DataTable();

            _dtMatch.Columns.Add("ExecType");
            _dtMatch.Columns.Add("SecurityExchange");
            _dtMatch.Columns.Add("SecurityType");
            _dtMatch.Columns.Add("Symbol");
            _dtMatch.Columns.Add("MaturityMonthYear");
            _dtMatch.Columns.Add("PutOrCall");
            _dtMatch.Columns.Add("StrikePrice");
            _dtMatch.Columns.Add("Side");
            _dtMatch.Columns.Add("OrderID");
            _dtMatch.Columns.Add("LastShares");
            _dtMatch.Columns.Add("LastPx");

            _dtMatch.Columns.Add("ClOrdID");
            _dtMatch.Columns.Add("ExecID");
            _dtMatch.Columns.Add("ExecRefID");

            _dtMatch.Columns.Add("ExecTransType");

            _dtMatch.Columns.Add("OrdStatus");
            _dtMatch.Columns.Add("SecurityType2");
            _dtMatch.Columns.Add("Symbol2");
            _dtMatch.Columns.Add("MaturityMonthYear2");
            _dtMatch.Columns.Add("PutOrCall2");
            _dtMatch.Columns.Add("StrikePrice2");
            _dtMatch.Columns.Add("Side1");
            _dtMatch.Columns.Add("Side2");
            _dtMatch.Columns.Add("TransactTime");
            _dtMatch.Columns.Add("LocalTransactTime");
            _dtMatch.Columns.Add("LegNum");

            dgvMatch.DataSource = _dtMatch;
            // _dtMatch.PrimaryKey = new DataColumn[] { _dtMatch.Columns["OrderID"] };
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            _Timer = new Timer();
            this._ServerRunning = false;
            init();

            _Timer.Interval = 10000;
            _Timer.Tick += new EventHandler(_Timer_Tick);
            _Timer.Start();

        }

        private void setDataAgent(bool flag)
        {
            if (flag)
            {


                this._ServerRunning = true;


                this._DataAgent.Start();

                this._Timer.Start();

            }
            else
            {
                this._ServerRunning = false;




                this._DataAgent.Stop();

                this._Timer.Stop();
            }


        }

        void _Timer_Tick(object sender, EventArgs e)
        {

            init();
        }
        private void init()
        {

            if (!this._ServerRunning)
            {
                setDataAgent(false);
                setDataAgent(true);
            }



            int closeweek = int.Parse(DataAgent.DEFAULTProvider.GetString("CloseTimeByWeek"));
            int begin = int.Parse(DataAgent.DEFAULTProvider.GetString("ClearDataTime"));
            int now = int.Parse(DateTime.Now.ToString("HHmm"));

            if (now > begin)//跨日
            {
                if (DataAgent.TradeDate != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (closeweek == -1 || (int)DateTime.Now.DayOfWeek == closeweek)
                    {

                        this.Close();
                    }
                    else
                    {
                        setDataAgent(false);
                        setDataAgent(true);
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            setDataAgent(false);
            this._DataAgent.Dispose();
        }

        private void btnSubscribe_Click(object sender, EventArgs e)
        {
            _DataAgent.Pats.Subscribe(this.txtExchangeName.Text.Trim(), txtContractName.Text.Trim(), txtContractDate.Text);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            PatsNative.NewOrderStruct NewOrder = new PatsNative.NewOrderStruct();

            NewOrder.TraderAccount =this.txtAccount.Text.Trim().PadRight(21, '\0');
            NewOrder.ExchangeName = this.txtExchangeName.Text.Trim().PadRight(11, '\0'); ;
            NewOrder.ContractName = this.txtSymbol.Text.Trim().PadRight(11, '\0');
            NewOrder.ContractDate = this.txtMaturityMonthYear.Text.Trim().PadRight(51, '\0'); ;
            if (this.txtBS.Text.Trim() == "S")
                NewOrder.BuyOrSell = 'S';
            else
                NewOrder.BuyOrSell = 'B';
            NewOrder.Price = this.txtPrice.Text.Trim().PadRight(21, '\0'); ;
            NewOrder.Price2 = this.txtStopPrice.Text.Trim().PadRight(21, '\0');
            NewOrder.Lots = int.Parse(this.txtQty.Text.Trim());
            NewOrder.OrderType =this.txtOrdType.Text.Trim() .PadRight(11, '\0');//Limit


            NewOrder.OpenOrClose = 'O';


            NewOrder.LinkedOrder = "".PadRight(11, '\0');
            NewOrder.GoodTillDate = "".PadRight(9, '\0');
            NewOrder.Reference = "".PadRight(26, '\0');
            NewOrder.ESARef = "".PadRight(51, '\0');
            NewOrder.TriggerDate = "".PadRight(9, '\0');
            NewOrder.TriggerTime = "".PadRight(7, '\0');
            NewOrder.BatchID = "".PadRight(11, '\0');
            NewOrder.BatchType = "".PadRight(11, '\0');
            NewOrder.BatchStatus = "".PadRight(11, '\0');
            NewOrder.ParentID = "".PadRight(11, '\0');
            NewOrder.BigRefField = "".PadRight(256, '\0');
            NewOrder.SenderLocationID = "".PadRight(33, '\0');
            NewOrder.Rawprice = "".PadRight(21, '\0');
            NewOrder.Rawprice2 = "".PadRight(21, '\0');
            NewOrder.ExecutionID = "".PadRight(71, '\0');
            NewOrder.ClientID = "".PadRight(21, '\0');
            NewOrder.APIMUser = "".PadRight(21, '\0');
            NewOrder.YDSPAudit = "".PadRight(11, '\0');
            NewOrder.ICSNearLegPrice = "".PadRight(11, '\0');

            NewOrder.ICSFarLegPrice = "".PadRight(11, '\0'); ;
            NewOrder.TicketType = "".PadRight(3, '\0');
            NewOrder.TicketVersion = "".PadRight(4, '\0');
            NewOrder.ExchangeField = "".PadRight(11, '\0');
            NewOrder.BOFID = "".PadRight(21, '\0');
            NewOrder.Badge = "".PadRight(6, '\0');
            NewOrder.LocalUserName = "".PadRight(11, '\0');
            NewOrder.LocalTrader = "".PadRight(21, '\0');



            NewOrder.LocalBOF = "".PadRight(21, '\0');
            NewOrder.LocalOrderID = "".PadRight(11, '\0');
            NewOrder.LocalExAcct = "".PadRight(11, '\0');
            NewOrder.RoutingID1 = "".PadRight(11, '\0');
            NewOrder.RoutingID2 = "".PadRight(11, '\0');
            NewOrder.clientIdShortCode = "".PadRight(21, '\0');
            NewOrder.executionDecision = "".PadRight(21, '\0');
            NewOrder.investmentDecision = "".PadRight(21, '\0');

            string orderid = "";
            _DataAgent.Pats.AddOrder(ref NewOrder, ref orderid);
        }

        private void btnOrderCancelReplace_Click(object sender, EventArgs e)
        {
            PatsNative.AmendOrderStruct AmendOrder = new PatsNative.AmendOrderStruct();

            AmendOrder.Price = this.txtPrice.Text.Trim().PadRight(21, '\0');
            AmendOrder.Price2 = this.txtStopPrice.Text.Trim().PadRight(21, '\0');
            AmendOrder.Lots = int.Parse(this.txtQty.Text.Trim());
            AmendOrder.Trader = this.txtAccount.Text.Trim().PadRight(21, '\0');
            AmendOrder.AmendOrderType = this.txtOrdType.Text.Trim().PadRight(11, '\0');


            string orderid = this.txtOrderno.Text.Trim();
            _DataAgent.Pats.AmendOrder(ref AmendOrder, ref orderid);

        }

        private void btnOrderCancel_Click(object sender, EventArgs e)
        {
            string orderid = this.txtOrderno.Text.Trim();
            PatsNative.OrderDetailStruct Details = new PatsNative.OrderDetailStruct();
            _DataAgent.Pats.CancelOrder(ref Details, ref orderid);
        }

        private void btnGetContractPosition_Click(object sender, EventArgs e)
        {
            _DataAgent.Pats.GetContractPosition(this.txtExchangeName.Text.Trim().PadRight(11, '\0')
, this.txtSymbol.Text.Trim().PadRight(11, '\0'), this.txtMaturityMonthYear.Text.Trim().PadRight(51, '\0')
, this.txtAccount.Text.Trim().PadRight(21, '\0'));
        }
    }
}
