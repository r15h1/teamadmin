using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Web.Models.AdminViewModels;
using System.Collections.Generic;
using TeamAdmin.Core.Caching;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("admin/notifications")]
    public class AdminNotificationsController : Controller
    {
        int clubId = 1;
        private INotificationRepository notificationRepository;        
        private IMapper mapper;
        private ICacheService cache;

        public AdminNotificationsController(INotificationRepository notificationRepository, IMapper mapper, ICacheService cache)
        {
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
            this.cache = cache;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var notifications = notificationRepository.GetNotifications(clubId);
            return View(mapper.Map<IEnumerable<Notification>>(notifications));
        }
        
        [HttpGet("{id}")]
        public IActionResult Details(long id)
        {
            var notification = notificationRepository.GetNotification(id);
            return View("Details", notification);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View("Details");
        }

        [HttpPost("save")]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Models.AdminViewModels.Notification notification)
        {
            if (!ModelState.IsValid || !HasValidDates(notification))
            {
                ModelState.AddModelError("dates", "Start Date must be greater than or equal to today's date. End Date must be greater than Start Date");
                return View("Details", notification);
            }

            SaveNotification(notification);
            cache.ResetNotifications();
            return RedirectToAction("Index");
        }

        private void SaveNotification(Notification notification)
        {
            Core.Notification n = new Core.Notification
            {
                ClubId = clubId,
                ExpiryDate = notification.ExpiryDate.Value,
                Message = notification.Message,
                StartDate = notification.StartDate.Value,
                NotificationId = notification.NotificationId
            };
            notificationRepository.SaveNotification(n);
        }

        [HttpGet("edit/{id:long}")]
        public IActionResult Edit(long id)
        {
            var notification = notificationRepository.GetNotification(id);
            return View("Details", mapper.Map<Notification>(notification));
        }

        private bool HasValidDates(Notification notification)
        {
            return notification.StartDate >= DateTime.Today && notification.ExpiryDate > notification.StartDate;
        }

        [HttpGet("delete/{id}")]
        public ActionResult Delete(long id)
        {
            var notification = notificationRepository.GetNotification(id);
            return View(mapper.Map<Notification>(notification));
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Notification notification)
        {
            try
            {
                notificationRepository.DeleteNotification(notification.NotificationId.Value);
                cache.ResetNotifications();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}