using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;
using System;
using TeamAdmin.Core;

namespace TeamAdmin.Lib.Repositories
{
    public class TeamRepository : ITeamRepository, IMediaRepository<Core.Team>
    {
        IMapper mapper;
        private object c;

        public TeamRepository()
        {
            mapper = AutoMapperFactory.GetMapper();
        }

        public IEnumerable<Core.Team> GetTeams()
        {
            var list = new List<Core.Team>();
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var teams = context.Teams.Where( t => !t.Deleted.HasValue || !t.Deleted.Value).ToList();
                teams.ForEach((t) => list.Add(MapTeamFromDB(t)));
                return list;
            }
        }

        public Core.Team SaveTeam(Core.Team team)
        {
            if (team.TeamId.HasValue)
                return UpdateTeam(team);

            return CreateTeam(team);
        }

        private Core.Team CreateTeam(Core.Team team)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var teamInfo = mapper.Map<EFContext.Team>(team);
                context.Teams.Add(teamInfo);
                context.SaveChanges();
                return MapTeamFromDB(teamInfo); ;
            }
        }

        private Core.Team UpdateTeam(Core.Team team)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var teaminfo = context.Teams.FirstOrDefault(t => t.ClubId == team.ClubId && t.TeamId == team.TeamId);
                if (teaminfo == null)
                    return team;

                teaminfo.Name = team.Name;
                context.Entry(teaminfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return MapTeamFromDB(teaminfo);
            }
        }

        private Core.Team MapTeamFromDB(EFContext.Team teamInfo)
        {
            return new Core.Team(teamInfo.ClubId)
            {
                TeamId = teamInfo.TeamId,
                Name = teamInfo.Name
            };
        }

        public bool DeleteTeam(int clubId, int teamId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var teamInfo = context.Teams.FirstOrDefault(t => t.ClubId == clubId && t.TeamId == teamId);
                if (teamInfo == null) return false;

                teamInfo.Deleted = true;
                context.Entry(teamInfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }

        public Core.Team GetTeam(int clubId, int teamId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var team = context.Teams.FirstOrDefault(t => t.ClubId == clubId && t.TeamId == teamId && (!t.Deleted.HasValue || !t.Deleted.Value));
                if (team != null)
                        return MapTeamFromDB(team);
            }

            return null;
        }

        public IEnumerable<Core.Media> AddMedia(int teamId, IEnumerable<Core.Media> mediaList)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var maxPosition = context.TeamMedia.Where(m => m.TeamId == teamId).Max(m => m.Position);

                List<EFContext.TeamMedia> list = mapper.Map<List<EFContext.TeamMedia>>(mediaList);
                list.ForEach(c => {
                    c.TeamId = teamId;
                    c.Position += maxPosition;
                });
                context.TeamMedia.AddRange(list);
                context.SaveChanges();
                return mapper.Map<List<Core.Media>>(list); ;
            }
        }

        public IEnumerable<Core.Media> GetMedia(int teamId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                return context.TeamMedia.Where(m => m.TeamId == teamId)
                            .Select(m => mapper.Map<Core.Media>(m))
                            .ToList();
            }
        }

        public int GetMediaCount(int teamId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                return context.TeamMedia.Where(m => m.TeamId == teamId).Count();
            }
        }

        public bool DeleteMedia(long mediaId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var media = context.TeamMedia.FirstOrDefault(m => m.MediaId == mediaId);
                if (media == null) return false;

                context.TeamMedia.Remove(media);
                context.SaveChanges();
                return true;
            }
        }

        public void UpdateMediaCaption(long mediaId, string newCaption)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var media = context.TeamMedia.FirstOrDefault(m => m.MediaId == mediaId);
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
                var selectedMedia = context.TeamMedia.FirstOrDefault(m => m.MediaId == mediaId);
                var mediaList = context.TeamMedia.Where(m => m.TeamId == selectedMedia.TeamId && m.MediaType == selectedMedia.MediaType);
                var outOfRangePosition = mediaList.Max(m => m.Position) + 1; //needed to avoid unique key violation (teamid, mediatype, position) on update
                if (selectedMedia == null || newPosition == selectedMedia.Position || newPosition < 1 || newPosition > mediaList.Count()) return false;

                return RecalculatePositions(context, selectedMedia, newPosition, outOfRangePosition);
            }
        }

        //successive updates needed to avoid unique key violation (teamid, mediatype, position) on update
        private bool RecalculatePositions(ClubContext context, EFContext.TeamMedia media, int newPosition, int outOfRangePosition)
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
                        context.TeamMedia.Where(m => m.TeamId == media.TeamId && m.MediaType == media.MediaType && m.Position > originalPosition && m.Position <= newPosition)
                                       .OrderBy(m => m.Position)
                                       .ToList()
                                       .ForEach(m => m.Position -= 1);
                    }
                    else
                    {
                        context.TeamMedia.Where(m => m.TeamId == media.TeamId && m.MediaType == media.MediaType && m.Position < originalPosition && m.Position >= newPosition)
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