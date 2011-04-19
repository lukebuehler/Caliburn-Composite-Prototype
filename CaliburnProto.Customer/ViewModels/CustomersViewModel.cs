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
        [ImportingConstructor]
        public CustomersViewModel(IMenuManager menuManger, IDockWindowManager windowManager)
        {
            menuManger.WithParent("Customer")
                .WithScopeOf(this)
                .ShowItem(new AddCustomerAction())
                .ShowItem(new ShowCustomerAction(windowManager));
        }

        static int count =0;
        public CustomersViewModel()
        {
            this.DisplayName = "Customers" + count;
            count++;
        }
    }
}
