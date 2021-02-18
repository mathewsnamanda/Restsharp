using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BandApi.Data;
using BandApi.Models;
using BandApi.profiles;
using BandApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BandApi.Controllers
{
    [Route("api/bands/{bandid}/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "90SecondsCacheProfile")]
    public class AlbumnsController : ControllerBase
    {
        private readonly IBandAlbumnRepo _repo;
        private readonly IMapper _mapper;

        public AlbumnsController(IBandAlbumnRepo repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public ActionResult<IEnumerable<albumdtos>> getalbums(Guid bandid)
        {
            if (!_repo.bandexist(bandid))
                return NotFound();
            var albumns = _repo.GetAlbums(bandid);
            return Ok(_mapper.Map<IEnumerable<albumdtos>>(albumns));
        } 
         [HttpGet("{albumnid}",Name="getalbumsforband")]
         [ResponseCache(Duration =120)]
        public ActionResult<albumdtos> getalbumsforband(Guid bandid,Guid albumnid)
        {
            if (!_repo.bandexist(bandid))
                return NotFound();
                var albumnrepo=_repo.GetAlbum(bandid,albumnid);
             if(albumnrepo==null)
                   return NotFound();
            return Ok(_mapper.Map<albumdtos>(albumnrepo));
        } 
        [HttpPost]
        public ActionResult<albumdtos> createalbumnband(Guid bandid,[FromBody]AlbumnForcreatingDtos albumnforcreatingdtos)
        {
             if(!_repo.bandexist(bandid))
             return NotFound();
             var albumnentity=_mapper.Map<album>(albumnforcreatingdtos);
             _repo.addalbumn(bandid,albumnentity);
             _repo.save();
             var albumntoreturn=_mapper.Map<albumdtos>(albumnentity);

        return CreatedAtRoute("getalbumsforband",new {bandid=bandid,albumnid=albumntoreturn.Id },albumntoreturn); 
        }
        [HttpPut("{albumnid}")]
        public ActionResult updatealbumnforband(Guid bandid,Guid albumnid,[FromBody]albumnforupdatingdtos albumn)
        {
            if(!_repo.bandexist(bandid))
             return NotFound();
            var albumnfromrepo=_repo.GetAlbum(bandid,albumnid);
            if(albumnfromrepo==null)
              {
                  //return NotFound();
                 var albumntoadd=_mapper.Map<album>(albumn);
                  albumntoadd.Id=albumnid;
                  _repo.addalbumn(bandid,albumntoadd);
                  _repo.save();
                  var albumtoreturn=_mapper.Map<albumdtos>(albumntoadd);
                  return CreatedAtRoute("getalbumsforband",new {bandid=bandid, albumnid=albumtoreturn.Id});
              
              }
              _mapper.Map(albumn,albumnfromrepo);
              _repo.updatealbumn(albumnfromrepo);
              _repo.save();
              return NoContent();

        }
        [HttpPatch("{albumnid}")]
        public ActionResult partiallyupdatealbumnforband(Guid bandid,Guid albumnid,[FromBody]JsonPatchDocument<albumnforupdatingdtos> patchDocument)
        {
            if(!_repo.bandexist(bandid))
             return NotFound();
            var albumnfromrepo=_repo.GetAlbum(bandid,albumnid);
            if(albumnfromrepo==null)
            {
             // return NotFound();
              var albumn=new albumnforupdatingdtos();
              patchDocument.ApplyTo(albumn);
              var albumntoadd=_mapper.Map<album>(albumn);
              albumntoadd.Id=albumnid;

              _repo.addalbumn(bandid,albumntoadd);
              _repo.save();

               var albumntoreturn=_mapper.Map<albumdtos>(albumntoadd);
              return CreatedAtRoute("getalbumsforband",new {bandid=bandid, albumnid=albumntoreturn.Id},albumntoreturn);
                  
            }
            var albumntopatch = _mapper.Map<albumnforupdatingdtos>(albumnfromrepo);
            patchDocument.ApplyTo(albumntopatch,ModelState);
            if(!TryValidateModel(albumntopatch))
            return ValidationProblem(ModelState);
              _mapper.Map(albumntopatch,albumnfromrepo);
              _repo.updatealbumn(albumnfromrepo);
              _repo.save();
              return NoContent();
        }
        [HttpDelete("{albumnid}")]
        public ActionResult deletealbumnforband(Guid bandid,Guid albumnid)
        {
            if(!_repo.bandexist(bandid))
             return NotFound();
            var albumnfromrepo=_repo.GetAlbum(bandid,albumnid);
            if(albumnfromrepo==null)
              return NotFound();
            _repo.deletealbumn(albumnfromrepo);
            _repo.save();
            return NoContent();
        }
    }
}
