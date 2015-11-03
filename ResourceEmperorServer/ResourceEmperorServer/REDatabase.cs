﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace ResourceEmperorServer
{
    public class REDatabase
    {
        private readonly string dsHostname;
        private readonly string dsUsername;
        private readonly string dsPassword;
        private readonly string dsDatabase;
        private MySqlConnection connection;

        public REDatabase(string hostname, string username, string password, string database)
        {
            dsHostname = hostname;
            dsUsername = username;
            dsPassword = password;
            dsDatabase = database;
        }
        public void Dispose()
        {
            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }
        public bool Connect()
        {
            string connectString = "server=" + dsHostname + ";uid=" + dsUsername + ";pwd=" + dsPassword + ";database=" + dsDatabase;
            connection = new MySqlConnection(connectString);
            connection.Open();

            return connection is MySqlConnection;
        }

        public string[] GetDataByUniqueID(int uniqueID, string[] requestItems, string table)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                StringBuilder sqlText = new StringBuilder();
                sqlText.Append("SELECT " + requestItems[0]);
                int requestNumber = requestItems.Length;
                for (int index1 = 1; index1 < requestNumber; index1++)
                {
                    sqlText.Append("," + requestItems[index1]);
                }
                sqlText.Append(" FROM " + table + " WHERE UniqueID=@uniqueID");
                using (MySqlCommand cmd = new MySqlCommand(sqlText.ToString(), connection))
                {
                    cmd.Parameters.AddWithValue("@uniqueID", uniqueID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string[] returnValue = new string[requestNumber];
                            for (int index1 = 0; index1 < requestNumber; index1++)
                            {
                                if (reader.IsDBNull(index1))
                                    returnValue[index1] = "";
                                else
                                    returnValue[index1] = reader.GetString(index1);
                            }
                            return returnValue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        public object[] GetDataByUniqueID(int uniqueID, string[] requestItems, TypeCode[] requestTypes, string table)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                StringBuilder sqlText = new StringBuilder();
                sqlText.Append("SELECT " + requestItems[0]);
                int requestNumber = requestItems.Length;
                for (int index1 = 1; index1 < requestNumber; index1++)
                {
                    sqlText.Append("," + requestItems[index1]);
                }
                sqlText.Append(" FROM " + table + " WHERE UniqueID=@uniqueID");
                using (MySqlCommand cmd = new MySqlCommand(sqlText.ToString(), connection))
                {
                    cmd.Parameters.AddWithValue("@uniqueID", uniqueID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            object[] returnValue = new object[requestNumber];
                            for (int index1 = 0; index1 < requestNumber; index1++)
                            {
                                if (reader.IsDBNull(index1))
                                    returnValue[index1] = null;
                                else
                                {
                                    switch (requestTypes[index1])
                                    {
                                        case TypeCode.Boolean:
                                            returnValue[index1] = reader.GetBoolean(index1);
                                            break;
                                        case TypeCode.String:
                                            returnValue[index1] = reader.GetString(index1);
                                            break;
                                        case TypeCode.Int32:
                                            returnValue[index1] = reader.GetInt32(index1);
                                            break;
                                        case TypeCode.Single:
                                            returnValue[index1] = reader.GetFloat(index1);
                                            break;
                                    }
                                }
                            }
                            return returnValue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        public bool InsertData(string[] insertItems, object[] insertValues, string table)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                StringBuilder sqlText = new StringBuilder();
                sqlText.Append("INSERT INTO " + table + " (" + insertItems[0]);
                int insertNumber = insertItems.Length;
                for (int index1 = 1; index1 < insertNumber; index1++)
                {
                    sqlText.Append("," + insertItems[index1]);
                }
                sqlText.Append(") values (@insertValue0");
                for (int index1 = 1; index1 < insertNumber; index1++)
                {
                    sqlText.Append(",@insertValue" + index1.ToString());
                }
                sqlText.Append(")");
                using (MySqlCommand cmd = new MySqlCommand(sqlText.ToString(), connection))
                {
                    for (int index1 = 0; index1 < insertNumber; index1++)
                    {
                        cmd.Parameters.AddWithValue("@insertValue" + index1.ToString(), insertValues[index1]);
                    }
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        public bool UpdateDataByUniqueID(int uniqueID, string[] updateItems, object[] updateValues, string table)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                StringBuilder sqlText = new StringBuilder();
                sqlText.Append("UPDATE " + table + " SET " + updateItems[0] + "=@updateValue0");
                int updateNumber = updateItems.Length;
                for (int index1 = 1; index1 < updateNumber; index1++)
                {
                    sqlText.Append("," + updateItems[index1] + "=@updateValue" + index1.ToString());
                }
                sqlText.Append(" where UniqueID=@uniqueID");
                using (MySqlCommand cmd = new MySqlCommand(sqlText.ToString(), connection))
                {
                    for (int index1 = 0; index1 < updateNumber; index1++)
                    {
                        cmd.Parameters.AddWithValue("@updateValue" + index1.ToString(), updateValues[index1]);
                    }
                    cmd.Parameters.AddWithValue("@uniqueID", uniqueID);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public string ToHexString(byte[] bytes)
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2").ToLower());
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        //Specific SQL Action
        public bool LoginCheck(string account, string password, out int playerUniqueID)
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                using (SHA512 sha512 = new SHA512CryptoServiceProvider())
                {
                    string passwordSHA512 = ToHexString(sha512.ComputeHash(Encoding.Default.GetBytes(password)));

                    String sqlText = "SELECT UniqueID FROM player WHERE Account=@account and Password=@password";
                    using (MySqlCommand cmd = new MySqlCommand(sqlText, connection))
                    {
                        cmd.Parameters.AddWithValue("@account", account);
                        cmd.Parameters.AddWithValue("@password", passwordSHA512);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                playerUniqueID = reader.GetInt32(0);
                                return true;
                            }
                            else
                            {
                                playerUniqueID = 0;
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                throw EX;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        public bool GetRanking(out Dictionary<string,int> ranking)
        {
            ranking = new Dictionary<string, int>();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
                String sqlText = "SELECT Account, Money FROM player order by Money DESC";
                using (MySqlCommand cmd = new MySqlCommand(sqlText, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            ranking.Add(reader.GetString(0),reader.GetInt32(1));
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                throw EX;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return true;
        }
    }
}
