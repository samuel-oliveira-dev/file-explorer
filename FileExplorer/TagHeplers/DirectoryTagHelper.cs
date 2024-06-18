using FileExplorer.Utilities;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FileExplorer.TagHeplers
{
    [HtmlTargetElement("directory")]
    public class DirectoryTagHelper : TagHelper
    {
        private readonly IConfiguration _configuration; 

        public DirectoryTagHelper(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            string path = _configuration["RootFolder"];
            var dir = new VirtualDirectory(path);
            dir.Tree.Explore(dir.Tree.Root);

            var html = dir.GenerateHtml(dir.Tree.Root);
            output.Content.SetContent(html);

        }
    }
}
