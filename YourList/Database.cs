using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Text;
using System.Collections.Generic;
using YourList.Models;
using System.Collections.ObjectModel;

namespace YourList
{
    class Database
    {
        static private string connectionString;
        static private SqlDataAdapter adapter;
        static private SqlCommand command;
        static private SqlConnection connection;
        static private DataTable dataTable;

        static Database()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        static public void CreateDatabase(string path)
        {
            string str = 
            "CREATE DATABASE YourList" +
            "on primary" +
            "(" +
                "name = N'yourlistdb'," +
                "filename = N'" + path + @"\yl.mdf'," +
                "size = 5Mb," +
                "maxsize = unlimited," +
                "filegrowth = 1Mb" +
            ")" +
            "LOG ON" +
            "(" +
                "name = N'yourlistdb_log'," +
                "filename = N'" + path + @"\yl.ldf'," +
                "size = 5Mb," +
                "maxsize = unlimited," +
                "filegrowth = 1Mb" +
            ")" +
            @" ALTER AUTHORIZATION ON DATABASE::[YourList] TO[NT AUTHORITY\СИСТЕМА]; ";

            SqlCommand myCommand = new SqlCommand(str, connection);
            try
            {
                connection.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("База данных 'Airport' создана успешно", "Создание базы данных", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Создание базы данных", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        static public void AddTask(string _Title, string _Note, string _Difficult, DateTime _DeadLine, DateTime? _Reminder = null)
        {

        }

        static public void CreateTables()
        {
            string str;

            using (FileStream fstream = File.OpenRead(@"Create_Table.sql"))
            {
                byte[] array = new byte[fstream.Length]; // Преобразуем строку в байты
                fstream.Read(array, 0, array.Length);    // Считываем данные 
                str = Encoding.Default.GetString(array); // Декодируем байты в строку
            }

            try
            {
                SqlCommand command = new SqlCommand(str, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static ObservableCollection<Task> GetTasks(int idUsr)
        {
            // название процедуры
            string sqlExpression = "sp_GetAllTask";

            ObservableCollection<Task> tmpList = new ObservableCollection<Task>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = idUsr
                };

                command.Parameters.Add(idParam);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tmpList.Add(new Task(
                            reader.GetBoolean(5),
                            reader.GetString(2),
                            reader.GetString(6),
                            reader.GetString(8),
                            reader.GetDateTime(3),
                            reader.GetDateTime(4)
                            ));
                    }
                }
                reader.Close();
                return tmpList;
            }
        }

        public static List<User> GetUsers()
        {
            string sqlExpression = "sp_GetUsers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                List<User> UL = new List<User>();
                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        UL.Add(new User(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3)
                        ));
                    }
                    
                }
                reader.Close();
                return UL;
            }
        }

        

        static public int Request(string _select, DataGrid _dataGrid = null) {
            dataTable = new DataTable();
            try
            {
                connection = new SqlConnection(connectionString);
                command = new SqlCommand(_select, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);
                if (_dataGrid != null)
                    _dataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return dataTable.Rows.Count;
        }
    }
}
