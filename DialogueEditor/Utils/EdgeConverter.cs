using DialogueEditor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DialogueEditor.Utils
{
    public class EdgeConverter : JsonConverter<Edge>
    {
        private readonly ObservableCollection<Node> _nodes;

        public EdgeConverter()
        {
        }

        public EdgeConverter(ObservableCollection<Node> nodes)
        {
            Debug.Assert(nodes != null);
            _nodes = nodes;
        }

        public override Edge Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string fromID = null;
            string toID = null;

            while (reader.Read()) {
                if (reader.TokenType == JsonTokenType.EndObject) {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName) {
                    string propertyName = reader.GetString();
                    _ = reader.Read();

                    if (propertyName == "From") {
                        fromID = reader.GetString();
                    } else if (propertyName == "To") {
                        toID = reader.GetString();
                    }
                }
            }

            Vertex fromVertex = (from n in _nodes
                                 from v in n.GetExitVertices()
                                 where v.ID == fromID
                                 select v).FirstOrDefault();
            Vertex toVertex = _nodes.First(n => n.ID == toID).EntryVertex;

            if (fromVertex == null || toVertex == null) {
                throw new JsonException("vertex not founded.");
            }

            return new Edge(fromVertex, toVertex);
        }

        public override void Write(Utf8JsonWriter writer, Edge value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("From", value.From.ID);
            writer.WriteString("To", value.To.ID);
            writer.WriteEndObject();
        }
    }
}
