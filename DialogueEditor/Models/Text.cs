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
    public class Text : BindableBase
    {
        public string ID { get; }

        private string _data;

        public string Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public Text()
        {
            ID = Guid.NewGuid().ToString("N");
        }

        [JsonConstructor]
        public Text(string id, string data)
        {
            Debug.Assert(id != null);
            ID = id;
            Data = data;
        }
    }
}
