using DialogueEditor.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace DialogueEditor.Models
{
    public sealed class Vertex : BindableBase
    {
        public string ID { get; }
        
        private Point _position;

        public Point Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public Vertex()
        {
            ID = Guid.NewGuid().ToString("N");
        }

        [JsonConstructor]
        public Vertex(string id, Point position)
        {
            Debug.Assert(id != null);
            ID = id;
            Position = position;
        }
    }
}
