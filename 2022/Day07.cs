
public class Day7 : IDay
{
    public string Output => throw new NotImplementedException();
    private bool lsMode = false;
    private TreeNode<FileOrFolder> CreateTree()
    {
        TreeNode<FileOrFolder> current = new TreeNode<FileOrFolder>(new FileOrFolder("/", 0, true));
        var root = current;
        foreach (var line in InputReader.GetInputLines("Day7.txt").Skip(1))
        {
            if(line == "$ ls")
            {
                lsMode =true;
                continue;
            }
            if(line.StartsWith("$ cd"))
            {
                lsMode = false;
                if(line.EndsWith(".."))
                    current = current.Parent;
                else
                    current = current.Children.Single(t => t.Item.Name == line.Split(" ").Last());
            }
            if(lsMode)
            {
                var f = new FileOrFolder(line.Split(" ")[0], line.Split(" ")[1]);
                current.AddChild(f);
            }
        }
        return root;
    }
    public int Star1()
    {
        var root = CreateTree();
        
        var nodes = new HashSet<TreeNode<FileOrFolder>>();
        root.GetTreeNodes(ref nodes);
        var folders = nodes.Where(t => t.Item.Size == 0);
        var listOfFoldersSub = new List<int>();;

        foreach (var folder in folders)
        {
            var dir = new HashSet<TreeNode<FileOrFolder>>();
            folder.GetTreeNodes(ref dir);
            var folderSize = dir.Sum(f => f.Item.Size);
            if(folderSize <= 100000)
                listOfFoldersSub.Add(folderSize);
        }

        return listOfFoldersSub.Sum();
    }

    public int Star2()
    {
        var root = CreateTree();
        var diskspace = 70000000;
        var minimumFree = 30000000;


        var nodes = new HashSet<TreeNode<FileOrFolder>>();
        root.GetTreeNodes(ref nodes);
        var folders = nodes.Where(t => t.Item.Size == 0);
        var listOfFoldersSub = new Dictionary<string, int>();

        foreach (var folder in folders)
        {
            var dir = new HashSet<TreeNode<FileOrFolder>>();
            folder.GetTreeNodes(ref dir);
            var folderSize = dir.Sum(f => f.Item.Size);
            if(!listOfFoldersSub.ContainsKey(folder.Item.Name))
                listOfFoldersSub.Add(folder.Item.Name, folderSize);
        }

        var usedSpace = listOfFoldersSub["/"];

        var freeSpace = diskspace-usedSpace;
        var requiredToRemove = minimumFree - freeSpace;

        return listOfFoldersSub.Where(t => t.Value >= requiredToRemove).Min(t => t.Value);
    }

    private class FileOrFolder
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public FileOrFolderType FileOrFolderType { get; set;}
        public FileOrFolder(string part1, string part2)
        {
            if(part1 == "dir")
            {
                Name = part2;
                FileOrFolderType = FileOrFolderType.Folder;
                Size = 0;
            }
            else
            {
                Size = int.Parse(part1);
                Name = part2;
                FileOrFolderType = FileOrFolderType.File;
            }
        }
        public FileOrFolder(string name, int size, bool root = false)
        {
            Name = name;
            FileOrFolderType = size == 0 ? FileOrFolderType.Folder : FileOrFolderType.File;
            if(root)
                FileOrFolderType = FileOrFolderType.Root;
            Size = size;
        }
        
    }

    private enum FileOrFolderType
    {
        File, 
        Folder,
        Root
    }
}
