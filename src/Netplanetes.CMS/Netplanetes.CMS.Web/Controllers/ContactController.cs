using Netplanetes.CMS.Web.Models.Contact;
using Netplanetes.CMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        /// <summary>
        /// Contact us ページ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult SendMail()
        {
            return View();
        }
        /// <summary>
        /// Contact us ページの
        /// ポスト先
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ViewResult SendMail(ContactUsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _contactService.SendMail(model);
            ViewBag.Message = "メッセージを送信しました。";
            return View("Complete");
        }

    }
}