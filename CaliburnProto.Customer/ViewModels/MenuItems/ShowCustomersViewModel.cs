using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using CaliburnProto.Infrastructure;
using System.Windows;

namespace CaliburnProto.Customer
{
    [Export(typeof(IMenuItemViewModel))]
    public class ShowCustomerViewModel : IMenuItemViewModel
    {
        public ShowCustomerViewModel()
        {
            DisplayName = "Show Customers";
        }

        public string DisplayName { get; set; }

        public void OnClick()
        {
            MessageBox.Show("Show them all");
        }
    }
}
