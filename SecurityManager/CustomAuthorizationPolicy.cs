using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;

namespace SecurityManager
{
    public class CustomAuthorizationPolicy : IAuthorizationPolicy
    {
        public CustomAuthorizationPolicy()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }
        public string Id
        {
            get;
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            //Kada klijent pogodi servera povezu se, thred prvo obavlja evaluate deo i stavlja identite
            // na custom principal. Znaci prvo ovo pa onda custom principal.
            //Na kontekst staviti identitet klijenta

            if (!evaluationContext.Properties.TryGetValue("Identities", out object list))
            {
                return false;
            }

            IList<IIdentity> identities = list as IList<IIdentity>;
            if (list == null || identities.Count <= 0)
            {
                return false;
            }

            WindowsIdentity windowsIdentity = identities[0] as WindowsIdentity;

            try
            {
                Audit.AuthenticationSuccess(Formatter.ParseName(windowsIdentity.Name));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            evaluationContext.Properties["Principal"] =
                new CustomPrincipal((WindowsIdentity)identities[0]);
            return true;
        }
    }
}
