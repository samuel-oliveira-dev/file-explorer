namespace FileExplorer.Utilities
{
    public class Node
    {
        public Content Content { get; set; }
        public List<Node> Childrens { get; set; } = new List<Node>();

        public Node(Content content) 
        {
            Content = content; 
        }
        public void AddChild(Node node)
        {
            Childrens.Add(node);   
        }
    }
}
