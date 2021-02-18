using AutoMapper;
using BandApi.Data;
using BandApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.profiles
{
    public class albumprofile:Profile
    {
        public albumprofile()
        {
            CreateMap<album, albumdtos>().ReverseMap();
            CreateMap<AlbumnForcreatingDtos, album>();
            CreateMap<albumnforupdatingdtos,album>().ReverseMap();
        }
    }
}
