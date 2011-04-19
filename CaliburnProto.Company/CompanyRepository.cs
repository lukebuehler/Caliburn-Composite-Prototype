using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;

namespace CaliburnProto.Company
{
    [Export]
    public class CompanyRepository : ObservableCollection<Company>
    {
        public CompanyRepository()
        {
            this.Add(new Company
            {
                Name = "Microsoft",
                Address = "Somewhere in Redmond",
                Contact = "Bill or Steve",
                Website = "http://www.microsoft.com"
            });

            this.Add(new Company
            {
                Name = "Apple",
                Address = "Infiniteloop, Cupertino",
                Contact = "Also Steve",
                Website = "http://www.apple.com"
            });

            this.Add(new Company
            {
                Name = "HP",
                Address = "Uhm Street",
                Contact = "The boss",
                Website = "http://www.hp.com"
            });

            this.Add(new Company
            {
                Name = "Google",
                Address = "Silicon valley street 15",
                Contact = "Sergey",
                Website = "http://www.bing.com"
            });
        }
    }
}
