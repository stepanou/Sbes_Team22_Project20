﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace SecurityManager
{
    class CustomPrincipal : IPrincipal
    {
        WindowsIdentity identity = null;
        public CustomPrincipal(WindowsIdentity windowsIdentity)
        {
            identity = windowsIdentity;
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string permission)
        {
            foreach (IdentityReference group in this.identity.Groups)
            {
                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));
                string groupName = Formatter.ParseName(name.ToString());
                string[] permissions;
                if (RolesConfig.GetPermissions(groupName, out permissions))
                {
                    if (!permissions.Contains(permission))
                    {
                        continue;
                    }
                    return true;
                    //return permissions.Contains(permission); ovo mi je diskutabilno
                                                                                                                       
                }
            }
            return false;
        }
    }
}
