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
    public class TabRegionViewModel : Conductor<IScreen>.Collection.OneActive
    {
        [ImportingConstructor]
        public TabRegionViewModel([ImportMany]IEnumerable<ITabViewModel> tabVMs)
        {
            this.Items.AddRange(tabVMs);
        }

    }
}
