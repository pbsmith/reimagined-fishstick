using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DAL
{
    public class ToDoListDao
    {
        private string connectionString;
        public ToDoListDao(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Task> ShowList()
        {
            List<Task> tasks = new List<Task>();

            string sql = @"SELECT id, title, description, time, is_completed FROM DB_List";

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Task task = new Task();
                        task.Id = Convert.ToInt32(reader["id"]);
                        task.Title = Convert.ToString(reader["title"]);
                        task.Description = Convert.ToString(reader["description"]);
                        task.Time = Convert.ToDateTime(reader["time"]);
                        task.Completion = Convert.ToInt32(reader["is_completed"]);
                        tasks.Add(task);
                    }
                }
            }
            catch(SqlException ex)
            {
                tasks = new List<Task>();
            }
            return tasks;
        }

        public Task AddTask(Task newTask)
        {
            Task task = new Task();

            int taskId = 0;

            string sql = "INSERT INTO DB_List (title, description, time, is_completed) " +
                "OUTPUT INSERTED.id VALUES (@title, @description, @time, @is_completed)";

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql,conn);

                    cmd.Parameters.AddWithValue("@title", newTask.Title);
                    cmd.Parameters.AddWithValue("@description", newTask.Description);
                    cmd.Parameters.AddWithValue("@time", newTask.Time);
                    cmd.Parameters.AddWithValue("@is_completed", newTask.Completion);

                    taskId = (int)cmd.ExecuteScalar();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            task = GetTaskById(taskId);
            return task;

        }

        public void RemoveTask(string title)
        {
            string sql = "DELETE FROM DB_List WHERE title = @title;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@title", title);
                    int numberOfRows = cmd.ExecuteNonQuery();
                    if (numberOfRows == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Task does not exist or title was entered incorrectly");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Successfully deleted the task from the list");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public Task GetTaskById(int id)
        {
            Task task = null;
            string sql = @"SELECT id, title, description, time, is_completed FROM DB_List WHERE id = @id;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return task;
        }

        public void CompleteTask(string title)
        {
            string sql = "UPDATE DB_List SET is_completed = @is_completed WHERE title = @title;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@is_completed", 1);
                    cmd.Parameters.AddWithValue("@title", title);
                    int numberOfRows = cmd.ExecuteNonQuery();

                    if (numberOfRows == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Task already marked complete");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Marked the task as complete");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void RemoveAll()
        {
            string sql = "DELETE FROM DB_List;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    
                    int numberOfRows = cmd.ExecuteNonQuery();
                    if (numberOfRows == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("List is already empty");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Successfully reset the list");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
