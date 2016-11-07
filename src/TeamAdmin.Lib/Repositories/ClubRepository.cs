using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;
using System;

namespace TeamAdmin.Lib.Repositories
{
    public class ClubRepository : IClubRepository, IMediaRepository<Core.Club>
    {
        IMapper mapper;
        public ClubRepository()
        {
            mapper = AutoMapperFactory.GetMapper();
        }

        public int Count
        {
            get
            {
                return Get().Count();
            }
        }

        public Core.Club Save(Core.Club club)
        {
            if (club.ClubId.HasValue) 
                return Update(club);

            return Create(club);
        }

        private Core.Club Create(Core.Club club)
        {
            var clubInfo = mapper.Map<EFContext.Club>(club);
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                context.Clubs.Add(clubInfo);
                context.SaveChanges();
            }
            return MapClubFromDB(clubInfo);
        }

        private Core.Club Update(Core.Club club)
        {
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var clubInfo = context.Clubs.FirstOrDefault(c => c.ClubId == club.ClubId);
                if (clubInfo == null) return club;

                clubInfo.Name = club.Name;
                clubInfo.City = club.Address.City;
                clubInfo.Country = club.Address.Country;
                clubInfo.PostalCode = club.Address.PostalCode;
                clubInfo.Province = club.Address.Province;
                clubInfo.Street = club.Address.Street;

                context.Entry(clubInfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();

                return MapClubFromDB(clubInfo);
            }
        }

        private Core.Club MapClubFromDB(EFContext.Club clubInfo)
        {
            return new Core.Club
            {
                ClubId = clubInfo.ClubId,
                Name = clubInfo.Name,
                Address = new Address
                {
                    City = clubInfo.City,
                    Country = clubInfo.Country,
                    PostalCode = clubInfo.PostalCode,
                    Province = clubInfo.Province,
                    Street = clubInfo.Street
                }
            };
        }

        public IEnumerable<Core.Club> Get()
        {
            List<Core.Club> list = new List<Core.Club>();
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var clubs = context.Clubs.Where(c => !c.Deleted.HasValue || !c.Deleted.Value).ToList();
                clubs.ForEach((c) => list.Add(MapClubFromDB(c)));
                return list;
            }
        }

        public Core.Club Get(int clubId)
        {
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var club = context.Clubs.FirstOrDefault(c => c.ClubId == clubId && (!c.Deleted.HasValue || !c.Deleted.Value));
                if (club != null)
                    return MapClubFromDB(club);
            }

            return null;
        }

        public bool Delete(int clubId)
        {
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var clubInfo = context.Clubs.FirstOrDefault(c => c.ClubId == clubId);
                if (clubInfo == null) return false;

                clubInfo.Deleted = true;
                context.Entry(clubInfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }

        public IEnumerable<Core.Media> AddMedia(int clubId, IEnumerable<Core.Media> mediaList)
        {
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                List<EFContext.ClubMedia> list = mapper.Map<List<EFContext.ClubMedia>>(mediaList);
                list.ForEach(c => c.ClubId = clubId);
                context.Media.AddRange(list);
                context.SaveChanges();
                return mapper.Map<List<Core.Media>>(list); ;
            }            
        }
    }
}