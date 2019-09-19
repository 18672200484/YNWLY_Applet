using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
//设备接口类
namespace ThinkCameraSDK.Core.SDK
{

    //委托事件
    //相机信息回调函数
    public delegate void CameraInfoCallBack(ref DeviceInterface.CameraInfo cameraInfo);
    /*******************************************************************
       日志函数回调定义
       参数见LogData定义
       *********************************************************************/

    public delegate void LogCallBack(IntPtr pData);

    /*******************************************************************
       回调函数参数说明
       pData数据内容，见AnalysisData结构体定义

       *********************************************************************/
    public delegate void RealdataCallBack(ref DeviceInterface.AnalysisData pAnalyData);

    /*******************************************************************
     回调函数参数说明
     pUserData 用户数据
     pHandle 相机控制句柄
     cameraStatus状态内容，见CAMERA_STATUS枚举定义

     *********************************************************************/
    public delegate void CameraStatusCallBack(IntPtr pUserData, IntPtr pHandle, DeviceInterface.CAMERA_STATUS cameraStatus);
    /*******************************************************************
        回调函数参数说明
        pHandle 相机句柄
        pUserData 用户数据
        pDeviceStatus数据内容，见pDeviceStatus结构体定义

        *********************************************************************/

    public delegate void DeviceStatusCallback(IntPtr pHandle, IntPtr pUserData, ref DeviceInterface.StDeviceStatus pDeviceStatus);

    /*******************************************************************
    回调函数参数说明
    pHandle 相机句柄
    pUserData 用户数据
    ConnStatus数据内容，见CAMERA_STATUS枚举定义

    *********************************************************************/
    public delegate void NetStatusCallback(IntPtr pHandle, IntPtr pUserData, DeviceInterface.CAMERA_STATUS ConnStatus);


    /*******************************************************************
回调函数参数说明
pHandle 相机句柄
pUserData 用户数据
ui64AlarmInfoData 报警数据
报警数据定义，每一bit表示一个设备状态信息，共计64个bit，最多可支持64种状态信息报警，bit位如果为1表示设备异常，0表示正常，默认为0
具体定义如下：
bit[0]     补光灯（闪光灯） 灯状态，0-正常，1-损坏
bit[1]     Led频闪灯状态，0-正常，1-损坏
bit[2]     线圈状态，0-正常，1-损坏
bit[3]     雷达状态，0-正常，1-损坏或者通信异常
bit[4]     待扩展

pDevRunInfo信息内容，见stDevRunInfo枚举定义

*********************************************************************/
    public delegate void DeviceRunStatusCallback(IntPtr pHandle, IntPtr pUserData, UInt64 ui64AlarmInfoData, ref DeviceInterface.stDevRunInfo DevRunInfo);

    /*******************************************************************
//设备客户端模式，传出设备信息的回调函数
参数说明：
    pHandle 监听到的设备句柄
    pUserData 用户传入数据
    pCameraInfo 设备信息
    cameraStatus 连接状态
*********************************************************************/

    public delegate void CameraHandleCallBack(IntPtr pHandle, IntPtr pUserData, StringBuilder pchCameraIP, DeviceInterface.CAMERA_STATUS cameraStatus);


    /*******************************************************************
//传出流量数据的回调函数
参数说明：
    pHandle 设备句柄
    pUserData 用户传入数据
    pTrafficFlowInfo 流量信息，见TrafficFlowInfo定义
        	
*********************************************************************/

    public delegate void TrafficFlowInfoCallBack(IntPtr pHandle, IntPtr pUserData, ref DeviceInterface.TrafficFlowInfo pTrafficFlowInfo);

    public class DeviceInterface
    {
        //宏定义和结构体定义
        public const int IP_MAX_LEN = 16; //IP地址字符串最大长度
        public const int MACADDR_LEN = 6; //MAC地址长度
        public const int DEVICECODE_LEN = 32;//设备编码长度
        public const int VERSION_LEN = 200;//版本长度
        public const int FIXED_INFO_LEN = 64;//设备安装信息长度
        public const int INVALID_HANLE = 0;//无效句柄
        public const int MAXERRORMSGLEN = 512;//错误描述长字节数
        public const int MAX_XMLFILENAME = 255;//XML文件名称最大长度
        public const int MAX_FILECOUNT = 128;//一个设备最多相关联的XML文件数
        public const int MAX_NAMESPACE_LEN = 32;//命名空间长度
        public const int MAX_NODENAME_LEN = 64;//参数名称最大长度
        public const int MAX_NODECODE_LEN = 20;//参数编号最大长度
        public const int MAX_PARAMSVALUE_LEN = 0x800; //参数值最大长度
        public const int MAX_OBJ_NUM = 8;//最多目标数
        public const int MAX_PLATE_LEN = 16;//车牌号码长度
        public const int MAX_TIME_LEN = 8;//时间长度
        public const int MAX_IMAGE_COUNT = 8;//最多图像数量
        public const int MAX_UPDATE_SENDBUFSIZE = 50 * 1024;//在线升级时，拆分包最大数限制
        public const int MAX_TEXTVALUE_LEN = 256;
        public const int MAX_HELPINFO_LEN = 1024;
        public const int XMLGET_TIMEOUT = 5000;
        public const int PARAMSET_TIEMOUT = 500;
        public const int PARAMGET_TIEMOUT = 500;
        public const int ALIGNMENT_BYTE = 4;
        public const int UPDATEFILENAME_LEN = 128;//升级文件最大程度
        public const int MAXLANE = 4;//最多车道数
        public const int MAX_VEHICLENAME_LEN = 16;      // 车型命名最大长度
        public const int MAX_VEHICLEBRAND_LEN = 32;//车品中字符串最大长度
        public const int MAX_VEHICLEBRANDYEAR_LEN =  64;//车品中年款字符最大长度
        public const int FOCUSING_ENABLE = 1;//调焦使能
        public const int FOCUSING_DISABLE = 0;//调焦不使能

        //函数返回值枚举
        public enum FEEKBACK_TYPE : uint
        {
            RESULT_OK = 0,//执行成功
            PARAMS_ERROR = 1,//参数不正确
            SOCKET_NULL = 2,//SOCKET句柄为NULL
            THREAD_FAIL = 3,//线程失败
            CAMERAINFO_NULL = 4, //接收处理搜索设备信息句柄为NULL
            CREATE_SCAN_FAIL = 5, //生成搜索命令失败
            SEND_SCAN_FAIL = 6,//发送搜索命令失败
            NO_FIND_DEVICE = 7,//没有发现设备
            DEVICE_OPENED = 8,//设备已经初始化打开
            CONTROL_THREAD_FAIL = 9,//生成控制处理数据线程失败
            XML_NULL = 10,//XML解析句柄为NULL
            XML_THREAD_FAIL = 11,//生成XML处理线程失败
            READ_XMLVERSION_FAIL = 12,//获取XML版本识别
            READ_EXTXML_FAIL = 13,//获取扩展XML
            DEVICE_DISCONNECT = 14,//和设备的连接断开
            NO_FIND_XML = 15,//根据命名空间，未找到对应XML文件
            NO_FIND_PARAMSNAME = 16,//没有找到参数名称
            SEND_DICTATE_FAIL = 17,//发送指令失败
            NO_FIND_NODE = 18,//没有找到对应的节点
            CREATE_DICTATE_FAIL = 19,//生成命令失败
            NO_INIT = 20,   //未初始化
            PING_ERROR = 21,//网络ping不通
            NO_HIS_FILE = 22,//没有历史数据
            OTHER_ERROR = 100,//其他错误
            DATA_THREAD_FAIL = 101,//生成数据处理数据线程失败
            CONVERT_BMP_FAIL = 102,//转换BMP失败
            HISDATA_NO_CONNECT = 103,//历史数据连接未连接
            OPEN_UPDATEFILE_FAIL = 150,//打开升级文件错误
            UPDATEFILE_ERROR = 151,//升级文件不正确
            NO_FIND_PCK = 152,//根据传入的参数，没有找到所有PCK文件
            SEND_FILEINFO_FAIL = 153,//发送文件信息失败
            VERSION_ERROR = 154,//版本错误
            LOADDBFILE_FAIL = 155,//装载DB文件失败
            NO_DBFILE = 156,//没有DB文件
            DB_VERSION_NOTSAME = 157,//设备当前DB版本和加载文件不一致
            TIME_OUT = 209//函数执行超时
        }

        //相机状态
        public enum CAMERA_STATUS : int
        {
            ERRORZERO = 0,//没有错误
            CREATE_ERROR = 1,//生成句柄错误
            CONNECT_ERROR = 2,//连接错误
            ABNORMALNET_ERROR = 3,//网络异常错误
            CONNECT_SUCCESS = 4//连接成功

        }
        //出厂信息
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct FactoryInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = DEVICECODE_LEN)]
            public string chDeviceCode;//设备编号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN)]
            public byte[] chMacAddr; //MAC地址
        }
        //用户信息
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct UserInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = FIXED_INFO_LEN)]
            public string chFixAddr;//安装地点
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = FIXED_INFO_LEN)]
            public string chFixDirection;//安装方向
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = FIXED_INFO_LEN)]
            public string chUserDefCode;//用户定义编码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = FIXED_INFO_LEN)]
            public string chUserDefInfo;//用户定义信息

        }

        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct CameraNetInfo
        {
            public UInt32 intCameraIP; //相机IP
            public UInt32 intMaskIP; //子网掩码
            public UInt32 intGatewayIP;  //网关
        }
        //另一个网络参数
         [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct CameraOtherNetInfo
        {
        	
	        public UInt16	ui8DualNet;				// 默认0x09 双网卡标识, other reserve
	        public UInt16	ui8NetName;				// ethxx 0x00:eth0 0x01:eth1; other reserve
	        public UInt32	ui32OtherIP;			// 另外一路IP
	        public UInt32	ui32OtherNetMask;		// 
	        public UInt32	ui32OtherDns;
	        public UInt32	ui32OtherGateWay;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN)]
            public byte[] uiOtherMac; //MAC地址
	              	
        };
        //设备基本信息
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct CameraInfo
        {
            public CameraNetInfo netInfo;
            public UserInfo userInfo;
            public FactoryInfo factoryInfo;
            public CameraOtherNetInfo OtherNetInfo;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VERSION_LEN)]
            public string chDeviceVersion;//设备版本
        }

        //设备日志数据结构体
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct LogData
        {
            public IntPtr pUserData;//用户设置视频数据接收时，传入的用户数据
            public IntPtr hCameraHandle;//相机句柄，表示处理那一台设备的日志数据
            public UInt32 nDataLen;//数据流长度
            public IntPtr pLogData;//日志流数据，二进制数据
        }


        //数据结构体
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct AnalysisData
        {
            public CameraInfo CamInfo;//设备配置信息
            public IntPtr pUserData;//用户设置视频数据接收时，传入的用户数据
            public IntPtr hCameraHandle;//相机句柄，表示处理那一台相机检测数据
            public byte chObjNum;//目标个数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_OBJ_NUM)]
            public IntPtr[] pDataInfo;//数据信息，针对不同的智能分析功能，对应不同的数据结构体（待定）
        }

        //车牌颜色

        public enum PLATE_COLOR : int
        {
            NON_PLATE = 0,//未知车牌颜色
            BLUE_COLOR,//蓝色
            WHITE_COLOR,//白色
            BLACK_COLOR,//黑色
            YELLOW_COLOR//黄色
        }

        //图像格式
        public enum IMAGE_FORMAT : int
        {
            //数据协议包中，图像格式宏定义
            IMGFORMAT_JPG = 0x00,//JPEG图
            IMGFORMAT_RGB = 0x01,//RGB图
            IMGFORMAT_RAW = 0x02,//RAW图
            IMGFORMAT_YUV = 0x03,//YUV图
            IMGFORMAT_BIN = 0x04 ///二值化图
        }

        //图像类型
        public enum IMAGE_TYPE : int
        {
            FULL_IMAGE = 0x00,//全景图
            SPECIAL_IMAGE = 0x01,//特征图
            CARFACE_IMAGE = 0x02,			// 车脸图
            PEOPLEFACE_IMAGE = 0x03,		// 人脸图
            READLIGHT_IMAGE = 0x04,			// 红灯图
            OTHER_IMAGE = 0x05
        }
        //车身颜色
        public enum HW_VHICLECOLOR : int
        {
            VCRDEFAULT = 0,	 // 空
            VCRWHITE = 1, 	 // 白色
            VCRGRAY = 2,		 // 灰色
            VCRYELLOW = 3,		// 黄色
            VCRPINK = 4,		// 粉色
            VCRRED = 5,			// 红色
            VCRPURPLE = 6,		// 紫色
            VCRGREEN = 7,		// 绿色
            VCRBLUE = 8,		// 蓝色
            VCRBROWN = 9,		// 棕色
            VCRBLACK = 10,		// 黑色
            VCROTHER = 11		// 其他色
        };


        // 车身颜色深浅的枚举类型
        public enum HW_VHICLECOLORWEIGHT : int
        {
            VCR_COLORWEIGHTNONE = 0,	// 空
            VCR_LIGHT_COLOR = 1,		// 浅色
            VCR_DARK_COLOR = 2			// 深色
        };

        //车品形态
        public enum VEHICLEBRAND_FLAG:int
        {
	        HEAD_BRAND = 0,//车头
	        TAIL_BRAND = 1,//车尾
	        ROADSIDE_BRAND = 2//路侧（例如：收费站、停车场之类场景）
        };
       
        //早期设备车型
        public enum HW_VEHICLETYPE : int
        {
	        MIDDLEWAGON  =0,    //中型货车
	        MIDDLECAR,			//中客车
	        MIDDLEBUS,			//大客车
	        LARGEWAGON,			//大货车
	        SMALLCAR ,			//小客车  
	        VAN					//面包车
        };

    //新车型分类，大类
    public enum HW_NEWVEHICLE_ROUGHTYPE : int
    {
	    UNKNOWN_TYPE = 0,//未知
	    BIG_TYPE = 1,//大型
	    MIDDLE_TYPE = 2,//中型
	    SMALL_TYPE = 3,//小型
	    TINY_TYPE = 4//微型
    	
    };

//新车型分类，详细类


    public enum HW_NEWVEHICLE_DETAILEDTYPE : int
    {
	    UNKNOWN_DETAILED_TYPE = 0,//	未知/Null
	    BIG_PASSENGERCAR = 1,//	大型客车
	    MIDDLE_PASSENGERCAR = 2,//	中型客车
	    SMALL_CAR = 3,//	轿车
	    SUV_CAR = 4,//	SUV
	    MPV_CAR = 5,//	MPV
	    TINY_VAN = 6,//	微型面包车
	    MIDDLE_VAN = 7,//	中型面包车
	    LARGE_TRUCK = 8,//	大货车
	    SMALL_TRUCK = 9,//	小货车
	    TRAILER_TRUCK = 10,//	挂车
	    CONTAINER_TRUCK = 11,//	集装箱
	    SPECIAL_VEHICLE = 12,//	特种车
	    MOTORCYCLE = 13,//	摩托车
	    TRICYCLE = 14//	三轮车
    };
        //触发模式
        public enum HW_TRIGGERMODE : int
        {
            VIDEO_TRIGGER = 0x20,//视频触发
            LOOP_TRIGGER = 0x00,//线圈触发
            RADAR_TRIGGER = 0x10,//雷达触发
            SOFT_TRIGGER = 0x30//模拟触发
        };

        public enum VEHICLE_PLATEMODE:int//车牌类型

        { 
	        UNKNOWN_PLATEMODE = 0,//未知 
	        SL_PANDJ_PLATEMODE = 1,//单层普通牌及军牌（蓝牌、单层黄牌、单层军牌）
	        DL_PANDJ_PLATEMODE = 2,	//双层黄牌、双层军牌
	        SL_J_PLATEMODE = 3,//单层警牌
	        SL_WJ_PLATEMODE = 4,	//单层WJ牌
	        DL_WJ_PLATEMODE = 5,			//5。双层武警牌
	        SL_SG_PLATEMODE = 6,			//6.  单层使馆牌
	        SL_MH_PLATEMODE = 9,			//9. 单层民航车牌
	        DL_2002_PLATEMODE = 10,	//10.双层2002式车牌
	        SLOWSPEED_PLATEMODE = 11 //11低速车牌
        }; 

        //图像属性
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct ImageAttr
        {
            public IMAGE_TYPE imgType;//图像类型
            public IMAGE_FORMAT imgFormat;//图像格式
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] ucMD5Val;//MD5值，仅对JPG图有效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TIME_LEN)]
            public byte[] chCaptureTime;//每个图像抓拍时间
            public UInt16 usWidth;//图像宽度
            public UInt16 usHeight;//图像高度
            public UInt32 uiImgLen;//图像大小
            public IntPtr pImgData;//图像数据

        }

        //传出的数据
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct TCData
        {
            public UInt32 ui32Serial;//目标序列号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TIME_LEN)]
            public byte[] chTime;//时间，年（减掉2000后的年份）、月、日、时、分、秒、毫秒（占两个字节）
            public UInt32 ui32EventType;			//事件类型(以便于存储)，0-普通事件，1-异常事件（表示有违法事件发生）
            public UInt32 ui32ObjType;			//目标类型，Bit0 –车辆，Bit1-行人，Bit2-非机动车 ，bit位为0，表示无相关信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PLATE_LEN)]
            public byte[] chPlate;//车牌号码
            public PLATE_COLOR PlateColor;//车牌颜色
            public HW_VHICLECOLOR VehicleColor;			//车身颜色
            public HW_VHICLECOLORWEIGHT VehicleColorWeight;//车身颜色深浅
            public UInt32 VehicleType;//车型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLENAME_LEN)]
            public byte[] ucVehicleName;    // 车型命名
            public HW_TRIGGERMODE TriggerMode;//触发模式
            public byte usVehicleSpeed;		// 车辆速度
            public byte ucDirection;        // 行驶方向，0表示下行；1表示上行
            public UInt16 us16PlateStartX;		//车牌起始点X
            public UInt16 us16PlateStartY;		//车牌起始点Y
            public UInt16 us16PlateWidth;			//车牌宽度
            public UInt16 us16PlateHeight;		//车牌高度

            public byte ucLaneId;			//车辆所占车道号，0-左车道，1-右车道
            public UInt32 ViolationInfo;		//车辆违法信息，
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] ucSingleCharCredibility;//车牌识别单个字符可信度
            public byte ucOverallCredibility;	//车牌识别整牌可信度
            public UInt16 usShutter;			//当前抓怕快门值
            public byte ucGain;				//当前抓怕增益值
            public byte ucBrightness;			//当前图像亮度值
            public UInt32 ui32ObjWidth;			//目标宽度(若目标类型为车辆，则为us16PlateWidth)
            public UInt32 ui32ObjHeight;			//目标高度(若目标类型为车辆，则为us16PlateHeight)
            public UInt32 ui32ObjStartx;			//目标起始点坐标X(若目标类型为车辆，则为us16PlateStartX)
            public UInt32 ui32ObjStarty;			//目标起始点坐标Y(若目标类型为车辆，则为us16PlateStartY)

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TIME_LEN)]
            public byte[] chRedLightStartTime;//红灯开始时间，此时间仅对TC500BP（即电警）设备有效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TIME_LEN)]
            public byte[] chRedLightEndTime;//红灯截止时间，此时间仅对TC500BP（即电警）设备有效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLEBRAND_LEN)]
            public byte[] ui8VehicleChildBrand;	//车辆子品牌
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLEBRAND_LEN)]
            public byte[] ui8VehicleBrand;		//车辆品牌
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLEBRAND_LEN)]
            public byte[] ui8VehicleManufacturer;	//车辆厂商
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLEBRAND_LEN)]
            public byte[] ui8VehicleType;    		//车辆类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLEBRAND_LEN)]
            public byte[] ui8VehicleSpecies;		//车辆种类
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLEBRANDYEAR_LEN)]
            public byte[] ui8VehicleYear;			//车辆年款	
            public UInt32 ui32CarBrandSerialNum;		//车品识别编号
	        public byte ui8VehicleBrandFlag;//车品形态
            public VEHICLE_PLATEMODE PlateMode;//车牌类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1975)]
            public byte[] ui8Rsved3;				//预留
            public UInt32 usImageCount;//图像个数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IMAGE_COUNT)]
            public ImageAttr[] ImgAttrs;//图像列表

        }

        //设备状态信息
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct StDeviceStatus
        {
            public Int32 i32Header;		//指令头   0xAA55A55A
            public Int32 i32Version;		//结构体版本
            public UInt32 u32Length;		//结构体的长度

            public Int32 i32StructEnable;	//结构体使能	系统刚启动时暂时未收到AVS消息，相关信息使用默认值，该结构体数据不可信
            //0:不使能 结构体信息不可信  1:使能

            //Net端信息							
            public UInt32 u32ArmBootTime;		//系统启动时间	通过time函数读取自1970年至今的秒数 无时区
            public Int32 i32CtlLinkcnt;		//控制端口连接数
            public Int32 i32DatLinkCnt;		//数据端口连接数
            public Int32 i32LogLinkCnt;		//日志端口连接数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public Int32[] i32NetReserve;

            //AVS端信息
            public UInt32 u32DspBootTime;		//DSP启动时间 	通过time函数读取自1970年至今的秒数 无时区
            public UInt32 u32DspBootCnt;		//DSP启动次数
            public Int32 i32FuctionCode;		//当前加载的DSP程序  目前默认为0
            public Int32 i32StatusCode;		//状态码 DSP AVS NET的状态  目前为 0
            public Int32 i32VideoCode;		//视频编码方式	0:H.264  1:Mpeg2

            public UInt16 ui16ColorMode;     //视频色彩模式
            public UInt16 ui16Bright;        //当前亮度
            public UInt32 ui32Shutter;       //当前快门
            public UInt32 ui32Slice;         //当前日夜片


        }

        //操作缓存数据用到的一些定义
        //缓存类型
        public enum BUFFER_TYPE : int
        {
            NO_BUFFER = 0,//没有缓存
            DOC_BUFFER = 1,//doc缓存方式
            TF_BUFFER = 2//TF卡缓存方式
        }

        //存储读取规则
        public enum READ_OR_SAVE_RULE : int
        {
            ILLEGALANDNORMAL = 0,//违法车辆和普通车辆都存储或读取
            ONLY_ILLEGAL = 1,//只存储或读取违法车辆
            ONLY_NORMAL = 2//只存储或读取正常车辆
        }

        //读取方式
        public enum READ_MODE : int
        {
            NO_TIME_LIMIT = 0,//没有时段限制
            DATE_LIMIT = 1,//按日期读
            TIMEINTERVAL_LIMIT = 2//按时段读
        }

        //时间定义
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct stClockParmas
        {
            public UInt32 uint32year;
            public UInt32 uint32month;
            public UInt32 uint32date;
            public UInt32 uint32hour;
            public UInt32 uint32minute;
            public UInt32 uint32second;
            public UInt32 uint32minisecond;
        };
        //单车道车流量统计信息
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct SingleLaneFlowInfo
        {
            public float fl32IdentifyRate;    //识别率
            public float fl32LaneRate;        //车道占有率
            public float fl32CarHeadGap;      //平均车头距
            public float fl32CarSpeed;        //平均车速

            //2012.12.10扩展的车型统计信息
            public UInt32 ui32CartNum;         //大车的数目
            public UInt32 ui32HandCartNum;     //小车的数目
            public UInt32 ui32ForeignNum;      //涉外车的数目
            public UInt32 ui32PoliceNum;       //军警车的数目
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] fl32Resvd1;//预留
        };

        //总的车流量统计信息
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct LaneFlowInfo
        {

            public float fl32IdentifyRate;    //识别率
            public float fl32CarSpeed;        //平均车速

            //2012.12.10扩展的车型统计信息
            public Int32 ui32TotalCartNum;    //统计到的总车道的大车的数目
            public Int32 ui32TotalHandCartNum;//统计到的总车道的小车的数目
            public Int32 ui32TotalForeignNum; //总车道的涉外车辆的数目
            public Int32 ui32TotalPoliceNum;  //总车道的军警车辆的数目
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32[] fl32Resvd1;//预留
        };



        //整体车流量统计信息
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct TrafficFlowInfo
        {
            public CameraInfo CamInfo;//设备配置信息
            public stClockParmas stStartClock;  //上传开始时间
            public stClockParmas stEndClock;    //上传结束时间
            public LaneFlowInfo strcLaneFlowInfo;	//总的车辆统计信息
            public UInt32 ui32LaneNum;// 车道数
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXLANE)]
            public SingleLaneFlowInfo[] strFlowInfo; //4个车道的统计信息
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public float[] fl32Resv;	//预留
        };


        //设备运行信息结构体   128字节   2012.12.21
        [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
        public struct stDevRunInfo
        {
            public CameraInfo CamInfo;//对应的设备配置信息
            public UInt32 ui32CamType;					//相机类型，0-TC200,1-TC500,2-TC280
            public UInt32 ui32ApplicationType;			//应用类型，0-AT1,1-AT2,2-AP1,3-AP2,4-待扩展
            public UInt32 ui32NerRunMode;					//net 运行模式，待定义，具体由netcontrol运行模式来确定
            public UInt32 ui32AVRunMode;					//av server运行模式，待定义，具体由avserver运行模式来确定
            public UInt32 ui32DspRunMode;					//dsp 运行模式，待定义，具体由dsp运行模式来确定
            public Byte int8MainBoardTemp;				//主板温度,bit[7]表示负数，范围-127~127
            public Byte int8PowerBoardTemp;				//电源板温度，,bit[7]表示负数，范围-127~127
            public Byte int8DocFlag;					//0-无，1-8G，2-16G，3-32G，4-64G
            public Byte Int8TFFlag;						//0-无，1-8G，2-16G，3-32G，4-64G
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            public UInt32[] ui32Rsv;	//预留

        };

        //接口函数声明

        /*******************************************************************
        函数实体：DWORD WINAPI HWTC_InitControl(CameraInfoCallBack fucCallback);
        函数名称: HWTC_InitControl
        函数说明：初始化控制库，并设置响应相机参数的回调函数
        参数说明：fucCallback响应相机参数的回调函数，见CameraInfoCallBack回调函数定义，
        该回调函数在线程中响应
        返回值：
            返回值是RESULT_OK表示成功，否则返回错误代码
	
        回调函数参数说明
        cameraInfo设备的基本信息，见CameraInfo结构体定义

        *********************************************************************/
        [DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "HWTC_InitControl", SetLastError = true)]
        public static extern UInt32 HWTC_InitControl(CameraInfoCallBack fucCallback);

        /*******************************************************************
        函数实体：DWORD WINAPI HWTC_Quit();
        函数名称: HWTC_Quit
        函数说明：退出SDK，释放所有资源
        参数说明：
        返回值：
        RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/
        [DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_Quit();


        /*******************************************************************
        函数实体：DWORD WINAPI HWTC_SearchCamera();
        函数名称: HWTC_SearchCamera
		  函数说明：搜索局域网内所有的设备
		  参数说明：
		  返回值：
			RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/
        [DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_SearchCamera();


        /*******************************************************************
    函数实体：DWORD WINAPI HWTC_OpenCamera(char *pchCameraIP, PCameraHandle pHandle,void * pUserData,CameraStatusCallBack StatusCallback);
    函数名称: HWTC_OpenCamera
    函数说明：打开指定IP设备的连接
    参数说明：pchCameraIP待连接的设备IP地址
    pHandle执行成功后，返回相机句柄；否则为INVALID_HANLE
    pUserData 传入用户数据，在回调时会用到
    StatusCallback 设备连接状态回调函数，
    返回值：

	    ESULT_OK表示成功，否则返回错误代码
    *********************************************************************/


        [DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "HWTC_OpenCamera", SetLastError = true)]
        public static extern UInt32 HWTC_OpenCamera(UInt32 intCameraIP, ref IntPtr pHandle, IntPtr ptrUserData, CameraStatusCallBack StatusCallback);


        /*******************************************************************
    函数实体：unsigned int __stdcall HWTC_ControlShutter(PCameraHandle pHandle, unsigned int nValue,unsigned int nTimeout)
    函数名称：HWTC_ControlShutter
    函数说明：控制快门值
    参数说明:
    pHandle相机句柄，由HWTC_ConnectCamera获取
    nValue 设置值
    nTimeout 执行命令超时时间
    返回值：
    返回值是RESULT_OK表示成功，否则返回错误代码


    *******************************************************************/
        [DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_ControlShutter(IntPtr pHandle, UInt32 nValue, IntPtr nTimeout);

        /*******************************************************************
        函数实体：DWORD WINAPI HWTC_CloseCamera(PCameraHandle pHandle);
        函数名称: HWTC_CloseCamera
        函数说明：关闭指定IP设备的连接
        参数说明：
	        要关闭的相机句柄pHandle
        返回值：
        RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/

        [DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_CloseCamera(IntPtr pHandle);
        /*******************************************************************
        函数实体：DWORD WINAPI HWTC_EnableCameraLog(bool bEnable);
        函数名称: HWTC_EnableCameraLog
        函数说明：是否开启相机传输日志功能，如果开启，相机则往上位机传输日志数据流；反之不传
        参数说明：
        intCameraIP 待接收日志的相机IP
        pHandle 反馈相机日志接收句柄
        bEnable true传输，false不传输
        返回值：
        RESULT_OK表示成功，否则返回错误代码
        注意：该功能仅供内部软件平台调用，对外不提供该接口功能
        ********************************************************************/
        //[DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi)]
        //public static extern UInt32 HWTC_EnableCameraLog(UInt32 intCameraIP, ref IntPtr pHandle,bool bEnable);




        /*******************************************************************
        函数实体：DWORD WINAPI HWTC_SetRevLogMode(void *pUserData,LogCallBack fucCallback,HWND hWnd,DWORD dwThreadID,DWORD dwMsg);
        函数名称：HWTC_SetRevLogMode
        函数说明：设置日志数据接收方式。接收数据分为两种方式，一种为回调函数方式；一种采用消息机制（消息机制中如果需要窗体接收消息时，需传入窗体句柄，如果需要线程接收消息时，则需传入线程ID）
        回调函数时，需要设置回调函数入口，并且可以传入用户数据，在回调函数参数中传出，回调函数定义如下：

        消息机制：根据用户定义的接收消息句柄和消息号，将解析的日志流以消息的方式通知用户接收，采用消息收数据时，消息参数WPARAM为0，
        LPARAM为一个相机句柄，用户通过HWTC_GetLogData获取日志数据。
        三种接收数据只能选择一种，如果用户不需要处理数据时，则需要将回调函数以及接收句柄和ID同时设置为NULL即可
        参数说明：	pUserData 用户数据			
        fucCallback 回调函数
        hWnd 接收消息的窗体句柄
        dwThreadID  接收消息的线程ID
        dwMsg 消息号
        返回值：
        RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/

        [DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "HWTC_SetRevLogMode", SetLastError = true)]
        public static extern UInt32 HWTC_SetRevLogMode(IntPtr pUserData, LogCallBack fucCallback,
             IntPtr hWnd, UInt32 dwThreadID, UInt32 dwMsg);


        /*******************************************************************
        函数实体：DWORD WINAPI HWTC_GetLogData (PCameraHandle pHandle,LogData*pData)
        函数名称：HWTC_GetLogData
        函数说明：获取日志数据
        参数说明:
        pHandle相机句柄，由响应消息参数LPARAM获取
        pData：要获取的日志数据，见LogData结构体定义
        返回值：
        RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/
        [DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_GetLogData(IntPtr pHandle, ref LogData pData);

        /*******************************************************************
           函数实体：unsigned int __stdcall HWTC_ControlShutter(PCameraHandle pHandle, unsigned int nValue,unsigned int nTimeout)
        函数名称：HWTC_ControlShutter
       函数说明：控制快门值
参数说明:
pHandle相机句柄，由HWTC_ConnectCamera获取
nValue 设置值
nTimeout 执行命令超时时间
返回值：
返回值是RESULT_OK表示成功，否则返回错误代码


*******************************************************************/
        [DllImport("HWTC_Control.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_ControlShutter(IntPtr pHandle, UInt32 nValue, UInt32 nTimeout);


        //数据接收库接口
        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_SetCallbackType(HWND hWnd);
        函数名称: HWTC_SetCallbackType
        函数说明：设置回调函数响应方式
        参数说明：
        hWnd 此参数是NULL时，所有回调函数在动态库的线程中调用；反之，在外部程序建立的线程或进程中响应
        返回值：
        返回值是RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/
        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_SetCallbackType(IntPtr hWnd);
        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_ConnectCamera(char *pchCameraIP, PCameraHandle *pHandle);
        函数名称: HWTC_ConnectCamera
        函数说明：打开指定IP设备的连接
        参数说明：pchCameraIP待连接的设备IP地址
        pHandle执行成功后，返回相机句柄；否则为INVALID_HANLE
        返回值：
        返回值是RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/

        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_ConnectCamera(StringBuilder pchCameraIP, ref IntPtr pHandle);

        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_DisconnectCamera(PCameraHandle pHandle)
        函数名称: HWTC_DisconnectCamera
        函数说明：断开指定IP设备的连接
        参数说明：
        pHandle 设备句柄，通过HWTC_ConnectCamera获取
        返回值：
        返回值是RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/

        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_DisconnectCamera(IntPtr pHandle);


        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_SetRecRealtimeDataMode(PCameraHandle pHandle,
        void *pUserData,
        RealdataCallBack fucCallback,
        HWND hWnd,unsigned int dwThreadID,unsigned int dwMsg);
        函数名称：HWTC_SetRecRealtimeDataMode
        函数说明：设置实时数据接收方式。接收实时数据分为两种方式，一种为回调函数方式；一种采用消息机制
            （消息机制中如果需要窗体接收消息时，需传入窗体句柄，如果需要线程接收消息时，则需传入线程ID）
            回调函数时，需要设置回调函数入口，并且可以传入用户数据，在回调函数参数中传出，回调函数定义如下：
            typedef void(CALLBACK * RealdataCallBack)( AnalysisData *pData);

            消息机制：根据用户定义的接收消息句柄和消息号，将解析的实时以消息的方式通知用户接收，采用消息收数据时，消息参数WPARAM为0，LPARAM为相机句柄类型，即pHandle，
            需要用户响应消息后调用HWTC_GetRealdata获取实时数据，调用该函数获取实时数据时，第一个参数为LPARAM参数
            两种接收数据只能选择一种，如果用户不需要处理数据时，则需要将回调函数以及接收句柄同时设置为NULL即可。
        参数说明：pHandle设备句柄，由HWTC_ConnectCamera函数返回，如果该参数为NULL，则认为是设置所有的设备接收数据方式；反之，设置对应设备的数据接收方式
            pUserData 用户数据			
            fucCallback 回调函数
            hWnd 接收消息句柄
            dwThreadID 接收消息的线程ID
            dwMsg 消息号
        返回值：返回值是RESULT_OK表示成功，否则返回错误代码

        *********************************************************************/
        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "HWTC_SetRecRealtimeDataMode", SetLastError = true)]
        public static extern UInt32 HWTC_SetRecRealtimeDataMode(IntPtr pHandle,
             IntPtr pUserData, RealdataCallBack fucCallback,
             IntPtr hWnd,
            UInt32 dwThreadID,
            UInt32 dwMsg);

        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_GetAnalysisData (PCameraHandle pHandle, AnalysisData *pData);
        函数名称：HWTC_GetAnalysisData
        函数说明：获取实时数据。在响应消息时调用该函数获取实时数据
        参数说明：
        pHandle设备句柄，由HWTC_ConnectCamera函数返回
        pData实时数据，见AnalysisData结构体定义
        返回值：
        返回值是RESULT_OK表示成功，否则返回错误代码

        *********************************************************************/
        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_GetAnalysisData(IntPtr pHandle, ref AnalysisData pData);



        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_Capture(PCameraHandle pHandle)
        函数名称：HWTC_ Capture
        函数说明：强制抓拍，向设备发送抓拍命令，设备抓拍图像和采集信息，并上传。该函数只保证参数下发成功后立即返回，不考虑设备是否执行该命令
        参数说明:
        pHandle相机句柄，由HWTC_ConnectCamera获取
        返回值：
        返回值是RESULT_OK表示成功，否则返回错误代码

        *******************************************************************/
        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_Capture(IntPtr pHandle);

        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_CaptureEx(PCameraHandle pHandle,unsigned int nTimeout)
        函数名称：HWTC_ CaptureEx
        函数说明：强制抓拍，向设备发送抓拍命令，设备抓拍图像和采集信息，并上传。该函数下发命令后，等待设备响应，直到收到设备传出的信息或者超时时间到，才返回。
		        参数说明:
        pHandle相机句柄，由HWTC_ConnectCamera获取
        nTimeout 阻塞超时时间
        返回值：
		        返回值是RESULT_OK表示成功，否则返回错误代码

        *******************************************************************/
        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_CaptureEx(IntPtr pHandle, UInt32 nTimeout);

        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_CheckStatusEx(PCameraHandle pHandle , CAMERA_STATUS *pConnStatus)
        函数名称：HWTC_CheckStatusEx
        函数说明：检测状态
        参数说明:
        pHandle相机句柄，由HWTC_ConnectCamera获取
        pConnStatus：状态标记，见CAMERA_STATUS枚举
        返回值：
        返回值是RESULT_OK表示成功，否则返回错误代码
        *******************************************************************/

        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_CheckStatusEx(IntPtr pHandle, ref CAMERA_STATUS pConnStatus);

        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_AdjustTime(PCameraHandle pHandle, DWORD64 dwTimeMs = 0)
        函数名称：HWTC_AdjustTime
        函数说明：给设备校时
        参数说明:
        pHandle相机句柄，由HWTC_ConnectCamera获取
        时间值，(1970-1-1 0:0:0以来的毫秒数)，64位整数；默认为0，当为0时，直接获取当前系统时间，进行校时
        返回值：
        返回值是RESULT_OK表示成功，否则返回错误代码


        *******************************************************************/

        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_AdjustTime(IntPtr pHandle, UInt64 dwTimeMs);

        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_RecordLog(bool bWriteLog);
        函数名称: HWTC _RecordLog
        函数说明：记录相机操作中的相关日志。日志文件自动生成在调用程序所在目录下，自动建立CameraDataLog文件夹，然后日志名称以时间命名，例如：“20110101.log”
        参数说明：
        bWriteLog true记录日志，false不记录日志
        返回值：
        RESULT_OK表示成功，否则返回错误代码

        *******************************************************************/

        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_RecordLog(bool bWriteLog);

        /*********************************************************************
        函数实体：HWTCDATAAPI unsigned int __stdcall HWTC_BinaryTOBmp(char*chBmpData,	
        BYTE *pbSrc,	
        int iSrcWidth,	
        int iSrcHeight
        );
        函数名称: HWTC _RecordLog
        函数说明：转换二值化图数据成BMP文件数据
        参数说明：
        chBmpFile	Bmp存储文件名称及路径。
        pbSrc	二值化图数据
        iSrcWidth	图像宽度
        iSrcHeight	图像高度
        返回值：
        RESULT_OK表示成功，否则返回错误代码


        *********************************************************************/

        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_BinaryTOBmp(StringBuilder chBmpFile,
                                   IntPtr pbSrc,
                                  int iSrcWidth,
                                  int iSrcHeight);


        /*********************************************************************
函数实体：HWTCDATAAPI unsigned int __stdcall HWTC_BinaryTOBmpData(unsigned char *pBmpData,	int *pBmpDataLen,
unsigned char *pbSrc,	
int iSrcWidth,	
int iSrcHeight
);
函数名称: HWTC_BinaryTOBmpData
函数说明：转换二值化图数据成BMP文件数据
参数说明：
pBmpData	Bmp数据，最大分配空间是400个字节
pBmpDataLen  传入pBmpData分配的空间，返回实际Bmp数据长度
pbSrc	二值化图数据
iSrcWidth	图像宽度
iSrcHeight	图像高度
返回值：
RESULT_OK表示成功，否则返回错误代码


*********************************************************************/

        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_BinaryTOBmpData(IntPtr pBmpData,
                                                               ref int pBmpDataLen,
                                                           IntPtr pbSrc,
                                                           int iSrcWidth,
                                                           int iSrcHeight
                                                           );

        /*******************************************************************
        函数实体：unsigned int __stdcall HWTC_RegStatusCallback(
        PCameraHandle pHandle,
        void *pUserData,
        DeviceStatusCallback fucDeviceStatus,
        NetStatusCallback fucNetStatus
        );
        函数名称: HWTC_RegStatusCallback
        函数说明：注册状态回调函数
        参数说明：
        pHandle 相机句柄
        pUserData 用户数据
        fucDeviceStatus 设备工作状态回调函数
        fucNetStatus 设备连接网络状态回调函数
        返回值：RESULT_OK表示成功，否则返回错误代码

        ******************************************************************/
        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "HWTC_RegStatusCallback", SetLastError = true)]
        public static extern UInt32 HWTC_RegStatusCallback(IntPtr pHandle,
                        IntPtr pUserData,
                        DeviceStatusCallback fucDeviceStatus,
                        NetStatusCallback fucNetStatus,
                        DeviceRunStatusCallback fucRunStatus);


        /*******************************************************************
函数实体：HWTCAPI unsigned int __stdcall HWTC_DataServer(char *pchServerIP,unsigned int nServerPort,bool bOpen);
函数名称: HWTC_DataServer
函数说明：启用服务，监听设备数据端连接。建议所连接设备IP固定时，不用采用此方式
参数说明：
pchServerIP 服务器端IP地址
nServerPort 端口号
bOpen true表示启用，false表示关闭对应的监听服务
返回值：RESULT_OK表示成功，否则返回错误代码
*********************************************************************/
        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi)]
        public static extern UInt32 HWTC_DataServer(StringBuilder pchServerIP, UInt32 nServerPort, bool bOpen);


        /*******************************************************************
        函数实体：HWTCAPI unsigned int __stdcall HWTC_RegCameraDataHandleCallBack(CameraHandleCallBack fucCallback,void *pUserData);
        函数名称: HWTC_RegCameraDataHandleCallBack
        函数说明：注册数据端句柄回调函数
        参数说明：
        fucCallback 回调函数，见CameraHandleCallBack声明
        pUserData 用户传入数据，在回调函数时传出
        返回值：RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/
        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "HWTC_RegCameraDataHandleCallBack", SetLastError = true)]
        public static extern UInt32 HWTC_RegCameraDataHandleCallBack(CameraHandleCallBack fucCallback, IntPtr pUserData);



        /*******************************************************************
        函数实体：HWTCAPI unsigned int __stdcall HWTC_RegTrafficFlowInfoCallBack(PCameraHandle pHandle,TrafficFlowInfoCallBack fucCallback,void *pUserData);
        函数名称: HWTC_RegTrafficFlowInfoCallBack
        函数说明：注册数据端句柄回调函数
        参数说明：
        pHandle 相机句柄，该参数为NULL，则该函数只调用一次，表示所有设备采集流量数据均由一个回调函数响应。
        fucCallback 回调函数，见TrafficFlowInfoCallBack定义
        pUserData 用户传入数据，在回调函数时传出
        返回值：RESULT_OK表示成功，否则返回错误代码
        *********************************************************************/

        [DllImport("HWTC_Data.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, EntryPoint = "HWTC_RegTrafficFlowInfoCallBack", SetLastError = true)]
        public static extern UInt32 HWTC_RegTrafficFlowInfoCallBack(IntPtr pHandle, TrafficFlowInfoCallBack fucCallback, IntPtr pUserData);


    }
}
