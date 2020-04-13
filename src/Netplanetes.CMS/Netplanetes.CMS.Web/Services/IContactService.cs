using Netplanetes.CMS.Web.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Web.Services
{
    public interface IContactService
    {
        bool SendMail(ContactUsViewModel model);
    }
}
