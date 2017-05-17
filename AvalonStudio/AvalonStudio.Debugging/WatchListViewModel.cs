namespace AvalonStudio.Debugging
{
    using Avalonia.Threading;
    using AvalonStudio.Extensibility;
    using AvalonStudio.Extensibility.Plugin;
    using AvalonStudio.MVVM;
    using Mono.Debugging.Client;
    using ReactiveUI;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public class WatchListViewModel : ToolViewModel, IExtension, IWatchList
    {
        protected IDebugManager2 DebugManager { get; set; }

        private readonly List<ObjectValue> watches;
        private List<string> _expressions;

        private ObservableCollection<ObjectValueViewModel> children;
        public List<ObjectValueViewModel> LastChangedRegisters { get; set; }

        public WatchListViewModel()
        {
            Dispatcher.UIThread.InvokeAsync(() => { IsVisible = false; });
            watches = new List<ObjectValue>();
            Title = "Watch List";
            Children = new ObservableCollection<ObjectValueViewModel>();
            LastChangedRegisters = new List<ObjectValueViewModel>();

            _expressions = new List<string>();

            Activation(); // for when we create the part outside of composition.
        }

        public ObservableCollection<ObjectValueViewModel> Children
        {
            get { return children; }
            set { this.RaiseAndSetIfChanged(ref children, value); }
        }

        public override Location DefaultLocation
        {
            get { return Location.RightMiddle; }
        }

        public virtual void BeforeActivation()
        {
            IoC.RegisterConstant(this, typeof(IWatchList));
        }

        public virtual void Activation()
        {
            DebugManager = IoC.Get<IDebugManager2>();

            if (DebugManager != null)
            {
                DebugManager.FrameChanged += DebugManager_FrameChanged;
                DebugManager.DebugSessionStarted += (sender, e) => { IsVisible = true; };

                DebugManager.DebugSessionEnded += (sender, e) =>
                {
                    IsVisible = false;
                    Clear();
                };
            }
        }

        private void DebugManager_FrameChanged(object sender, System.EventArgs e)
        {
            var expressions = DebugManager.SelectedFrame.GetExpressionValues(_expressions.ToArray(), false);

            InvalidateObjects(expressions);
        }

        public void InvalidateObjects(params ObjectValue[] variables)
        {
            var updated = new List<ObjectValue>();
            var removed = new List<ObjectValue>();

            for (var i = 0; i < watches.Count; i++)
            {
                var watch = watches[i];

                var currentVar = variables.FirstOrDefault(v => v.Name == watch.Name);

                if (currentVar == null)
                {
                    removed.Add(watch);
                }
                else
                {
                    updated.Add(watch);
                }
            }

            foreach (var variable in variables)
            {
                var currentVar = updated.FirstOrDefault(v => v.Name == variable.Name);

                if (currentVar == null)
                {
                    watches.Add(variable);
                    Add(variable);
                }
                else
                {
                    var currentVm = Children.FirstOrDefault(c => c.Model.Name == currentVar.Name);

                    currentVm?.ApplyChange(variable);
                }
            }

            foreach (var removedvar in removed)
            {
                watches.Remove(removedvar);
                Remove(removedvar);
            }
        }

        public void Add(ObjectValue model)
        {
            var newWatch = new ObjectValueViewModel(this, model);

            Children.Add(newWatch);
        }

        public void Remove(ObjectValue value)
        {
            Children.Remove(Children.FirstOrDefault(c => c.Model.Name == value.Name));
        }

        private void ApplyChange(ObjectValue change)
        {
            foreach (var watch in Children)
            {
                if (watch.ApplyChange(change))
                {
                    break;
                }
            }
        }

        public virtual void Clear()
        {
            Children.Clear();

            watches.Clear();
        }

        public bool AddWatch(string expression)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(expression))
            {
                if (DebugManager.SessionActive && !DebugManager.Session.IsRunning && DebugManager.Session.IsConnected && !_expressions.Contains(expression))
                {
                    _expressions.Add(expression);

                    var watch = DebugManager.SelectedFrame.GetExpressionValue(expression, false);

                    InvalidateObjects(watch);

                    return true;
                }
            }

            return result;
        }
    }
}