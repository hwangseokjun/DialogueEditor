using DialogueEditor.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.Models
{
    public sealed class Edge : BindableBase
    {
        public Vertex From { get; }
        public Vertex To { get; }

        public Edge(Vertex from, Vertex to)
        {
            Debug.Assert(from != null);
            Debug.Assert(to != null);
            From = from;
            To = to;
        }
    }
}
