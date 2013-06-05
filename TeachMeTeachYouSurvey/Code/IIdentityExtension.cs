using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace TeachMeTeachYouSurvey
{
    public static class IIdentityExtension
    {
        public static string UserId(this IIdentity identity)
        {
            return (identity as FormsIdentity).Ticket.UserData;
        }
    }
}