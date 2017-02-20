using AutoMapper;
using System.Collections.Generic;
using System;
using TeamAdmin.Core;
using System.Xml.Serialization;
using System.IO;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal static class AutoMapperFactory
    {
        private static IMapper mapper;

        static AutoMapperFactory()
        {
            mapper = AutoMapperConfiguration.CreateMapper();
        }

        private static MapperConfiguration AutoMapperConfiguration{
            get
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Core.Club, EFContext.Club>()
                        .ForMember(d => d.ClubId, opt => opt.MapFrom(src => src.ClubId))
                        .ForMember(d => d.City, opt => opt.MapFrom(src => src.Address.City))
                        .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Address.Country))
                        .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                        .ForMember(d => d.Province, opt => opt.MapFrom(src => src.Address.Province))
                        .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address.Street))
                        .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode));

                    cfg.CreateMap<Core.Team, EFContext.Team>().ForMember(m => m.TeamMedia, opt => opt.ResolveUsing<CoreTeamMediaResolver>());
                    cfg.CreateMap<EFContext.Team, Core.Team>();

                    cfg.CreateMap<Core.Media, EFContext.ClubMedia>().ForMember(m => m.MediaType, opt => opt.MapFrom(src => (byte)src.MediaType));
                    cfg.CreateMap<EFContext.ClubMedia, Core.Media>().ForMember(m => m.MediaType, opt => opt.MapFrom(src => (int)src.MediaType));

                    cfg.CreateMap<Core.Media, EFContext.TeamMedia>().ForMember(m => m.MediaType, opt => opt.MapFrom(src => (byte)src.MediaType));
                    cfg.CreateMap<EFContext.TeamMedia, Core.Media>().ForMember(m => m.MediaType, opt => opt.MapFrom(src => (int)src.MediaType));

                    cfg.CreateMap<Core.Event, EFContext.Event>().ForMember(m => m.EventType, opt => opt.MapFrom(src => (byte)src.EventType));
                    cfg.CreateMap<EFContext.Event, Core.Event>()
                        .ForMember(m => m.EventType, opt => opt.MapFrom(src => (int)src.EventType))
                        .ForMember(m => m.Teams, opt => opt.ResolveUsing<TeamEventResolver>());

                    cfg.CreateMap<Core.Post, EFContext.Post>()
                        .ForMember(m => m.PostMedia, opt => opt.ResolveUsing<CorePostMediaResolver>())
                        .ForMember(m => m.PostStatus, opt => opt.MapFrom(src => (byte)src.PostStatus));
                    
                    cfg.CreateMap<EFContext.Post, Core.Post>()
                        .ForMember(m => m.Media, opt => opt.ResolveUsing<DBPostMediaResolver>())
                        .ForMember(m => m.PostStatus, opt => opt.MapFrom(src => (int)src.PostStatus));

                    cfg.CreateMap<Player, Core.Player>()
                        .ForMember(m => m.ContactInfo, opt => opt.ResolveUsing<CorePlayerContactInfoResolver>())
                        .ForMember(m => m.Address, opt => opt.ResolveUsing<CorePlayerAddressResolver>());

                    cfg.CreateMap<Core.Player, EFContext.Player>()
                        .ForMember(m => m.Address, opt => opt.MapFrom(a => a.Address.Street))
                        .ForMember(m => m.City, opt => opt.MapFrom(a => a.Address.City))
                        .ForMember(m => m.Country, opt => opt.MapFrom(a => a.Address.Country))
                        .ForMember(m => m.Province, opt => opt.MapFrom(a => a.Address.Province))
                        .ForMember(m => m.PostalCode, opt => opt.MapFrom(a => a.Address.PostalCode))
                        .ForMember(m => m.ContactInfo, opt => opt.ResolveUsing<DBPlayerContactInfoResolver>());

                });
            }
        }

        internal static IMapper GetMapper()
        {
            return mapper;
        }
    }

    internal class DBPlayerContactInfoResolver : IValueResolver<Core.Player, Player, string>
    {
        public string Resolve(Core.Player source, Player destination, string destMember, ResolutionContext context)
        {
            if(source.ContactInfo != null)   {
                var serializer = new  XmlSerializer(source.ContactInfo.GetType());
                using (StringWriter textWriter = new StringWriter())
                {
                    serializer.Serialize(textWriter, source.ContactInfo);
                    return textWriter.ToString();
                }
            }
            return null;
        }        
    }

    internal class CorePlayerContactInfoResolver : IValueResolver<Player, Core.Player, List<ContactInfo>>
    {
        public List<ContactInfo> Resolve(Player source, Core.Player destination, List<ContactInfo> destMember, ResolutionContext context)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ContactInfo>));
            if(!string.IsNullOrWhiteSpace(source.ContactInfo))
            {
                using (TextReader reader = new StringReader(source.ContactInfo))
                {
                    return (List<ContactInfo>)serializer.Deserialize(reader);
                }
            }
            return null;
        }
    }

    internal class CorePlayerAddressResolver : IValueResolver<EFContext.Player, Core.Player, Address>
    {
        public Address Resolve(Player source, Core.Player destination, Address address, ResolutionContext context)
        {
            return new Address
            {
                City = source.City,
                Country = source.Country,
                PostalCode = source.PostalCode,
                Province = source.Province,
                Street = source.Address
            };
        }
    }

    internal class CoreTeamMediaResolver : IValueResolver<Core.Team, EFContext.Team, ICollection<TeamMedia>>
    {
        public ICollection<TeamMedia> Resolve(Core.Team source, Team destination, ICollection<TeamMedia> destMember, ResolutionContext context)
        {
            var mediaSet = new List<TeamMedia>();
            foreach (var m in source.Media)
                mediaSet.Add(new TeamMedia { Position = m.Position, MediaType = (byte)m.MediaType, Url = m.Url, MediaId = m.MediaId, Caption = m.Caption, TeamId = source.TeamId });

            return mediaSet;
        }
    }

    internal class CorePostMediaResolver : IValueResolver<Core.Post, EFContext.Post, ICollection<PostMedia>>
    {
        public ICollection<PostMedia> Resolve(Core.Post source, Post destination, ICollection<PostMedia> destMember, ResolutionContext context)
        {
            var mediaSet = new List<PostMedia>();
            foreach (var m in source.Media)
                mediaSet.Add(new PostMedia { Position = m.Position, MediaType = (byte)m.MediaType, Url = m.Url, MediaId = m.MediaId, Caption = m.Caption, PostId = source.PostId });

            return mediaSet;
        }
    }

    internal class DBPostMediaResolver : IValueResolver<EFContext.Post, Core.Post, IEnumerable<Core.Media>>
    {
        IEnumerable<Core.Media> IValueResolver<Post, Core.Post, IEnumerable<Core.Media>>.Resolve(Post source, Core.Post destination, IEnumerable<Core.Media> destMember, ResolutionContext context)
        {
            var mediaSet = new List<Core.Media>();
            if(source.PostMedia != null)
                foreach (var m in source.PostMedia)
                    mediaSet.Add(new Core.Media { Position = m.Position, MediaType = (Core.MediaType)m.MediaType, Url = m.Url, MediaId = m.MediaId, Caption = m.Caption });

            return mediaSet;
        }
    }

    internal class TeamEventResolver : IValueResolver<EFContext.Event, Core.Event, IList<Core.Team>>
    {
        public IList<Core.Team> Resolve(Event source, Core.Event destination, IList<Core.Team> destMember, ResolutionContext context)
        {
            var teams = new List<Core.Team>();
            if (source.ClubTeamEvents != null && source.ClubTeamEvents.Count > 0)
                foreach (var ev in source.ClubTeamEvents)
                    if(ev.TeamId.HasValue)
                        teams.Add(context.Mapper.Map<Core.Team>(ev.Team));

            return teams;
        }
    }
}