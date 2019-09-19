using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Data;
using System.Data.Common;
using CMCS.DapperDber.Dbs.OracleDb;

namespace CMCS.CommonSynch.Utilities
{
    /// <summary>
    /// SQL语句构建
    /// </summary>
    public class OracleSqlBuilder
    {
        /// <summary>
        /// Oracle数据库关键字
        /// </summary>
        public static string[] OracleKeywords;

        /// <summary>
        ///  生成判断表是否存在的 SELECT 语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string BuildHasTableSQL(string tableName)
        {
            return "select count(TABLE_NAME) from USER_TABLES where TABLE_NAME='" + tableName.ToUpper() + "'";
        }

        /// <summary>
        /// 生成判断某个字段是否存在的SELECT语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="colmunName"></param>
        /// <returns></returns>
        public static string BuildHasColumnSQL(string tableName, string colmunName)
        {
            return string.Format("SELECT COUNT(*) FROM USER_TAB_COLUMNS WHERE TABLE_NAME = '{0}' AND COLUMN_NAME = '{1}'", tableName.ToUpper(), colmunName.ToUpper());
        }

        /// <summary>
        /// 比较两个表字段是否一样
        /// </summary>
        /// <param name="tb1"></param>
        /// <param name="tb2"></param>
        /// <returns></returns>
        public static bool CompareTableField(DataTable tb1, DataTable tb2)
        {
            if (tb1.Columns.Count != tb2.Columns.Count) return false;

            foreach (DataColumn item in tb1.Columns)
            {
                if (!tb2.Columns.Contains(item.ColumnName.ToUpper())) return false;
            }
            return true;
        }

        /// <summary>
        /// 比较两个表字段是否一样 并返回不一致的列名 以tb1为主
        /// </summary>
        /// <param name="tb1"></param>
        /// <param name="tb2"></param>
        /// <returns></returns>
        public static IList<DataColumn> CompareTableFieldAndReturn(DataTable tb1, DataTable tb2)
        {
            IList<DataColumn> columnNoContains = new List<DataColumn>();

            foreach (DataColumn item in tb1.Columns)
            {
                if (!tb2.Columns.Contains(item.ColumnName.ToUpper()))
                {
                    columnNoContains.Add(item);
                }
            }
            return columnNoContains;
        }

        /// <summary>
        /// 生成添加列的SQL
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static string BuildAddColumnSQL(IList<DataColumn> columns, OracleDapperDber serverDber)
        {
            StringBuilder sql = new StringBuilder();
            foreach (DataColumn item in columns)
            {
                sql.AppendFormat("alter table {0} add {1} {2} {3}", item.Table.TableName, item.ColumnName, SystemTypeToOracleType(item.DataType, item.MaxLength), DefaultValueChange(GetColumnDefaultValue(item.Table.TableName, item.ColumnName, serverDber)));
            }
            return sql.ToString();
        }

        /// <summary>
        /// 生成列的默认值的SQL
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="serverDber"></param>
        /// <returns></returns>
        public static string BuildColumnDefaultSQL(IList<DataColumn> columns, OracleDapperDber serverDber)
        {
            StringBuilder sql = new StringBuilder();
            foreach (DataColumn item in columns)
            {
                sql.AppendFormat(Environment.NewLine + "comment on column {0}.{1} is '{2}'", item.Table.TableName, item.ColumnName, GetColumnComment(item.Table.TableName, item.ColumnName, serverDber));
            }
            return sql.ToString();

        }

        /// <summary>
        /// 获取列默认值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="serverDber"></param>
        /// <returns></returns>
        public static string GetColumnDefaultValue(string tableName, string columnName, OracleDapperDber serverDber)
        {
            return serverDber.ExecuteDataTable(string.Format("SELECT DATA_DEFAULT FROM USER_TAB_COLUMNS WHERE TABLE_NAME ='{0}' AND COLUMN_NAME = '{1}'", tableName.ToUpper(), columnName.ToUpper())).Rows[0][0].ToString();
        }

        /// <summary>
        /// 获取列注释
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="serverDber"></param>
        /// <returns></returns>
        public static string GetColumnComment(string tableName, string columnName, OracleDapperDber serverDber)
        {
            return serverDber.ExecuteDataTable(string.Format("select COMMENTS from user_col_comments where Table_Name='{0}' and COLUMN_NAME='{1}'", tableName.ToUpper(), columnName.ToUpper())).Rows[0][0].ToString();
        }

        /// <summary>
        /// 生成空表的查询SQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string BuildGetNullTableSQL(string tableName)
        {
            return string.Format("select * from {0} where 1<>1", tableName);
        }

        /// <summary>
        /// 生成需要同步数据的查询SQL
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="synchField"></param>
        /// <returns></returns>
        public static string BuildGetNeedSynchTableSQL(string tableName, string synchField, string synchValue, string type = "下达")
        {
            if (type == "下达")
                return string.Format("select * from {0} where {1} not like '%{2}%' or {1} is null order by CreateDate", tableName, synchField, synchValue);
            else
                return string.Format("select * from {0} where {1} = '0' or {1} is null order by CreateDate", tableName, synchField, synchValue);
        }

        /// <summary>
        /// 生成 INSERT SQL语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="synchField"></param>
        /// <param name="synchValue"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string BuildInsertSQL(string tableName, string synchField, string synchValue, DataRow dr)
        {
            StringBuilder strbColumn = new StringBuilder();
            StringBuilder strbValue = new StringBuilder();

            foreach (DataColumn column in dr.Table.Columns)
            {
                //新增时同步标识默认为已同步
                if (column.ColumnName == synchField.ToUpper())
                {
                    strbColumn.AppendFormat("{0},", column.ColumnName.ToUpper());
                    strbValue.AppendFormat("'{0}',", ToDbValue(synchValue, column.DataType));
                    continue;
                }
                switch (column.DataType.ToString())
                {
                    case "System.String":
                        strbColumn.AppendFormat("{0},", column.ColumnName.ToUpper());
                        strbValue.AppendFormat("'{0}',", ToDbValue(dr[column.ColumnName].ToString(), column.DataType));
                        break;
                    case "System.DateTime":
                        strbColumn.AppendFormat("{0},", column.ColumnName.ToUpper());
                        strbValue.AppendFormat("to_date('{0}','yyyy/mm/dd HH24:MI:SS'),", ToDbValue(dr[column.ColumnName].ToString(), column.DataType));
                        break;
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Single":
                    case "System.Double":
                    case "System.Decimal":
                        strbColumn.AppendFormat("{0},", column.ColumnName.ToUpper());
                        strbValue.AppendFormat("{0},", ToDbValue(dr[column.ColumnName].ToString(), column.DataType));
                        break;
                }
            }

            return string.Format("INSERT INTO {0}({1}) values ({2})", tableName.ToUpper(), strbColumn.ToString().TrimEnd(','), strbValue.ToString().TrimEnd(','));
        }

        /// <summary>
        /// 生成 UPDATE SQL语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="synchField"></param>
        /// <param name="synchValue"></param>
        /// <param name="primaryKey"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static string BuildUpdateSQL(string tableName, string synchField, string synchValue, string primaryKey, DataRow dr)
        {
            StringBuilder strbUpdate = new StringBuilder();

            foreach (DataColumn column in dr.Table.Columns)
            {
                //修改时同步标识默认为已同步
                if (column.ColumnName == synchField.ToUpper())
                {
                    strbUpdate.AppendFormat("{0}='{1}',", column.ColumnName.ToUpper(), ToDbValue(synchValue, column.DataType));
                    continue;
                }
                switch (column.DataType.ToString())
                {
                    case "System.String":
                        strbUpdate.AppendFormat("{0}='{1}',", column.ColumnName.ToUpper(), ToDbValue(dr[column.ColumnName].ToString(), column.DataType));
                        break;
                    case "System.DateTime":
                        strbUpdate.AppendFormat("{0}=to_date('{1}','yyyy/mm/dd HH24:MI:SS'),", column.ColumnName.ToUpper(), ToDbValue(dr[column.ColumnName].ToString(), column.DataType));
                        break;
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Single":
                    case "System.Double":
                    case "System.Decimal":
                        strbUpdate.AppendFormat("{0}={1},", column.ColumnName.ToUpper(), ToDbValue(dr[column.ColumnName].ToString(), column.DataType));
                        break;
                }
            }

            return string.Format("UPDATE {0} SET {1} WHERE {2}='{3}'", tableName.ToUpper(), strbUpdate.ToString().TrimEnd(','), primaryKey, dr[primaryKey].ToString());
        }

        /// <summary>
        /// 生成更新同步标识的SQL语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="primaryKeyValue"></param>
        /// <param name="synchField"></param>
        /// <returns></returns>
        public static string BuildUpdateSynchFieldSQL(string tableName, string primaryKey, string primaryKeyValue, string synchField, string synchValue, string type = "下达")
        {
            if (type == "下达")
                return string.Format("UPDATE {0} SET {1}={1}||'|{4}' WHERE {2}='{3}'", tableName.ToUpper(), synchField, primaryKey, primaryKeyValue, synchValue);
            else
                return string.Format("UPDATE {0} SET {1}='{4}' WHERE {2}='{3}'", tableName.ToUpper(), synchField, primaryKey, primaryKeyValue, synchValue);
        }

        /// <summary>
        /// 生成判断数据是否存在的 SELECT 语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string BuildHasRecordSQL(string tableName, string primaryKey, string value)
        {
            return string.Format("select count(1) from {0} where {1}='{2}'", tableName, primaryKey, value);
        }

        /// <summary>
        /// 转化为指定 INSERT 插入语句的字段值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToDbValue(string value, Type type)
        {
            switch (type.FullName)
            {
                case "System.String":
                    return value != null ? value : string.Empty;
                case "System.DateTime":
                    DateTime resDt;
                    if (!DateTime.TryParse(value, out resDt))
                        resDt = DateTime.MinValue;
                    return resDt.ToString("yyyy-MM-dd HH:mm:ss");
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    decimal resDec;
                    if (!Decimal.TryParse(value, out resDec))
                        resDec = 0;
                    return resDec.ToString();
                default:
                    return value;
            }
        }

        /// <summary>
        /// 系统类型转换为Oracle类型
        /// </summary>
        /// <param name="sysType"></param>
        /// <returns></returns>
        public static string SystemTypeToOracleType(Type sysType, int length)
        {
            switch (sysType.FullName)
            {
                case "System.String":
                    return string.Format("NVARCHAR2({0})", length);
                case "System.DateTime":
                    return string.Format("TIMESTAMP({0})", length);
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                case "System.Single":
                case "System.Double":
                case "System.Decimal":
                    return "NUMBER";
            }
            return "NVARCHAR2(64)";
        }

        /// <summary>
        /// 系统数据转换
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string DefaultValueChange(string defaultValue)
        {
            if (string.IsNullOrEmpty(defaultValue))
                return string.Empty;
            else
                return string.Format(" default {0}", defaultValue.TrimEnd('\n'));
        }
    }
}
