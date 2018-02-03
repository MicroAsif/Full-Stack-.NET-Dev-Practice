using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GigHub.Controllers.Api;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>(); 
        }
    }
}