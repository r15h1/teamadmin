using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Web.Models.AdminViewModels
{
    public class Notification
    {
        public long? NotificationId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Message { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        public bool IsActive
        {
            get
            {
                return DateTime.Today >= StartDate && DateTime.Today < ExpiryDate;
            }
        }

        public string Status
        {
            get
            {
                return DateTime.Today < StartDate ? "Future" : (DateTime.Today >= ExpiryDate ? "Expired" : "Active");
            }
        }
    }
}
