using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Xml.Serialization;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using TeamAdmin.Lib.zz;

namespace TeamAdmin.Web.Controllers
{
    /// <summary>
    /// temporary controller - will be removed when concept of pages added
    /// </summary>
    [Route("")]
    public class zzController : Controller
    {
        private IClubRepository clubRepository;

        public zzController(IClubRepository clubRepository)
        {
            this.clubRepository = clubRepository;
        }


        [HttpGet("tryout-application")]
        public IActionResult TryOut()
        {
            return View();
        }

        [HttpPost("tryout-application")]
        public IActionResult TryOut(TryOutModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var message = new Core.Message
            {
                Body = Serialize(model),
                DateCreated = DateTime.UtcNow,
                Email = model.PrimaryGuardianEmail,
                MessageType = Core.MessageType.TryOut,
                Name = $"{model.PrimaryGuardianFullName}",
                Subject = $"TryOut Application {model.PlayerFirstName} {model.PlayerLastName}"
            };
            clubRepository.SaveMessage(message);
            return View(model);
        }

        [HttpGet("4-to-8-years-old-community-program-house-league")]
        public IActionResult SummerCamp()
        {
            return View();
        }

        [HttpPost("4-to-8-years-old-community-program-house-league")]
        public IActionResult SummerCamp(SummerCamp model)
        {
            if (!ModelState.IsValid) return View(model);

            var message = new Core.Message
            {
                Body = Serialize(model),
                DateCreated = DateTime.UtcNow,
                Email = model.Email,
                MessageType = Core.MessageType.SummerCamp,
                Name = $"{model.PlayerFullName}",
                Subject = $"4-8 House League Registration for {model.PlayerFullName}"
            };
            clubRepository.SaveMessage(message);
            return View(model);
        }

        [HttpGet("2006-to-2009")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost("2006-to-2009")]
        public IActionResult Registration(Registration model)
        {
            if (!ModelState.IsValid) return View(model);

            var message = new Core.Message
            {
                Body = Serialize(model),
                DateCreated = DateTime.UtcNow,
                Email = model.PrimaryGuardianEmail,
                MessageType = Core.MessageType.Registration,
                Name = $"{model.PrimaryGuardianFullName}",
                Subject = $"Register {model.PlayerFirstName} {model.PlayerLastName} for {model.AgeGroup}"
            };
            clubRepository.SaveMessage(message);
            return View(model);
        }

        private string Serialize(object myobj)
        {
            if (myobj == null) return null;
            
            var serializer = new XmlSerializer(myobj.GetType());
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, myobj);
                return textWriter.ToString();
            }            
        }
    }
}