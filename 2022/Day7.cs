
public class Day7 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1()
    {
        TreeNode<int> tree = new TreeNode<int>(1);

        tree.AddChild(11);
        var tolv =tree.AddChild(12);
        tolv.AddChild(111);

        var noder = new HashSet<TreeNode<int>>();
        tree.GetTreeNodes(ref noder);
        foreach (var n in noder)
        {
            Console.WriteLine(n.Item);
        }

        return 1;
    }

    public int Star2()
    {
        throw new NotImplementedException();
    }
}
