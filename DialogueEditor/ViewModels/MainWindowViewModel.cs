using DialogueEditor.Common;
using DialogueEditor.Models;
using DialogueEditor.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DialogueEditor.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private const int _delay = 100;
        private const double _maxzoom = 3.0;
        private const double _minzoom = 0.3;

        private string _title;
        private double _zoom;
        private Node _currentNode;
        
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public double Zoom
        {
            get => _zoom;
            set => SetProperty(ref _zoom, value);
        }
        public Node CurrentNode
        {
            get => _currentNode;
            set => SetProperty(ref _currentNode, value);
        }
        public ObservableCollection<Node> Nodes { get; private set; }
        public ObservableCollection<Edge> Edges { get; private set; }

        public DelegateCommand AddActionNodeCommand { get; private set; }
        public DelegateCommand AddBranchNodeCommand { get; private set; }
        public DelegateCommand AddConditionNodeCommand { get; private set; }
        public DelegateCommand AddDialogueNodeCommand { get; private set; }
        public DelegateCommand RemoveNodeCommand { get; private set; }
        public ParameterCommand AddEdgeCommand { get; private set; }
        public DelegateCommand ZoomInCommand { get; private set; }
        public DelegateCommand ZoomOutCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand LoadCommand { get; private set; }

        public MainWindowViewModel()
        {
            Nodes = new ObservableCollection<Node>();
            Edges = new ObservableCollection<Edge>();
            AddActionNodeCommand = new DelegateCommand(AddActionNode);
            AddBranchNodeCommand = new DelegateCommand(AddBranchNode);
            AddConditionNodeCommand = new DelegateCommand(AddConditionNode);
            AddDialogueNodeCommand = new DelegateCommand(AddDialogueNode);
            RemoveNodeCommand = new DelegateCommand(RemoveNode, CanRemoveNode);
            AddEdgeCommand = new ParameterCommand(AddEdge);
            ZoomInCommand = new DelegateCommand(ZoomIn, CanZoomIn);
            ZoomOutCommand = new DelegateCommand(ZoomOut, CanZoomOut);
            SaveCommand = new DelegateCommand(Save);
            LoadCommand = new DelegateCommand(Load);
            Zoom = 1.0;
            Title = "Dialogue editor";
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Zoom)) {
                ZoomInCommand.RaiseCanExecuteChanged();
                ZoomOutCommand.RaiseCanExecuteChanged();
            }

            if (propertyName is nameof(CurrentNode)) {
                RemoveNodeCommand.RaiseCanExecuteChanged();
            }
        }

        private void AddActionNode()
        {
            Nodes.Add(new ActionNode());
        }

        private void AddBranchNode()
        {
            Nodes.Add(new BranchNode());
        }

        private void AddConditionNode()
        {
            Nodes.Add(new ConditionNode());
        }

        private void AddDialogueNode()
        {
            Nodes.Add(new DialogueNode());
        }

        private void RemoveNode()
        {
            _currentNode.RemoveEdges(Edges);
            _ = Nodes.Remove(_currentNode);
        }

        private bool CanRemoveNode()
        {
            return _currentNode != null;
        }

        private void AddEdge(object parameter)
        {
            if (parameter is ValueTuple<object, object> tuple) {
                Vertex from = (Vertex)tuple.Item1;
                Vertex to = (Vertex)tuple.Item2;
                Edge current = Edges.FirstOrDefault(e => e.From == from);
                Edge edge = new Edge(from, to);
                Edges.Add(edge);

                if (current != null) {
                    _ = Edges.Remove(current);
                }
            }
        }

        private void ZoomIn()
        {
            const double mul = 1.2;
            double zoom = _zoom * mul;
            Zoom = zoom < _maxzoom ? zoom : _maxzoom;
            _ = Task.Delay(_delay);
        }

        private bool CanZoomIn()
        {
            return _zoom < _maxzoom;
        }

        private void ZoomOut()
        {
            const double mul = 0.8;
            double zoom = _zoom * mul;
            Zoom = _minzoom < zoom ? zoom : _minzoom;
            _ = Task.Delay(_delay);
        }

        private bool CanZoomOut()
        {
            return _minzoom < _zoom;
        }

        private void Save()
        {
            var options = new JsonSerializerOptions {
                Converters = { new NodeConverter(), new EdgeConverter() },
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(new { Edges, Nodes }, options);
            Debug.WriteLine(json);
        }

        private void Load()
        {
            throw new NotImplementedException();
        }
    }
}
