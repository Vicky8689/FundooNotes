using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Helper
{
    public class EmailSender
    {

        public static bool sendMail(string email ,string message)
        {


            var secretKey = "eiuqltbulaeruyjm";
            var from = "vickyy400043@gmail.com";
            var smtpServer = "smtp.gmail.com";
            var port = 587;
            var enableSSL = true;
            try
            {

                //mail messege setup
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(from),
                    Subject = "FundooNote Web Api",
                    Body = message
                };
                mailMessage.To.Add(email);
                //smtp setup
                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = port,
                    Credentials = new NetworkCredential(from, secretKey),
                    EnableSsl = enableSSL

                };
                smtpClient.Send(mailMessage);
                return true;

            }catch(Exception ex) { return false; }
            



        }
    }
}
