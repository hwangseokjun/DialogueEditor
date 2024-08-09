using DialogueEditor.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DialogueEditor.Models
{
    public sealed class BranchNode : Node
    {
        private BranchNodeItem _currentItem;

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public BranchNodeItem CurrentItem
        {
            get => _currentItem;
            set => SetProperty(ref _currentItem, value);
        }
        public ObservableCollection<BranchNodeItem> Items { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public DelegateCommand AddBranchCommand { get; private set; }

        public BranchNode() : base(NodeType.Branch)
        {
            Items = new ObservableCollection<BranchNodeItem>();
            AddBranchCommand = new DelegateCommand(AddBranch, CanAddBranch);
        }

        private void AddBranch()
        {
            Items.Add(new BranchNodeItem());
        }

        private bool CanAddBranch()
        {
            return true;
        }

        public sealed override void RemoveEdges(in ObservableCollection<Edge> edges)
        {
            base.RemoveEdges(edges);

            foreach (BranchNodeItem item in Items) {
                Edge edgeOut = edges.FirstOrDefault(e => e.From == item.ExitVertex);
                _ = edges.Remove(edgeOut);
            }
        }

        public sealed override List<Vertex> GetExitVertices()
        {
            throw new NotImplementedException();
        }
    }
}
