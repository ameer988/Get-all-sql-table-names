using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    public class Program
    {
        static SqlConnection con;
        static Program()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        }
       
        public IList<string> ListTables(SqlConnection connection)
        {
            List<string> tables = new List<string>();
            con.Open();
            //get all table names 
            DataTable dt = connection.GetSchema("Tables");
            //add table names 
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }
            return tables;
        }

        static void Main(string[] args)
        {
            Program obj = new Program();
            IList<string> list= obj.ListTables(con);
            //tables display asc order
            foreach (var item in list.OrderBy(x=>x))
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
