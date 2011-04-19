using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Caliburn.Micro.Validation;

namespace Caliburn.Micro.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class WebAddressValidator : RegularExpressionAttribute, IValidationControl
	{
        public WebAddressValidator()
            : base(@"^http(s)?://[a-z0-9-]+(.[a-z0-9-]+)*(:[0-9]+)?(/.*)?$") 
		{ }

		#region IValidationControl

		/// <summary>
		/// When true a validation controller will 
		/// </summary>
		public bool ValidateWhileDisabled { get; set; }

		/// <summary>
		/// If not defined the guard property to check for disabled state is Can[PropertyName]
		/// However it may be necessary to test another guard property and this is the place 
		/// to specify the alternative property to query.
		/// </summary>
		public string GuardProperty { get; set; }

		#endregion
    }
}
