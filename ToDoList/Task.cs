using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public abstract class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UrgencyLevel { get; set; }
        public int Time { get; set; }

        public Task(string title, string description, int urgencyLevel, int time)
        {
            Title = title;
            Description = description;
            UrgencyLevel = urgencyLevel;
            Time = time;
        }





    }
}
