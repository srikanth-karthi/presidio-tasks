public class Binarytree
{
    public TreeNode root;

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public int MinDepth(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        if (root.left == null && root.right == null)
        {
            return 1;
        }

        int minDepth = int.MaxValue;

        if (root.left != null)
        {
            minDepth = Math.Min(minDepth, MinDepth(root.left));
        }

        if (root.right != null)
        {
            minDepth = Math.Min(minDepth, MinDepth(root.right));
        }

        return minDepth + 1;
    }

    public void Addnode(int node)
    {
        root = AddNodeRecursive(root, node);
    }

    private TreeNode AddNodeRecursive(TreeNode current, int node)
    {
        if (current == null)
        {
            return new TreeNode(node);
        }

        if (node < current.val)
        {
            current.left = AddNodeRecursive(current.left, node);
        }
        else if (node > current.val)
        {
            current.right = AddNodeRecursive(current.right, node);
        }

        return current;
    }
}


