using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("admin/contacts")]
    public class AdminContactsController : Controller
    {
        private IClubRepository clubRepository;

        public AdminContactsController(IClubRepository clubRepository)
        {
            this.clubRepository = clubRepository;
        }

        public IActionResult Index()
        {
            var messages = clubRepository.GetMessages();
            return View(messages);
        }

        [HttpGet("{messageId}")]
        public IActionResult Details(long messageId)
        {
            var message = clubRepository.GetMessage(messageId);
            return View(FormatMessage(message));
        }

        private Message FormatMessage(Message message)
        {
            switch (message.MessageType)
            {                
                case MessageType.Registration:
                    return FormatRegistration(message);
                case MessageType.SummerCamp:
                    return FormatSummerCamp(message);
                case MessageType.TryOut:
                    return FormatTryOut(message);
                default:
                    return message;

            }
        }

        //implement these and deploy
        private Message FormatTryOut(Message message)
        {
            throw new NotImplementedException();
        }

        private Message FormatSummerCamp(Message message)
        {
            throw new NotImplementedException();
        }

        private Message FormatRegistration(Message message)
        {
            throw new NotImplementedException();
        }
    }
}