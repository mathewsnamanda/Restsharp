using BandApi.Data;
using BandApi.helpers;
using BandApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.Services
{
    public class BandAlbumnRepo : IBandAlbumnRepo
    {
        private readonly DataContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public BandAlbumnRepo(DataContext context, IPropertyMappingService propepertservice)
        {
            _context = context?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propepertservice;
        }
        public void addalbumn(Guid bandid, album album)
        {
            if (bandid == Guid.Empty)
                throw new ArgumentNullException(nameof(bandid));
            if (album == null)
                throw new ArgumentNullException(nameof(album));
            album.BandId = bandid;
            _context.albums.Add(album);
        }

        public void addband(band band)
        {
            if (band == null)
                throw new ArgumentNullException(nameof(band));
                _context.bands.Add(band);
        }

        public bool albumnexist(Guid albumnid)
        {
           if(albumnid==Guid.Empty)
                throw new ArgumentNullException(nameof(albumnid));
            return _context.albums.Any(albumnexist => albumnexist.Id == albumnid);
        }

        public bool bandexist(Guid bandid)
        {
            if (bandid == Guid.Empty)
                throw new ArgumentNullException(nameof(bandid));
            return _context.bands.Any(albumnexist => albumnexist.Id == bandid);
        }

        public void deletealbumn(album album)
        {
          if(album==null)
                throw new ArgumentNullException(nameof(album));
            _context.albums.Remove(album);
        }

        public void deleteband(band band)
        {
            if (band == null)
                throw new ArgumentNullException(nameof(band));
            _context.bands.Remove(band);
        }

        public album GetAlbum(Guid bandid, Guid albumnid)
        {
            if (bandid == Guid.Empty)
                throw new ArgumentNullException(nameof(bandid));
            if (albumnid == Guid.Empty)
                throw new ArgumentNullException(nameof(albumnid));
            return _context.albums.Where(a => a.BandId == bandid && a.Id == albumnid).FirstOrDefault();
        }

        public IEnumerable<album> GetAlbums(Guid bandid)
        {
            if (bandid == Guid.Empty)
                throw new ArgumentNullException(nameof(bandid));
            return _context.albums.Where(a => a.BandId == bandid).OrderBy(a => a.Title).ToList();
        }

        public IEnumerable<band> getallbands()
        {
            return _context.bands.ToList();
        }

        public band GetBand(Guid bandid)
        {
            if (bandid == Guid.Empty)
                throw new ArgumentNullException(nameof(bandid));
            return _context.bands.FirstOrDefault(addalbumn => addalbumn.Id == bandid);
        }

        public IEnumerable<band> getbands(IEnumerable<Guid> bandids)
        {
            if(bandids==null)
               throw new ArgumentNullException(nameof(bandids));
            return _context.bands.Where(b => bandids.Contains(b.Id)).OrderBy(m => m.Name).ToList();
        }
         public pagelist<band> getbands(BandsResourceParameter bandsresourceparameter)
        {
            if(bandsresourceparameter==null)
            throw new ArgumentNullException(nameof(bandsresourceparameter));
           /* if(string.IsNullOrWhiteSpace(bandsresourceparameter.mainGenre) && string.IsNullOrEmpty(bandsresourceparameter.searchquery))
               return getallbands();
            */
               var collections=_context.bands as IQueryable<band>;

               if(!string.IsNullOrEmpty(bandsresourceparameter.mainGenre))
               {
               var mainGenre=bandsresourceparameter.mainGenre.Trim();
               collections=collections.Where(b=>b.MainGenre==mainGenre);
               }


                 if(!string.IsNullOrEmpty(bandsresourceparameter.searchquery))
               {
               var searchquery=bandsresourceparameter.searchquery.Trim();
               collections=collections.Where(b=>b.Name.Contains(searchquery));
               }

            if (!string.IsNullOrWhiteSpace(bandsresourceparameter.OrderBy))
            {
                var bandpropertymappingdictionary =
                _propertyMappingService.GetPropertyMapping<banddtos, band>();
                collections = collections.ApplySort(bandsresourceparameter.OrderBy,
                    bandpropertymappingdictionary);
            }


            return  pagelist<band>.Create(collections,bandsresourceparameter.PageNumber,
                  bandsresourceparameter.PageSize);
              /*collections
              .Skip(bandsresourceparameter.PageSize*bandsresourceparameter.PageNumber-1)
              .Take(bandsresourceparameter.PageSize)
              .ToList();
              */
        }
        public bool save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void updatealbumn(album album)
        {
        //    not implemented
        //repository pattern- is an abstraction that reduces complexity and aims to make the code safe for the repository implementation, persistance ignorant.
        }

        public void updateband(band band)
        {
            //    not implemented
        //repository pattern- is an abstraction that reduces complexity and aims to make the code safe for the repository implementation, persistance ignorant.
       
        }
    }
}
