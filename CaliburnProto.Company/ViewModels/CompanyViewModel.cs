using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.ComponentModel.DataAnnotations;
using Caliburn.Micro.Validation;

namespace CaliburnProto.Company.ViewModels
{
    public class CompanyViewModel : ValidatingScreen<CompanyViewModel>, IDataErrorInfo
    {
        private Company company;
        private bool isNew = false;

        public CompanyViewModel(Company company)
        {
            this.company = company;
            isNew = false;
        }

        public CompanyViewModel()
        {
            this.company = new Company();
            isNew = true;
        }

        [Required(ErrorMessage = "Name is required.")]
        public string Name
        {
            get { return company.Name; }
            set { company.Name = value; NotifyOfPropertyChange(()=>Name);}
        }

        [Required(ErrorMessage = "Address is required.")]
        public string Address
        {
            get { return company.Address; }
            set { company.Address = value; NotifyOfPropertyChange(() => Address); }
        }

        [Required(ErrorMessage = "Website is required.")]
        [WebAddressValidator(ErrorMessage = "The format of the email address is not valid")]
        public string Website
        {
            get { return company.Website; }
            set { company.Website = value; NotifyOfPropertyChange(() => Website); }
        }

        public string Contact
        {
            get { return company.Contact; }
            set { company.Contact = value; NotifyOfPropertyChange(() => Contact); }
        }

        public void Save()
        {
            
        }

        private bool canSave = true;
        public bool CanSave
        {
            get { return canSave; }
        }

        protected override void OnError(List<string> errors)
        {
            canSave = errors.Count == 0;
            NotifyOfPropertyChange(() => CanSave);
        }

        protected override void OnColumnError(string columnName, Dictionary<Type, string> errors)
        {
            canSave = errors.Count == 0;
            NotifyOfPropertyChange(() => CanSave);
        }
    }
}
