using DialogueEditor.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DialogueEditor.Models
{
    public enum NodeType { Action, Branch, Condition, Dialogue }

    public abstract class Node : BindableBase
    {
        public string ID { get; }
        public NodeType Type { get; }

        private Point _position;
        
        public Point Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }
        public Vertex EntryVertex { get; }

        public Node()
        {
            ID = Guid.NewGuid().ToString("N");
            EntryVertex = new Vertex();
        }
        
        public Node(NodeType type) : this()
        {
            Type = type;
        }

        public Node(string id, Vertex entryVertex, NodeType type)
        {
            Debug.Assert(entryVertex != null);
            ID = id;
            EntryVertex = entryVertex;
            Type = type;
        }

        public abstract List<Vertex> GetExitVertices();

        public virtual void RemoveEdges(in ObservableCollection<Edge> edges)
        {
            Debug.Assert(edges != null);
            List<Edge> edgesIn = edges.Where(e => e.To == EntryVertex).ToList();
            
            foreach (Edge e in edgesIn) {
                _ = edges.Remove(e);
            }
        }
    }
}
