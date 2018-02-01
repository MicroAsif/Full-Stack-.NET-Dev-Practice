using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        // GET: Gigs
        private readonly ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Create()
        {
            var genres = new GigFormViewModel
            {
                Genres =  _context.Genres.ToList()
            };
            return View(genres);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            var gigModel = new Gig()
            {
                Venue = viewModel.Venue, 
                ArtistId = User.Identity.GetUserId() , 
                DateTime = viewModel.DateTime, 
                GenreId = viewModel.Genre 
            };
            _context.Gigs.Add(gigModel);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}