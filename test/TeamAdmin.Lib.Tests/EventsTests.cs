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

            eventRepository.CreateEvent(club, ev);
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

            eventRepository.CreateEvent(teams, ev);
        }
    }
}
