using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OrigialDateTime { get; set; }
        public string OrifinalVanue { get; set; }

        [Required]
        public Gig Gig { get; set; } 
    }
}