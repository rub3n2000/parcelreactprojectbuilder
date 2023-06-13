using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace DotnetReact.Controllers
{
    public class Home : ControllerBase
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", "index.html"), "text/HTML");
        }
    }
}