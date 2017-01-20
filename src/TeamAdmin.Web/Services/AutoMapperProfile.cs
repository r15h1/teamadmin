using AutoMapper;
using System.Collections.Generic;
using TeamAdmin.Core;
using TeamAdmin.Web.Models.AdminViewModels;

namespace TeamAdmin.Web.Services
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.AdminViewModels.News, Core.Post>()
                .ForMember(dest => dest.Media, opt => opt.ResolveUsing<MediaResolver>());

            CreateMap<Models.AdminViewModels.Event, Core.Event>();
        }
    }

    public class MediaResolver : IValueResolver<Models.AdminViewModels.News, Core.Post, IEnumerable<Media>>
    {
        public IEnumerable<Media> Resolve(News news, Post post, IEnumerable<Media> media, ResolutionContext context)
        {
            int position = 1;
            var mediaSet = new List<Media>();
            foreach(var image in news.Images)            
                mediaSet.Add(new Media { Position = position++, MediaType = MediaType.IMAGE, Url = image });

            return mediaSet;
        }        
    }
}
