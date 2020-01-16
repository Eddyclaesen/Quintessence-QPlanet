using System.Web;
using System.Web.Security;

namespace Quintessence.QPlanet.Infrastructure.Security
{
    public static class IdentityHelper
    {
        public static FormsIdentity RetrieveIdentity(HttpContextBase context)
        {
            if (context.User != null)
            {
                if (context.Request.IsAuthenticated)
                {
                    var identity = context.User.Identity as FormsIdentity;

                    return identity;
                }
            }
            return null;
        }

        public static FormsIdentity RetrieveIdentity(HttpContext context)
        {
            if (context.User != null)
            {
                if (context.Request.IsAuthenticated)
                {
                    var identity = context.User.Identity as FormsIdentity;

                    return identity;
                }
            }
            return null;
        }
    }
}
