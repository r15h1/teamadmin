using System;
using AutoMapper;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;
using System.Linq;
using System.Collections.Generic;

namespace TeamAdmin.Lib.Repositories
{
    public class ClubRepository : IClubRepository
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
            if (club.Id.HasValue) 
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
                var clubInfo = context.Clubs.Where(c => c.ClubId == club.Id).FirstOrDefault();
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
                Id = clubInfo.ClubId,
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

        public bool Delete(int clubId)
        {
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var clubInfo = context.Clubs.Where(c => c.ClubId == clubId).FirstOrDefault();
                if (clubInfo == null) return false;

                clubInfo.Deleted = true;
                context.Entry(clubInfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }
    }
}
