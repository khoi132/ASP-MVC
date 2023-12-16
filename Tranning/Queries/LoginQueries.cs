using Microsoft.Data.SqlClient;
using Tranning.Models;

namespace Tranning.Queries
{
    public class LoginQueries
    {
        //chua cac logic sql xu ly voi database
        public LoginModel CheckLoginUser(string username, string password) 
        {
            LoginModel dataUser = new LoginModel();
            using (SqlConnection conn = DatabaseConnection.GetSqlConnection()) 
            {
                string querySql = "SELECT * FROM users WHERE username = @username AND password = @password";
                //@username va @password : tham so truyen vao cau lenh sql va gia tri duoc nhan tu 2 bien string username vaf string password
                //tao 1 doi tuong command de thuc thi cau lenh sql
                SqlCommand cmd = new SqlCommand(querySql, conn);
                //xu ly truyen gia tri vao cho tham so trong sql
                cmd.Parameters.AddWithValue("@username", username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", password ?? (object)DBNull.Value);
                // mo ket noi toi database
                conn.Open();
                //thuc thi lenh sql
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read()) 
                    {
                        //do du lieu tu table trong database vao model minh da dinh nghia
                        dataUser.UserID = reader["id"].ToString();
                        dataUser.Username = reader["username"].ToString();
                        dataUser.EmailUser = reader["email"].ToString();
                        dataUser.RoleID = Convert.ToInt32(reader["role_id"]);

                        dataUser.PhoneUser = reader["phone"].ToString();
                        dataUser.FullName = reader["full_name"].ToString();
                        dataUser.ExtraCode = reader["extra_code"].ToString();
                        dataUser.Gender = reader["gender"].ToString();
                        dataUser.Status = reader["status"].ToString();
                    }
                }




            }
            return dataUser;
        }
    }
}
