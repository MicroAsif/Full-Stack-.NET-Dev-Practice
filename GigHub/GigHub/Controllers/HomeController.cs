using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(string query = null)
        {
            var upCommingGigs = _context.Gigs.Include(g => g.Artist).Include(x=> x.Genre).Where(x => x.DateTime >= DateTime.Now && !x.IsCancel);

            if (!string.IsNullOrWhiteSpace(query))
            {
                upCommingGigs = upCommingGigs.Where(g => g.Artist.Name.Contains(query) ||
                                                         g.Venue.Contains(query) ||
                                                         g.Genre.Name.Contains(query));
            }
            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upCommingGigs, 
                ShowActivity = User.Identity.IsAuthenticated, 
                Heading = "Upcoming Gigs", 
                SearchTerm = query
            };
            return View("Gigs",viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}