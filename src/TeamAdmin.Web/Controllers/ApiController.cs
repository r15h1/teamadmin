using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TeamAdmin.Lib.Util;
using TeamAdmin.Web.Models.ApiViewModels;

namespace TeamAdmin.Web.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ApiController : Controller
    {
        private IHostingEnvironment environment;

        public ApiController(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFileCollection files)
        {
            List<string> locs = new List<string>();
            if (files == null || files.Count == 0) files = Request.Form.Files;
            var location = Settings.ImageDirectory + $"\\{DateTime.Today.ToString("yyyy-MM")}";

            if (!Directory.Exists(location)) Directory.CreateDirectory(location);
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string filename = GenerateFileName(location, file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(location, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        locs.Add($"<img src='{Settings.ImageUrlRoot}{DateTime.Today.ToString("yyyy-MM")}/{filename}'>");
                    }
                }
            }
            return new JsonResult(new UploadedData { InitialPreview = locs });
        }

        private string GenerateFileName(string location, string fileName)
        {
            int counter = 1;
            string tempFileName = fileName;
            while (System.IO.File.Exists(Path.Combine(location, tempFileName)))
            {
                tempFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + counter + Path.GetExtension(fileName);
                counter++;
            }
            return tempFileName;
        }
    }
}