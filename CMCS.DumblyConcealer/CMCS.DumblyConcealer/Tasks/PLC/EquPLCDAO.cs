using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.DAO;
using CMCS.DumblyConcealer.Enums;
using ModBus.Tcp;
using System.Threading;
using CMCS.Common;

namespace CMCS.DumblyConcealer.Tasks.PLC
{
    public class EquPLCDAO
    {
        private ManualResetEvent allDone = new ManualResetEvent(false);

        CommonDAO commonDAO = CommonDAO.GetInstance();
        /// <summary>
        /// 开始处理
        /// </summary>
        /// <param name="output"></param>
        public void StartTask(Action<string, eOutputType> output)
        {
            CmcsCMEquipment equipment = commonDAO.SelfDber.Entity<CmcsCMEquipment>("where EquipmentName='气动传输'");
            if (equipment != null)
            {
                IList<CmcsCMEquipment> equipments = commonDAO.SelfDber.Entities<CmcsCMEquipment>("where Parentid=:Parentid order by Sequence asc", new { Parentid = equipment.Id });
                List<ModBusTcp_Net> list = new List<ModBusTcp_Net>();
                foreach (CmcsCMEquipment item in equipments)
                {
                    System.Net.IPAddress address;
                    if (!System.Net.IPAddress.TryParse(item.EquipmentCode, out address))
                        continue;

                    ModBusTcp_Net tcpNet = new ModBusTcp_Net();
                    tcpNet.Connect(item.EquipmentCode);
                    tcpNet.MachineCode = item.EquipmentName;
                    list.Add(tcpNet);
                }
                Read(list, output);
            }
        }

        /// <summary>
        /// 读取并处理下位机信号
        /// </summary>
        /// <param name="tcp_net"></param>
        /// <param name="output"></param>
        public void Read(List<ModBusTcp_Net> tcp_net, Action<string, eOutputType> output)
        {
            while (true)
            {
                int res = 0;
                //allDone.Reset();
                #region MyRegion

                foreach (ModBusTcp_Net item in tcp_net)
                {
                    if (item.MachineCode.Contains("风机"))
                    {
                        #region 下位机信号
                        /*风机1跳闸I AT %IX0.0: BOOL;//紧急断电  初始为0  
                        风机2跳闸I AT %IX0.1: BOOL;

                        运转准备按钮I AT %IX0.4: BOOL;
                         *没有用
                        整机急停I AT %IX0.5: BOOL;
                        相序检测I AT %IX0.7: BOOL;

                        位置1检测到位I AT %IX1.0: BOOL;//吸气到位 默认为1 到位为0
                        位置2检测到位I AT %IX1.1: BOOL;//吹气到位
                         * 剩下的没用
                        位置5检测到位I AT %IX1.2: BOOL;
                        位置6检测到位I AT %IX1.3: BOOL;
                        位置5检测到位化验室I AT %IX1.4: BOOL;
                        位置6检测到位化验室I AT %IX1.5: BOOL;

                        变频器1启动输出Q  AT %QX0.4: BOOL;//风机启动 1 2共用  默认为0  
                        变频器2启动输出Q  AT %QX0.5: BOOL;
                        运转准备指示输出Q  AT %QX0.6: BOOL;
                        主站蜂鸣器输出Q  AT %QX0.7: BOOL;

                        电机1使能输出Q AT %QX1.0: BOOL;//换向小电机转动
                        */
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "风机1跳闸", item.ReadDisCrete("0").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "风机2跳闸", item.ReadDisCrete("1").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "运转准备按钮", item.ReadDisCrete("4").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "整机急停", item.ReadDisCrete("5").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "相序检测", item.ReadDisCrete("7").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置1检测到位", item.ReadDisCrete("8").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置2检测到位", item.ReadDisCrete("9").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置5检测到位", item.ReadDisCrete("10").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置6检测到位", item.ReadDisCrete("11").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置5检测到位化验室", item.ReadDisCrete("12").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置6检测到位化验室", item.ReadDisCrete("13").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "变频器1启动输出", item.ReadCoil("4").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "变频器2启动输出", item.ReadDisCrete("5").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "运转准备指示输出", item.ReadCoil("6").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "主站蜂鸣器输出", item.ReadDisCrete("7").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机1使能输出", item.ReadCoil("8").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机2使能输出", item.ReadDisCrete("9").ToString()) ? 1 : 0;

                        #endregion

                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, "风机运转", item.ReadCoil("4").ToString()) ? 1 : 0;
                        if (item.ReadDisCrete("8") == 0)
                            res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, "气送方向", "吸气") ? 1 : 0;
                        else if (item.ReadDisCrete("9") == 0)
                            res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, "气送方向", "吹气") ? 1 : 0;
                    }
                    else if (item.MachineCode.Contains("换向器"))
                    {
                        #region 下位机信号
                        /*  旁通处管道检测I AT %IX0.0: BOOL; 默认0  有瓶为1
                            管道1样瓶检测I AT %IX0.1: BOOL;
                            管道2样瓶检测I AT %IX0.2: BOOL;
                            管道3样瓶检测I AT %IX0.3: BOOL;
                            管道4样瓶检测I AT %IX0.4: BOOL;
                            旁通处管道检测Ifz AT %IX0.5: BOOL;


                            位置检测1I AT %IX1.0: BOOL;默认1 检测到为0 
                            位置检测2I AT %IX1.1: BOOL;
                            位置检测3I AT %IX1.2: BOOL;
                            位置检测4I AT %IX1.3: BOOL;
                            位置检测5I AT %IX1.4: BOOL;
                            位置检测6I AT %IX1.5: BOOL;

                            使能1输出 	AT %QX0.4: BOOL;//电机转动
                         */
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "旁通处管道检测", item.ReadDisCrete("0").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "管道1样瓶检测", item.ReadDisCrete("1").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "管道2样瓶检测", item.ReadDisCrete("2").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "管道3样瓶检测", item.ReadDisCrete("3").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "管道4样瓶检测", item.ReadDisCrete("4").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "旁通处管道检测", item.ReadDisCrete("5").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测1", item.ReadDisCrete("8").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测2", item.ReadDisCrete("9").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测3", item.ReadDisCrete("10").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测4", item.ReadDisCrete("11").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测5", item.ReadDisCrete("12").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测6", item.ReadDisCrete("13").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "使能1输出", item.ReadCoil("4").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "使能2输出", item.ReadCoil("5").ToString()) ? 1 : 0;

                        #endregion

                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_位置1", item.ReadDisCrete("8") == 1 ? "0" : "1") ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_位置2", item.ReadDisCrete("9") == 1 ? "0" : "1") ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_位置3", item.ReadDisCrete("10") == 1 ? "0" : "1") ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_位置4", item.ReadDisCrete("11") == 1 ? "0" : "1") ? 1 : 0;

                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_样瓶检测1", item.ReadDisCrete("1").ToString()) ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_样瓶检测2", item.ReadDisCrete("2").ToString()) ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_样瓶检测3", item.ReadDisCrete("3").ToString()) ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_样瓶检测4", item.ReadDisCrete("4").ToString()) ? 1 : 0;

                    }
                    else if (item.MachineCode.Contains("发送站"))
                    {
                        #region 下位机信号
                        /*
                            管道物料检测I AT %IX0.0: BOOL;托盘推入管内 发送站是否有瓶
                            样瓶到位检测I AT %IX0.1: BOOL; 推入托盘

                            位置1检测到位I AT %IX1.1: BOOL; 上升到位 默认1 检测到位为0
                            位置2检测到位I AT %IX1.0: BOOL; 下降到位
                            本地复位I AT %IX1.3: BOOL;
                         * 
                            电机1使能输出Q AT %QX0.2: BOOL; 电机
                            发送站允许入瓶气送准备好输出Q  AT %QX0.3: BOOL; 当前站是否空闲  默认0 空闲 1 
                            黄色指示灯亮输出Q  AT %QX0.5: BOOL;
                            绿色指示灯亮输出Q  AT %QX0.6: BOOL;
                            子站蜂鸣器输出Q  AT %QX0.7: BOOL;
                         */
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "管道物料检测", item.ReadDisCrete("0").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "样瓶到位检测", item.ReadDisCrete("1").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "满瓶信号", item.ReadDisCrete("2").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "存查样口有瓶信号", item.ReadDisCrete("3").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "存查样请求吹气信号", item.ReadDisCrete("4").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置2检测到位", item.ReadDisCrete("8").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置1检测到位", item.ReadDisCrete("9").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "本地复位", item.ReadDisCrete("11").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机1使能输出", item.ReadCoil("2").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "发送站允许入瓶气送准备好输出", item.ReadCoil("3").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "气送正在续吹输出", item.ReadCoil("4").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "黄色指示灯亮输出", item.ReadCoil("5").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "绿色指示灯亮输出", item.ReadCoil("6").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "子站蜂鸣器输出", item.ReadCoil("7").ToString()) ? 1 : 0;

                        #endregion

                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_托盘上升到位", item.ReadDisCrete("9") == 1 ? "0" : "1") ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_托盘下降到位", item.ReadDisCrete("8") == 1 ? "0" : "1") ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_样瓶检测", item.ReadDisCrete("1").ToString()) ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_管道物料检测", item.ReadDisCrete("0").ToString()) ? 1 : 0;

                    }
                    else if (item.MachineCode.Contains("人工收发站"))
                    {
                        #region 下位机信号
                        /*
                         管道物料检测I AT %IX0.0: BOOL;
                         样瓶到位检测I AT %IX0.1: BOOL;

                        位置检测1I AT %IX1.1: BOOL;上升到位 默认1 检测到位为0
                        位置检测2I AT %IX1.0: BOOL;下降到位
                        位置检测5I AT %IX1.2: BOOL; //管道关闭 默认1 有信号 0
                        位置检测6I AT %IX1.3: BOOL; //管道打开 

                        发送站允许入瓶输出Q  AT %QX0.4: BOOL;
                        黄色指示灯亮输出Q  AT %QX0.5: BOOL;
                        绿色指示灯亮输出Q  AT %QX0.6: BOOL;
                        子站蜂鸣器输出Q  AT %QX0.7: BOOL;
                        电机1使能输出Q AT %QX1.0: BOOL;
                        电机2使能输出Q AT %QX1.1: BOOL;//管道打开关闭电机
                         */
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "管道物料检测", item.ReadDisCrete("0").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "样瓶到位检测", item.ReadDisCrete("1").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "化验室站满瓶信号", item.ReadDisCrete("3").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "化验室门检测信号左", item.ReadDisCrete("4").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "化验室门检测信号右", item.ReadDisCrete("5").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测1", item.ReadDisCrete("9").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测2", item.ReadDisCrete("8").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测5", item.ReadDisCrete("10").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置检测6", item.ReadDisCrete("11").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "发送站允许入瓶输出", item.ReadCoil("4").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "黄色指示灯亮输出", item.ReadCoil("5").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "绿色指示灯亮输出", item.ReadCoil("6").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "子站蜂鸣器输出", item.ReadCoil("7").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机1使能输出", item.ReadCoil("8").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机2使能输出", item.ReadCoil("9").ToString()) ? 1 : 0; 
                        #endregion

                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_托盘上升到位", item.ReadDisCrete("9") == 1 ? "0" : "1") ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_托盘下降到位", item.ReadDisCrete("8") == 1 ? "0" : "1") ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_样瓶检测", item.ReadDisCrete("1").ToString().ToString()) ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_管道物料检测", item.ReadDisCrete("0").ToString().ToString()) ? 1 : 0;
                        if (item.ReadDisCrete("10") == 0)
                            res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_管道关闭", "1") ? 1 : 0;
                        else if (item.ReadDisCrete("11") == 0)
                            res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_管道打开", "1") ? 1 : 0;
                    }
                    else if (item.MachineCode == "化验室接收站")
                    {
                        #region 下位机信号
                        /*
                         管道物料检测I AT %IX0.0: BOOL;
                        样瓶到位检测I AT %IX0.1: BOOL;

                        化验室站满瓶信号I AT %IX0.3: BOOL;
                        化验室门检测信号I AT %IX0.4: BOOL;

                        位置1检测到位I AT %IX1.1: BOOL;//托盘上升到位
                        位置2检测到位I AT %IX1.0: BOOL;//下降到位
                        位置3检测到位I AT %IX1.4: BOOL;//缩回到位 
                        位置4检测到位I AT %IX1.5: BOOL;//推出到位  默认 1 推出 0
                        位置5检测到位I AT %IX1.2: BOOL;//管道关闭
                        位置6检测到位I AT %IX1.3: BOOL;//管道打开

                        黄色指示灯亮输出Q  AT %QX0.5: BOOL;
                        绿色指示灯亮输出Q  AT %QX0.6: BOOL;
                        子站蜂鸣器输出Q  AT %QX0.7: BOOL;
                        电机1使能输出Q AT %QX1.0: BOOL;//托盘
                        电机2使能输出Q AT %QX1.1: BOOL;//滑块
                        电机3复位 AT %QX3.0: BOOL;//缩回
                        电机3打开 AT %QX3.1: BOOL;//推出
                        电机3制动 AT %QX3.2: BOOL;//刹车
                         */
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "管道物料检测", item.ReadDisCrete("0").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "样瓶到位检测", item.ReadDisCrete("1").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "化验室站满瓶信号", item.ReadDisCrete("3").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "化验室门检测信号左", item.ReadDisCrete("4").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "归批机准备好信号", item.ReadDisCrete("5").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置1检测到位", item.ReadDisCrete("9").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置2检测到位", item.ReadDisCrete("8").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置3检测到位", item.ReadDisCrete("12").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置4检测到位", item.ReadDisCrete("13").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置5检测到位", item.ReadDisCrete("10").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置6检测到位", item.ReadDisCrete("11").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "发送站允许入瓶输出", item.ReadCoil("4").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "黄色指示灯亮输出", item.ReadCoil("5").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "绿色指示灯亮输出", item.ReadCoil("6").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "子站蜂鸣器输出", item.ReadCoil("7").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机1使能输出", item.ReadCoil("8").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机2使能输出", item.ReadCoil("9").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机3复位", item.ReadCoil("16").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机3打开", item.ReadCoil("17").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机3制动", item.ReadCoil("18").ToString()) ? 1 : 0; 
                        #endregion

                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_托盘上升到位", item.ReadDisCrete("9") == 1 ? "0" : "1") ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_托盘下降到位", item.ReadDisCrete("8") == 1 ? "0" : "1") ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_样瓶检测", item.ReadDisCrete("1").ToString().ToString()) ? 1 : 0;
                        if (item.ReadDisCrete("10") == 0)
                            res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_管道关闭", "1") ? 1 : 0;
                        else if (item.ReadDisCrete("11") == 0)
                            res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_管道打开", "1") ? 1 : 0;
                    }
                    else if (item.MachineCode.Contains("收发站") && item.MachineCode.Contains("存样柜"))
                    {
                        #region 下位机信号
                        /*
                         管道物料检测I AT %IX0.0: BOOL;
                        样瓶到位检测I AT %IX0.1: BOOL;


                        位置1检测到位I AT %IX1.0: BOOL;//关闭到位 
                        位置2检测到位I AT %IX1.1: BOOL;//打开到位  

                        电机1使能输出Q AT %QX0.2: BOOL;
                        发送站允许入瓶气送准备好输出Q  AT %QX0.3: BOOL;
                        黄色指示灯亮输出Q  AT %QX0.5: BOOL;
                        绿色指示灯亮输出Q  AT %QX0.6: BOOL;
                        子站蜂鸣器输出Q  AT %QX0.7: BOOL;
                         */
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "管道物料检测", item.ReadDisCrete("0").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "样瓶到位检测", item.ReadDisCrete("1").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置1检测到位", item.ReadDisCrete("8").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "位置2检测到位", item.ReadDisCrete("9").ToString()) ? 1 : 0;

                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "电机1使能输出", item.ReadCoil("2").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "发送站允许入瓶气送准备好输出", item.ReadCoil("3").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "黄色指示灯亮输出", item.ReadCoil("5").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "绿色指示灯亮输出", item.ReadCoil("6").ToString()) ? 1 : 0;
                        //res += commonDAO.SetSignalDataValue(item.MachineCode, "子站蜂鸣器输出", item.ReadCoil("7").ToString()) ? 1 : 0;
                        #endregion

                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_样瓶检测", item.ReadDisCrete("1").ToString().ToString()) ? 1 : 0;
                        res += commonDAO.SetSignalDataValue(GlobalVars.MachineCode_QD, item.MachineCode + "_管道物料检测", item.ReadDisCrete("0").ToString().ToString()) ? 1 : 0;
                    }
                }
                #endregion
                output(string.Format("同步下位机信号：{0}条", res), eOutputType.Normal);
                //allDone.WaitOne();

                Thread.Sleep(1000);
                //allDone.Set();
            }
        }
    }
}
