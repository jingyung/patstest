using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net.Sockets;
using pfcf.tool.server;
namespace FTrade
{

    public class NetWorkIdItem
    {
        public int TokenId = -1;
        public string OldSeq = "";
        public string NewSeq = "";
        public string UserId = "";
        public string Data = "";
 
    }
    public class UserInfo
    {
        string _UserId;
        string _LoginTime;
        string _CANO = "";

        public string CANO = "";
        public string UserId = "";
        public string LoginTime = "";

    }
    public class AccountCore
    {
        private Dictionary<string, bool> _ACTNOFlag = null;

        public Dictionary<string, bool> ACTNOFlag
        {
            get { return _ACTNOFlag; }
            set { _ACTNOFlag = value; }
        }


        private Dictionary<SocketAsyncEventArgs, UserInfo> _SUSERIDFO = null;

        public Dictionary<SocketAsyncEventArgs, UserInfo> SUSERIDFO
        {
            get { return _SUSERIDFO; }
            set { _SUSERIDFO = value; }
        }
        //SOCKET對帳號
        private Dictionary<SocketAsyncEventArgs, Dictionary<string, string>> _SI = null;

        public Dictionary<SocketAsyncEventArgs, Dictionary<string, string>> SI
        {
            get { return _SI; }
            set { _SI = value; }
        }
        //SOCKET對USERID  
        private Dictionary<SocketAsyncEventArgs, Dictionary<string, string>> _SU = null;

        public Dictionary<SocketAsyncEventArgs, Dictionary<string, string>> SU
        {
            get { return _SU; }
            set { _SU = value; }
        }


        //帳號對SOCKET
        private Dictionary<string, Dictionary<int, SocketAsyncEventArgs>> _IS = null;

        public Dictionary<string, Dictionary<int, SocketAsyncEventArgs>> IS
        {
            get { return _IS; }
            set { _IS = value; }
        }
        //USERID對SOCKET
        private Dictionary<string, Dictionary<int, SocketAsyncEventArgs>> _US = null;

        public Dictionary<string, Dictionary<int, SocketAsyncEventArgs>> US
        {
            get { return _US; }
            set { _US = value; }
        }

        //USERID 對帳號
        private Dictionary<string, Dictionary<string, string>> _UI = null;

        public Dictionary<string, Dictionary<string, string>> UI
        {
            get { return _UI; }
            set { _UI = value; }
        }

        //networkid對SEQ、代打交易帳號
        private Dictionary<string, NetWorkIdItem> _NetworkIdItem;

        public Dictionary<string, NetWorkIdItem> NetworkIdItem
        {
            get { return _NetworkIdItem; }
            set { _NetworkIdItem = value; }
        }

        private DataTable _dtAccount;

        public AccountCore()
        {
            _ACTNOFlag = new Dictionary<string, bool>();
            _UI = new Dictionary<string, Dictionary<string, string>>();
            _NetworkIdItem = new Dictionary<string, NetWorkIdItem>();
            _SUSERIDFO = new Dictionary<SocketAsyncEventArgs, UserInfo>();
            _IS = new Dictionary<string, Dictionary<int, SocketAsyncEventArgs>>();
            _SI = new Dictionary<SocketAsyncEventArgs, Dictionary<string, string>>();
            _US = new Dictionary<string, Dictionary<int, SocketAsyncEventArgs>>();
            _SU = new Dictionary<SocketAsyncEventArgs, Dictionary<string, string>>();
        }

        public void Combine(DataTable dt)
        {
            if (dt == null)
            {
                throw new Exception("account is not null");
            }
            this._dtAccount = dt;
            this._UI = new Dictionary<string, Dictionary<string, string>>();
            foreach (DataRow row in this._dtAccount.Rows)
            {
                Dictionary<string, string> dictionary;
                if (!this._UI.ContainsKey(row["userid"].ToString().Trim()))
                {
                    dictionary = new Dictionary<string, string>();
                    dictionary.Add(row["customerid"].ToString().Trim(), row["customerid"].ToString().Trim());
                    this._UI.Add(row["userid"].ToString().Trim(), dictionary);
                }
                else
                {
                    dictionary = this._UI[row["userid"].ToString().Trim()];
                    if (!dictionary.ContainsKey(row["customerid"].ToString().Trim()))
                    {
                        dictionary.Add(row["customerid"].ToString().Trim(), row["customerid"].ToString().Trim());
                    }
                }
            }
        }

        public void AddUser(string UserId)
        {
            try
            {
                lock (_UI)
                {

                    if (!_UI.ContainsKey(UserId.Trim()))
                    {
                        Dictionary<string, string> U = new Dictionary<string, string>();

                        _UI.Add(UserId.Trim(), U);
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUser(string UserId, string[] comact)
        {
            try
            {
                lock (_UI)
                {
                    for (int i = 0; i < comact.Length; i++)
                    {
                        if (!_UI.ContainsKey(UserId.Trim()))
                        {
                            Dictionary<string, string> U = new Dictionary<string, string>();
                            U.Add(comact[i], comact[i]);
                            _UI.Add(UserId.Trim(), U);
                        }
                        else
                        {
                            Dictionary<string, string> U = (Dictionary<string, string>)_UI[UserId.Trim()];

                            if (!U.ContainsKey(comact[i]))
                            {
                                U.Add(comact[i], comact[i]);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddSAEA(string UserId, SocketAsyncEventArgs SAEA)
        {
            try
            {
                if (!_UI.ContainsKey(UserId)) return false;
                Dictionary<string, string> U = (Dictionary<string, string>)_UI[UserId];
                DataToken tk = (DataToken)SAEA.UserToken;
                foreach (string UKey in U.Keys)
                {
                    lock (_IS)
                    {


                        if (!_IS.ContainsKey(UKey))
                        {
                            Dictionary<int, SocketAsyncEventArgs> SS = new Dictionary<int, SocketAsyncEventArgs>();
                            SS.Add(tk.TokenId, SAEA);

                            _IS[UKey] = SS;

                        }
                        else
                        {
                            Dictionary<int, SocketAsyncEventArgs> SS = (Dictionary<int, SocketAsyncEventArgs>)_IS[UKey];
                            if (!SS.ContainsKey(tk.TokenId))
                                SS.Add(tk.TokenId, SAEA);
                        }
                    }

                    lock (_SI)
                    {
                        if (!_SI.ContainsKey(SAEA))
                        {
                            Dictionary<string, string> InvestorAcnos = new Dictionary<string, string>();
                            InvestorAcnos.Add(UKey, UKey);
                            _SI[SAEA] = InvestorAcnos;
                        }
                        else
                        {
                            Dictionary<string, string> InvestorAcno = (Dictionary<string, string>)_SI[SAEA];
                            if (!InvestorAcno.ContainsKey(UKey))
                                InvestorAcno.Add(UKey, UKey);
                        }
                    }

                }
                lock (_US)
                {
                    if (!_US.ContainsKey(UserId))
                    {
                        Dictionary<int, SocketAsyncEventArgs> SS = new Dictionary<int, SocketAsyncEventArgs>();
                        SS.Add(tk.TokenId, SAEA);
                        _US[UserId] = SS;
                    }
                    else
                    {
                        Dictionary<int, SocketAsyncEventArgs> SS = (Dictionary<int, SocketAsyncEventArgs>)_US[UserId];
                        if (!SS.ContainsKey(tk.TokenId))
                            SS.Add(tk.TokenId, SAEA);
                    }
                }
                lock (_SU)
                {
                    if (!_SU.ContainsKey(SAEA))
                    {
                        Dictionary<string, string> UserIds = new Dictionary<string, string>();
                        UserIds.Add(UserId, UserId);
                        _SU[SAEA] = UserIds;
                    }
                    else
                    {
                        Dictionary<string, string> UserIds = (Dictionary<string, string>)_SU[SAEA];
                        if (!UserIds.ContainsKey(UserId))
                            UserIds.Add(UserId, UserId);
                    }


                }

                lock (_SUSERIDFO)
                {
                    if (!_SUSERIDFO.ContainsKey(SAEA))
                    {
                        UserInfo u = new UserInfo();
                        u.UserId = UserId;
                        u.LoginTime = DateTime.Now.ToString("HH:mm:ss");
                        _SUSERIDFO.Add(SAEA, u);
                    }
                    else
                    {
                        _SUSERIDFO[SAEA].UserId = UserId;

                        _SUSERIDFO[SAEA].LoginTime = DateTime.Now.ToString("HH:mm:ss");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        public Dictionary<int, SocketAsyncEventArgs> FindUserIdSAEA(string UserId)
        {
            try
            {
                if (_US == null) return null;

                lock (_US)
                {
                    if (_US.ContainsKey(UserId))
                        return _US[UserId];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Dictionary<int, SocketAsyncEventArgs> FindSAEA(string InvestorAcno)
        {
            try
            {
                if (_IS == null) return null;
                lock (_IS)
                {
                    if (_IS.ContainsKey(InvestorAcno))
                        return _IS[InvestorAcno];
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoveSAEA(SocketAsyncEventArgs SAEA)
        {
            try
            {

                DataToken tk = (DataToken)SAEA.UserToken;
                Dictionary<string, string> SItemp = null;
                lock (_SI)
                {
                    if (_SI == null) return;
                    if (!_SI.ContainsKey(SAEA)) return;
                    Dictionary<string, string> SI = (Dictionary<string, string>)_SI[SAEA];
                    SItemp = new Dictionary<string, string>(SI);
                    _SI.Remove(SAEA);
                }
                lock (_IS)
                {
                    if (SItemp != null)
                    {
                        foreach (string SIkey in SItemp.Keys)
                        {

                            Dictionary<int, SocketAsyncEventArgs> SS = (Dictionary<int, SocketAsyncEventArgs>)_IS[SIkey];
                            SS.Remove(tk.TokenId);
                        }
                    }

                }
                lock (_SU)
                {
                    if (!_SU.ContainsKey(SAEA)) return;
                    Dictionary<string, string> SU = (Dictionary<string, string>)_SU[SAEA];




                    foreach (string SUkey in SU.Keys)
                    {

                        Dictionary<int, SocketAsyncEventArgs> SS = (Dictionary<int, SocketAsyncEventArgs>)_US[SUkey];
                        SS.Remove(tk.TokenId);
                    }
                    _SU.Remove(SAEA);
                }
                lock (_SUSERIDFO)
                {
                    if (_SUSERIDFO.ContainsKey(SAEA))
                    {

                        _SUSERIDFO.Remove(SAEA);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        public void AddNetWorkIdItem(SocketAsyncEventArgs S, string NetworkId, string Seq, string UserId, string Data)
        {
            try
            {
                lock (_NetworkIdItem)
                {
                    NetWorkIdItem item = new NetWorkIdItem();
                    //DataToken tk = (DataToken)S.UserToken;
                    //item.TokenId = tk.TokenId;
                    item.OldSeq = Seq;
                    item.NewSeq = Seq;
                    item.UserId = UserId;
                    item.Data = Data;
                    _NetworkIdItem[NetworkId] = item;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdSeqItem(string NetworkId, string Seq)
        {
            try
            {
                lock (_NetworkIdItem)
                {
                    string key = NetworkId;
                    if (_NetworkIdItem.ContainsKey(key))
                    {
                        _NetworkIdItem[key].NewSeq = Seq;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public NetWorkIdItem FindNetWorkIdItem(byte[] NetworkId)
        {
            try
            {
                if (_NetworkIdItem == null) return null;
                lock (_NetworkIdItem)
                {
                    string key = System.Text.ASCIIEncoding.ASCII.GetString(NetworkId);
                    if (_NetworkIdItem.ContainsKey(key))
                    {
                        return _NetworkIdItem[key];
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NetWorkIdItem FindNetWorkIdItem(string NetworkId)
        {
            try
            {
                if (_NetworkIdItem == null) return null;
                lock (_NetworkIdItem)
                {
                    string key = NetworkId;
                    if (_NetworkIdItem.ContainsKey(key))
                    {
                        return _NetworkIdItem[key];
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserInfo FindUserInfo(SocketAsyncEventArgs S)
        {
            UserInfo u = null;
            try
            {
                lock (_SUSERIDFO)
                {
                    if (_SUSERIDFO.ContainsKey(S))
                    {
                        u = _SUSERIDFO[S];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return u;
        }

        public void UpdUserInfoCano(SocketAsyncEventArgs S, string cano)
        {
            UserInfo u = null;
            try
            {
                lock (_SUSERIDFO)
                {
                    if (_SUSERIDFO.ContainsKey(S))
                    {
                        u = _SUSERIDFO[S];
                        u.CANO = cano;
                        _SUSERIDFO[S] = u;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void AddACTNOFlag(string actno)
        {
            lock (_ACTNOFlag)
            {
                _ACTNOFlag[actno] = true;
            }
        }
        public bool FindACTNOFlag(string actno)
        {
            bool ret = false;
            lock (_ACTNOFlag)
            {
                _ACTNOFlag.TryGetValue(actno, out ret);

            }
            return ret;
        }

        public void Close()
        {
            if (_SI != null)
                _SI.Clear();
            if (_SU != null)
                _SU.Clear();
            if (_IS != null)
                _IS.Clear();
            if (_UI != null)
                _UI.Clear();
            if (_NetworkIdItem != null)
                _NetworkIdItem.Clear();

            if (_SUSERIDFO != null)
                _SUSERIDFO.Clear();
            if (_ACTNOFlag != null)
                _ACTNOFlag.Clear();
            _ACTNOFlag = null;
            _SI = null;
            _SU = null;
            _IS = null;
            _IS = null;
            _SUSERIDFO = null;
        }

    }


    public class  QueryCore<T>
    {

        //流水序號對SOCKET
        private Dictionary<string, T> _NS = null;

        public Dictionary<string,T> NS
        {
            get { return _NS; }
            set { _NS = value; }
        }

        public QueryCore()
        {
            _NS = new Dictionary<string, T>();
        }

        public bool Add(string NetWorkId,T SAEA)
        {
            try
            {
                lock (_NS)
                {
                    if (!_NS.ContainsKey(NetWorkId))
                    {
                        _NS.Add(NetWorkId, SAEA);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        public T FindSAEA(string NetWorkId)
        {
            try
            {

                lock (_NS)
                {
                    if (_NS.ContainsKey(NetWorkId))
                    {
                        return _NS[NetWorkId];
                    }
                    else return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void RemoveSAEA(string NetWorkId)
        {
            try
            {
                lock (_NS)
                {
                    if (_NS.ContainsKey(NetWorkId))
                    {
                        _NS.Remove(NetWorkId);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void Close()
        {
            if (_NS != null)
                _NS.Clear();

            _NS = null;

        }

    }

}
