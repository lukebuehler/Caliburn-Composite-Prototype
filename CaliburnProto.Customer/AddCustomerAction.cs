using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using CaliburnProto.Infrastructure;
using System.Windows;

namespace CaliburnProto.Customer
{
    //[Export(typeof(IActionItem))]
    public class AddCustomerAction : ActionItem
    {
        public AddCustomerAction()
            : base("AddCustomer")
        {
        }

        public override void Execute()
        {
            MessageBox.Show("Customer created");
        }


    }
}
