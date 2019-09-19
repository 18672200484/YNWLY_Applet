using System;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.TrainInFactory
{

    [Serializable]
    [CMCS.DapperDber.Attrs.DapperBind("CmcsTbtrainRecognition")]
    public class CmcsTrainRecognition : EntityBase2
    {
        public Decimal OrderNumber { get; set; }
        public String CarModel { get; set; }
        public String CarNumber { get; set; }
        public DateTime CrossTime { get; set; }
        public decimal Speed { get; set; }
        public int Status { get; set; }
        public string MachineCode { get; set; }
        public string DataFlag { get; set; }
    }
}
