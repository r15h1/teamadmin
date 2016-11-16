using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using TeamAdmin.Lib.Tests.Repositories;
using Xunit;

namespace TeamAdmin.Lib.Tests
{
    [Collection("EventFixtureCollection")]
    public class EventsTests
    {
        private Club club;
        private IEnumerable<Team> teams;
        private IEventRepository eventRepository;

        public EventsTests(ClubFixture fixture)
        {
            club = fixture.Clubs.FirstOrDefault();
            teams = fixture.Teams.Where(t => t.ClubId == club.ClubId);
            eventRepository = new EventRepository();
        }

        [Fact]
        public void ClubLevelEventCreation()
        {
            Event ev = new Event()
            {
                EventType = EventType.GAME,
                Title = "Game at Soccer Field",
                Description = "SAAC soccer game against Panthers",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2)
            };

            var e = eventRepository.CreateEvent(club, ev);
            Assert.NotNull(e);
            Assert.NotNull(e.EventId);
            Assert.True(e.EventType == ev.EventType);
            Assert.True(e.Description.Equals(ev.Description));
            Assert.True(e.Title.Equals(ev.Title));
            Assert.True(e.StartDate.Equals(ev.StartDate));
            Assert.True(e.EndDate.Equals(ev.EndDate));
        }

        [Fact]
        public void TeamLevelEventCreation()
        {
            Event ev = new Event()
            {
                EventType = EventType.GAME,
                Title = "Game at Soccer Field",
                Description = "SAAC soccer game against Panthers",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2)
            };

            var e = eventRepository.CreateEvent(teams, ev);
            Assert.NotNull(e);
            Assert.NotNull(e.EventId);
            Assert.True(e.EventType == ev.EventType);
            Assert.True(e.Description.Equals(ev.Description));
            Assert.True(e.Title.Equals(ev.Title));
            Assert.True(e.StartDate.Equals(ev.StartDate));
            Assert.True(e.EndDate.Equals(ev.EndDate));
        }

        [Fact]
        public void GetEventByIdTeamLevel()
        {
            Event ev = new Event()
            {
                EventType = EventType.GAME,
                Title = "Game at Soccer Field",
                Description = "SAAC soccer game against Panthers",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2)
            };

            var e = eventRepository.CreateEvent(teams, ev);
            var evbyid = eventRepository.GetEvent(e.EventId.Value);
            Assert.NotNull(evbyid);
            Assert.True(evbyid.EventType == ev.EventType);
            Assert.True(evbyid.Description.Equals(ev.Description));
            Assert.True(evbyid.Title.Equals(ev.Title));
            Assert.True(evbyid.StartDate.ToString("dd/MM/yyyy hh:mm").Equals(ev.StartDate.ToString("dd/MM/yyyy hh:mm")));
            Assert.True(evbyid.EndDate.ToString("dd/MM/yyyy hh:mm").Equals(ev.EndDate.ToString("dd/MM/yyyy hh:mm")));
        }

        [Fact]
        public void GetEventByIdClubLevel()
        {
            Event ev = new Event()
            {
                EventType = EventType.GAME,
                Title = "Game at Soccer Field",
                Description = "SAAC soccer game against Panthers",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2)
            };

            var e = eventRepository.CreateEvent(club, ev);
            var evbyid = eventRepository.GetEvent(e.EventId.Value);
            Assert.NotNull(evbyid);
            Assert.True(evbyid.EventType == ev.EventType);
            Assert.True(evbyid.Description.Equals(ev.Description));
            Assert.True(evbyid.Title.Equals(ev.Title));
            Assert.True(evbyid.StartDate.ToString("dd/MM/yyyy hh:mm").Equals(ev.StartDate.ToString("dd/MM/yyyy hh:mm")));
            Assert.True(evbyid.EndDate.ToString("dd/MM/yyyy hh:mm").Equals(ev.EndDate.ToString("dd/MM/yyyy hh:mm")));
        }

        [Fact]
        public void DeleteClubEvent()
        {
            Event ev = new Event()
            {
                EventType = EventType.GAME,
                Title = "Game at Soccer Field",
                Description = "SAAC soccer game against Panthers",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2)
            };

            var e = eventRepository.CreateEvent(club, ev);
            var result = eventRepository.DeleteEvent(e.EventId.Value);
            Assert.True(result);
            Event deletedEvent = eventRepository.GetEvent(e.EventId.Value);
            Assert.Null(deletedEvent);
        }

        [Fact]
        public void DeleteTeamEvent()
        {
            Event ev = new Event()
            {
                EventType = EventType.GAME,
                Title = "Game at Soccer Field",
                Description = "SAAC soccer game against Panthers",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2)
            };

            var e = eventRepository.CreateEvent(teams, ev);
            var result = eventRepository.DeleteEvent(e.EventId.Value);
            Assert.True(result);
            Event deletedEvent = eventRepository.GetEvent(e.EventId.Value);
            Assert.Null(deletedEvent);
        }
    }
}
