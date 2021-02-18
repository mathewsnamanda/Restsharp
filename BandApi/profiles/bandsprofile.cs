using AutoMapper;
using BandApi.Data;
using BandApi.helpers;
using BandApi.Models;

namespace BandApi.profiles
{
    public class bandsprofile:Profile
    {
        public bandsprofile()
        {
            CreateMap<band, banddtos>()
            .ForMember(
                dest => dest.FoundYearsAgo,
                opt  =>  opt.MapFrom(src => $"{src.Founded.ToString("yyyy")} ({src.Founded.GetYearsAgo()}) years ago"));
                CreateMap<bandcreatedtos,band>();
        }
    }
}
