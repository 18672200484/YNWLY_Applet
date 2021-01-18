using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.DAO;

namespace CMCS.Common
{
	/// <summary>
	/// 全局变量
	/// </summary>
	public static class GlobalVars
	{
		/// <summary>
		/// 管理员账号
		/// </summary>
		public static string AdminAccount = "admin";
		/// <summary>
		/// 公共程序配置键名
		/// </summary>
		public static string CommonAppletConfigName = "公共配置";
		/// <summary>
		/// 第三方设备上位机心跳状态名
		/// </summary>
		public static string EquHeartbeatName = "上位机心跳";

		#region 皮带采样机

		/// <summary>
		/// 设备编码 - 入场皮带采样机 #1
		/// </summary>
		public static string MachineCode_InPDCYJ_1 = "#1入场皮带采样机";

		/// <summary>
		/// 设备编码 - 入场皮带采样机 #2
		/// </summary>
		public static string MachineCode_InPDCYJ_2 = "#2入场皮带采样机";

		/// <summary>
		/// 设备编码 - 出场皮带采样机 #1
		/// </summary>
		public static string MachineCode_OutPDCYJ_1 = "#1出场皮带采样机";

		/// <summary>
		/// 设备编码 - 出场皮带采样机 #2
		/// </summary>
		public static string MachineCode_OutPDCYJ_2 = "#2出场皮带采样机";

		/// <summary>
		/// 接口类型 - 徐州赛摩皮带采样机
		/// </summary>
		public static string InterfaceType_PDCYJ_In = "徐州赛摩入场皮带采样机";

		/// <summary>
		/// 接口类型 - 徐州赛摩皮带采样机
		/// </summary>
		public static string InterfaceType_PDCYJ_Out = "徐州赛摩出场皮带采样机";

		#endregion

		#region 火车机械采样机

		/// <summary>
		/// 设备编码 - 火车机械采样机 #1
		/// </summary>
		public static string MachineCode_HCJXCYJ_1 = "#1火车机械采样机";

		/// <summary>
		/// 设备编码 - 火车机械采样机 #2
		/// </summary>
		public static string MachineCode_HCJXCYJ_2 = "#2火车机械采样机";

		/// <summary>
		/// 接口类型 - 火车机械采样机
		/// </summary>
		public static string InterfaceType_HCJXCYJ = "徐州赛摩火车机械采样机";

		#endregion

		#region 全自动制样机

		/// <summary>
		/// 设备编码 - 全自动制样机 #1
		/// </summary>
		public static string MachineCode_QZDZYJ_1 = "#1全自动制样机";
		/// <summary>
		/// 设备编码 - 全自动制样机 #2
		/// </summary>
		public static string MachineCode_QZDZYJ_2 = "#2全自动制样机";
		/// <summary>
		/// 设备编码 - 全自动制样机 #3
		/// </summary>
		public static string MachineCode_QZDZYJ_3 = "#3全自动制样机";

		/// <summary>
		/// 接口类型 - 全自动制样机
		/// </summary>
		public static string InterfaceType_QZDZYJ = "全自动制样机";

		/// <summary>
		/// 开元设备编码 - 全自动制样机 #1 开元
		/// </summary>
		public static string MachineCode_QZDZYJ_KY_1 = "01";

		/// <summary>
		/// 开元设备编码 - 全自动制样机 #2 开元
		/// </summary>
		public static string MachineCode_QZDZYJ_KY_2 = "02";

		/// <summary>
		/// 开元设备编码 - 全自动制样机 #3 开元
		/// </summary>
		public static string MachineCode_QZDZYJ_KY_3 = "03";

		#endregion

		#region 智能存样柜

		/// <summary>
		/// 设备编码 - 智能存样柜
		/// </summary>
		public static string MachineCode_CYG1 = "#1智能存样柜";

		/// <summary>
		/// 设备编码 - 智能存样柜
		/// </summary>
		public static string MachineCode_CYG2 = "#2智能存样柜";

		/// <summary>
		/// 开元设备编码 - 智能存样柜
		/// </summary>
		public static string MachineCode_CYG1_KY = "1";

		/// <summary>
		/// 开元设备编码 - 智能存样柜
		/// </summary>
		public static string MachineCode_CYG2_KY = "2";

		#endregion

		#region 气动传输

		/// <summary>
		/// 设备编码 - 气动传输
		/// </summary>
		public static string MachineCode_QD = "气动传输";

		#endregion

		#region 轨道衡

		/// <summary>
		/// 设备编码 - #1轨道衡
		/// </summary>
		public static string MachineCode_GDH_1 = "#1动态衡";

		#endregion

		#region 车号识别

		/// <summary>
		/// 设备编码 - #1火车入厂车号识别
		/// </summary>
		public static string MachineCode_HCRCCHSB = "#1车号识别";

		#endregion

		#region 翻车机

		/// <summary>
		/// 设备编码 - 翻车机
		/// </summary>
		public static string MachineCode_TrunOver = "翻车机";

		/// <summary>
		/// 设备编码 - 翻车机 #1
		/// </summary>
		public static string MachineCode_TrunOver_1 = "#1翻车机";

		/// <summary>
		/// 设备编码 - 翻车机 #2
		/// </summary>
		public static string MachineCode_TrunOver_2 = "#2翻车机";

		/// <summary>
		/// 设备编码 - 火车车号识别 #1
		/// </summary>
		public static string MachineCode_Recognition_1 = "#1火车车号识别";

		/// <summary>
		/// 设备编码 - 火车车号识别 #2
		/// </summary>
		public static string MachineCode_Recognition_2 = "#2火车车号识别";

		#endregion

		#region 汽车机械采样机

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #1
		/// </summary>
		public static string MachineCode_QCJXCYJ_1 = "#1汽车机械采样机";

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #2
		/// </summary>
		public static string MachineCode_QCJXCYJ_2 = "#2汽车机械采样机";

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #1 开元
		/// </summary>
		public static string MachineCode_QCJXCYJ_KY_1 = "1号机";

		/// <summary>
		/// 设备编码 - 汽车机械采样机 #2 开元
		/// </summary>
		public static string MachineCode_QCJXCYJ_KY_2 = "2号机";

		/// <summary>
		/// 设备编码 - 长沙开元矩阵合样归批机 
		/// </summary>
		public static string MachineCode_PackingBatch_KY = "矩阵合样归批机";

		/// <summary>
		/// 卸料机状态
		/// </summary>
		public static string XLJ_Machine = "卸料机";

		/// <summary>
		/// 接口类型 - 长沙开元汽车机械采样机
		/// </summary>
		public static string InterfaceType_CSKY_QCJXCYJ = "长沙开元汽车机械采样机";

		/// <summary>
		/// 接口类型 - 长沙开元矩阵合样归批机
		/// </summary>
		public static string InterfaceType_CSKY_PackingBatch = "长沙开元矩阵合样归批机";
		#endregion

		#region 汽车智能化

		/// <summary>
		/// 设备编码-汽车智能化-入厂端
		/// </summary>
		public static string MachineCode_QC_Queue_1 = "汽车智能化-入厂端";

		public static string MachineCode_XSQC_Queue_1 = "汽车智能化-销售煤入厂端";
		/// <summary>
		/// 设备编码-汽车智能化-#1过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_1 = "汽车智能化-#1过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#2过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_2 = "汽车智能化-#2过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#3过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_3 = "汽车智能化-#3过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#4过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_4 = "汽车智能化-#4过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#5过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_5 = "汽车智能化-#5过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#6过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_6 = "汽车智能化-#6过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#7过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_7 = "汽车智能化-#7过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#8过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_8 = "汽车智能化-#8过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#9过衡端
		/// </summary>
		public static string MachineCode_QC_Weighter_9 = "汽车智能化-#9过衡端";
		/// <summary>
		/// 设备编码-汽车智能化-#1机械采样机端
		/// </summary>
		public static string MachineCode_QC_JxSampler_1 = "汽车智能化-#1机械采样机端";
		/// <summary>
		/// 设备编码-汽车智能化-#2机械采样机端
		/// </summary>
		public static string MachineCode_QC_JxSampler_2 = "汽车智能化-#2机械采样机端";
		/// <summary>
		/// 设备编码-汽车智能化-出厂端
		/// </summary>
		public static string MachineCode_QC_Out_1 = "汽车智能化-出厂端";
		/// <summary>
		/// 设备编码-汽车智能化-#1成品仓
		/// </summary>
		public static string MachineCode_QC_Order_1 = "汽车智能化-#1成品仓";

		#endregion

		#region 化验室网络管理

		/// <summary>
		/// 设备编码 - 化验室网络管理
		/// </summary>
		public static string MachineCode_AssayManage = "化验室网络管理";

		/// <summary>
		/// 设备编码 - 化验室温湿度仪
		/// </summary>
		public static string MachineCode_AssayTemper = "化验室温湿度仪";

		#endregion

		#region 煤场测温仪

		/// <summary>
		/// 设备编码 - 煤场温度测试仪
		/// </summary>
		public static string MachineCode_MCCW = "煤场温度测试仪";

		#endregion

		#region 成品仓

		/// <summary>
		/// 设备编码 - 成品仓 #1
		/// </summary>
		public static string Poduct_Pot_1 = "#1成品仓";

		/// <summary>
		/// 设备编码 - 成品仓 #2
		/// </summary>
		public static string Poduct_Pot_2 = "#2成品仓";

		/// <summary>
		/// 设备编码 - 成品仓 #3
		/// </summary>
		public static string Poduct_Pot_3 = "#3成品仓";
		#endregion

		#region 斗轮机

		/// <summary>
		/// 设备编码 - 斗轮机 #1
		/// </summary>
		public static string MachineCode_DouLunJi_1 = "#1斗轮机";

		/// <summary>
		/// 设备编码 - 斗轮机 #2
		/// </summary>
		public static string MachineCode_DouLunJi_2 = "#2斗轮机";

		#endregion

		/// <summary>
		/// 设备编码 - 矩阵合样归批机
		/// </summary>
		public static string MachineCode_JoinBacth = "矩阵合样归批机";

		/// <summary>
		/// 服务器当前时间
		/// </summary>
		public static DateTime ServerNowDateTime
		{
			get { return Convert.ToDateTime(CommonDAO.GetInstance().SelfDber.ExecuteDataTable("select sysdate from dual").Rows[0][0]); }
		}
	}
}
