using DialogueEditor.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DialogueEditor.Models
{
    public sealed class BranchNodeItem : BindableBase
    {
        public Text Text { get; }
        public Vertex ExitVertex { get; }

        public BranchNodeItem()
        {
            Text = new Text();
            ExitVertex = new Vertex();
        }

        [JsonConstructor]
        public BranchNodeItem(Text text, Vertex exitVertex)
        {
            Debug.Assert(text != null);
            Debug.Assert(exitVertex != null);
            Text = text;
            ExitVertex = exitVertex;
        }
    }
}
