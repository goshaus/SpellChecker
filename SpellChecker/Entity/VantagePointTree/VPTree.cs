using System;
using System.Collections.Generic;


namespace SpellChecker.Entity.VantagePointTree
{
    public class VPTree<T>
        where T : class
    {
        private T[] data;
        private double tau;
        private VPTreeNode root;
        private Random randomizer;
        private INodeAnalyzer<T> analyzer;


        public VPTree()
        {
            this.randomizer = new Random();
        }

        /// <summary>
        /// Create VP-tree using INodeAnalyzer.CalculateDistance for node distance calculating
        /// </summary>
        public void Create(T[] data, INodeAnalyzer<T> analyzer)
        {
            this.data = data;
            this.analyzer = analyzer;
            this.root = this.CreateNode(0, data.Length);
        }


        private VPTreeNode CreateNode(int lowerIndex, int upperIndex)
        {
            if (upperIndex == lowerIndex)
            {
                return null;
            }

            VPTreeNode node = new VPTreeNode(lowerIndex);

            if (upperIndex - lowerIndex > 1)
            {
                Swap(data, lowerIndex, this.randomizer.Next(lowerIndex + 1, upperIndex));
                int medianIndex = (upperIndex + lowerIndex) / 2;
                int comparsion(T i1, T i2) => Comparer<int>.Default.Compare(this.analyzer.CalculateDistance(data[lowerIndex], i1),
                                                                            this.analyzer.CalculateDistance(data[lowerIndex], i2));

                NthElement(data, lowerIndex + 1, medianIndex, upperIndex - 1, comparsion);
                            
                node.radius = this.analyzer.CalculateDistance(this.data[lowerIndex], this.data[medianIndex]);
                node.left = CreateNode(lowerIndex + 1, medianIndex);
                node.right = CreateNode(medianIndex, upperIndex);
            }

            return node;
        }

        /// <summary>
        /// Rearranges the elements in the range (first, last), in such a way that the element at the nth position is the element that would be in that order
        /// </summary>
        private void NthElement<T>(T[] array, int startIndex, int nthToSeek, int endIndex, Comparison<T> comparison)
        {
            int from = startIndex;
            int to = endIndex;

            while (from < to)
            {
                int r = from, w = to;
                T mid = array[(r + w) / 2];

                while (r < w)
                {
                    if (comparison(array[r], mid) > -1)
                    {
                        Swap(array, w, r);
                        w--;
                    }
                    else
                    {
                        r++;
                    }
                }

                if (comparison(array[r], mid) > 0)
                {
                    r--;
                }

                if (nthToSeek <= r)
                {
                    to = r;
                }
                else
                {
                    from = r + 1;
                }
            }
        }


        private void Swap<T>(T[] arr, int index1, int index2)
        {
            T temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }


        /// <summary>
        /// Search target nearest neighbors in VP-tree 
        /// </summary>
        public void Search(T target, out T[] results)
        {
            List<HeapItem> closestHits = new List<HeapItem>();
            this.tau = this.analyzer.MaxDistance;

            Search(root, target, closestHits);

            List<T> returnResults = new List<T>();
            for (int i = closestHits.Count - 1; i > -1; i--)
            {
                returnResults.Add(this.data[closestHits[i].index]);
            }

            results = returnResults.ToArray();
        }


        private void Search(VPTreeNode node, T target, List<HeapItem> closestHits)
        {
            if (node == null)
            {
                return;
            }

            int dist = this.analyzer.SearchComparator(this.data[node.dataIndex], target);

            if (dist <= this.tau)
            {
                closestHits.Add(new HeapItem(node.dataIndex, dist));
            }

            if (node.left == null && node.right == null)
            {
                return;
            }

            if (dist < node.radius)
            {
                if (dist - this.tau <= node.radius)
                {
                    this.Search(node.left, target, closestHits);
                }

                if (dist + this.tau >= node.radius)
                {
                    this.Search(node.right, target, closestHits);
                }
            }
            else
            {
                if (dist + this.tau >= node.radius)
                {
                    this.Search(node.right, target, closestHits);
                }

                if (dist - this.tau <= node.radius)
                {
                    this.Search(node.left, target, closestHits);
                }
            }
        }
    }
}