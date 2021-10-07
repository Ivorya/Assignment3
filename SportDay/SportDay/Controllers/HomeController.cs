using SportDay.Models;
using SportDay.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportDay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.EmailAddress;
                    String subject = model.Title;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.SendFeedback(toEmail, subject, contents);

                    ViewBag.Result = "Thanks for rating our service";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

    }
    }