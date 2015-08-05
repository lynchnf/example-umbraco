using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ContactUsSurfaceController : SurfaceController
    {
        public ActionResult Index()
        {
            return PartialView("ContactUsForm", new ContactUsModel());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactUsModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            MailMessage mailMessage = new MailMessage();
            string fromEmail = CurrentPage.GetProperty("fromEmail").Value.ToString();
            mailMessage.From = new System.Net.Mail.MailAddress(fromEmail);

            int toDepartmentId = model.ToDepartmentId;
            //TODO Replace this with a database query.
            string toEmail = "default@example.com";
            if (toDepartmentId == 1)
            {
                toEmail = "one@example.com";
            }
            else if (toDepartmentId == 2)
            {
                toEmail = "two@example.com";
            }
            else if (toDepartmentId == 3)
            {
                toEmail = "three@example.com";
            }
            mailMessage.To.Add(toEmail);

            string subject = model.Subject;
            string body = "-------------------------------------------------------------------------------\r\n";
            body += "    Message sent from " + model.ReplyToEmail + " on " + DateTime.Now.ToShortDateString() + ".\r\n";
            body += "-------------------------------------------------------------------------------\r\n";
            if (model.Message == null || model.Message == "")
            {
                subject += "<eom>";
            }
            else
            {
                body += model.Message;
            }
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);

            var successMessage = CurrentPage.GetProperty("successMessage").Value.ToString();
            TempData["contactUsSuccess"] = successMessage;

            return RedirectToCurrentUmbracoPage();
        }
    }
}