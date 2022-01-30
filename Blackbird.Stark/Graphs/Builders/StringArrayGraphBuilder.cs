﻿using System;
using System.Collections.Generic;
using Blackbird.Stark.Graphs.Search;

namespace Blackbird.Stark.Graphs.Builders
{
    public class StringArrayGraphBuilder : IGraphBuilder<string[], char, int>
    {
        public char Obstacle { get; set; } = 'X';
        public char Start { get; set; } = 'M';
        
        private readonly Queue<Entry<char, int>> _queue = new Queue<Entry<char, int>>();

        public GraphNode<char, int> Build(string[] s)
        {
            GraphNode<char, int> root = null;
            var (i, j) = Search(Start, s);
            var eRoot = new Entry<char, int>()
            {
                Value = Start,
                I = i,
                J = j
            };
            _queue.Enqueue(eRoot);
            var processed = new bool[s.Length, s[0].Length];
            while (_queue.Count > 0)
            {
                var n = _queue.Dequeue();
                var node = new GraphNode<char, int> { Value = n.Value, Status = DiscoveryStatus.Undiscovered, Parent = n.Parent };
                processed[n.I, n.J] = true;
                if (node.Parent == null)
                {
                    if (root != null)
                        throw new InvalidOperationException();
                    root = node;
                }
                else
                {
                    node.Parent.Children.Add(node);
                }
                //enqueue children
                //upper
                if (n.I - 1 >= 0 && s[n.I - 1][n.J] != Obstacle && processed[n.I - 1, n.J] == false)
                {
                    _queue.Enqueue(new Entry<char, int> { I = n.I - 1, J = n.J, Parent = node, Value = s[n.I - 1][n.J] });
                }
                //right
                if (n.J + 1 < s[n.I].Length && s[n.I][n.J + 1] != Obstacle && processed[n.I, n.J + 1] == false)
                {
                    _queue.Enqueue(new Entry<char, int> { I = n.I, J = n.J + 1, Parent = node, Value = s[n.I][n.J + 1] });
                }
                //down
                if (n.I + 1 < s.Length && s[n.I + 1][n.J] != Obstacle && processed[n.I + 1, n.J] == false)
                {
                    _queue.Enqueue(new Entry<char, int> { I = n.I + 1, J = n.J, Parent = node, Value = s[n.I + 1][n.J] });
                }
                //left
                if (n.J - 1 >= 0 && s[n.I][n.J - 1] != Obstacle && processed[n.I, n.J - 1] == false)
                {
                    _queue.Enqueue(new Entry<char, int> { I = n.I, J = n.J - 1, Parent = node, Value = s[n.I][n.J - 1] });
                }
            }
            return root;
        }

        private (int, int) Search(char c, string[] m)
        {
            for (int i = 0; i < m.Length; i++)
                for (int j = 0; j < m[i].Length; j++)
                {
                    if (m[i][j] == c)
                        return (i, j);
                }
            throw new KeyNotFoundException();
        }

        private class Entry<TKey, TData> where TKey : IEquatable<TKey>
        {
            public int I { get; set; }

            public int J { get; set; }

            public TKey Value { get; set; }

            public GraphNode<TKey, TData> Parent { get; set; }
        }
    }
}
