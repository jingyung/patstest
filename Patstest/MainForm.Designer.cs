namespace FTrade
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvStatus = new System.Windows.Forms.DataGridView();
            this.tabControl_Status = new System.Windows.Forms.TabControl();
            this.tabPage_status = new System.Windows.Forms.TabPage();
            this.lstSysMsg = new System.Windows.Forms.ListBox();
            this.tabPage_connections = new System.Windows.Forms.TabPage();
            this.lstConnections = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnOrder = new System.Windows.Forms.Button();
            this.txtTIF = new System.Windows.Forms.TextBox();
            this.btnGetContractPosition = new System.Windows.Forms.Button();
            this.txtSecurity = new System.Windows.Forms.TextBox();
            this.btnMOrderCancelReplace = new System.Windows.Forms.Button();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.txtSecurityExchange = new System.Windows.Forms.TextBox();
            this.txtMaturityMonthYear = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtPutOrCall = new System.Windows.Forms.TextBox();
            this.txtStrikePrice = new System.Windows.Forms.TextBox();
            this.txtOrdType = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtStopPrice = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtSide2 = new System.Windows.Forms.TextBox();
            this.btnOrderCancelReplace = new System.Windows.Forms.Button();
            this.txtSide1 = new System.Windows.Forms.TextBox();
            this.txtOrigClOrdID = new System.Windows.Forms.TextBox();
            this.txtStrikePrice2 = new System.Windows.Forms.TextBox();
            this.txtOrderno = new System.Windows.Forms.TextBox();
            this.txtPutOrCall2 = new System.Windows.Forms.TextBox();
            this.btnOrderCancel = new System.Windows.Forms.Button();
            this.txtMaturityMonthYear2 = new System.Windows.Forms.TextBox();
            this.txtBS = new System.Windows.Forms.TextBox();
            this.txtSymbol2 = new System.Windows.Forms.TextBox();
            this.txtSecurity2 = new System.Windows.Forms.TextBox();
            this.txtExchangeName = new System.Windows.Forms.TextBox();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.txtContractName = new System.Windows.Forms.TextBox();
            this.txtContractDate = new System.Windows.Forms.TextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvMarket = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvReply = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgvMatch = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).BeginInit();
            this.tabControl_Status.SuspendLayout();
            this.tabPage_status.SuspendLayout();
            this.tabPage_connections.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarket)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReply)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatch)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvStatus);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl_Status);
            this.splitContainer1.Size = new System.Drawing.Size(921, 539);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvStatus
            // 
            this.dgvStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStatus.Location = new System.Drawing.Point(0, 0);
            this.dgvStatus.Margin = new System.Windows.Forms.Padding(2);
            this.dgvStatus.Name = "dgvStatus";
            this.dgvStatus.RowTemplate.Height = 27;
            this.dgvStatus.Size = new System.Drawing.Size(921, 333);
            this.dgvStatus.TabIndex = 0;
            // 
            // tabControl_Status
            // 
            this.tabControl_Status.Controls.Add(this.tabPage_status);
            this.tabControl_Status.Controls.Add(this.tabPage_connections);
            this.tabControl_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_Status.Location = new System.Drawing.Point(0, 0);
            this.tabControl_Status.Name = "tabControl_Status";
            this.tabControl_Status.SelectedIndex = 0;
            this.tabControl_Status.Size = new System.Drawing.Size(921, 202);
            this.tabControl_Status.TabIndex = 39;
            // 
            // tabPage_status
            // 
            this.tabPage_status.Controls.Add(this.lstSysMsg);
            this.tabPage_status.Location = new System.Drawing.Point(4, 22);
            this.tabPage_status.Name = "tabPage_status";
            this.tabPage_status.Size = new System.Drawing.Size(913, 176);
            this.tabPage_status.TabIndex = 2;
            this.tabPage_status.Text = "系統訊息";
            this.tabPage_status.UseVisualStyleBackColor = true;
            // 
            // lstSysMsg
            // 
            this.lstSysMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lstSysMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSysMsg.ItemHeight = 12;
            this.lstSysMsg.Location = new System.Drawing.Point(0, 0);
            this.lstSysMsg.Name = "lstSysMsg";
            this.lstSysMsg.Size = new System.Drawing.Size(913, 176);
            this.lstSysMsg.TabIndex = 2;
            this.lstSysMsg.TabStop = false;
            // 
            // tabPage_connections
            // 
            this.tabPage_connections.Controls.Add(this.lstConnections);
            this.tabPage_connections.Location = new System.Drawing.Point(4, 22);
            this.tabPage_connections.Name = "tabPage_connections";
            this.tabPage_connections.Size = new System.Drawing.Size(913, 176);
            this.tabPage_connections.TabIndex = 1;
            this.tabPage_connections.Text = "已連線資訊";
            this.tabPage_connections.UseVisualStyleBackColor = true;
            // 
            // lstConnections
            // 
            this.lstConnections.BackColor = System.Drawing.Color.MistyRose;
            this.lstConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConnections.ItemHeight = 12;
            this.lstConnections.Location = new System.Drawing.Point(0, 0);
            this.lstConnections.Name = "lstConnections";
            this.lstConnections.Size = new System.Drawing.Size(913, 176);
            this.lstConnections.TabIndex = 1;
            this.lstConnections.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(935, 571);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(927, 545);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "server";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(927, 545);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "test";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtAccount);
            this.splitContainer2.Panel1.Controls.Add(this.btnOrder);
            this.splitContainer2.Panel1.Controls.Add(this.txtTIF);
            this.splitContainer2.Panel1.Controls.Add(this.btnGetContractPosition);
            this.splitContainer2.Panel1.Controls.Add(this.txtSecurity);
            this.splitContainer2.Panel1.Controls.Add(this.btnMOrderCancelReplace);
            this.splitContainer2.Panel1.Controls.Add(this.txtSymbol);
            this.splitContainer2.Panel1.Controls.Add(this.txtSecurityExchange);
            this.splitContainer2.Panel1.Controls.Add(this.txtMaturityMonthYear);
            this.splitContainer2.Panel1.Controls.Add(this.textBox1);
            this.splitContainer2.Panel1.Controls.Add(this.txtPutOrCall);
            this.splitContainer2.Panel1.Controls.Add(this.txtStrikePrice);
            this.splitContainer2.Panel1.Controls.Add(this.txtOrdType);
            this.splitContainer2.Panel1.Controls.Add(this.txtPrice);
            this.splitContainer2.Panel1.Controls.Add(this.txtStopPrice);
            this.splitContainer2.Panel1.Controls.Add(this.txtQty);
            this.splitContainer2.Panel1.Controls.Add(this.txtSide2);
            this.splitContainer2.Panel1.Controls.Add(this.btnOrderCancelReplace);
            this.splitContainer2.Panel1.Controls.Add(this.txtSide1);
            this.splitContainer2.Panel1.Controls.Add(this.txtOrigClOrdID);
            this.splitContainer2.Panel1.Controls.Add(this.txtStrikePrice2);
            this.splitContainer2.Panel1.Controls.Add(this.txtOrderno);
            this.splitContainer2.Panel1.Controls.Add(this.txtPutOrCall2);
            this.splitContainer2.Panel1.Controls.Add(this.btnOrderCancel);
            this.splitContainer2.Panel1.Controls.Add(this.txtMaturityMonthYear2);
            this.splitContainer2.Panel1.Controls.Add(this.txtBS);
            this.splitContainer2.Panel1.Controls.Add(this.txtSymbol2);
            this.splitContainer2.Panel1.Controls.Add(this.txtSecurity2);
            this.splitContainer2.Panel1.Controls.Add(this.txtExchangeName);
            this.splitContainer2.Panel1.Controls.Add(this.btnSubscribe);
            this.splitContainer2.Panel1.Controls.Add(this.txtContractName);
            this.splitContainer2.Panel1.Controls.Add(this.txtContractDate);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer2.Size = new System.Drawing.Size(921, 539);
            this.splitContainer2.SplitterDistance = 199;
            this.splitContainer2.TabIndex = 60;
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(41, 71);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(2);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(56, 18);
            this.btnOrder.TabIndex = 86;
            this.btnOrder.Text = "Order";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // txtTIF
            // 
            this.txtTIF.Location = new System.Drawing.Point(530, 60);
            this.txtTIF.Margin = new System.Windows.Forms.Padding(2);
            this.txtTIF.Name = "txtTIF";
            this.txtTIF.Size = new System.Drawing.Size(76, 22);
            this.txtTIF.TabIndex = 85;
            this.txtTIF.Text = "TIF";
            // 
            // btnGetContractPosition
            // 
            this.btnGetContractPosition.Location = new System.Drawing.Point(742, 84);
            this.btnGetContractPosition.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetContractPosition.Name = "btnGetContractPosition";
            this.btnGetContractPosition.Size = new System.Drawing.Size(141, 18);
            this.btnGetContractPosition.TabIndex = 84;
            this.btnGetContractPosition.Text = "GetContractPosition";
            this.btnGetContractPosition.UseVisualStyleBackColor = true;
            this.btnGetContractPosition.Click += new System.EventHandler(this.btnGetContractPosition_Click);
            // 
            // txtSecurity
            // 
            this.txtSecurity.Location = new System.Drawing.Point(121, 133);
            this.txtSecurity.Margin = new System.Windows.Forms.Padding(2);
            this.txtSecurity.Name = "txtSecurity";
            this.txtSecurity.Size = new System.Drawing.Size(76, 22);
            this.txtSecurity.TabIndex = 60;
            this.txtSecurity.Text = "FUT";
            // 
            // btnMOrderCancelReplace
            // 
            this.btnMOrderCancelReplace.Location = new System.Drawing.Point(164, 84);
            this.btnMOrderCancelReplace.Margin = new System.Windows.Forms.Padding(2);
            this.btnMOrderCancelReplace.Name = "btnMOrderCancelReplace";
            this.btnMOrderCancelReplace.Size = new System.Drawing.Size(112, 18);
            this.btnMOrderCancelReplace.TabIndex = 83;
            this.btnMOrderCancelReplace.Text = "MOrderCancelReplace";
            this.btnMOrderCancelReplace.UseVisualStyleBackColor = true;
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(200, 133);
            this.txtSymbol.Margin = new System.Windows.Forms.Padding(2);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(76, 22);
            this.txtSymbol.TabIndex = 61;
            this.txtSymbol.Text = "Symbol";
            // 
            // txtSecurityExchange
            // 
            this.txtSecurityExchange.Location = new System.Drawing.Point(41, 133);
            this.txtSecurityExchange.Margin = new System.Windows.Forms.Padding(2);
            this.txtSecurityExchange.Name = "txtSecurityExchange";
            this.txtSecurityExchange.Size = new System.Drawing.Size(76, 22);
            this.txtSecurityExchange.TabIndex = 62;
            this.txtSecurityExchange.Text = "SecurityExchange";
            // 
            // txtMaturityMonthYear
            // 
            this.txtMaturityMonthYear.Location = new System.Drawing.Point(301, 133);
            this.txtMaturityMonthYear.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaturityMonthYear.Name = "txtMaturityMonthYear";
            this.txtMaturityMonthYear.Size = new System.Drawing.Size(76, 22);
            this.txtMaturityMonthYear.TabIndex = 63;
            this.txtMaturityMonthYear.Text = "201609";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(369, 111);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 22);
            this.textBox1.TabIndex = 82;
            this.textBox1.Text = "OrigClOrdID";
            // 
            // txtPutOrCall
            // 
            this.txtPutOrCall.Location = new System.Drawing.Point(390, 133);
            this.txtPutOrCall.Margin = new System.Windows.Forms.Padding(2);
            this.txtPutOrCall.Name = "txtPutOrCall";
            this.txtPutOrCall.Size = new System.Drawing.Size(76, 22);
            this.txtPutOrCall.TabIndex = 64;
            this.txtPutOrCall.Text = "0";
            // 
            // txtStrikePrice
            // 
            this.txtStrikePrice.Location = new System.Drawing.Point(470, 133);
            this.txtStrikePrice.Margin = new System.Windows.Forms.Padding(2);
            this.txtStrikePrice.Name = "txtStrikePrice";
            this.txtStrikePrice.Size = new System.Drawing.Size(76, 22);
            this.txtStrikePrice.TabIndex = 65;
            this.txtStrikePrice.Text = "0";
            // 
            // txtOrdType
            // 
            this.txtOrdType.Location = new System.Drawing.Point(621, 111);
            this.txtOrdType.Margin = new System.Windows.Forms.Padding(2);
            this.txtOrdType.Name = "txtOrdType";
            this.txtOrdType.Size = new System.Drawing.Size(76, 22);
            this.txtOrdType.TabIndex = 81;
            this.txtOrdType.Text = "Limit";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(530, 86);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(76, 22);
            this.txtPrice.TabIndex = 66;
            this.txtPrice.Text = "Price";
            // 
            // txtStopPrice
            // 
            this.txtStopPrice.Location = new System.Drawing.Point(530, 111);
            this.txtStopPrice.Margin = new System.Windows.Forms.Padding(2);
            this.txtStopPrice.Name = "txtStopPrice";
            this.txtStopPrice.Size = new System.Drawing.Size(76, 22);
            this.txtStopPrice.TabIndex = 80;
            this.txtStopPrice.Text = "StopPrice";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(621, 86);
            this.txtQty.Margin = new System.Windows.Forms.Padding(2);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(76, 22);
            this.txtQty.TabIndex = 67;
            this.txtQty.Text = "10";
            // 
            // txtSide2
            // 
            this.txtSide2.Location = new System.Drawing.Point(568, 158);
            this.txtSide2.Margin = new System.Windows.Forms.Padding(2);
            this.txtSide2.Name = "txtSide2";
            this.txtSide2.Size = new System.Drawing.Size(76, 22);
            this.txtSide2.TabIndex = 79;
            this.txtSide2.Text = "1";
            // 
            // btnOrderCancelReplace
            // 
            this.btnOrderCancelReplace.Location = new System.Drawing.Point(41, 93);
            this.btnOrderCancelReplace.Margin = new System.Windows.Forms.Padding(2);
            this.btnOrderCancelReplace.Name = "btnOrderCancelReplace";
            this.btnOrderCancelReplace.Size = new System.Drawing.Size(114, 18);
            this.btnOrderCancelReplace.TabIndex = 68;
            this.btnOrderCancelReplace.Text = "OrderCancelReplace";
            this.btnOrderCancelReplace.UseVisualStyleBackColor = true;
            this.btnOrderCancelReplace.Click += new System.EventHandler(this.btnOrderCancelReplace_Click);
            // 
            // txtSide1
            // 
            this.txtSide1.Location = new System.Drawing.Point(568, 133);
            this.txtSide1.Margin = new System.Windows.Forms.Padding(2);
            this.txtSide1.Name = "txtSide1";
            this.txtSide1.Size = new System.Drawing.Size(76, 22);
            this.txtSide1.TabIndex = 78;
            this.txtSide1.Text = "1";
            // 
            // txtOrigClOrdID
            // 
            this.txtOrigClOrdID.Location = new System.Drawing.Point(369, 86);
            this.txtOrigClOrdID.Margin = new System.Windows.Forms.Padding(2);
            this.txtOrigClOrdID.Name = "txtOrigClOrdID";
            this.txtOrigClOrdID.Size = new System.Drawing.Size(76, 22);
            this.txtOrigClOrdID.TabIndex = 69;
            this.txtOrigClOrdID.Text = "OrigClOrdID";
            // 
            // txtStrikePrice2
            // 
            this.txtStrikePrice2.Location = new System.Drawing.Point(470, 163);
            this.txtStrikePrice2.Margin = new System.Windows.Forms.Padding(2);
            this.txtStrikePrice2.Name = "txtStrikePrice2";
            this.txtStrikePrice2.Size = new System.Drawing.Size(76, 22);
            this.txtStrikePrice2.TabIndex = 77;
            this.txtStrikePrice2.Text = "0";
            // 
            // txtOrderno
            // 
            this.txtOrderno.Location = new System.Drawing.Point(290, 86);
            this.txtOrderno.Margin = new System.Windows.Forms.Padding(2);
            this.txtOrderno.Name = "txtOrderno";
            this.txtOrderno.Size = new System.Drawing.Size(76, 22);
            this.txtOrderno.TabIndex = 70;
            this.txtOrderno.Text = "Orderno";
            // 
            // txtPutOrCall2
            // 
            this.txtPutOrCall2.Location = new System.Drawing.Point(390, 163);
            this.txtPutOrCall2.Margin = new System.Windows.Forms.Padding(2);
            this.txtPutOrCall2.Name = "txtPutOrCall2";
            this.txtPutOrCall2.Size = new System.Drawing.Size(76, 22);
            this.txtPutOrCall2.TabIndex = 76;
            this.txtPutOrCall2.Text = "0";
            // 
            // btnOrderCancel
            // 
            this.btnOrderCancel.Location = new System.Drawing.Point(41, 115);
            this.btnOrderCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnOrderCancel.Name = "btnOrderCancel";
            this.btnOrderCancel.Size = new System.Drawing.Size(114, 18);
            this.btnOrderCancel.TabIndex = 71;
            this.btnOrderCancel.Text = "OrderCancel";
            this.btnOrderCancel.UseVisualStyleBackColor = true;
            this.btnOrderCancel.Click += new System.EventHandler(this.btnOrderCancel_Click);
            // 
            // txtMaturityMonthYear2
            // 
            this.txtMaturityMonthYear2.Location = new System.Drawing.Point(301, 163);
            this.txtMaturityMonthYear2.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaturityMonthYear2.Name = "txtMaturityMonthYear2";
            this.txtMaturityMonthYear2.Size = new System.Drawing.Size(76, 22);
            this.txtMaturityMonthYear2.TabIndex = 75;
            this.txtMaturityMonthYear2.Text = "201609";
            // 
            // txtBS
            // 
            this.txtBS.Location = new System.Drawing.Point(448, 86);
            this.txtBS.Margin = new System.Windows.Forms.Padding(2);
            this.txtBS.Name = "txtBS";
            this.txtBS.Size = new System.Drawing.Size(76, 22);
            this.txtBS.TabIndex = 72;
            this.txtBS.Text = "1";
            // 
            // txtSymbol2
            // 
            this.txtSymbol2.Location = new System.Drawing.Point(200, 163);
            this.txtSymbol2.Margin = new System.Windows.Forms.Padding(2);
            this.txtSymbol2.Name = "txtSymbol2";
            this.txtSymbol2.Size = new System.Drawing.Size(76, 22);
            this.txtSymbol2.TabIndex = 74;
            this.txtSymbol2.Text = "Symbol";
            // 
            // txtSecurity2
            // 
            this.txtSecurity2.Location = new System.Drawing.Point(121, 163);
            this.txtSecurity2.Margin = new System.Windows.Forms.Padding(2);
            this.txtSecurity2.Name = "txtSecurity2";
            this.txtSecurity2.Size = new System.Drawing.Size(76, 22);
            this.txtSecurity2.TabIndex = 73;
            // 
            // txtExchangeName
            // 
            this.txtExchangeName.Location = new System.Drawing.Point(41, 44);
            this.txtExchangeName.Name = "txtExchangeName";
            this.txtExchangeName.Size = new System.Drawing.Size(100, 22);
            this.txtExchangeName.TabIndex = 0;
            this.txtExchangeName.Text = "CME_CBT";
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(414, 44);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(75, 23);
            this.btnSubscribe.TabIndex = 3;
            this.btnSubscribe.Text = "Subscribe";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
            // 
            // txtContractName
            // 
            this.txtContractName.Location = new System.Drawing.Point(163, 44);
            this.txtContractName.Name = "txtContractName";
            this.txtContractName.Size = new System.Drawing.Size(100, 22);
            this.txtContractName.TabIndex = 1;
            this.txtContractName.Text = "CORN";
            // 
            // txtContractDate
            // 
            this.txtContractDate.Location = new System.Drawing.Point(281, 44);
            this.txtContractDate.Name = "txtContractDate";
            this.txtContractDate.Size = new System.Drawing.Size(100, 22);
            this.txtContractDate.TabIndex = 2;
            this.txtContractDate.Text = "MAY18";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(921, 336);
            this.tabControl2.TabIndex = 59;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvMarket);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(913, 310);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "market";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvMarket
            // 
            this.dgvMarket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMarket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMarket.Location = new System.Drawing.Point(2, 2);
            this.dgvMarket.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMarket.Name = "dgvMarket";
            this.dgvMarket.RowTemplate.Height = 27;
            this.dgvMarket.Size = new System.Drawing.Size(909, 306);
            this.dgvMarket.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvReply);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage4.Size = new System.Drawing.Size(913, 310);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "reply";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvReply
            // 
            this.dgvReply.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReply.Location = new System.Drawing.Point(2, 2);
            this.dgvReply.Margin = new System.Windows.Forms.Padding(2);
            this.dgvReply.Name = "dgvReply";
            this.dgvReply.RowTemplate.Height = 27;
            this.dgvReply.Size = new System.Drawing.Size(909, 306);
            this.dgvReply.TabIndex = 1;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgvMatch);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(913, 310);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "match";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgvMatch
            // 
            this.dgvMatch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatch.Location = new System.Drawing.Point(0, 0);
            this.dgvMatch.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMatch.Name = "dgvMatch";
            this.dgvMatch.RowTemplate.Height = 27;
            this.dgvMatch.Size = new System.Drawing.Size(913, 310);
            this.dgvMatch.TabIndex = 2;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.listBox1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(913, 310);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "msg";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(913, 310);
            this.listBox1.TabIndex = 1;
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(41, 16);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(100, 22);
            this.txtAccount.TabIndex = 87;
            this.txtAccount.Text = "APPID_031_TA";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 571);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "FTrade";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatus)).EndInit();
            this.tabControl_Status.ResumeLayout(false);
            this.tabPage_status.ResumeLayout(false);
            this.tabPage_connections.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarket)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReply)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatch)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvStatus;
        private System.Windows.Forms.TabControl tabControl_Status;
        private System.Windows.Forms.TabPage tabPage_status;
        internal System.Windows.Forms.ListBox lstSysMsg;
        private System.Windows.Forms.TabPage tabPage_connections;
        internal System.Windows.Forms.ListBox lstConnections;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtContractDate;
        private System.Windows.Forms.TextBox txtContractName;
        private System.Windows.Forms.TextBox txtExchangeName;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvMarket;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dgvReply;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgvMatch;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.TextBox txtTIF;
        private System.Windows.Forms.Button btnGetContractPosition;
        private System.Windows.Forms.TextBox txtSecurity;
        private System.Windows.Forms.Button btnMOrderCancelReplace;
        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.TextBox txtSecurityExchange;
        private System.Windows.Forms.TextBox txtMaturityMonthYear;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtPutOrCall;
        private System.Windows.Forms.TextBox txtStrikePrice;
        private System.Windows.Forms.TextBox txtOrdType;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtStopPrice;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtSide2;
        private System.Windows.Forms.Button btnOrderCancelReplace;
        private System.Windows.Forms.TextBox txtSide1;
        private System.Windows.Forms.TextBox txtOrigClOrdID;
        private System.Windows.Forms.TextBox txtStrikePrice2;
        private System.Windows.Forms.TextBox txtOrderno;
        private System.Windows.Forms.TextBox txtPutOrCall2;
        private System.Windows.Forms.Button btnOrderCancel;
        private System.Windows.Forms.TextBox txtMaturityMonthYear2;
        private System.Windows.Forms.TextBox txtBS;
        private System.Windows.Forms.TextBox txtSymbol2;
        private System.Windows.Forms.TextBox txtSecurity2;
        private System.Windows.Forms.TextBox txtAccount;
    }
}

