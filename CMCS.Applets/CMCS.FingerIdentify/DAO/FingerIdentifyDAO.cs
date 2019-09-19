using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.FingerIdentify.Enums;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.Enums;
using CMCS.Common.Entities.BeltSampler;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Entities.Fuel;
using System.IO;

namespace CMCS.FingerIdentify.DAO
{
    public class FingerIdentifyDAO
    {
        private static FingerIdentifyDAO instance;

        public static FingerIdentifyDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new FingerIdentifyDAO();
            }

            return instance;
        }

        private FingerIdentifyDAO()
        {

        }

        /// <summary>
        /// 获取用户已注册指纹数
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int GetFingerCount(User user)
        {
            return Dbers.GetInstance().SelfDber.Count<CmcsFinger>("where UserId=:UserId", new { UserId = user.PartyId });
        }

        /// <summary>
        /// 根据指纹名称获取指纹
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fingerName"></param>
        /// <returns></returns>
        public CmcsFinger GetFinerByFingerName(string userId, string fingerName)
        {
            return Dbers.GetInstance().SelfDber.Entity<CmcsFinger>("where UserId=:UserId and FingerName=:FingerName", new { UserId = userId, FingerName = fingerName });
        }

        /// <summary>
        /// 根据用户Id获取指纹数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CmcsFinger> GetFingerByUserId(string userId)
        {
            return Dbers.GetInstance().SelfDber.Entities<CmcsFinger>("where UserId=:UserId", new { UserId = userId });
        }

        /// <summary>
        /// 加入指纹
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool InsertFinger(User user, CmcsFinger userfinger)
        {
            CmcsFinger finger = Dbers.GetInstance().SelfDber.Entity<CmcsFinger>("where UserId=:UserId and FingerName=:FingerName", new { UserId = user.PartyId, FingerName = userfinger.FingerName });
            if (finger == null)
            {
                finger = userfinger;
                finger.UserId = user.PartyId;
                return Dbers.GetInstance().SelfDber.Insert(finger) > 0;
            }
            else
            {
                finger.FingerUrl = userfinger.FingerUrl;
                return Dbers.GetInstance().SelfDber.Update(finger) > 0;
            }
        }

        /// <summary>
        /// 删除用户指纹
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DelFinger(CmcsFinger userfinger)
        {
            CmcsFinger finger = Dbers.GetInstance().SelfDber.Get<CmcsFinger>(userfinger.Id);
            if (finger != null)
            {
                //if (File.Exists(finger.FingerUrl))
                //    Directory.Delete(finger.FingerUrl, true);
                delLicenseFiles(finger.FingerUrl.Substring(0, finger.FingerUrl.LastIndexOf(@"\") + 1), finger.Id);
                return Dbers.GetInstance().SelfDber.Delete<CmcsFinger>(userfinger.Id) > 0;
            }
            return false;
        }

        /// <summary>  
        /// 删除指定文件夹下指定文件名的文件  
        /// </summary>  
        /// <param name="url">文件夹地址</param>  
        /// <param name="name">要删除的文件名</param>--自带去除扩展名  
        /// <returns></returns>  
        public bool delLicenseFiles(string url, string name)
        {
            try
            {
                DirectoryInfo Folder = new DirectoryInfo(url);

                foreach (FileInfo file in Folder.GetFiles())
                {
                    if (name == file.Name.Substring(0, file.Name.LastIndexOf('.')))
                    {
                        file.Delete();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
