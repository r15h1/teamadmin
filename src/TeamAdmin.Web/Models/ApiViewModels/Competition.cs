using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using TeamAdmin.Web.Formatters;

namespace TeamAdmin.Web.Models.ApiViewModels
{
    public class Competition
    {
        public long? CompetitionId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime? EndDate { get; set; }
    }
}
