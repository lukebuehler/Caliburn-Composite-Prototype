using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using CaliburnProto.Company.ViewModels;
using CaliburnProto.Infrastructure;

namespace CaliburnProto.Company
{
    public class ShowCompaniesAction: ActionItem
    {
        public ShowCompaniesAction()
            : base("Show Companies")
        {
        }

        public override void Execute()
        {
            var manager = IoC.Get<IDockWindowManager>();
            var vm = IoC.Get<CompaniesViewModel>();
            manager.ShowDocumentWindow(vm, null, false);
        }
    }
}
