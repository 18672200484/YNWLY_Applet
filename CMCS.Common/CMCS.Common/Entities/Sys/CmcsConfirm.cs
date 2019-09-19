using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Entities.Sys
{
    /// <summary>
    /// 确认信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("cmcstbconfirm")]
    public class CmcsConfirm : EntityBase1
    {
        private string confirmType;
        /// <summary>
        /// 确认类型
        /// </summary>
        public string ConfirmType
        {
            get { return confirmType; }
            set { confirmType = value; }
        }

        private string objectId;
        /// <summary>
        /// 业务Id
        /// </summary>
        public string ObjectId
        {
            get { return objectId; }
            set { objectId = value; }
        }

        private string resultId;
        /// <summary>
        /// 结果Id
        /// </summary>
        public string ResultId
        {
            get { return resultId; }
            set { resultId = value; }
        }
    }
}
