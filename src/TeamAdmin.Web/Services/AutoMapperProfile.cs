using AutoMapper;
using System.Collections.Generic;
using TeamAdmin.Core;
using TeamAdmin.Web.Models.AdminViewModels;
using System;
using System.Linq;

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

            CreateMap<Models.AdminViewModels.Team, Core.Team>().ReverseMap()
                .ForMember(dest => dest.Images, opt => opt.ResolveUsing<TeamViewModelImageResolver>())
                .ForMember(dest => dest.Uniforms, opt => opt.ResolveUsing<TeamViewModelUniformResolver>());

            CreateMap<Models.AdminViewModels.Player, Core.Player>()
                .ForMember(m => m.Address, opt => opt.ResolveUsing<CorePlayerAddressResolver>());

            CreateMap<Core.Player, Models.AdminViewModels.Player>()
                .ForMember(m => m.Address, opt => opt.MapFrom(p => p.Address.Street))
                .ForMember(m => m.City, opt => opt.MapFrom(p => p.Address.City))
                .ForMember(m => m.PostalCode, opt => opt.MapFrom(p => p.Address.PostalCode))
                .ForMember(m => m.Province, opt => opt.MapFrom(p => p.Address.Province));
        }
    }

    public class CorePlayerAddressResolver : IValueResolver<Models.AdminViewModels.Player, Core.Player, Address>
    {
        public Address Resolve(Models.AdminViewModels.Player source, Core.Player destination, Address address, ResolutionContext context)
        {
            return new Address
            {
                City = source.City,
                //Country = source.Country,
                PostalCode = source.PostalCode,
                Province = source.Province,
                Street = source.Address
            };
        }
    }

    public class TeamViewModelImageResolver : IValueResolver<Core.Team, Models.AdminViewModels.Team, IEnumerable<string>>
    {
        public IEnumerable<string> Resolve(Core.Team source, Models.AdminViewModels.Team destination, IEnumerable<string> destMember, ResolutionContext context)
        {
            var imagelist = new List<string>();
            if (source.Media != null)
                foreach (var image in source.Media.Where(x => x.MediaType == MediaType.PICTURE))
                    if (!string.IsNullOrWhiteSpace(image.Url))
                        imagelist.Add(image.Url);

            return imagelist;
        }
    }

    public class TeamViewModelUniformResolver : IValueResolver<Core.Team, Models.AdminViewModels.Team, IEnumerable<string>>
    {
        public IEnumerable<string> Resolve(Core.Team source, Models.AdminViewModels.Team destination, IEnumerable<string> destMember, ResolutionContext context)
        {            
            var imagelist = new List<string>();
            if (source.Media != null)
                foreach (var image in source.Media.Where(x => x.MediaType == MediaType.UNIFORM))
                    if (!string.IsNullOrWhiteSpace(image.Url))
                        imagelist.Add(image.Url);

            return imagelist;
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
