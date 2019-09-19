using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using HslCommunication;
using HslCommunication.ModBus;

namespace ModBus.Tcp
{
    /// <summary>
    /// ModBus读取类
    /// </summary>
    public class ModBusTcp_Net
    {
        private ModbusTcpNet busTcpClient = null;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string MachineCode;

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="dataFormat">数据格式 0 ABCD 1 BADC 2 CDAB 3 DCBA</param>
        /// <param name="isReverse">是否字符串颠倒</param>
        /// <param name="startWithZero">首地址从0开始</param>
        public void SetbusTcp(int dataFormat, bool isReverse = false, bool startWithZero = true)
        {
            if (busTcpClient != null)
            {
                switch (dataFormat)
                {
                    case 0: busTcpClient.DataFormat = HslCommunication.Core.DataFormat.ABCD; break;
                    case 1: busTcpClient.DataFormat = HslCommunication.Core.DataFormat.BADC; break;
                    case 2: busTcpClient.DataFormat = HslCommunication.Core.DataFormat.CDAB; break;
                    case 3: busTcpClient.DataFormat = HslCommunication.Core.DataFormat.DCBA; break;
                    default: break;
                }
                busTcpClient.IsStringReverse = isReverse;
                busTcpClient.AddressStartWithZero = startWithZero;
            }
        }

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">端口</param>
        /// <param name="station">站号</param>
        /// <returns></returns>
        public bool Connect(string ip, int port = 502, byte station = 1)
        {
            try
            {
                busTcpClient = new ModbusTcpNet(ip, port, station);
                OperateResult connect = busTcpClient.ConnectServer();
                if (connect.IsSuccess)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        public bool CloseConnect()
        {
            if (busTcpClient != null)
            {
                // 断开连接
                OperateResult connect = busTcpClient.ConnectClose();
                if (connect.IsSuccess)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 读取离散输入bool变量
        /// </summary>
        /// <param name="address">地址 施耐德离散地址%IX0.0-%IX0.7 对应ModBus地址 010000-010007 %IX1.0-%IX1.7 对应 010008-010015</param>
        /// <returns></returns>
        public int ReadDisCrete(string address)
        {
            OperateResult<bool> result = busTcpClient.ReadDiscrete(address);
            if (result.IsSuccess)
            {
                return result.Content ? 1 : 0;
            }
            return 0;
        }

        /// <summary>
        /// 读取线圈输入bool变量
        /// </summary>
        /// <param name="address">地址 施耐德线圈地址%QX0.0-%QX0.7 对应ModBus地址 010000-010007 %QX1.0-%QX1.7 对应 010008-010015</param>
        /// <returns></returns>
        public int ReadCoil(string address)
        {
            OperateResult<bool> result = busTcpClient.ReadCoil(address);
            if (result.IsSuccess)
            {
                return result.Content ? 1 : 0;
            }
            return 0;
        }
    }
}
