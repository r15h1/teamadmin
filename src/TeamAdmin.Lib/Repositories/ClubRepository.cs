using System;
using AutoMapper;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;
using System.Linq;

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
                using (var context = ClubContextFactory.Create<ClubContext>())
                {
                    return context.Clubs.Count();
                }
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
    }
}
