using DialogueEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DialogueEditor.Utils
{
    public class NodeConverter : JsonConverter<Node>
    {
        public override Node Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument document = JsonDocument.ParseValue(ref reader)) {
                JsonElement root = document.RootElement;
                JsonElement element = root.GetProperty("Type");
                NodeType type = (NodeType)element.GetInt32();

                switch (type) {
                case NodeType.Action:
                    return JsonSerializer.Deserialize<ActionNode>(root.GetRawText(), options);
                case NodeType.Branch:
                    return JsonSerializer.Deserialize<BranchNode>(root.GetRawText(), options);
                case NodeType.Condition:
                    return JsonSerializer.Deserialize<ConditionNode>(root.GetRawText(), options);
                case NodeType.Dialogue:
                    return JsonSerializer.Deserialize<DialogueNode>(root.GetRawText(), options);
                default:
                    throw new NotSupportedException();
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, Node value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
