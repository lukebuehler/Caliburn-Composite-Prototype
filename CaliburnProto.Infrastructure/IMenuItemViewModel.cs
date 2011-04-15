using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CaliburnProto.Infrastructure
{
    public interface IMenuItemViewModel
    {
        string DisplayName { get; set; }
        void OnClick();
    }
}
