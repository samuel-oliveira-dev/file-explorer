using FileExplorer.Utilities;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FileExplorer.TagHeplers
{
    [HtmlTargetElement("directory")]
    public class DirectoryTagHelper(IConfiguration configuration) : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            string path = configuration["RootFolder"];
            var dir = new VirtualDirectory(path);
            dir.Tree.Explore(dir.Tree.Root);

            var html = dir.GenerateHtml(dir.Tree.Root);
            output.Content.SetHtmlContent(html);

        }
    }
}
