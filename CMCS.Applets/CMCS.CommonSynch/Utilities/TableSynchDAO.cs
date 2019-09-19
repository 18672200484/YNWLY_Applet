using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
//
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Sys;
using CMCS.DumblyConcealer.Tasks.CarSynchronous.Enums;
using CMCS.DumblyConcealer.Tasks.CarSynchronous;

namespace CMCS.CommonSynch.Utilities
{
    public class TableSynchDAO
    {
        public TableSynchDAO()
        {
            InitPerformer();
        }

        System.Timers.Timer timer1 = new System.Timers.Timer();

        CommonAppConfig _CommonAppConfig;

        #region Event

        public delegate void OutputInfoEventHandler(string info, eOutputType outputType);
        public event OutputInfoEventHandler OutputInfo;

        public delegate void OutputErrorEventHandler(string describe, Exception ex);
        public event OutputErrorEventHandler OutputError;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        void InitPerformer()
        {
            _CommonAppConfig = CommonAppConfig.GetInstance();

            timer1.Interval = 5000;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
        }

        /// <summary>
        /// 输出信息
        /// </summary>
        /// <param name="describe"></param>
        /// <param name="ex"></param>
        void OutputInfoMethod(string describe, eOutputType outputType)
        {
            if (OutputInfo != null) OutputInfo(describe, outputType);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="describe"></param>
        /// <param name="ex"></param>
        void OutputErrorMethod(string describe, Exception ex)
        {
            if (OutputError != null) OutputError(describe, ex);
        }

        void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer1.Interval = this._CommonAppConfig.SynchInterval * 1000;
            timer1.Stop();

            Synch();

            timer1.Start();
        }

        /// <summary>
        /// 检测IP是否Ping通
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public bool TestPing(string ip)
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(ip);
            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 同步数据
        /// </summary>
        private void Synch()
        {
            if (!TestPing(this._CommonAppConfig.ServerIp))
            {
                OutputInfoMethod(string.Format("服务器网络已断开"), eOutputType.Error);
                return;
            }
            OracleDapperDber serverDber = new OracleDapperDber(this._CommonAppConfig.ServerConnStr);

            OracleDapperDber clientDber = new OracleDapperDber(this._CommonAppConfig.ClientConnStr);

            if (string.IsNullOrEmpty(this._CommonAppConfig.SyncIdentifier))
            {
                OutputInfoMethod(string.Format("同步标识未设置"), eOutputType.Error);
                return;
            }
            foreach (TableSynch tableSynch in this._CommonAppConfig.TableSynchs)
            {
                try
                {
                    if (!tableSynch.Enabled) continue;
                    // 未设置表名则跳过
                    if (string.IsNullOrEmpty(tableSynch.TableName.Trim()))
                    {
                        OutputInfoMethod(string.Format("【{0}】 同步未执行，原因：未设置表名(TableName)参数", tableSynch.SynchTitle), eOutputType.Error);
                        continue;
                    }
                    // 未设置主键名则跳过
                    if (string.IsNullOrEmpty(tableSynch.PrimaryKey.Trim()))
                    {
                        OutputInfoMethod(string.Format("【{0}】 同步未执行，原因：未设置主键(PrimaryKey)参数", tableSynch.TableName), eOutputType.Error);
                        continue;
                    }
                    // 未设置同步标识则跳过
                    if (string.IsNullOrEmpty(tableSynch.SynchField.Trim()))
                    {
                        OutputInfoMethod(string.Format("【{0}】 同步未执行，原因：未设置同步标识字段(SynchField)参数", tableSynch.TableName), eOutputType.Error);
                        continue;
                    }
                    //判断两个库表是否存在
                    if (serverDber.Execute(OracleSqlBuilder.BuildHasTableSQL(tableSynch.TableName)) == 0)
                    {
                        OutputInfoMethod(string.Format("【{0}】 同步未执行，原因：服务器端不存在此表{1}", tableSynch.SynchTitle, tableSynch.TableName), eOutputType.Error);
                        continue;
                    }
                    if (clientDber.Execute(OracleSqlBuilder.BuildHasTableSQL(tableSynch.TableName)) == 0)
                    {
                        OutputInfoMethod(string.Format("【{0}】 同步未执行，原因：就地端不存在此表{1}", tableSynch.SynchTitle, tableSynch.TableName), eOutputType.Error);
                        continue;
                    }

                    //判断两个库表字段是否一致
                    DataTable dt1 = serverDber.ExecuteDataTable(OracleSqlBuilder.BuildGetNullTableSQL(tableSynch.TableName));
                    DataTable dt2 = clientDber.ExecuteDataTable(OracleSqlBuilder.BuildGetNullTableSQL(tableSynch.TableName));
                    if (!OracleSqlBuilder.CompareTableField(dt1, dt2))
                    {
                        OutputInfoMethod(string.Format("【{0}】两端表{1}列名不一致", tableSynch.SynchTitle, tableSynch.TableName), eOutputType.Important);
                        IList<DataColumn> columns = OracleSqlBuilder.CompareTableFieldAndReturn(dt1, dt2);
                        if (columns != null && columns.Count > 0)
                        {
                            clientDber.Execute(OracleSqlBuilder.BuildAddColumnSQL(columns, serverDber));
                            clientDber.Execute(OracleSqlBuilder.BuildColumnDefaultSQL(columns, serverDber));
                            OutputInfoMethod(string.Format("【{0}】列已同步", tableSynch.SynchTitle, tableSynch.TableName), eOutputType.Important);
                        }
                    }

                    int res = 0;

                    switch (tableSynch.SynchType)
                    {
                        case "上传":
                            res = Upload(serverDber, clientDber, tableSynch);
                            OutputInfoMethod(string.Format("【{0}】 本次上传 {1} 条", tableSynch.SynchTitle, res), eOutputType.Normal);
                            break;
                        case "下达":
                            res = Download(serverDber, clientDber, tableSynch);
                            OutputInfoMethod(string.Format("【{0}】 本次下达 {1} 条", tableSynch.SynchTitle, res), eOutputType.Normal);
                            break;
                        case "双向":
                            //先下达 再上传
                            res = Download(serverDber, clientDber, tableSynch);
                            OutputInfoMethod(string.Format("【{0}】 本次下达 {1} 条", tableSynch.SynchTitle, res), eOutputType.Normal);

                            res = Upload(serverDber, clientDber, tableSynch);
                            OutputInfoMethod(string.Format("【{0}】 本次上传 {1} 条", tableSynch.SynchTitle, res), eOutputType.Normal);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    OutputErrorMethod(string.Format("【{0}】　{1}同步失败", tableSynch.SynchTitle, tableSynch.SynchType), ex);
                }
            }
            //删除数据
            foreach (CmcsWaitForHandleEvent item in clientDber.Entities<CmcsWaitForHandleEvent>("where DataFlag=0 and EventCode like '%删除%'"))
            {
                bool isSuccess = false;

                eEventCode eventCode;
                bool a = Enum.TryParse<eEventCode>(item.EventCode, out eventCode);
                if (!Enum.TryParse<eEventCode>(item.EventCode, out eventCode)) continue;

                switch (eventCode)
                {
                    case eEventCode.汽车智能化_删除未完成运输记录:
                        isSuccess = clientDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = item.ObjectId }) > 0;
                        if (isSuccess) OutputInfoMethod(string.Format("事件：{0}  ObjectId：{1}", eEventCode.汽车智能化_删除未完成运输记录.ToString(), item.ObjectId), eOutputType.Normal);
                        break;

                    case eEventCode.汽车智能化_删除入厂煤运输记录:
                        isSuccess = clientDber.DeleteBySQL<CmcsBuyFuelTransport>("where Id=:Id", new { Id = item.ObjectId }) > 0;
                        if (isSuccess) OutputInfoMethod(string.Format("事件：{0}  ObjectId：{1}", eEventCode.汽车智能化_删除入厂煤运输记录.ToString(), item.ObjectId), eOutputType.Normal);
                        break;

                    case eEventCode.汽车智能化_删除出厂煤运输记录:
                        isSuccess = clientDber.DeleteBySQL<CmcsSaleFuelTransport>("where Id=:Id", new { Id = item.ObjectId }) > 0;
                        if (isSuccess) OutputInfoMethod(string.Format("事件：{0}  ObjectId：{1}", eEventCode.汽车智能化_删除出厂煤运输记录.ToString(), item.ObjectId), eOutputType.Normal);
                        break;

                    default:
                        break;
                }
                if (isSuccess)
                {
                    item.DataFlag = 1;
                    clientDber.Update(item);
                }
            }
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="server"></param>
        /// <param name="client"></param>
        /// <param name="tableSynch"></param>
        /// <returns></returns>
        private int Upload(OracleDapperDber server, OracleDapperDber client, TableSynch tableSynch)
        {
            int synchCount = 0;

            DataTable dtClient = client.ExecuteDataTable(OracleSqlBuilder.BuildGetNeedSynchTableSQL(tableSynch.TableName, tableSynch.SynchField, this._CommonAppConfig.SyncIdentifier, "上传"));
            foreach (DataRow dr in dtClient.Rows)
            {
                string execSql = string.Empty;

                if (server.ExecuteDataTable(OracleSqlBuilder.BuildHasRecordSQL(tableSynch.TableName, tableSynch.PrimaryKey, dr[tableSynch.PrimaryKey].ToString())).Rows[0][0].ToString() == "0")
                    execSql = OracleSqlBuilder.BuildInsertSQL(tableSynch.TableName, tableSynch.SynchField, this._CommonAppConfig.SyncIdentifier, dr);
                else
                    execSql = OracleSqlBuilder.BuildUpdateSQL(tableSynch.TableName, tableSynch.SynchField, this._CommonAppConfig.SyncIdentifier, tableSynch.PrimaryKey, dr);

                if (server.Execute(execSql) > 0)
                {
                    synchCount += 1;
                    client.Execute(OracleSqlBuilder.BuildUpdateSynchFieldSQL(tableSynch.TableName, tableSynch.PrimaryKey, dr[tableSynch.PrimaryKey].ToString(), tableSynch.SynchField, this._CommonAppConfig.SyncIdentifier, "上传"));
                }
            }

            return synchCount;
        }

        /// <summary>
        /// 下达
        /// </summary>
        /// <param name="server"></param>
        /// <param name="client"></param>
        /// <param name="tableSynch"></param>
        /// <returns></returns>
        private int Download(OracleDapperDber server, OracleDapperDber client, TableSynch tableSynch)
        {
            int synchCount = 0;

            DataTable dtServer = server.ExecuteDataTable(OracleSqlBuilder.BuildGetNeedSynchTableSQL(tableSynch.TableName, tableSynch.SynchField, this._CommonAppConfig.SyncIdentifier));
            foreach (DataRow dr in dtServer.Rows)
            {
                string execSql = string.Empty;

                if (client.ExecuteDataTable(OracleSqlBuilder.BuildHasRecordSQL(tableSynch.TableName, tableSynch.PrimaryKey, dr[tableSynch.PrimaryKey].ToString())).Rows[0][0].ToString() == "0")
                    execSql = OracleSqlBuilder.BuildInsertSQL(tableSynch.TableName, tableSynch.SynchField, this._CommonAppConfig.SyncIdentifier, dr);
                else
                    execSql = OracleSqlBuilder.BuildUpdateSQL(tableSynch.TableName, tableSynch.SynchField, this._CommonAppConfig.SyncIdentifier, tableSynch.PrimaryKey, dr);

                if (client.Execute(execSql) > 0)
                {
                    synchCount += 1;
                    server.Execute(OracleSqlBuilder.BuildUpdateSynchFieldSQL(tableSynch.TableName, tableSynch.PrimaryKey, dr[tableSynch.PrimaryKey].ToString(), tableSynch.SynchField, this._CommonAppConfig.SyncIdentifier));
                }
            }

            return synchCount;
        }

        /// <summary>
        /// 开始同步
        /// </summary>
        public void StartSynch()
        {
            this.timer1.Start();
        }

        /// <summary>
        /// 停止同步
        /// </summary>
        public void StopSynch()
        {
            this.timer1.Stop();
        }
    }

    /// <summary>
    /// 输出信息类型
    /// </summary>
    public enum eOutputType
    {
        /// <summary>
        /// 普通
        /// </summary>
        [Description("#BD86FA")]
        Normal,
        /// <summary>
        /// 重要
        /// </summary>
        [Description("#A50081")]
        Important,
        /// <summary>
        /// 警告
        /// </summary>
        [Description("#F9C916")]
        Warn,
        /// <summary>
        /// 错误
        /// </summary>
        [Description("#DB2606")]
        Error
    }
}
