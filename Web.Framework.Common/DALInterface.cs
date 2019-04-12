using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Web.Framework.Common
{
    public interface DALInterface
    {
        bool Insert(object obj);

        bool Update(object obj);

        bool Delete(object obj);
    }
}