using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using myonline.Framework.Data;
using System.Configuration;
using myonline.Framework.Util;
using System.Data;

namespace myonline.Biz.ADM
{
    public class ClsAudioRecording
    {
        #region "VARIABLES"
        string _strCon = string.Empty;
        #endregion

        #region "PROPERTIES"
        public int Flag { get; set; }

        public int VideoRecordingId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public string AudioFile { get; set; }

        public string ImageFile { get; set; }

        public int AudioRecordingType { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string AreaType { get; set; }

        public int Top { get; set; }

        public string FromDate { get; set; }

        public string Time { get; set; }

        public string ToDate { get; set; }

        public int AudioRecordingId { get; set; }

        public int videoId { get; set; }
        
        public string BlobData { get; set; }

       
        
        #endregion

        #region "CONSTRUCTOR"
        public ClsAudioRecording()
        {
            //string sWebSite = ConfigurationSettings.AppSettings["RunServer"].ToString();
            //string sPWD = "";
            //string sUSERID = "";
            //string sDBIP = "";
            //string sDBNAME = "";

            //if (sWebSite == "TEST")
            //{
            //    sDBIP = System.Configuration.ConfigurationSettings.AppSettings["DBTS_TESTDB_IP"].ToString();
            //    sDBNAME = System.Configuration.ConfigurationSettings.AppSettings["DBTS_TESTDB_NAME"].ToString();
            //    sUSERID = (System.Configuration.ConfigurationSettings.AppSettings["DBTS_TESTDB_USERID"].ToString());
            //    sPWD = (System.Configuration.ConfigurationSettings.AppSettings["DBTS_TESTDB_PWD"].ToString());

            //    _strCon = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", sDBIP, sDBNAME, sUSERID, sPWD);
            //    // _strCon = string.Format("Data Source=BR2;Initial Catalog=db_myonline_13;Integrated Security=true");
            //}
            //else
            //{
            //    sDBIP = System.Configuration.ConfigurationSettings.AppSettings["DBTS_REALDB_IP"].ToString();
            //    sDBNAME = System.Configuration.ConfigurationSettings.AppSettings["DBTS_REALDB_NAME"].ToString();
            //    sUSERID = (System.Configuration.ConfigurationSettings.AppSettings["DBTS_REALDB_USERID"].ToString());
            //    sPWD = (System.Configuration.ConfigurationSettings.AppSettings["DBTS_REALDB_PWD"].ToString());

            //    _strCon = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", sDBIP, sDBNAME, sUSERID, sPWD);
            //}

            _strCon = ClsCommon.ConnectionString;
        }
        #endregion

        #region "USER DEFINED FUNCTIONS"
        public DataTable GetDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = 
                {    
                    new SqlParameter("@Flag",Flag)                    
                    ,new SqlParameter("@PatientId",  PatientId)    
                    , new SqlParameter("@AudioRecordingType",  AudioRecordingType)   
                    , new SqlParameter("@AreaType",  AreaType)   
                    , new SqlParameter("@Top",  Top)   
                };

                using (DBHelper common = new DBHelper(_strCon, myonline.Framework.Data.TransactionType.None))
                {
                    dt = common.ExecuteDataTableSP("usp_AudioRecording", param);
                }
            }
            catch { }
            return dt;
        }

        public DataTable Get_Latest_AudioFile()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = 
                {    
                     new SqlParameter("@PatientId",  PatientId)    
                    , new SqlParameter("@AudioRecordingType",  AudioRecordingType)   
                   
                };

                using (DBHelper common = new DBHelper(_strCon, myonline.Framework.Data.TransactionType.None))
                {
                    dt = common.ExecuteDataTableSP("USP_Get_Latest_Audio_File", param);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlingClass.HandleException(ex);
            }
            return dt;
        }

        public bool Save()
        {
            bool hasSave = false;

            try
            {
                SqlParameter[] param = 
                {     new SqlParameter("@Flag",Flag)                                
                    , new SqlParameter("@PatientId",  PatientId)  
                    , new SqlParameter("@DoctorId",  DoctorId)  
                    , new SqlParameter("@AudioFile",   AudioFile)                      
                    , new SqlParameter("@AudioRecordingType",   AudioRecordingType)    
                     , new SqlParameter("@CreatedDate",  CreatedDate) 
                      , new SqlParameter("@AreaType",  AreaType) 
                      , new SqlParameter("@BlobData", BlobData)
                };

                using (DBHelper common = new DBHelper(_strCon, myonline.Framework.Data.TransactionType.None))
                {
                    if (common.ExecuteNonQuerySP("usp_AudioRecording", param) > 0)
                    {
                        hasSave = true;
                    }
                }
            }
            catch { }

            return hasSave;
        }

        public bool DeleteMonitorVideo()
        {
            bool hasSave = false;

            try
            {
                SqlParameter[] param = 
                {     new SqlParameter("@Flag",3)                                
                    , new SqlParameter("@videoId",  videoId) 
                };

                using (DBHelper common = new DBHelper(_strCon, myonline.Framework.Data.TransactionType.None))
                {
                    if (common.ExecuteNonQuerySP("SaveAllInOneMonitorVideos", param) > 0)
                    {
                        hasSave = true;
                    }
                }
            }
            catch { }

            return hasSave;
        }

        public DataSet GetMonitorVideo()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] param = 
                {    
                     new SqlParameter("@flag",  2)    
                     ,new SqlParameter("@PatientId",PatientId) 
                     ,new SqlParameter("@Time",Time)
                     ,new SqlParameter("@fromDate",FromDate)
                     ,new SqlParameter("@todate",ToDate)
                };

                using (DBHelper common = new DBHelper(_strCon, myonline.Framework.Data.TransactionType.None))
                {
                    dt = common.ExecuteDataTableSP("SaveAllInOneMonitorVideos", param);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlingClass.HandleException(ex);
            }
            ds.Tables.Add(dt);
            return ds;
            //return dt;
        }
        public bool SaveMonitorVideo()
        {
            bool hasSave = false;

            try
            {
                SqlParameter[] param = 
                {  
                   new SqlParameter("@flag",Flag)                      
                  ,new SqlParameter("@PatientId",PatientId)                      
                  ,new SqlParameter("@File",AudioFile)                                          
                  ,new SqlParameter("@ImageFile",ImageFile) 
                  ,new SqlParameter("@fromDate",FromDate)
                  ,new SqlParameter("@todate",ToDate)                                
                };

                using (DBHelper common = new DBHelper(_strCon, myonline.Framework.Data.TransactionType.None))
                {
                    if (common.ExecuteNonQuerySP("SaveAllInOneMonitorVideos", param) > 0)
                    {
                        hasSave = true;
                    }
                }
            }
            catch { }

            return hasSave;
        }

        public bool ServiceSave()
        {
            bool hasSave = false;

            try
            {
                SqlParameter[] param = 
                {     new SqlParameter("@Flag",Flag)                                
                    , new SqlParameter("@PatientId",  PatientId)  
                    , new SqlParameter("@DoctorId",  DoctorId)  
                    , new SqlParameter("@AudioFile",   AudioFile)                      
                    , new SqlParameter("@AudioRecordingType",   AudioRecordingType)    
                     , new SqlParameter("@CreatedDate",  CreatedDate) 
                      , new SqlParameter("@AreaType",  AreaType)  
                };

                using (DBHelper common = new DBHelper(_strCon, myonline.Framework.Data.TransactionType.None))
                {
                    if (common.ExecuteNonQuerySP("usp_Service_AudioRecording", param) > 0)
                    {
                        hasSave = true;
                    }
                }
            }
            catch { }

            return hasSave;
        }

        public DataTable ServiceGetDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] param = 
                {    
                    new SqlParameter("@Flag",Flag)                    
                    ,new SqlParameter("@PatientId",  PatientId)    
                    , new SqlParameter("@AudioRecordingType",  AudioRecordingType)   
                    , new SqlParameter("@AreaType",  AreaType)   
                    , new SqlParameter("@FromDate",  FromDate == string.Empty ? null : FromDate)   
                    , new SqlParameter("@ToDate",  ToDate == string.Empty ? null : ToDate)   
                    , new SqlParameter("@Top",  Top)   
                };

                using (DBHelper common = new DBHelper(_strCon, myonline.Framework.Data.TransactionType.None))
                {
                    dt = common.ExecuteDataTableSP("usp_Service_AudioRecording", param);
                }
            }
            catch { }
            return dt;
        }

        public bool DeleteRecord()
        {
            bool hasDelete = false;

            try
            {
                SqlParameter[] param = 
                {     new SqlParameter("@Flag",Flag)                                
                    , new SqlParameter("@AudioRecordingId",  AudioRecordingId)  
                };

                using (DBHelper common = new DBHelper(_strCon, myonline.Framework.Data.TransactionType.None))
                {
                    if (common.ExecuteNonQuerySP("usp_AudioRecording", param) > 0)
                    {
                        hasDelete = true;
                    }
                }
            }
            catch { }

            return hasDelete;
        }
        #endregion
    }
}
