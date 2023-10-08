// See https://aka.ms/new-console-template for more information

using System.Net.Security;
using System.Threading.Channels;
using ToDoList;

namespace ProgramMain;
    class Program
{
    static void Main(string[] args)
    {
        UserInterface ui = new UserInterface();
        ui.Start();
    }

    public class Solution
    {
        public bool IsValid(string s)
        {
            int counter = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    for (int j = i; i < (s.Length - i); j++)
                    {
                        if (s[i + 1] == ')')
                        {
                            counter++;
                        }
                    }
                }
                if (s[i] == '[')
                {
                    for (int j = i; i < (s.Length - i); j++)
                    {
                        if (s[i + 1] == ']')
                        {
                            counter++;
                        }
                    }
                }
                if (s[i] == '{')
                {
                    for (int j = i; i < (s.Length - i); j++)
                    {
                        if (s[i + 1] == '}')
                        {
                            counter++;
                        }
                    }
                }
            }
            if (counter == s.Length / 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
