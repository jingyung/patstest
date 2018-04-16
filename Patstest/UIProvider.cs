using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace FTrade
{
    public class UIProvider
    {
        //const
        private const string DATETIME_FORMAT = "yyyy/MM/dd HH:mm:ss.fff";
        //private propertes
        private System.Windows.Forms.ListBox _lstSysMsg;
        private System.Windows.Forms.ListBox _lstConnections;
        private System.Windows.Forms.Form _frmMainForm;
        private System.Windows.Forms.DataGridView _dgvStatus;
        public void ClearData()
        {
            _lstConnections.Items.Clear();
        }
        public System.Windows.Forms.DataGridView DgvStatus
        {
            get { return _dgvStatus; }
            set { _dgvStatus = value; }
        }


        //handler
        private delegate void AddMessageHandler(string Data);

        private delegate void AddConnStatusHandler(string ConnectionName, string ConnectionIp, string ConnectionPort);
        private delegate void ChangeConnStatusHandler(string ConnectionName, string ConnectionStatus, string ConnectionRemark);
        private delegate void ChangeCountHandler(int Count);

        public ListBox lstSysMsg { get { return this._lstSysMsg; } set { this._lstSysMsg = value; } }
        public ListBox lstConnections { get { return this._lstConnections; } set { this._lstConnections = value; } }
        public Form frmMainForm { get { return this._frmMainForm; } set { this._frmMainForm = value; } }


        public UIProvider()
        {
        }

        public void displayStatusMsg(string strMsg)
        {
            try
            {
                _frmMainForm.Invoke(new AddMessageHandler(this.AddStatusMessage), new object[] { strMsg });
            }
            catch (Exception ex)
            {

            }
        }


        public void Add(string ConnectionName, string ConnectionIp, string ConnectionPort)
        {
            try
            {

                this._frmMainForm.Invoke(new AddConnStatusHandler(this.AddConnStatus), new object[] { ConnectionName, ConnectionIp, ConnectionPort });
            }
            catch (Exception ex)
            {
            }

        }
        public void Change(string ConnectionName, string ConnectionStatus, string ConnectionRemark)
        {
            try
            {
                this._frmMainForm.Invoke(new ChangeConnStatusHandler(this.ChangeConnStatus), new object[] { ConnectionName, ConnectionStatus, ConnectionRemark });
            }
            catch (Exception ex)
            {
            }


        }
        private void AddConnStatus(string ConnectionName, string ConnectionIp, string ConnectionPort)
        {
            DataTable dt = (DataTable)_dgvStatus.DataSource;

            DataRow dr = dt.Rows.Find(ConnectionName);
            if (dr == null)
            {
                dr = dt.NewRow();
                dr["ConnectionName"] = ConnectionName;
                if (ConnectionPort.Length > 0)
                {
                    dr["ConnectionIp"] = ConnectionIp + ":" + ConnectionPort;
                }
                else
                {
                    dr["ConnectionIp"] = ConnectionIp  ;
                }


                dt.Rows.Add(dr);
            }
        }
        private void ChangeConnStatus(string ConnectionName, string ConnectionStatus, string ConnectionRemark)
        {
            DataTable dt = (DataTable)_dgvStatus.DataSource;

            DataRow dr = dt.Rows.Find(ConnectionName);
            if (dr != null)
            {
                dr.BeginEdit();

                dr["ConnectionName"] = ConnectionName;
                dr["ConnectionStatus"] = ConnectionStatus;
                dr["ConnectionRemark"] = ConnectionRemark;

                dr.EndEdit();
            }
        }



        private void AddStatusMessage(string astring_msg)
        {
            if (_lstSysMsg.Items.Count > 100)
            {
                _lstSysMsg.Items.RemoveAt(0);
            }
            _lstSysMsg.Items.Add(DateTime.Now.ToString(DATETIME_FORMAT) + " " + astring_msg);
        }

        private void AddConnections(string astring_msg)
        {

            _lstConnections.Items.Add(astring_msg);
        }
        private void RemoveConnections(string astring_msg)
        {

            _lstConnections.Items.Remove(astring_msg);
        }



        public void displayAddConnections(string strMsg)
        {
            _frmMainForm.Invoke(new AddMessageHandler(this.AddConnections), new object[] { strMsg });
        }

        public void displayRemoveConnections(string strMsg)
        {
            _frmMainForm.Invoke(new AddMessageHandler(this.RemoveConnections), new object[] { strMsg });
        }


    }
}
