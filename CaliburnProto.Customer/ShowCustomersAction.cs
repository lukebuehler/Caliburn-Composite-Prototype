using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using CaliburnProto.Infrastructure;
using System.Windows;
using CaliburnProto.Customer.ViewModels;

namespace CaliburnProto.Customer
{
    //[Export(typeof(IActionItem))]
    public class ShowCustomerAction : ActionItem
    {
        IDockWindowManager windowManager;

        [ImportingConstructor]
        public ShowCustomerAction(IDockWindowManager windowManager) : base("Show Customers")
        {
            this.windowManager = windowManager;
        }

        public override void Execute()
        {
            var customersVM = IoC.Get<CustomersViewModel>();
            windowManager.ShowDocumentWindow(customersVM, null, false);
        }
    }
}
