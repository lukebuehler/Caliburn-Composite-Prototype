using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using CaliburnProto.Infrastructure;
using System.Collections.ObjectModel;

namespace CaliburnProto.ViewModels
{
    [Export]
    public class MenuRegionViewModel
    {
        [ImportingConstructor]
        public MenuRegionViewModel([ImportMany]IEnumerable<IMenuItemViewModel> tabVMs)
        {
            foreach (var menuViewModel in tabVMs)
                items.Add(menuViewModel);
        }

        private ObservableCollection<IMenuItemViewModel> items =new ObservableCollection<IMenuItemViewModel>();
        public ObservableCollection<IMenuItemViewModel> Items
        {
            get { return items; }
        }
    }
}
