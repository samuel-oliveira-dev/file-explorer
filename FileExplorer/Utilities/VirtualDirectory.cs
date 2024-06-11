namespace FileExplorer.Utilities
{
    public class VirtualDirectory
    {
        public string Path { get; set; }
        public Tree Tree { get; set; }
        public VirtualDirectory(string path)
        {
            Path = path;
            Tree = new Tree(Build(Path, new Stack<string>(), new HashSet<string>()));
        }   

        private Node Build(string path, Stack<string> stack, HashSet<string> visitedPaths)
        {
            if (visitedPaths.Contains(path))
            {
                return null;
            }
            visitedPaths.Add(path);

            var node = new Node(new Folder(GetFolder(path)));
            foreach(var file in GetFilesNames(path))
            {
                node.AddChild(new Node(new File(file)));
            }
            var folders = GetFoldersNames(path);
            if (folders.Any())
            {
                foreach (var folder in folders)
                {
                    stack.Push(folder);
                    var newPath = System.IO.Path.Combine(path, folder);
                    var newNode = Build(newPath, stack, visitedPaths);
                    if(newNode != null)
                    {
                        node.AddChild(newNode);
                    }
                    stack.Pop();
                }
            }

            return node;



        }

        private string GetFolder(string path)
        {
            return System.IO.Path.GetFileName(path);
        }

        public List<DirectoryInfo> GetFoldersInfos(string path)
        {
            var folders = Directory.GetDirectories(path).Select(d => new DirectoryInfo(d)).ToList();
            return folders;
        }
        public List<string> GetFoldersNames(string path)
        {
            var folders = Directory.GetDirectories(path).Select(d => new DirectoryInfo(d)).Select(p => p.Name).ToList();
            return folders;
        }

        public List<string> GetFilesNames(string path)
        {
            var files = Directory.GetFiles(path).Select(f => new FileInfo(f)).Select(s => s.Name).ToList();
            return files;
        }

        public string QueueToPath(Queue<string> queue)
        {
            var result = "";

            foreach (var item in queue)
            {
                result += $"\\{item}";
            }

            return result;
        }

        public string StackToPath(Stack<string> queue)
        {
            var result = "";

            foreach (var item in queue)
            {
                result = $"\\{item}" + result;
            }

            return result;
        }
    }
}
