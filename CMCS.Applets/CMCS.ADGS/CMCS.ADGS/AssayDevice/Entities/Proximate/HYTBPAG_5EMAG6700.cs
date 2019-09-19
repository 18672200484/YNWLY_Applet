using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.DapperDber.Attrs;

namespace CMCS.ADGS.Win.Entities
{
    /// <summary>
    /// ������
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("HYTBGFY")]
    public class HYTBPAG_5EMAG6700
    {
        [DapperPrimaryKey]
        public string PKID { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public string SampleName { get; set; }
        /// <summary>
        /// ��Ʒ˳��
        /// </summary>
        public int ObjCode { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>	 
        public DateTime Date_Ex { get; set; }
        /// <summary>
        /// ��ˮ����
        /// </summary>		
        public decimal Mad { get; set; }
        /// <summary>
        /// �ոɻ��ҷ�
        /// </summary>		
        public decimal Aad { get; set; }
        /// <summary>
        /// �ոɻ��ӷ���
        /// </summary>		
        public decimal Vad { get; set; }
        /// <summary>
        /// ˮ�ҿ�������
        /// </summary>		
        public decimal EmptyGGWeight { get; set; }
        /// <summary>
        /// ˮ��ú����
        /// </summary>		
        public decimal ColeWeight { get; set; } 
        /// <summary>
        /// ��ֵ
        /// </summary>		
        public decimal Qbad { get; set; }
        /// <summary>
        /// ������Ա
        /// </summary>		
        public string Operator { get; set; }
        /// <summary>
        /// �豸���
        /// </summary>
        public string MachineCode { get; set; }
    }
}
