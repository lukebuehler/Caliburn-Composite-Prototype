using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using CaliburnProto.Infrastructure;

namespace CaliburnProto.Customer.ViewModels
{
    [Export(typeof(IDockViewModel))]
    [Export(typeof(CustomersViewModel))]
    public class CustomersViewModel : Screen, IDockViewModel
    {
        [ImportingConstructor]
        public CustomersViewModel(IMenuManager menuManger, IDockWindowManager windowManager)
        {
            DisplayName = "Customers";

            //setup the menu
            menuManger.WithParent("Customer")
                .ShowItem(new ShowCustomerAction(windowManager))
                .WithScopeOf(this)
                .ShowItem(new AddCustomerAction());

        }

        public void ToggleScreen()
        {
            if (IsActive)
            {
                var deactivatable = this as IDeactivate;
                deactivatable.Deactivate(false);
            }
            else
            {
                var activatable = this as IActivate;
                activatable.Activate();
            }
        }
    }
}
