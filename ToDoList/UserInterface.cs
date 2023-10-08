using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class UserInterface
    {
        public void Start()
        {
            //bool exit = false;
            //Console.WriteLine("Please enter the full file path of the .txt file you wish to utilize: ");
            //string filePath = Console.ReadLine();
            //string inputList = FileReader.GetToDoList(filePath);
            //List<Task> currentToDoList = ListFormat(inputList);

            //while (!exit)
            //{
            //    DisplayMenu();

            //    string userInput = Console.ReadLine();

            //    switch (userInput)
            //    {
            //        case "1":

            //            Console.Clear();
            //            break;
            //        case "2":

            //            Console.Clear();
            //            break;
            //        case "3":

            //            Console.Clear();
            //            break;
            //        case "E":
            //        case "e":
            //            exit = true;
            //            break;
            //        default:
            //            Console.WriteLine("Please enter a valid command");
            //            Console.WriteLine();
            //            break;
            //    }
            //}
        }

        private void DisplayMenu()
        {
            //TODO print to-do list first

            Console.WriteLine();
            Console.WriteLine("Please pick an option: ");
            Console.WriteLine("1 - Review To-Do List");
            Console.WriteLine("2 - Add a Task");
            Console.WriteLine("3 - Mark Task as Complete");
            Console.WriteLine("E - Exit the Program");
        }
    }
}
