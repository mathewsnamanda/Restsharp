using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BandscollectionController : ControllerBase
    {
          private readonly IBandAlbumnRepo _repo;
        private readonly IMapper _mapper;

        public BandscollectionController(IBandAlbumnRepo repo,IMapper mapper)
        {
            _repo = repo?? throw new ArgumentNullException(nameof(repo));
          _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
      
        }
        [HttpGet("({ids})",Name="getbandcollections")]
        public IActionResult getbandcollections([FromRoute]
        [ModelBinder(BinderType=typeof(arraymodelbinder))]IEnumerable<Guid> ids)
        {
            if(ids==null)
            return BadRequest();
            var bandentities=_repo.getbands(ids); 
            if(ids.Count()!=bandentities.Count())
            return NotFound();
            var bandstoreturn=_mapper.Map<IEnumerable<banddtos>>(bandentities);
            return Ok(bandstoreturn);
        }
        [HttpPost]
        public ActionResult<IEnumerable<banddtos>> createbandcollection([FromBody]IEnumerable<bandcreatedtos> bandcollection )
        {
    
         var bandentities=_mapper.Map<IEnumerable<band>>(bandcollection);
         foreach(var band in bandentities){
             _repo.addband(band);
         }
         _repo.save();
          var collectiontoreturn=_mapper.Map<IEnumerable<banddtos>>(bandentities);
          var idsstring=string.Join(",",collectiontoreturn.Select(album=>album.Id));
          return CreatedAtRoute("getbandcollections",new{ids=idsstring},collectiontoreturn);
        }
    }
}