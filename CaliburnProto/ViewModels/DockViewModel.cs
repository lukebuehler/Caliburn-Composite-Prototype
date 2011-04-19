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
    public class DockViewModel : ViewAware
    {
        IDockWindowManager dockWindowManager;
        List<ITabViewModel> viewModels;

        [ImportingConstructor]
        public DockViewModel([ImportMany]IEnumerable<ITabViewModel> tabVMs)
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

            var manager = IoC.Get<IDockWindowManager>();
            foreach (var vm in viewModels)
                manager.ShowDocumentWindow(vm, null);
        }

    }
}
