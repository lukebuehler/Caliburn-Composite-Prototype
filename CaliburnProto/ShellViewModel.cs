using System.Windows;

namespace CaliburnProto {

using System.ComponentModel.Composition;
using Caliburn.Micro;
    using CaliburnProto.ViewModels;

    [Export(typeof(IShell))]
    public class ShellViewModel : IShell
    {
        [Import]
        public DockViewModel DockRegion { get; set; }

        [Import]
        public MenuViewModel MenuRegion { get; set; }
    }
}
