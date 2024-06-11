namespace FileExplorer.Utilities
{
    public class Tree
    {
        public Node Root { get; private set; }

        public Tree(Node root)
        {
            Root = root;
        }
        public void Explore(Node node)
        {
            if(node == null) return;
            foreach(var child in node.Childrens)
            {
                Console.WriteLine(child.Content.Value);
                Explore(child);
            }
        }

        public Node Explore(Func<Node> function)
        {
            return Root;
        }
    }
}
