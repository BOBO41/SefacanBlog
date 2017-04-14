using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sefacan.Framework.Infrastracture
{
    public class UserPrincipal : IPrincipal
    {
        public IIdentity Identity => throw new NotImplementedException();

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}