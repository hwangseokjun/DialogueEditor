using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DialogueEditor.Models
{
    public sealed class DialogueNode : Node
    {
        public Text Text { get; }
        public Vertex ExitVertex { get; }

        public DialogueNode() : base(NodeType.Dialogue)
        {
            Text = new Text();
            ExitVertex = new Vertex();
        }

        [JsonConstructor]
        public DialogueNode(string id, Text text, Vertex entryVertex, Vertex exitVertex) : base(id, entryVertex, NodeType.Dialogue)
        {
            Debug.Assert(text != null);
            Debug.Assert(exitVertex != null);
            Text = text;
            ExitVertex = exitVertex;
        }

        public sealed override void RemoveEdges(in ObservableCollection<Edge> edges)
        {
            base.RemoveEdges(edges);
            Edge edgeOut = edges.FirstOrDefault(e => e.From == ExitVertex);
            _ = edges.Remove(edgeOut);
        }

        public sealed override List<Vertex> GetExitVertices()
        {
            return new List<Vertex> { ExitVertex };
        }
    }
}
