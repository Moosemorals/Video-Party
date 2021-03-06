using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace uk.osric.VideoParty.Controllers {
    public class VideoController : Controller {

        [HttpGet("Video/{name}")]
        public IActionResult Video(string name) {

            return PhysicalFile(@"C:\Users\Osric Wilkinson\Desktop\5x11-di.webm", "video/webm");
        }
    }
}
