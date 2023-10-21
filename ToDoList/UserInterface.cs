using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL;

namespace ToDoList
{
    public class UserInterface
    {
        string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=ToDoList;Integrated Security=True";
        private ToDoListDao listDao;

        public UserInterface()
        {
            listDao = new ToDoListDao(connectionString);
        }

        public void Start()
        {
            bool exit = false;

            while (!exit)
            {
                DisplayMenu();

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        PreviewToDoList();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        PreviewToDoList();
                        Console.WriteLine();
                        Console.WriteLine("Carefully type in the title of the task you want to mark completed: ");
                        string title = Console.ReadLine();
                        listDao.CompleteTask(title);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        ToDoListAdd();
                        break;
                    case "4":
                        Console.Clear();
                        ToDoListRemove();
                        break;
                    case "E":
                    case "e":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid command");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private void DisplayMenu()
        {
            //TODO print to-do list first

            Console.WriteLine();
            Console.WriteLine(" ______________________________");
            Console.WriteLine("|    Please pick an option:    |");
            Console.WriteLine("| 1 - Preview To-Do List       |");
            Console.WriteLine("| 2 - Mark a Task as Completed |");
            Console.WriteLine("| 3 - Add to List              |");
            Console.WriteLine("| 4 - Remove from List         |");
            Console.WriteLine("| E - Exit the Program         |");
            Console.WriteLine("|______________________________|");
            Console.WriteLine();
        }

        private void PreviewToDoList()
        {
            try
            {
                List<Task> temp = listDao.ShowList();

                Console.WriteLine();

                if (temp.Count == 0)
                {
                    Console.WriteLine("No tasks in current To-Do list");
                }
                else
                {
                    foreach (Task t in temp)
                    {
                        string comp = CompletedCheck(t);

                        char pad = ' ';
                        Console.WriteLine();
                        Console.WriteLine("TASK| " + t.Title.ToUpper().PadRight(30, pad) + t.Time.ToString().PadRight(16, pad) + "                " + comp + "\n" + "DESCRIPTION| " + t.Description);
                        Console.WriteLine();
                        Console.WriteLine("-----------------------------------------------------------------------------");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void ToDoListAdd()
        {
            Task task = new Task();

            Console.WriteLine("Please enter a task title: ");

            task.Title = Console.ReadLine();

            Console.WriteLine("Please enter a task description: ");
            task.Description = Console.ReadLine();

            task.Time = new DateTime();
            while (task.Time == new DateTime(0001, 01, 01))
            {
                try
                {
                    Console.WriteLine("Please enter a due date (yyyy/mm/dd): ");
                    task.Time = DateTime.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Please enter a valid date in the provided format");
                }
            }
            Console.Clear();
            Console.WriteLine("Successfully added task - Press enter");
            Console.ReadLine();
            Console.Clear();

            task.Completion = 0;

            listDao.AddTask(task);
        }

        private void ToDoListRemove()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(" _____________________________________________________ ");
            Console.WriteLine("|    Please select a removal option:                  |");
            Console.WriteLine("| 1 - Remove a task by title                          |");
            Console.WriteLine("| 2 - Remove all tasks from the list (Complete reset) |");
            Console.WriteLine("|_____________________________________________________|");
            Console.WriteLine();
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.Clear();
                    PreviewToDoList();
                    Console.WriteLine("Carefully type in the title of the task you want to delete: ");
                    string title = Console.ReadLine();
                    listDao.RemoveTask(title);
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Are you sure? There is no getting it back (Y/N)");
                    string userInput2 = Console.ReadLine();
                    switch (userInput2)
                    {
                        case "Y":
                        case "y":
                            listDao.RemoveAll();
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "N":
                        case "n":
                            break;
                        default:
                            Console.WriteLine("Invalid response: Press enter to return to menu");
                            Console.ReadLine();
                            break;
                    }
                    break;
            }
        }

        public string CompletedCheck(Task task)
        {
            string comp = "";
            if (task.Completion == 0)
            {
                comp = "[ ]";
            }
            else
            {
                comp = "[X]";
            }
            return comp;
        }
    }
}
