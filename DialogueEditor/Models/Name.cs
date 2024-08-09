using DialogueEditor.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.Models
{
    public class Name : BindableBase
    {
        public string ID { get; }

        private string _data;
        
        public string Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public Name()
        {
            ID = Guid.NewGuid().ToString("N");
        }
    }
}
