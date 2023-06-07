using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBConnec
    {
        protected SqlConnection conn = new SqlConnection(@"Data Source=WINDOWS-11\SQLEXPRESS01;Initial Catalog=DOAN1_NUOCHOA;Integrated Security=True");
    }
}
