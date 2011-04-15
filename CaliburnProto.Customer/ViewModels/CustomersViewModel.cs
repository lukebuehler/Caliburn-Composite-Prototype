using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using CaliburnProto.Infrastructure;

namespace CaliburnProto.Customer.ViewModels
{
    [Export(typeof(ITabViewModel))]
    public class CustomersViewModel : Conductor<IScreen>.Collection.OneActive, ITabViewModel
    {
        public CustomersViewModel()
        {
            this.DisplayName = "Customers";
        }
    }
}
