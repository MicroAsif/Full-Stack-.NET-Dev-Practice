using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }
        public bool IsCancel { get; private set; }
        
        public ApplicationUser Artist { get; set; }
        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }
      
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendence> Attendences { get; private set; }

        public Gig()
        {
            Attendences = new Collection<Attendence>();
        }

        public void Cancel()
        {
            IsCancel = true;
            var notification = Notification.GigCanceled(this);

            foreach (var attendee in Attendences.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime getDateTime, string viewModelVenue, byte viewModelGenre)
        {
            var notificaion = Notification.GigUpdated(this, getDateTime, viewModelVenue);

            Venue = viewModelVenue;
            DateTime = getDateTime;
            GenreId = viewModelGenre;

            foreach (var attendee in Attendences.Select(a =>a.Attendee))
            {
               attendee.Notify(notificaion);
            }
        }
    }
}