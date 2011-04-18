using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using CaliburnProto.Infrastructure;
using System.Windows;
using CaliburnProto.Customer.ViewModels;

namespace CaliburnProto.Customer
{
    [Export(typeof(IMenuItemViewModel))]
    public class ShowCustomerViewModel : IMenuItemViewModel
    {
        IDockAwareWindowManager windowManager;

        [ImportingConstructor]
        public ShowCustomerViewModel(IDockAwareWindowManager windowManager)
        {
            DisplayName = "Show Customers";
            this.windowManager = windowManager;
        }

        public string DisplayName { get; set; }

        public void OnClick()
        {
            //MessageBox.Show("Show them all");
            var customersVM = new CustomersViewModel();
            windowManager.ShowDocumentWindow(customersVM, null, false);
        }
    }
}
