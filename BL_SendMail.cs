using System;
using System.Net.Mail;
using System.Net;
using myonline.Framework.Util;

namespace myonline.Biz.ADM
{
    public class BL_SendMail
    {
        public bool SendMailToPatient(string TO, string Sub, string msg, bool hasCC = false, string cc1 = "", string cc2 = "")
        {
            bool status = false;
            try
            {

                string StrBody = "";
                MailMessage mail = new MailMessage();
                mail.Priority = MailPriority.High;
                mail.To.Add(TO);

                if (hasCC)
                {
                    mail.CC.Add(cc1);
                    mail.CC.Add(cc2);
                }

                // mail.Bcc.Add("neeraj@brsoftech.in");
                //mail.To.Add("amit_jain_online@yahoo.com");

                //mail.From = new MailAddress("developer@brsoftech.com", "HR@Payroll");
                mail.From = new MailAddress(ClsCommon.FromEmailAddress, ClsCommon.FromEmailName);
                mail.Subject = Sub;
                mail.IsBodyHtml = true;

                StrBody = msg;
                mail.Body = StrBody;


                //SmtpClient smtp = new SmtpClient();
                //NetworkCredential Credentials = new NetworkCredential("developer@brsoftech.com", "brsoftech@123");
                SmtpClient smtp = new SmtpClient(ClsCommon.MailIpAddress);
                NetworkCredential Credentials = new NetworkCredential(ClsCommon.FromEmailAddress, ClsCommon.FromEmailPassword);
                smtp.Credentials = Credentials;
                smtp.Send(mail);
                status = true;
            }
            catch (Exception Ex)
            {
                ExceptionHandlingClass.HandleException(Ex);
                status = false;

            }
            return status;

        }
    }
}