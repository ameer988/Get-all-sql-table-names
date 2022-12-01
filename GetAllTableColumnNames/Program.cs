using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAllTableColumnNames
{
    class Program
    {
        static void Main(string[] args)
        {
            try

            {
                Console.WriteLine("Enter table name");
                string tableName = Console.ReadLine();
                var objDataset1 = new DataSet();
                var presentTable = new DataSet();
                #region Connection String For Database
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                conn.Open();
                #endregion Connection String For Database

                #region Is Table Present
                // Check table present or not in database

                var isPresentTable = new SqlCommand("Select * from INFORMATION_SCHEMA.TABLES where table_name='" + tableName + "';", conn);

                var res = new SqlDataAdapter(isPresentTable);

                res.Fill(presentTable);

                if (presentTable.Tables[0].Rows.Count == 0)
                {
                    Console.WriteLine("Table Not Present");
                }
                #endregion

                SqlCommand objCmdSelect = new SqlCommand("select * from " + tableName + ";", conn);
                var objAdapter1 = new SqlDataAdapter(objCmdSelect);
                objAdapter1.Fill(objDataset1);
                Console.WriteLine("Column Names of Table ");
                Console.WriteLine();
                //Featch column name of a table from the dataset
                foreach (DataColumn column in objDataset1.Tables[0].Columns)
                {
                    Console.WriteLine(column.ColumnName);
                }
               
                conn.Close();
            }

            catch (Exception)
            {
                throw;
            }
            Console.ReadKey();
        }
    }
}
