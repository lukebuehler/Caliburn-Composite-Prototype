using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using CaliburnProto.Infrastructure;
using System.Collections.ObjectModel;

namespace CaliburnProto.ViewModels
{
    [Export(typeof(IMenuManager))]
    [Export(typeof(MenuViewModel))]
    public class MenuViewModel : ActionItemManager, IMenuManager
    {
        [ImportingConstructor]
        public MenuViewModel([ImportMany] IEnumerable<IActionItem> actionItems)
        {
            this.ShowItem(new ActionItem("File", null));
            foreach (var menuViewModel in actionItems)
                Items.Add(menuViewModel);
        }
    }
}
