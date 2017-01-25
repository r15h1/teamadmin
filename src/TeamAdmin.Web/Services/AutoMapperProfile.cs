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
                .ForMember(dest => dest.Media, opt => opt.ResolveUsing<CoreMediaResolver>());

            CreateMap<Core.Post, Models.AdminViewModels.News>()
                .ForMember(dest => dest.Images, opt => opt.ResolveUsing<NewsMediaResolver>());

            CreateMap<Models.AdminViewModels.Event, Core.Event>();
        }
    }

    public class CoreMediaResolver : IValueResolver<Models.AdminViewModels.News, Core.Post, IEnumerable<Media>>
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
}
