using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class DalController
    {

        private string CONNECTION_STRING;
        public DalController(string BasePath)
        {
            CONNECTION_STRING = $"Data Source={BasePath}; Version=3;";
            if (System.IO.File.Exists(BasePath))
            {
                return;
            }
            SQLiteConnection.CreateFile(BasePath);
            string[] commands = {"CREATE TABLE 'boards' ('email' TEXT,'taskId' INTEGER,PRIMARY KEY('email')); ",
            "CREATE TABLE 'columns' ('id' INTEGER PRIMARY KEY AUTOINCREMENT,'name' TEXT NOT NULL,'collimit' INTEGER NOT NULL,'email' TEXT NOT NULL,'ordinal' INTEGER NOT NULL); ",
            "CREATE TABLE 'tasks' ('id' INTEGER,'email' TEXT,'creationTime' TEXT NOT NULL,'dueDate' TEXT NOT NULL,'title' TEXT NOT NULL,'description' TEXT NOT NULL,'column' INTEGER NOT NULL,PRIMARY KEY('email', 'id')); ",
            "CREATE TABLE 'users'('email' TEXT NOT NULL,'nickname' TEXT NOT NULL,'password' TEXT NOT NULL,PRIMARY KEY('email'));"};
            using (var connection = new SQLiteConnection(CONNECTION_STRING))
            {
                try
                {
                    connection.Open();
                    foreach (string c in commands)
                    {
                        SQLiteCommand command = new SQLiteCommand
                        {
                            Connection = connection,
                            CommandText = c

                        };
                        command.ExecuteNonQuery();
                        command.Dispose();
                    }

                }
                catch (SQLiteException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void DeleteData()
        {
            query($"delete from users;delete from boards;delete from columns;delete from tasks");
        }
        public void query(string query)
        {
            using (var connection = new SQLiteConnection(CONNECTION_STRING))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = query
                };
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(new System.Diagnostics.StackTrace().ToString());
                    throw new Exception(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
        }
        public List<object[]> select(string query,int numColumns)
        {
            List<object[]> result = new List<object[]>();
            using (var connection = new SQLiteConnection(CONNECTION_STRING))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = query
                };
                try
                {
                    connection.Open();
                    SQLiteDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        object[] row = new object[numColumns];
                        for (int i = 0; i < numColumns; i++)
                        {
                            row[i] = dataReader.GetValue(i);
                        }
                        result.Add(row);
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine(new System.Diagnostics.StackTrace().ToString());
                    throw new Exception(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
            }
            return result;

        }
        public List<User> LoadAllUsers()
        {
            List<User> results = new List<User>();
            List<object[]> list = select("SELECT * FROM users", 3);
            foreach (object[] row in list)
            {
                results.Add(new User(Convert.ToString(row[0]), Convert.ToString(row[2]), Convert.ToString(row[1])));
            }
            return results;
        }
        public List<Board> LoadAllBoards()
        {
            List<Board> results = new List<Board>();
            List<object[]> list = select("SELECT * FROM boards", 2);
            foreach (object[] row in list)
            {
                results.Add(new Board(Convert.ToInt32(row[1]), Convert.ToString(row[0])));
            }
            return results;
        }
        public List<Column> LoadColumns(string email)
        {
            List<Column> results = new List<Column>();
            List<object[]> list = select($"SELECT * FROM columns WHERE email='{email}' ORDER BY ordinal", 5);
            foreach (object[] row in list)
            {
                results.Add(new Column(Convert.ToInt64(row[0]), Convert.ToString(row[1]), Convert.ToInt32(row[2]), Convert.ToString(row[3]), Convert.ToInt32(row[4])));
            }
            return results;
        }
        public List<Task> LoadTasks(long column)
        {
            List<Task> results = new List<Task>();
            List<object[]> list = select($"SELECT * FROM tasks WHERE column={column}", 7);
            foreach (object[] row in list)
            {
                results.Add(new Task(Convert.ToInt32(row[0]), Convert.ToString(row[1]),DateTime.Parse(Convert.ToString(row[2])), DateTime.Parse(Convert.ToString(row[3])), Convert.ToString(row[4]), Convert.ToString(row[5]), Convert.ToInt64(row[6])));
            }
            return results;
        }
    }
}
