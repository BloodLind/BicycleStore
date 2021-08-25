using BicycleStore.BikesDatabase.Context;
using BicycleStore.BikesDatabase.Models;
using BicycleStore.BikesDatabase.Repositories;
using BicycleStore.Core.Infrastructure.Interfaces;
using BicycleStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Web.Controllers
{
    public class HomeController : Controller
    {
      

        IRepository<Bicycle> bicycleRepository;
        public HomeController( IRepository<Bicycle> bicycleRepository)
        {
           this.bicycleRepository = bicycleRepository;
        }

        public IActionResult Index()
        {
          
            return View(bicycleRepository.GetAll());
        }


       
        //Code for send order to user email
       
            //if (BicycleContext.Orders.FirstOrDefault(o => o.UserName == order.UserName) != null)
            //{
            //    ModelState.AddModelError("UserName", "this username is used");
            //}
            //if (ModelState.IsValid)
            //{
            //    BicycleContext.Orders.Add(order);
            //    BicycleContext.SaveChanges();
            //    MailAddress from = new MailAddress("spamtemp0azaza@gmail.com", "my logg");
            //    MailAddress to = new MailAddress(order.Mail);
            //    MailMessage m = new MailMessage(from, to);
            //    m.Subject = "Log";
            //    m.Body = $"<h2>Лучше бы машину купил, {order.UserName} {BicycleContext.Bicycles.FirstOrDefault(b => b.Id == order.BicycleId).Model}</h2>";
            //    m.IsBodyHtml = true;
            //    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            //    smtp.Credentials = new NetworkCredential("spamtemp0azaza@gmail.com", "hryfprkojvshqkpi");
            //    smtp.EnableSsl = true;
            //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    smtp.Send(m);
            //    string cntnt = $"Лучше бы машину купил, {order.UserName}";


            //    return RedirectToAction($"Index", new { content = cntnt });
            //}

            //return View();

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

