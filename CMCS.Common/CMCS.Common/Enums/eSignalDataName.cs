using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
    /// <summary>
    /// 信号名
    /// </summary>
    public enum eSignalDataName
    {
        #region 综合
        系统,
        程序状态,
        设备状态,
        //系统,
        当前车号,
        当前车号2,
        当前车Id,
        当前车Id2,
        超时预警,
        超时预警2,
        #endregion

        #region 皮带秤

        速度,
        累计量,
        瞬时流量,

        #endregion

        #region 汽车智能化

        当前运输记录Id,

        地感1信号,
        地感2信号,
        地感3信号,
        地感4信号,
        地感5信号,
        地感6信号,
        地感7信号,
        地感8信号,

        确认按钮,

        对射1信号,
        对射2信号,
        对射3信号,

        信号灯1,
        信号灯2,

        道闸1升杆,
        道闸2升杆,
        道闸3升杆,
        道闸4升杆,

        上磅方向,
        地磅仪表_稳定,
        地磅仪表_实时重量,

        车号识别1_连接状态,
        车号识别2_连接状态,
        LED屏1_连接状态,
        LED屏2_连接状态,
        地磅仪表_连接状态,
        IO控制器_连接状态,

        #endregion

        #region 机械采样机
        采样编码,
        开始时间,
        采样点数,
        采样流程状态,
        #endregion

        #region 封装归批机
        操作模式,
        #endregion

        #region 火车车号识别
        当前记录Id,
        当日已过车数,
        当日待翻车数,
        #endregion

        #region 斗轮机
        大车行走位置,
        #endregion
    }
}
