using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackbird.Stark.Trees;
using Blackbird.Stark.Trees.Nodes;

namespace Blackbird.Stark.Extensions
{
    public static class TreeExtensions
    {
        public static void PrintTree<TK, TV>(this ITree<TK, TV> self) where TK: IComparable<TK>
        {
            switch (self)
            {
                case AvlTree<TK, TV> avlTree:
                    avlTree.PrintTree();
                    break;
                case BinaryTree<TK, TV> binaryTree:
                    binaryTree.PrintTree();
                    break;
                case RbTree<TK, TV> rbTree:
                    rbTree.PrintTree();
                    break;
            }
        }
        
        public static void PrintTree<TK, TV>(this AvlTree<TK, TV> self) where TK : IComparable<TK>
        {
            RenderTree(self._root);
        }

        public static void PrintTree<TK, TV>(this BinaryTree<TK, TV> self) where TK : IComparable<TK>
        {
            RenderTree(self._root);
        }

        public static void PrintTree<TK, TV>(this RbTree<TK, TV> self) where TK : IComparable<TK>
        {
            if(self.Count > 0)
                RenderTree(self._root);
            else
                Console.WriteLine("RbTree is empty");
        }

        private static void RenderTree<TK,TV>(BinaryNode<TK,TV> tree) where TK: IComparable<TK>
        {
            var map = GenerateLevelMap(tree);
            var maxLevelHeight = map.Select(x => x.Count).Max();
            var str = new List<string>();
            for (int i = 0; i < map.Count; i++)
            {
                foreach (var node in map[i])
                {
                    if(node.IsRoot)
                        str.Add(RenderKey(i,node.Key));
                    else
                    {
                        var parentKey = RenderKey(i - 1, node.Parent.Key);
                        var parentIndex = str.IndexOf(parentKey);
                        if (node == node.Parent.Right)
                        {
                            str.Insert(parentIndex+1, RenderKey(i, node.Key));
                        }
                        else
                        {
                            if (node.Parent.HasBothChildren)
                            {
                                if (parentIndex+1 < str.Count && str[parentIndex + 1].StartsWith(i.ToString()))
                                {
                                    str.Insert(parentIndex + 2, RenderKey(i, node.Key));
                                }
                                else
                                {
                                    str.Insert(parentIndex + 1, RenderKey(i, node.Key));
                                }
                            }
                            else
                            {
                                str.Insert(parentIndex+1, RenderKey(i, node.Key));
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < str.Count; i++)
            {
                for (int j = 0; j < str[i].Length; j++)
                {
                    if (str[i][j] == '╚')
                    {
                        for (int k = i-1; k >= 0; k--)
                        {
                            if (j < str[k].Length)
                            {
                                if (str[k][j] == ' ')
                                {
                                    var sb = new StringBuilder(str[k]) {[j] = '║'};
                                    str[k] = sb.ToString();
                                }
                                else
                                {
                                    if (str[k][j] == '╚')
                                    {
                                        var sb = new StringBuilder(str[k]){[j] = '╠'};
                                        str[k] = sb.ToString();
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            foreach (var line in str)
            {
                Console.WriteLine(line);
            }
        }
        private static string RenderKey<TK>(int level, TK key) where TK : IComparable<TK>
        {
            var sb = new StringBuilder();
            sb.Append($"{level}:");
            
            if(level > 1)
            {
                for (int i = 0; i < level-1; i++)
                    sb.Append("    ");
            }
            if (level != 0)
            {
                sb.Append("╚═══");
            }

            sb.Append(key);
            return sb.ToString();
        }
        private static List<List<BinaryNode<TK,TV>>> GenerateLevelMap<TK,TV>(BinaryNode<TK,TV> tree) where TK: IComparable<TK>
        {
            var levels = new List<List<BinaryNode<TK,TV>>>();
            int currentLevel = 0;
            levels.Add(new List<BinaryNode<TK,TV>>() {tree});
            var isThereUnprocessedNodes = true;
            while (isThereUnprocessedNodes)
            {
                if (levels[currentLevel].All(x => !x.HasChildren))
                    isThereUnprocessedNodes = false;
                else
                {
                    levels.Add(new List<BinaryNode<TK, TV>>());
                    foreach (var node in levels[currentLevel])
                    {
                        if (node.HasChildren)
                        {
                            if(node.HasLeftChild)
                                levels[currentLevel+1].Add(node.Left);
                            if(node.HasRightChild)
                                levels[currentLevel+1].Add(node.Right);
                        }
                    }
                    currentLevel++;
                }
            }

            return levels;
        }
    }
}