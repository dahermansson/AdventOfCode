namespace AoC.Utils;

public class TreeNode<T> where T: notnull
{
    public TreeNode(T item)
    {
        Children = new List<TreeNode<T>>();
        Item = item;
    }
    public TreeNode<T> Parent { get; set; } = null!;
    public List<TreeNode<T>> Children { get; set; }
    public T Item {get; set;}
    public TreeNode<T> AddChild(T item)
    {
        var child = new TreeNode<T>(item){ Parent = this};
        Children.Add(child);
        return child;
    }

    public void GetTreeNodes(ref HashSet<TreeNode<T>> nodes)
    {
        nodes.Add(this);
        foreach (var child in Children)
        {
            child.GetTreeNodes(ref nodes);
        }
    }

    public IEnumerable<T> EnumerateNodes()
    {
        yield return this.Item;
        foreach (var child in Children)
            child.EnumerateNodes();
    }
}