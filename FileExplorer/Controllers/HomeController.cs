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
            Console.WriteLine(html);
            return View();
        }

        
        public string GenerateHtml(Node node)
        {
            var code = "";
            if(!node.Childrens.Any()) 
            {
                
                return code;
            } else
            {
                code += "<ul>";
                foreach(var child in node.Childrens)
                {
                    if(child.Content is Folder)
                    {
                        code += $"<li><i class=\"bi bi-caret-down mx-2\"></i><i class=\"bi bi-folder mx-2\"></i>{child.Content.Value}" + GenerateHtml(child) + "</li>";
                    } else
                    {
                        if(child.Content is Utilities.File)
                        {
                            code += $"<li><i class=\"bi-file-earmark mx-2\"></i>{child.Content.Value}" + GenerateHtml(child) + "</li>";
                        }
                    }
                    
                 }

                code += "</ul>";
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
