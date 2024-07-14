using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace HW3_ATM
{
    class Program
    {
        public static List<Option> options;
        static void Main(string[] args)
        {
            options = new List<Option>
            {
                new Option("Sign in", () => SignInFileds()),
                new Option("Log in", () =>  WriteTemporaryMessage("How Are You")),
                new Option("Leave", () => Environment.Exit(0)),
            };

            // Set the default index of the selected item to be the first
            int index = 0;

            // Write the menu out
            WriteMenu(options, options[index]);

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        WriteMenu(options, options[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        WriteMenu(options, options[index]);
                    }
                }
                // Handle different action for the option
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();

        }
        // Default action of all the options. You can create more methods
        static void WriteTemporaryMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Thread.Sleep(3000);
            WriteMenu(options, options.First());
        }

        static void SignInFileds()
        {
            Console.Clear();
            Console.WriteLine("Email");
            var email = Console.ReadLine();
            if (!IsValidEmail(email))
            {
                Console.WriteLine("Email is not valid.");
            }
            
            Console.WriteLine("Password");
            var password = Console.ReadLine();
            Console.WriteLine("Confirm Password");
            var confirmPassword = Console.ReadLine();
            if(password != confirmPassword)
            {
                Console.WriteLine("Password isn't the same");
            }
            Thread.Sleep(1000);
            WriteMenu(options, options.First());
        }

        static bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        static void WriteMenu(List<Option> options, Option selectedOption)
        {
            Console.Clear();

            foreach (Option option in options)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
            }
        }
    }

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }

        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }

    /*Console.WriteLine("Please enter email");
    var email = Console.ReadLine();

    Console.WriteLine("Please enter password");
    var password = Console.ReadLine();
    */
   /* Console.WriteLine("Please choose option: Log in, Sign in or Leave");
            var option = Console.ReadLine();
           
            if (option == "Log in" || option == "login" || option == "log")
            {
                Console.WriteLine("Please enter email");
                var email1 = Console.ReadLine();

                Console.WriteLine("Please enter password");
                var password1 = Console.ReadLine();
            }
            //else if (option == "Sign in" || option == "signin" || option == "sing")
            //{
            //    Console.WriteLine("Please enter email");
            //    var email2 = Console.ReadLine();

            //    Console.WriteLine("Please enter password");
            //    var password2 = Console.ReadLine();

            //    Console.WriteLine("Please enter the same password");
            //    var password21 = Console.ReadLine();
            //}
            else if (option == "leave" || option == "Leave")
            {
                Environment.Exit(1);
            }*/
        }
    

