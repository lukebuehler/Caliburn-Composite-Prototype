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
        List<IDockViewModel> viewModels;

        [ImportingConstructor]
        public DockViewModel([ImportMany]IEnumerable<IDockViewModel> tabVMs)
        {
            viewModels = new List<IDockViewModel>(tabVMs);
        }
        /// <summary>
        ///   Called when an attached view's Loaded event fires.
        /// </summary>
        /// <param name = "view">The displayed view.</param>
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            dockWindowManager = IoC.Get<IDockWindowManager>();
            foreach (var vm in viewModels)
                dockWindowManager.ShowDocumentWindow(vm, null);
        }

    }
}
