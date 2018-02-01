using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public DateTime DateTime
        {
            get
            {
                return DateTime.Parse($"{Date} {Time}");
            }
        }

        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public byte Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        
    }
}