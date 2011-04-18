using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using CaliburnProto.Infrastructure;

namespace CaliburnProto.ViewModels
{
    [Export]
    public class DockRegionViewModel : Conductor<IScreen>.Collection.AllActive
    {
        IDockAwareWindowManager dockWindowManager;
        List<ITabViewModel> viewModels;

        [ImportingConstructor]
        public DockRegionViewModel([ImportMany]IEnumerable<ITabViewModel> tabVMs)
        {
            viewModels = new List<ITabViewModel>(tabVMs);
        }
        /// <summary>
        ///   Called when an attached view's Loaded event fires.
        /// </summary>
        /// <param name = "view">The displayed view.</param>
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            var manager = IoC.Get<IDockAwareWindowManager>();
            foreach (var vm in viewModels)
                manager.ShowDocumentWindow(vm, null);
        }

    }
}
