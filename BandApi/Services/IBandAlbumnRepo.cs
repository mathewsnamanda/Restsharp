using BandApi.helpers;
using BandApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandApi.Services
{
   public interface IBandAlbumnRepo
    {

        pagelist<band> getbands(BandsResourceParameter bandsresourceparameter);
        IEnumerable<album> GetAlbums(Guid bandid);
        album GetAlbum(Guid bandid, Guid albumnid);
        void addalbumn(Guid bandid, album album);
        void updatealbumn(album album);
        void deletealbumn(album album);
        bool albumnexist(Guid albumnid);

        IEnumerable<band> getallbands();
        band GetBand(Guid bandid);

        IEnumerable<band> getbands(IEnumerable<Guid> bandids);
        void addband(band band);
        void updateband(band band);
        void deleteband(band band);
        bool bandexist(Guid bandid);

        bool save();


    }
}
