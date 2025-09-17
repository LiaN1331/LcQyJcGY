// 代码生成时间: 2025-09-17 09:37:02
using System;
using System.Data.SqlClient;
using System.Web;

namespace AntiSqlInjectionExample
{
    // 一个简单的类，用于演示如何防止SQL注入
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        // 构造函数，传入数据库连接字符串
        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        // 一个方法，用于安全地查询数据库
        public SqlDataReader SafeQuery(string query, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        return command.ExecuteReader();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("数据库操作失败", ex);
            }
        }

        // 使用参数化查询防止SQL注入的一个示例
        public SqlDataReader GetProductsByCategory(int categoryId)
        {
            // 使用参数化查询来防止SQL注入
            string query = "SELECT * FROM Products WHERE CategoryId = @CategoryId";
            SqlParameter parameter = new SqlParameter("@CategoryId", categoryId);
            return SafeQuery(query, new SqlParameter[] { parameter });
        }
    }

    // 用于Web页面的代码，展示如何使用DatabaseHelper类
    public class WebPageExample
    {
        public void DisplayProductsByCategory(HttpContext context, int categoryId)
        {
            try
            {
                DatabaseHelper dbHelper = new DatabaseHelper("YourConnectionStringHere");
                using (SqlDataReader reader = dbHelper.GetProductsByCategory(categoryId))
                {
                    while (reader.Read())
                    {
                        context.Response.Write("Product Name: " + reader["ProductName"] + "
");
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message + "
");
            }
        }
    }
}
