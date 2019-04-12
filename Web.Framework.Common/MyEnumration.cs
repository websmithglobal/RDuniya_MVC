using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Common
{
    public class MyEnumration
    {
        public enum MyStatus { Active = 1, DeActive = 2, Suspend = 3 }
        public enum Userlevel { ADMIN = 0, MD = 1, SD = 2, R = 3, APIUSER = 4, STAFF = 5, NONE = 99 }
        public enum EntryMode { ADD, EDIT, DELETE, GET }
        public enum Operation { WHERE, LIKE, AND, OR, NONE }
        public enum AppRoles { FINANCE = 1, SUBADMIN = 2, PARTNER = 3 }
        public enum GENDER { MALE = 1, FEMALE = 2, OTHER = 3 }

        public enum TICKETSTATUS { CREATED = 1, RESOLVED = 2 }

        public enum LOGTYPE { DATA , APIREQ, APIRES , OUTRES , APIERROR }

        public enum ApiAuthentication { OK, FAIL }

        public enum ApiNotApplicable { NA }

        public enum ROfferMyStatus { Active = 1, Suspend = 2 }

        public enum DMTDOCUMENTSTATUS { PENDING = 1, APPROVED = 2, REJECTED = 3}

        public enum DMTAPPROVALSTATUS { APPROVED = 1, BLOCKED = 2 }

    }
}
