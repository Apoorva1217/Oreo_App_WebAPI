using Microsoft.Extensions.Configuration;
using OreoAppCommonLayer.Model;
using OreoAppRepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OreoAppRepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        public SqlConnection sqlconnection;

        public readonly IConfiguration configuration;
        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
            sqlconnection = new SqlConnection(this.configuration.GetConnectionString("OreoContext"));
        }

        public static string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        public bool RegisterAdmin(AdminRegistration register)
        {
            try
            {
                using (this.sqlconnection)
                {
                    var password = Encryptdata(register.Password);

                    SqlCommand sqlCommand = new SqlCommand("spAddAdmin", this.sqlconnection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@FullName", register.FullName);
                    sqlCommand.Parameters.AddWithValue("@Phone", register.Phone);
                    sqlCommand.Parameters.AddWithValue("@Email", register.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", password);

                    this.sqlconnection.Open();

                    var result = sqlCommand.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }

        public AdminRegistration AdminLogin(AdminLogin login)
        {
            AdminRegistration adminRegistration = new AdminRegistration();
            try
            {
                using (this.sqlconnection)
                {
                    var password = Encryptdata(login.Password);

                    SqlCommand command = new SqlCommand("spAdminLogin", sqlconnection);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", login.Email);
                    command.Parameters.AddWithValue("@Password", password);

                    this.sqlconnection.Open();

                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            adminRegistration.AdminId = sqlDataReader.GetInt32(0);
                            adminRegistration.FullName = sqlDataReader.GetString(1);
                            adminRegistration.Phone = sqlDataReader.GetString(2);
                            adminRegistration.Email = sqlDataReader.GetString(3);
                            adminRegistration.Password = sqlDataReader.GetString(4);
                        }
                    }          
                }
                return adminRegistration;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }
    }
}
