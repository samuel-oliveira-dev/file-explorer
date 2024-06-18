using FileExplorer.Models;
using FileExplorer.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileExplorer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string path = _configuration["RootFolder"];
            var dir = new VirtualDirectory(path);
            dir.Tree.Explore(dir.Tree.Root);

            var html = GenerateHtml(dir.Tree.Root);
            return View();
        }

        
        public string GenerateHtml(Node node)
        {
            var code = "";
            if(node == null)
            {
                return "";
            }

            if(node.Childrens.Count() == 0)
            {
                return $"<li>{node.Content.Value}</li>";
            } else
            {
                code = $"<ul>";
                foreach(var child in node.Childrens)
                {
                    code = code + GenerateHtml(child);
                }
                code = code + "</ul>";
            }


            return code;

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
