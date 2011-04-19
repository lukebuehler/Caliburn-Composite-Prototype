using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using CaliburnProto.Infrastructure;

namespace CaliburnProto.Company.ViewModels
{
    [Export(typeof(IDockViewModel))]
    [Export(typeof(CompaniesViewModel))]
    public class CompaniesViewModel : Conductor<CompanyViewModel>.Collection.OneActive, IDockViewModel
    {
        private CompanyRepository repository;

        [ImportingConstructor]
        public CompaniesViewModel(CompanyRepository repository, IMenuManager menuManager)
        {
            this.DisplayName = "Companies";

            foreach (var company in repository)
            {
                this.ActivateItem(new CompanyViewModel(company));
            }

            menuManager.WithParent("Company")
                .ShowItem(new ShowCompaniesAction());
        }
    }
}
