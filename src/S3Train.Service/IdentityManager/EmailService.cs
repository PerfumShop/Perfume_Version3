﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace S3Train.IdentityManager
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            if (message == null)
                return;

            await Task.Factory.StartNew(() => { sendMail(message); });
        }

        void sendMail(IdentityMessage message)
        {
            #region formatter
            string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
            string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

            #endregion
            //có thể vào app setting trong webConfig để thiết lập email vs passWord
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("traxuanson456@gmail.com");
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("traxuanson456@gmail.com", "long1234");
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;

            smtpClient.Send(msg);

            //try
            //{
            //    var senderEmail = new MailAddress(from, "Xuan Son");
            //    var receiverEmail = new MailAddress(to, "Receiver");
            //    var password = "long1234";

            //    var smtp = new SmtpClient
            //    {
            //        Host = "smtp.gmail.com",
            //        Port = 587,
            //        EnableSsl = true,
            //        DeliveryMethod = SmtpDeliveryMethod.Network,
            //        UseDefaultCredentials = false,
            //        Credentials = new NetworkCredential(senderEmail.Address, password)
            //    };
            //    using (var mess = new MailMessage(senderEmail, receiverEmail)
            //    {
            //        Subject = subject,
            //        Body = body
            //    })
            //    {
            //        smtp.Send(mess);
            //    }
            //}
            //catch
            //{
            //    throw new NotImplementedException();
            //}
        }
    }
}
