namespace SpellChecker.Entity.VantagePointTree
{
    public class VPTreeNode
    {
        public int dataIndex;
        public int radius;
        public VPTreeNode left;
        public VPTreeNode right;


        public VPTreeNode(int dataIndex)
        {
            this.dataIndex = dataIndex;
        }
    }
}
