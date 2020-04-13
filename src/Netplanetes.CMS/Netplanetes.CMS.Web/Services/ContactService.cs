using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Netplanetes.CMS.Web.Services
{
    public class ContactService : IContactService
    {
        public bool SendMail(Models.Contact.ContactUsViewModel model)
        {
            string to = ConfigurationManager.AppSettings["NetPlanetes_MailTo"];

            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();
            message.IsBodyHtml = false;
            message.To.Add(to);
            // outlook.com の場合は from の偽装が行えない
            //message.From = new MailAddress(model.Email, model.Name);
            message.Subject = model.Subject;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} <{1}>さんからお問い合わせがありました.\n\n", model.Name, model.Email);
            builder.AppendLine(model.Body);
            message.Body = builder.ToString();

            client.Send(message);

            return true;
        }
    }
}