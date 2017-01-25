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

        public int ClubCount
        {
            get
            {
                return GetClubs().Count();
            }
        }

        public Core.Club SaveClub(Core.Club club)
        {
            if (club.ClubId.HasValue) 
                return UpdateClub(club);

            return CreateClub(club);
        }

        private Core.Club CreateClub(Core.Club club)
        {
            var clubInfo = mapper.Map<EFContext.Club>(club);
            using (var context = ContextFactory.Create<ClubContext>())
            {
                context.Clubs.Add(clubInfo);
                context.SaveChanges();
            }
            return MapClubFromDB(clubInfo);
        }

        private Core.Club UpdateClub(Core.Club club)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var clubInfo = context.Clubs.FirstOrDefault(c => c.ClubId == club.ClubId);
                if (clubInfo == null) return null;

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

        public IEnumerable<Core.Club> GetClubs()
        {
            List<Core.Club> list = new List<Core.Club>();
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var clubs = context.Clubs.Where(c => !c.Deleted.HasValue || !c.Deleted.Value).ToList();
                clubs.ForEach((c) => list.Add(MapClubFromDB(c)));
                return list;
            }
        }

        public Core.Club GetClub(int clubId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var club = context.Clubs.FirstOrDefault(c => c.ClubId == clubId && (!c.Deleted.HasValue || !c.Deleted.Value));
                if (club == null) return null;

                return MapClubFromDB(club);
            }            
        }

        public bool DeleteClub(int clubId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
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
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var maxPosition = context.ClubMedia.Where(m => m.ClubId == clubId).Max(m => m.Position);

                List<EFContext.ClubMedia> list = mapper.Map<List<EFContext.ClubMedia>>(mediaList);
                list.ForEach(c => {
                    c.ClubId = clubId;
                    c.Position += maxPosition;
                });
                context.ClubMedia.AddRange(list);
                context.SaveChanges();
                return mapper.Map<List<Core.Media>>(list); ;
            }            
        }

        public IEnumerable<Core.Media> GetMedia(int clubId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                return context.ClubMedia.Where(m => m.ClubId == clubId)
                            .Select(m => mapper.Map<Core.Media>(m))
                            .ToList();
            }
        }

        public int GetMediaCount(int clubId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                return context.ClubMedia.Where(m => m.ClubId == clubId).Count();
            }
        }

        public bool DeleteMedia(long mediaId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var media = context.ClubMedia.FirstOrDefault(m => m.MediaId == mediaId);
                if (media == null) return false;

                context.ClubMedia.Remove(media);
                context.SaveChanges();
                return true;
            }           
        }

        public void UpdateMediaCaption(long mediaId, string newCaption)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var media = context.ClubMedia.FirstOrDefault(m => m.MediaId == mediaId);
                if (media != null)
                {
                    media.Caption = newCaption;
                    context.SaveChanges();
                }                
            }
        }

        public bool SetMediaPosition(long mediaId, int newPosition)
        {
            using (var context = ContextFactory.Create<ClubContext>())           
            {                
                var selectedMedia = context.ClubMedia.FirstOrDefault(m => m.MediaId == mediaId);
                var mediaList = context.ClubMedia.Where(m => m.ClubId == selectedMedia.ClubId && m.MediaType == selectedMedia.MediaType);
                var outOfRangePosition = mediaList.Max(m => m.Position) + 1; //needed to avoid unique key violation (clubid, mediatype, position) on update
                if (selectedMedia == null || newPosition == selectedMedia.Position || newPosition < 1 || newPosition > mediaList.Count()) return false;

                return RecalculatePositions(context, selectedMedia, newPosition, outOfRangePosition);
            }
            
        }

        //successive updates needed to avoid unique key violation (clubid, mediatype, position) on update
        private bool RecalculatePositions(ClubContext context, EFContext.ClubMedia media, int newPosition, int outOfRangePosition)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var originalPosition = media.Position;                   
                    media.Position = outOfRangePosition;
                    context.SaveChanges();

                    if (newPosition > originalPosition)
                    {
                        context.ClubMedia.Where(m => m.ClubId == media.ClubId && m.MediaType == media.MediaType && m.Position > originalPosition && m.Position <= newPosition)
                                       .OrderBy(m => m.Position)
                                       .ToList()
                                       .ForEach(m => m.Position -= 1);
                    }
                    else
                    {
                        context.ClubMedia.Where(m => m.ClubId == media.ClubId && m.MediaType == media.MediaType && m.Position < originalPosition && m.Position >= newPosition)
                                        .OrderByDescending(m => m.Position)
                                        .ToList()
                                        .ForEach(m => m.Position += 1);
                    }

                    context.SaveChanges();
                    media.Position = newPosition;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }
    }
}