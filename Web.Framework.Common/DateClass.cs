using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Web.Framework.Common;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;

public static class DateClass
{

    
    public static DateTime GetDate(this String datestring)
    {
        if(!string.IsNullOrEmpty(datestring))
        {
            String[] d = datestring.Split('/');
            return new DateTime(Convert.ToInt32(d[2]), Convert.ToInt32(d[1]), Convert.ToInt32(d[0]));

        }
        return DateTime.MinValue;
    }
    
    public static bool isInRoles(this IPrincipal user, params string[] Roles)
    {
        foreach (string str in Roles)
        {
            if (user.IsInRole(str)) { return true; }
        }
        return false;
    }


    public static MyEnumration.Userlevel GetUserlevel(this IPrincipal user)
    {
        if(user.IsInRole("administrator"))
        {
            return MyEnumration.Userlevel.ADMIN;
        }
        else if (user.IsInRole("apiuser"))
        {
            return MyEnumration.Userlevel.APIUSER;
        }
        else if (user.IsInRole("masterdistributor"))
        {
            return MyEnumration.Userlevel.MD;
        }
        else if (user.IsInRole("distributor"))
        {
            return MyEnumration.Userlevel.SD;
        }
        else if (user.IsInRole("retailer"))
        {
            return MyEnumration.Userlevel.R;
        }       
        return MyEnumration.Userlevel.NONE;
    }

    public static String GetUserRole(this IPrincipal user)
    {
        if (user.IsInRole("administrator"))
        {
            return "ADMIN";
        }
        else if (user.IsInRole("apiuser"))
        {
            return "APIUSER";
        }
        else if (user.IsInRole("masterdistributor"))
        {
            return "MASTER DISTRIBUTOR";
        }
        else if (user.IsInRole("distributor"))
        {
            return "DISTRIBUTOR";
        }
        else if (user.IsInRole("retailer"))
        {
            return "RETAILER";
        }
        return string.Empty;
    }

}

