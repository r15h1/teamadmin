using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeamAdmin.Web.Models
{
    public class ReCaptcha
    {
        [Required(ErrorMessage = "Please click on the 'I\'m not a robot' box at the bottom of the page")]
        [FromForm]
        [FromBody]
        public string CaptchaResponse { get; set; }
    }

    public class CaptchaVerification
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> Errors { get; set; }
    }
}