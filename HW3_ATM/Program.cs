using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HW3_ATM
{
    class Program
    {
        public static List<Option> options;
        static Dictionary<string, string> _credentials = new Dictionary<string, string>();
        static Dictionary<string, double> _balance = new Dictionary<string, double>();

        static void Main(string[] args)
        {
           

            options = new List<Option>
            {
                new Option("Sign in", () => SignInFileds()),
                new Option("Log in", () =>  LogInFileds()),
                new Option("Leave", () => Environment.Exit(0)),
            };

            

            int index = 0;

            WriteMenu(options, options[index]);

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

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
               
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    index = 0;
                }
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();
        }

        static void SignInFileds()
        {
            var email = String.Empty;
            var password = String.Empty;

            Console.Clear();
            Console.WriteLine("Create account:");
            Console.WriteLine("Email:");
             email = Console.ReadLine();

            if (!IsEmailExist(email))
            {
                Console.WriteLine("Password:");
                password = Console.ReadLine();
                Console.WriteLine("Confirm Password:");
                var confirmPassword = Console.ReadLine();
                if (password != confirmPassword)
                {
                    Console.WriteLine("Password isn't the same.");
                }
                _credentials.Add(email, password);
                _balance.Add(email, 0.0);
                Console.WriteLine("Successfully created an account.");
                PauseBeforeContinuing();
                WriteMenu(options, options.First());
            }
            else
            {
                Console.WriteLine("Your email exist.");
                PauseBeforeContinuing();
                WriteMenu(options, options.First());
            }
        }


        static bool IsEmailExist(string email)
        {
            return _credentials.ContainsKey(email);
        }

        static bool IsValidCredentials(string email, string password)
        {
            return _credentials[email].Contains(password);
        }

        static void LogInFileds()
        {
            var email = String.Empty;
            bool res = false;

            while (!res) 
            {
                Console.Clear();
                Console.WriteLine("Email:");
                email = Console.ReadLine();

                if (IsEmailExist(email))
                {
                    Console.WriteLine("Password:");
                    var password = Console.ReadLine();
                    if (!IsValidCredentials(email, password))
                    {

                        Console.WriteLine("Email or password is wrong. Please try again.");
                        PauseBeforeContinuing();
                    }
                    else
                    {
                        res = SubMenu(email);
                    }
                }
                else
                {
                    Console.WriteLine("Please Sign In");
                    PauseBeforeContinuing();
                    WriteMenu(options, options.First());
                }
                res = true;
            }
        }

        static void CheckBalance(string email)
        {
            bool showBalance = false;
            while (!showBalance)
            {
                Console.Clear();
                Console.WriteLine("Balance:");
                Console.WriteLine(_balance[email]);
                PauseBeforeContinuing();
                showBalance = SubMenu(email);
            }
        }

        static void Deposite(string email)
        {
            bool showDeposit = false;
           
            while (!showDeposit)
            {
                Console.Clear();
                Console.WriteLine("Deposite:");
                var money = Convert.ToDouble(Console.ReadLine());
                _balance[email] = _balance[email] + money;
                Console.WriteLine($"Deposite: {_balance[email]}");
                showDeposit = SubMenu(email);
            }
        }

        static void Withdraw(string email)
        {
            bool show = false;
            while (!show)
            {
                Console.Clear();
                Console.WriteLine("Withdraw:");
                var money = Convert.ToDouble(Console.ReadLine());
                _balance[email] = _balance[email] - money;
                Console.WriteLine($"Deposite: {_balance[email]}");
                show = SubMenu(email);
            }
        }

        static void LogOff()
        {
             WriteMenu(options, options.First()); 
        }

        static bool SubMenu(string email = "")
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Check Balance");
            Console.WriteLine("2) Deposit");
            Console.WriteLine("3) Withdraw");
            Console.WriteLine("4) Log Out");
            Console.WriteLine("Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    CheckBalance(email);
                    PauseBeforeContinuing();
                    return true;
                case "2":
                    Deposite(email);
                    PauseBeforeContinuing();
                    return true;
                case "3":
                    Withdraw(email);
                    PauseBeforeContinuing();
                    return true;
                case "4":
                    LogOff();
                    PauseBeforeContinuing();
                    return false;
                default:
                    return true;
            }
        }

        static void PauseBeforeContinuing()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void WriteMenu(List<Option> options, Option selectedOption)
        {
            Console.Clear();
            Console.WriteLine("Welcom to ATM:");

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
}


