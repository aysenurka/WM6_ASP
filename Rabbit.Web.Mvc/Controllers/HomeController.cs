using System;
using Rabbit.Models.Entities;
using Rabbit.Web.Mvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Rabbit.BLL.Repository;

namespace Rabbit.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private static Publisher _publisher;

        public ActionResult Index()
        {
            var list = new List<Customer>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(new Customer()
                {
                    Name = Faker.NameFaker.FirstName(),
                    Surname = Faker.NameFaker.LastName(),
                    Address = Faker.LocationFaker.Street() + " " + Faker.LocationFaker.City() + " " + Faker.LocationFaker.Country(),
                    Email = Faker.InternetFaker.Email(),
                    Phone = "05" + Faker.PhoneFaker.Phone().Replace("-", "").Substring(1, 9)
                });
            }

            var dataString = JsonConvert.SerializeObject(list);
            _publisher = new Publisher(dataString, "Customer");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            Random rnd = new Random();
            var count = new CustomerRepo().Queryable().Count();
            var customerIds = new CustomerRepo().Queryable().ToList().OrderBy(x => rnd.Next(count)).Take(1000).Select(x => x.Id).ToList();

            foreach (var item in customerIds)
            {
                var dataString = JsonConvert.SerializeObject(new MailLog()
                {
                    CustomerId = item,
                    Message = Faker.CompanyFaker.Name(),
                    Subject = Faker.LocationFaker.Country()
                });
                _publisher = new Publisher(dataString, "MailLog");
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}