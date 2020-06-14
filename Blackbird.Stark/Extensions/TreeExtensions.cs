using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackbird.Stark.Trees;

namespace Blackbird.Stark.Extensions
{
    public static class TreeExtensions
    {
        public static void PrintTree<K, V>(this ITree<K, V> self) where K: IComparable<K>
        {
            if (self is AvlTree<K, V> avlTree)
            {
                avlTree.PrintTree();
            }

            if (self is BinaryTree<K, V> binaryTree)
            {
                binaryTree.PrintTree();
            }
        }

        public static void PrintTree<K, V>(this AvlTree<K, V> self) where K : IComparable<K>
        {
            RenderTree(self._root);
        }

        public static void PrintTree<K, V>(this BinaryTree<K, V> self) where K : IComparable<K>
        {
            RenderTree(self._root);
        }
        private static void RenderTree<K,V>(BinaryNode<K,V> tree) where K: IComparable<K>
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
                    if (str[i][j] == '┕')
                    {
                        for (int k = i-1; k >= 0; k--)
                        {
                            if (j < str[k].Length) 
                                if(str[k][j] == ' ')
                                {
                                    var sb = new StringBuilder(str[k]) {[j] = '|'};
                                    str[k] = sb.ToString();
                                }
                                else
                                {
                                    break;
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

        private static string RenderKey<K>(int level, K key) where K : IComparable<K>
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
                sb.Append("┕---");
            }

            sb.Append(key);
            return sb.ToString();
        }

        private static List<List<BinaryNode<K,V>>> GenerateLevelMap<K,V>(BinaryNode<K,V> tree) where K: IComparable<K>
        {
            var levels = new List<List<BinaryNode<K,V>>>();
            int currentLevel = 0;
            levels.Add(new List<BinaryNode<K,V>>() {tree});
            var isThereUnprocessedNodes = true;
            while (isThereUnprocessedNodes)
            {
                if (levels[currentLevel].All(x => !x.HasChildren))
                    isThereUnprocessedNodes = false;
                else
                {
                    levels.Add(new List<BinaryNode<K, V>>());
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