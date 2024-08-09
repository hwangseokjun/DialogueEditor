using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.Models
{
    public sealed class ActionNode : Node
    {
        public ActionNode() : base(NodeType.Action)
        {
        }

        public sealed override List<Vertex> GetExitVertices()
        {
            throw new NotImplementedException();
        }

        public sealed override void RemoveEdges(in ObservableCollection<Edge> edges)
        {
            throw new NotImplementedException();
        }
    }
}
