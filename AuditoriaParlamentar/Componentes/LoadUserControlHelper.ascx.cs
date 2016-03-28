using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AuditoriaParlamentar.Componentes
{
    public partial class LoadUserControlHelper : System.Web.UI.UserControl
    {
        public LoadUserControlHelper(){}
        private string _userControlParameter;
        public string UserControlParameter
        {
            get { return _userControlParameter; }
            set { _userControlParameter = value; }
        }
    }
}