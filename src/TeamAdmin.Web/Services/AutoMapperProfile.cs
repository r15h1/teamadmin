using AutoMapper;
using System.Collections.Generic;
using TeamAdmin.Core;
using TeamAdmin.Web.Models.AdminViewModels;
using System;

namespace TeamAdmin.Web.Services
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.AdminViewModels.News, Core.Post>()
                .ForMember(dest => dest.Media, opt => opt.ResolveUsing<CoreMediaResolver>());

            CreateMap<Core.Post, Models.AdminViewModels.News>()
                .ForMember(dest => dest.Images, opt => opt.ResolveUsing<NewsMediaResolver>());

            CreateMap<Models.AdminViewModels.Event, Core.Event>()
                .ForMember(dest => dest.Teams, opt => opt.ResolveUsing<ViewModelTeamEventResolver>());

            CreateMap<Core.Event, Models.AdminViewModels.Event>()
                .ForMember(dest => dest.Teams, opt => opt.ResolveUsing<CoreTeamEventResolver>());

            CreateMap<Models.AdminViewModels.Team, Core.Team>().ReverseMap();
        }
    }

    public class CoreMediaResolver : IValueResolver<Models.AdminViewModels.News, Core.Post, IEnumerable<Media>>
    {
        public IEnumerable<Media> Resolve(News news, Post post, IEnumerable<Media> media, ResolutionContext context)
        {
            int position = 1;
            var mediaSet = new List<Media>();
            foreach(var image in news.Images)            
                mediaSet.Add(new Media { Position = position++, MediaType = MediaType.PICTURE, Url = image });

            return mediaSet;
        }        
    }

    public class NewsMediaResolver : IValueResolver<Core.Post, Models.AdminViewModels.News, IEnumerable<string>>
    {
        public IEnumerable<string> Resolve(Post source, News destination, IEnumerable<string> images, ResolutionContext context)
        {
            var imagelist = new List<string>();
            if(source.Media != null)
                foreach (var image in source.Media)
                    if(!string.IsNullOrWhiteSpace(image.Url))
                        imagelist.Add(image.Url);

            return imagelist;
        }
    }

    public class CoreTeamEventResolver : IValueResolver<Core.Event, Models.AdminViewModels.Event, List<int>>
    {
        public List<int> Resolve(Core.Event source, Models.AdminViewModels.Event destination, List<int> destMember, ResolutionContext context)
        {
            var teams = new List<int>();
            if (source.Teams != null && source.Teams.Count > 0)
                foreach (var team in source.Teams)
                    teams.Add(team.TeamId.Value);

            return teams;
        }
    }

    public class ViewModelTeamEventResolver : IValueResolver<Models.AdminViewModels.Event, Core.Event, IList<Core.Team>>
    {
        public IList<Core.Team> Resolve(Models.AdminViewModels.Event source, Core.Event destination, IList<Core.Team> destMember, ResolutionContext context)
        {
            var teams = new List<Core.Team>();
            if (source.Teams != null && source.Teams.Count > 0)
                foreach (var teamid in source.Teams)
                    teams.Add(new Core.Team(1) { TeamId = teamid });

            return teams;
        }
    }
}
