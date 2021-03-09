using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace uk.osric.VideoParty.Controllers {
    public class VideoController : Controller {

        [HttpGet("Video/{name}")]
        public IActionResult Video(string name) {

            if (name == "0") { 
                return PhysicalFile(@"C:\Users\Osric Wilkinson\Desktop\5x11.webm", "video/webm", true);
            } else {
                return PhysicalFile(@"C:\Users\Osric Wilkinson\Desktop\5x11-sub.webm", "video/webm", true); 
            }
        }
    }
}
