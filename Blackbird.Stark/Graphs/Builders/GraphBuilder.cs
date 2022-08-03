using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Blackbird.Stark.Graphs.Builders;

public interface IBuilder<T>
{
    void AddEdge(GraphEdge<T> edge);
    void AddVertex(T from, T to);
    GraphEdge<T> GetEdge(T name);
}

public class GraphEdge<T>
{
    public T Name { get; set; }
    public List<GraphEdge<T>> Vertices { get; set; }

    public GraphEdge()
    {
        Vertices = new List<GraphEdge<T>>();
    }
}

public class GraphBuilder<T>: IBuilder<T> where T:IComparable<T>
{
    private readonly Dictionary<T, GraphEdge<T>> _graph = new Dictionary<T, GraphEdge<T>>();

    public void AddEdge(GraphEdge<T> edge)
    {
        if (!_graph.ContainsKey(edge.Name))
        {
            _graph.Add(edge.Name, edge);
        }
    }

    public void AddVertex(T from, T to)
    {
        if (_graph.ContainsKey(to) && _graph.ContainsKey(from))
        {
            var source = _graph[from];
            var dst = _graph[to];
            if(!source.Vertices.Contains(dst))
                source.Vertices.Add(dst);
            if(!dst.Vertices.Contains(source))
                dst.Vertices.Add(source);
        }
    }

    private bool IsValidGraph()
    {
        //graph with 1 edge is always valid graph
        if (_graph.Count > 1)
        {
            foreach (var edge in _graph)
            {
                //if there is multiple edges and at least 1 without vertex graph is invalid
                if (edge.Value.Vertices.Count == 0)
                    return false;
            }
        }
        
        return true;
    }

    public GraphEdge<T> GetEdge(T name)
    {
        if (IsValidGraph())
        {
            return _graph[name];
        }
        else
        {
            throw new InvalidGraphException();
        }
    }
}

public class InvalidGraphException : Exception
{
}