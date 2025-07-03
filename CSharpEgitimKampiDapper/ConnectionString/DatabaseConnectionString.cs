using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEgitimKampiDapper.ConnectionString
{
    public static class DatabaseConnectionString
    {
        public static void test()
        {
            SqlConnection connection = new SqlConnection("Server=BARAN;initial Catalog=EgitimKampiDapper;integrated security=true");
        }
       
    }
}
