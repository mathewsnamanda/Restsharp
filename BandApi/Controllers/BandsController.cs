using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using BandApi.Data;
using BandApi.helpers;
using BandApi.Models;
using BandApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BandApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumnRepo _repo;
        private readonly IMapper _mapper;
        private readonly IPropertyMappingService _propertymappingservice;
        private readonly IpropertyValidationService _propertyvalidationservice;

        public BandsController(IBandAlbumnRepo repo,IMapper mapper, IPropertyMappingService propertymappingservice, IpropertyValidationService ipropertyValidationService)
        {
            _repo = repo?? throw new ArgumentNullException(nameof(repo));
          _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
            _propertymappingservice = propertymappingservice ?? throw new ArgumentNullException(nameof(propertymappingservice));
            _propertyvalidationservice = ipropertyValidationService ?? throw new ArgumentNullException(nameof(ipropertyValidationService));
        }
        //http://localhost/api/bands?fields=id,name
        [HttpGet(Name="getbands")]
        [HttpHead]
        public IActionResult getbands([FromQuery]BandsResourceParameter bandsresourceparameter)
        {
            if (!_propertymappingservice.ValidMappingExist<banddtos, band>(bandsresourceparameter.OrderBy))
                return BadRequest();

            if (!_propertyvalidationservice.HasValidProperties<banddtos>(bandsresourceparameter.Fields))
                return BadRequest();

           var bands= _repo.getbands(bandsresourceparameter);  
           var previouspagelink= bands.HasPrevious?
           createbandurl(bandsresourceparameter,uritype.previouspage) : null;
            var nextpagelink= bands.HasNext?
           createbandurl(bandsresourceparameter,uritype.nextpage) : null;

           var metadata=new
           {
               totalcount= bands.TotalCount,
               pagesize=bands.PageSize,
               currentpage=bands.CurrentPage,
               totalpages=bands.TotalPages,
               previouspagelink=previouspagelink,
               nextpagelink=nextpagelink
           };
           Response.Headers.Add("Pagination",JsonSerializer.Serialize(metadata));
          /*  var bandsdtos = new List<banddtos>();
            foreach(var band in bands)
            {
                bandsdtos.Add(new banddtos()
                {
                    Id=band.Id,
                    Name=band.Name,
                    MainGenre=band.MainGenre,
                    FoundYearsAgo=$"{band.Founded.ToString("yyyy")}(" +
                    $"{band.Founded.GetYearsAgo()} Years Ago)"
                });
            }
            */
            return Ok(_mapper.Map<IEnumerable<banddtos>>(bands).ShapeData(bandsresourceparameter.Fields));
        }
        [HttpGet("{bandid}",Name="getband")]
        public IActionResult getband(Guid bandid,string fields)
        {

            if (!_propertyvalidationservice.HasValidProperties<banddtos>(fields))
                return BadRequest();

            var band = _repo.GetBand(bandid);


            if(band==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<banddtos>(band).ShapeData(fields));
        }
        [HttpPost]
        public ActionResult<banddtos> createband([FromBody]bandcreatedtos dtos)
        {
            var bandentities=_mapper.Map<band>(dtos);
            _repo.addband(bandentities);
            _repo.save();
            var bandtoreturn=_mapper.Map<banddtos>(bandentities);
           return CreatedAtRoute("getband",new {bandid=bandtoreturn.Id},bandtoreturn); 
        }
        [HttpDelete("{bandid}")]
        public ActionResult deleteband(Guid bandid)
        {
               var bandfromrepo=_repo.GetBand(bandid);
               if(bandfromrepo==null)
               return NotFound();
               _repo.deleteband(bandfromrepo);
               _repo.save();
               return NoContent();
        }
        [HttpOptions]
        public IActionResult getbandoptions()
        {
            Response.Headers.Add("Allow","GET,POST,DELETE,HEAD,OPTIONS");
            return Ok();
        }
        private string createbandurl(BandsResourceParameter bandsresourceparameter,uritype uritype)
        {

        switch(uritype){
            case uritype.previouspage:
            return Url.Link("getbands", new 
            {
                fields= bandsresourceparameter.Fields,
                orderBy=bandsresourceparameter.OrderBy,
                pageNumber=bandsresourceparameter.PageNumber - 1,
                pageSize=bandsresourceparameter.PageSize,
                mainGenre=bandsresourceparameter.mainGenre,
                searchquery=bandsresourceparameter.searchquery
        });
         case uritype.nextpage:
            return Url.Link("getbands", new 
            {
                fields = bandsresourceparameter.Fields,
                orderBy = bandsresourceparameter.OrderBy,
                pageNumber =bandsresourceparameter.PageNumber + 1,
                pageSize=bandsresourceparameter.PageSize,
                mainGenre=bandsresourceparameter.mainGenre,
                searchquery=bandsresourceparameter.searchquery
        });
          default:
            return Url.Link("getbands", new 
            {
                fields = bandsresourceparameter.Fields,
                orderBy = bandsresourceparameter.OrderBy,
                pageNumber =bandsresourceparameter.PageNumber,
                pageSize=bandsresourceparameter.PageSize,
                mainGenre=bandsresourceparameter.mainGenre,
                searchquery=bandsresourceparameter.searchquery
        });
        }

        }
    }
}
